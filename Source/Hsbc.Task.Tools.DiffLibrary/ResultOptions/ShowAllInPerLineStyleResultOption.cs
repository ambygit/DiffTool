using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ReportBuilder;
using Hsbc.Task.Tools.DiffLibrary.ResultFilter;
using Hsbc.Task.Tools.DiffLibrary.ResultPresenter;

namespace Hsbc.Task.Tools.DiffLibrary.ResultOptions
{
    public class ShowAllInPerLineStyleResultOption : IDiffResultOption<IDiffResult>
    {
        private readonly IDiffResultPresenter _diffResultPresenter;
        private readonly IDiffResultFilter _diffResultFilter;
        private readonly IDiffResultReportBuilder<string, IDiffResult> _diffResultBuilder; 

        public ShowAllInPerLineStyleResultOption()
        {
            _diffResultPresenter = new PerLineStyleDiffResultPresenter();
            _diffResultFilter = new NullDiffResultFilter();
            _diffResultBuilder = DiffResultBuilderFactory.Instance.GetBuilder<string>(typeof(PerLineDiffResult), this);
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