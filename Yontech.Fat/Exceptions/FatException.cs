using System;

namespace Yontech.Fat.Exceptions
{
    public class FatException : Exception
    {
        public FatException(string message) : base(message) { }
        public FatException(string message, Exception innerException) : base(message, innerException) { }
    }
}
