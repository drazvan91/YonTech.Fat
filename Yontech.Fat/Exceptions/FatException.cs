using System;

namespace Yontech.Fat.Exceptions
{
    public class FatException : Exception
    {
        public FatException(string message, params object[] args)
            : base(string.Format(message, args)) { }

        public FatException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
