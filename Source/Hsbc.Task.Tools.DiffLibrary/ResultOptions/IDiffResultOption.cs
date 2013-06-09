using Hsbc.Task.Tools.DiffLibrary.Model;
using Hsbc.Task.Tools.DiffLibrary.ReportBuilder;
using Hsbc.Task.Tools.DiffLibrary.ResultFilter;
using Hsbc.Task.Tools.DiffLibrary.ResultPresenter;

namespace Hsbc.Task.Tools.DiffLibrary.ResultOptions
{
    public interface IDiffResultOption<TResult> where TResult : IDiffResult
    {
        IDiffResultFilter DiffResultFilter { get; }
        IDiffResultPresenter DiffResultPresenter { get; }
        IDiffResultReportBuilder<string, TResult> DiffResultBuilder { get; }
    }
}