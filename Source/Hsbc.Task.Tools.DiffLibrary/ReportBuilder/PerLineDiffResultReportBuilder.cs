using System;
using System.Collections.Generic;
using System.Linq;
using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ResultOptions;
using Hsbc.Task.Tools.DiffLibrary.ResultPresenter;

namespace Hsbc.Task.Tools.DiffLibrary.ReportBuilder
{
    internal class PerLineDiffResultReportBuilder : IDiffResultReportBuilder<string, IDiffResult>
    {
        private readonly IDiffResultOption<IDiffResult> _diffResultOption;
        private readonly List<PerLineDiffResult> _perLineDiffResult;
        private readonly IDiffResultPresenter _diffResultPresenter;

        public PerLineDiffResultReportBuilder(IDiffResultOption<IDiffResult> diffResultOption)
        {
            _diffResultOption = diffResultOption;
            _diffResultPresenter = diffResultOption.DiffResultPresenter;
            _perLineDiffResult = new List<PerLineDiffResult>();
        }

        public void Append(EDiffStatus diffStatus, int recordPosition, string record, bool isSourceRecord)
        {
            var diffResultRecord = PerLineDiffResult.Create(diffStatus, recordPosition, record, isSourceRecord);
            if (_diffResultOption.DiffResultFilter.CanAppend(diffResultRecord))
            {
                _perLineDiffResult.Add(diffResultRecord);
            }
        }

        public List<IDiffResult> GetRecords()
        {
            //Did not had time to install and move all setup to 4.0 - was best suited to use Covariance.
            return _perLineDiffResult.Cast<IDiffResult>().ToList();
        }

        public void Print(Action<string> printingAction)
        {
            foreach (var perLineDiffResult in _perLineDiffResult)
            {
                var resultToPrint = string.Format("{0}\t{1}", _diffResultPresenter.PresentPrefix(perLineDiffResult.DiffStatus), perLineDiffResult.AffectedLine);                
                printingAction(resultToPrint);
            }
        }
    }
}