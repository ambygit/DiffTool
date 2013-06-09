using Hsbc.Task.Tools.DiffLibrary.Model;

namespace Hsbc.Task.Tools.DiffLibrary.ResultPresenter
{
    public interface IDiffResultPresenter
    {
        string PresentPrefix(EDiffStatus diffStatus);        
    }

    public interface IRangeDiffResultPresenter : IDiffResultPresenter
    {
        string PresentRange(Range sourceRange, EDiffStatus diffStatus, Range targetRange);  
    }

}