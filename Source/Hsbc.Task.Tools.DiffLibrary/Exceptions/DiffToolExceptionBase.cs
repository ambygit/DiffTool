using System;

namespace Hsbc.Task.Tools.DiffLibrary.Exceptions
{
    public abstract class DiffToolExceptionBase : ApplicationException
    {
        protected DiffToolExceptionBase(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }
    }
}