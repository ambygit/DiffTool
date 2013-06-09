using Hsbc.Task.Tools.DiffLibrary.Model;

namespace Hsbc.Task.Tools.DiffLibrary.ResultFilter
{
    public class ShowOnlyDifferencesResultFilter : IDiffResultFilter
    {
        public bool CanAppend(IDiffResult diffResultRecord)
        {
            return diffResultRecord.DiffStatus != EDiffStatus.None;
        }
    }
}