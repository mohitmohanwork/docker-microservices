using System;

namespace ZNxt.Net.Core.Exceptions
{
    public class DuplicateDBIDException : ExceptionBase
    {
        public DuplicateDBIDException(int errorCode, string message, Exception ex = null)
            : base(errorCode, message, ex)
        {
        }
    }
}