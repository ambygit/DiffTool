using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ReportBuilder;
using Hsbc.Task.Tools.DiffLibrary.ResultFilter;
using Hsbc.Task.Tools.DiffLibrary.ResultPresenter;

namespace Hsbc.Task.Tools.DiffLibrary.ResultOptions
{
    public class DefaultDiffResultOption : IDiffResultOption<IDiffResult>
    {
        private readonly IDiffResultFilter _diffResultFilter;
        private readonly IDiffResultPresenter _diffResultPresenter;
        private readonly IDiffResultReportBuilder<string, IDiffResult> _diffResultBuilder; 

        public DefaultDiffResultOption()
        {
            _diffResultFilter = new ShowOnlyDifferencesResultFilter();
            _diffResultPresenter = new UnixStyleDiffResultPresenter();
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