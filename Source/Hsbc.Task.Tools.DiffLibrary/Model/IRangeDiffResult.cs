namespace Hsbc.Task.Tools.DiffLibrary.Model
{
    public interface IRangeDiffResult : IDiffResult
    {
        Range SourceRange { get; }
        Range TargetRange { get; }        
    }
}