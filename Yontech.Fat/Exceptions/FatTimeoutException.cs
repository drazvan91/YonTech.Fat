using System;

namespace Yontech.Fat.Exceptions
{
    public class FatTimeoutException : Exception
    {
        public FatTimeoutException(string message, params object[] args)
            : base(string.Format(message, args)) { }

        public FatTimeoutException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
