using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ReportBuilder;
using Hsbc.Task.Tools.DiffLibrary.ResultPresenter;

namespace Hsbc.Task.Tools.DiffLibrary.ResultOptions
{
    public class ShowOnlyDifferencesInPerLineStyleResultOption : ShowOnlyDifferencesOptionBase
    {
        private readonly IDiffResultPresenter _diffResultPresenter;
        private readonly IDiffResultReportBuilder<string, IDiffResult> _diffResultBuilder; 

        public ShowOnlyDifferencesInPerLineStyleResultOption()
        {
            _diffResultPresenter = new PerLineStyleDiffResultPresenter();
            _diffResultBuilder = DiffResultBuilderFactory.Instance.GetBuilder<string>(typeof(PerLineDiffResult), this);
        }

        public override IDiffResultPresenter DiffResultPresenter
        {
            get { return _diffResultPresenter; }
        }

        public override IDiffResultReportBuilder<string, IDiffResult> DiffResultBuilder
        {
            get { return _diffResultBuilder; }
        }
    }
}