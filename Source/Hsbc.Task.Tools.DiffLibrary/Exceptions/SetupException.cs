namespace Hsbc.Task.Tools.DiffLibrary.Exceptions
{
    public class SetupException : DiffToolExceptionBase
    {
        public SetupException(string messageFormat, params object[] args) : base(messageFormat, args)
        {
        }
    }
}