using System;

namespace Yontech.Fat.Exceptions
{
    public class FatTestCaseException : FatException
    {
        public int BrowserId { get; }
        internal FatTestCaseException(int browserId, string message, params object[] args)
            : base(string.Format(message, args))
        {
            this.BrowserId = browserId;
        }

        internal FatTestCaseException(int browserId, string message, Exception innerException)
            : base(message, innerException)
        {
            this.BrowserId = browserId;
        }
    }
}
