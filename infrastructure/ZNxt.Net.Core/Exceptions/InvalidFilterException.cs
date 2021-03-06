using System;

namespace ZNxt.Net.Core.Exceptions
{
    public class InvalidFilterException : ExceptionBase
    {
        public InvalidFilterException(int errorCode, string message, Exception ex = null)
            : base(errorCode, message, ex)
        {
        }
    }
}