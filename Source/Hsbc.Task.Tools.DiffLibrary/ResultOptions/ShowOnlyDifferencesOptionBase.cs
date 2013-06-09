using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ReportBuilder;
using Hsbc.Task.Tools.DiffLibrary.ResultFilter;
using Hsbc.Task.Tools.DiffLibrary.ResultPresenter;

namespace Hsbc.Task.Tools.DiffLibrary.ResultOptions
{
    public abstract class ShowOnlyDifferencesOptionBase : IDiffResultOption<IDiffResult>
    {
        private readonly IDiffResultFilter _diffResultFilter;


        protected ShowOnlyDifferencesOptionBase()
        {
            _diffResultFilter = new ShowOnlyDifferencesResultFilter();
            
        }

        public IDiffResultFilter DiffResultFilter
        {
            get { return _diffResultFilter; }
        }

        public abstract IDiffResultPresenter DiffResultPresenter { get; }

        public abstract IDiffResultReportBuilder<string, IDiffResult> DiffResultBuilder { get; }
    }
}