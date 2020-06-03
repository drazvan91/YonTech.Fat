using System;

namespace Yontech.Fat.Exceptions
{
    public class FatAssertException : FatException
    {
        internal FatAssertException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        internal FatAssertException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
