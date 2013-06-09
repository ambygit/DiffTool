using Hsbc.Task.Tools.DiffLibrary.Model;

namespace Hsbc.Task.Tools.DiffLibrary.ResultFilter
{
    public class NullDiffResultFilter : IDiffResultFilter
    {
        public bool CanAppend(IDiffResult diffResultRecord)
        {
            return true;
        }
    }
}