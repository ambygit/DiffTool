using Hsbc.Task.Tools.DiffLibrary.Model;

namespace Hsbc.Task.Tools.DiffLibrary.ResultFilter
{
    public interface IDiffResultFilter
    {
        bool CanAppend(IDiffResult diffResultRecord);
    }
}