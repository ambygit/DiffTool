using System;
using System.Collections.Generic;
using System.Linq;
using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ResultOptions;
using Hsbc.Task.Tools.DiffLibrary.ResultPresenter;

namespace Hsbc.Task.Tools.DiffLibrary.ReportBuilder
{
    internal class LinesRangeDiffResultReportBuilder : IDiffResultReportBuilder<string,IDiffResult>
    {
        private readonly IRangeDiffResultPresenter _diffResultPresenter;
        private readonly List<LinesRangeDiffResult> _lineRangeDiffResults;
        private LinesRangeDiffResult _currentRangeBucket = new LinesRangeDiffResult(){DiffStatus = EDiffStatus.None};
        private readonly IDiffResultOption<IDiffResult> _diffResultOption;

        public LinesRangeDiffResultReportBuilder(IDiffResultOption<IDiffResult> diffResultOption)
        {
            _diffResultPresenter = diffResultOption.DiffResultPresenter as IRangeDiffResultPresenter;
            _lineRangeDiffResults = new List<LinesRangeDiffResult>();
            _diffResultOption = diffResultOption;
        }

        public void Append(EDiffStatus diffStatus, int recordPosition, string record, bool isSourceRecord)
        {
            var diffResultRecord = PerLineDiffResult.Create(diffStatus, recordPosition, record, isSourceRecord);
            if (_diffResultOption.DiffResultFilter.CanAppend(diffResultRecord))
            {
                //The per line results will be appended in order, do require bucketing into range
                AddIntoRange(diffResultRecord);
            }
        }

        private void AddIntoRange(PerLineDiffResult perlineDiffResult)
        {
            if (CheckIfSameStatus(perlineDiffResult))
            {
                //should modify current range bucket
                _currentRangeBucket.Modify(perlineDiffResult);
            }
            else
            {       
                _currentRangeBucket = LinesRangeDiffResult.CreateNew(perlineDiffResult);
                _lineRangeDiffResults.Add(_currentRangeBucket);
            }
        }

        private bool CheckIfSameStatus(PerLineDiffResult perlineDiffResult)
        {
            return _currentRangeBucket.DiffStatus == perlineDiffResult.DiffStatus;
        }

        //Need to work more, possible requires a change in LCS 
        //private bool IsDeletedAndAdded(PerLineDiffResult perLineDiffResult)
        //{
        //    return _currentRangeBucket.DiffStatus == EDiffStatus.Deleted &&
        //           perLineDiffResult.DiffStatus == EDiffStatus.Added;
        //}

        public List<IDiffResult> GetRecords()
        {
            //Did not had time to install and move all setup to 4.0 - was best suited to use Covariance.
            return _lineRangeDiffResults.Cast<IDiffResult>().ToList();
        }

        public void Print(Action<string> printingAction)
        {            
            foreach (var linesRangeDiffResult in _lineRangeDiffResults)
            {
                printingAction(_diffResultPresenter.PresentRange(linesRangeDiffResult.SourceRange,
                                                                 linesRangeDiffResult.DiffStatus,
                                                                 linesRangeDiffResult.TargetRange));
                foreach (var affectedLine in linesRangeDiffResult.AffectedLines)
                {
                    var resultToPrint = string.Format("{0}\t{1}", _diffResultPresenter.PresentPrefix(linesRangeDiffResult.DiffStatus), affectedLine);
                    printingAction(resultToPrint);
                }
            }
        }
    }
}