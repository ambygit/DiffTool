using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ReportBuilder;
using Hsbc.Task.Tools.DiffLibrary.ResultFilter;
using Hsbc.Task.Tools.DiffLibrary.ResultPresenter;

namespace Hsbc.Task.Tools.DiffLibrary.ResultOptions
{
    public class ShowAllInLineRangeStyleResultOption : IDiffResultOption<IDiffResult>
    {
        private readonly IDiffResultPresenter _diffResultPresenter;
        private readonly IDiffResultFilter _diffResultFilter;
        private readonly IDiffResultReportBuilder<string, IDiffResult> _diffResultBuilder; 

        public ShowAllInLineRangeStyleResultOption()
        {
            _diffResultPresenter = new UnixStyleDiffResultPresenter();
            _diffResultFilter = new NullDiffResultFilter();
            _diffResultBuilder = DiffResultBuilderFactory.Instance.GetBuilder<string>(typeof(LinesRangeDiffResult), this);
        }

        public IDiffResultFilter DiffResultFilter
        {
            get { return _diffResultFilter; }
        }

        public IDiffResultPresenter DiffResultPresenter
        {
            get { return _diffResultPresenter; }
        }

        public IDiffResultReportBuilder<string, IDiffResult> DiffResultBuilder
        {
            get { return _diffResultBuilder; }
        }
    }
}