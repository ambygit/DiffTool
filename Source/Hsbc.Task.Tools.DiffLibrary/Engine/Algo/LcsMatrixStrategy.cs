using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ReportBuilder;

namespace Hsbc.Task.Tools.DiffLibrary.Engine.Algo
{
    /// <summary>
    /// SourceLines => Column in LCS
    /// TargetLines => Row in LCS
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    internal class LcsMatrixStrategy<TSource,TResult> where TResult : IDiffResult
    {
        private readonly IEqualityComparer<TSource> _comparer; 

        private readonly TSource[] _sourceLines;
        private readonly TSource[] _targetLines;
        private readonly int _preSkip;
        private readonly int _postSkip;
        private int[,] _matrix;
        private readonly int _totalSkip;
        private readonly IDiffResultReportBuilder<TSource, TResult> _resultReportBuilder;

        public LcsMatrixStrategy(TSource[] sourceLines, TSource[] targetLines, int preSkip, int postSkip,
                                 IEqualityComparer<TSource> equalityComparer,
                                 IDiffResultReportBuilder<TSource, TResult> resultReportBuilder)
        {
            _resultReportBuilder = resultReportBuilder;
            _sourceLines = sourceLines;
            _targetLines = targetLines;
            _preSkip = preSkip;
            _postSkip = postSkip;
            _totalSkip = _preSkip + _postSkip;
            _comparer = equalityComparer;
        }

        public void Calculate()
        {
            PrepareMatrix();
            UpdateBuilder();
        }

        private void PrepareMatrix()
        {
            if (_totalSkip >= _sourceLines.Length || _totalSkip >= _targetLines.Length)
                return;

            // only create a matrix large enough for the unskipped contents of the diff'ed arrays
            _matrix = new int[_sourceLines.Length - _totalSkip + 1, _targetLines.Length - _totalSkip + 1];

            for (int sourceIndexIncrement = 1; sourceIndexIncrement <= _sourceLines.Length - _totalSkip; sourceIndexIncrement++)
            {
                int sourceIndex = _preSkip + sourceIndexIncrement - 1;

                for (int targetIndexIncrement = 1, targetIndex = _preSkip + 1; targetIndexIncrement <= _targetLines.Length - _totalSkip; targetIndexIncrement++, targetIndex++)
                {
                    _matrix[sourceIndexIncrement, targetIndexIncrement] = _comparer.Equals(_sourceLines[sourceIndex], _targetLines[targetIndex - 1])
                                        ? _matrix[sourceIndexIncrement - 1, targetIndexIncrement - 1] + 1
                                        : Math.Max(_matrix[sourceIndexIncrement, targetIndexIncrement - 1], _matrix[sourceIndexIncrement - 1, targetIndexIncrement]);
                }
            }

        }

        private void UpdateBuilder()
        {
             for (int i = 0; i < _preSkip; i++)
            {
                //FireLineUpdate(DiffType.None, _left[i]);
                _resultReportBuilder.Append(EDiffStatus.None, i, _sourceLines[i], true);
            }

            if(_preSkip >= _sourceLines.Length)
            {
                //We consumed all of source lines, all lines of target are to be added
                for(int rowIndex=_preSkip; rowIndex<_targetLines.Length;rowIndex++)
                {
                    _resultReportBuilder.Append(EDiffStatus.Added,rowIndex+1,_targetLines[rowIndex], false);
                }
            }

            if (_postSkip >= _sourceLines.Length)
            {
                //We consumed all of source lines, all lines of target are to be added
                for (int rowIndex = 0; rowIndex < _targetLines.Length - _postSkip; rowIndex++)
                {
                    _resultReportBuilder.Append(EDiffStatus.Added, rowIndex+1, _targetLines[rowIndex], false);
                }
            }

            int totalSkip = _preSkip + _postSkip;

            if (!(_totalSkip >= _sourceLines.Length || _totalSkip >= _targetLines.Length))
            {
                //Im this scenario only matrix is created
                AppendLcsDiff(_sourceLines.Length - totalSkip, _targetLines.Length - totalSkip);
            }

            int leftLen = _sourceLines.Length;
            for (int i = _postSkip; i > 0; i--)
            {
                _resultReportBuilder.Append(EDiffStatus.None, leftLen, _sourceLines[leftLen - i], true);
            }
        }


        private void AppendLcsDiff(int columnIndex, int rowIndex)
        {
            if (columnIndex > 0 && rowIndex > 0 &&
                _comparer.Equals(_sourceLines[_preSkip + columnIndex - 1], _targetLines[_preSkip + rowIndex - 1]))
            {
                AppendLcsDiff(columnIndex - 1, rowIndex - 1);
                int recordPosition = _preSkip + columnIndex;
                _resultReportBuilder.Append(EDiffStatus.None,recordPosition, _sourceLines[recordPosition-1], true);
            }
            else
            {
                int topValue = rowIndex > 0 ? _matrix[columnIndex, rowIndex - 1] : 0;
                int leftValue = columnIndex > 0 ? _matrix[columnIndex - 1, rowIndex] : 0;
                if (rowIndex > 0 &&
                    (columnIndex == 0 ||
                    topValue >= leftValue))
                {
                    //Move Up
                    AppendLcsDiff(columnIndex, rowIndex - 1);
                    int recordPosition = _preSkip + rowIndex;
                    _resultReportBuilder.Append(EDiffStatus.Added,recordPosition,_targetLines[recordPosition-1], false);
                }
                else if (columnIndex > 0 &&
                    (rowIndex == 0 ||
                    topValue < leftValue))
                {
                    //Move Left
                    AppendLcsDiff(columnIndex - 1, rowIndex);
                    int recordPosition = _preSkip + columnIndex;
                    _resultReportBuilder.Append(EDiffStatus.Deleted,recordPosition,_sourceLines[recordPosition-1], true);
                }
            }

        }

        [Conditional("DEBUG")]
        private void PrintLcsMatrix()
        {
            int totalSkip = _preSkip + _postSkip;
            var columnCount = _sourceLines.Length - totalSkip + 1;
            var rowCount = _targetLines.Length - totalSkip + 1;
            //Print Left
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                if (columnIndex == 0)
                {
                    Console.Write("\t");
                }
                else
                {
                    Console.Write("\t" + _sourceLines[columnIndex - 1]);
                }
            }
            Console.Write(Environment.NewLine);
            for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                if (columnIndex != 0)
                {
                    Console.Write(_targetLines[columnIndex - 1]);
                }
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    Console.Write("\t" + _matrix[columnIndex, rowIndex]);
                }
                Console.Write(Environment.NewLine);
            }
        }
        
    }
}