﻿using System;

namespace Yontech.Fat.Exceptions
{
    public class FatAssertException : FatException
    {
        public FatAssertException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        public FatAssertException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
