using System;
using System.Collections.Generic;
using System.IO;
using Hsbc.Task.Tools.DiffLibrary.Engine.Algo;
using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ReportBuilder;
using Hsbc.Task.Tools.DiffLibrary.ResultOptions;

namespace Hsbc.Task.Tools.DiffLibrary.Engine.Main
{
    public interface IDiffCalculator<TResult> where TResult : IDiffResult
    {
        IDiffResultReportBuilder<string, TResult> Run(string sourceFile, string targetFile);
    }

    public class DiffCalculator : IDiffCalculator<IDiffResult>
    {
        private readonly IEqualityComparer<string> _comparer = StringComparer.InvariantCulture;
        private LcsMatrixStrategy<string, IDiffResult> _lcsAlgo;
        private readonly IDiffResultOption<IDiffResult> _diffResultOption;

        public DiffCalculator():this (new DefaultDiffResultOption()){}

        public DiffCalculator(IDiffResultOption<IDiffResult> diffResultOption)
        {
            _diffResultOption = diffResultOption;
        }

        public IDiffResultReportBuilder<string, IDiffResult> Run(string sourceFile, string targetFile)
        {
            var sourceLines = File.ReadAllLines(sourceFile);
            var targetLines = File.ReadAllLines(targetFile);

            return Calculate(sourceLines, targetLines);
        }

        private IDiffResultReportBuilder<string, IDiffResult> Calculate(string[] sourceLines, string[] targetLines)
        {
            var preSkip = CalculatePreSkip(sourceLines, targetLines);
            var postSkip = CalculatePostSkip(preSkip, sourceLines, targetLines);

            _lcsAlgo = new LcsMatrixStrategy<string, IDiffResult>(sourceLines, targetLines, preSkip, postSkip, _comparer,
                                                     _diffResultOption.DiffResultBuilder);
            _lcsAlgo.Calculate();
            return _diffResultOption.DiffResultBuilder;
        }
        

        private int CalculatePostSkip(int preSkip, string[] sourceLines, string[] targetLines)
        {
            int postSkip=0;
            int leftLen = sourceLines.Length;
            int rightLen = targetLines.Length;
            while (postSkip < leftLen && postSkip < rightLen &&
                postSkip < (leftLen - preSkip) &&
                _comparer.Equals(sourceLines[leftLen - postSkip - 1], targetLines[rightLen - postSkip - 1]))
            {
                postSkip++;
            }

            return postSkip;
        }

        /// <summary>
        /// This method is an optimization that
        /// skips matching elements at the start of
        /// the arrays being diff'ed
        /// </summary>
        private int CalculatePreSkip(string[] sourceLines, string[] targetLines)
        {
            int preSkip=0;
            int leftLen = sourceLines.Length;
            int rightLen = targetLines.Length;
            while (preSkip < leftLen && preSkip < rightLen &&
                _comparer.Equals(sourceLines[preSkip], targetLines[preSkip]))
            {
                preSkip++;
            }

            return preSkip;
        }

    }
}
