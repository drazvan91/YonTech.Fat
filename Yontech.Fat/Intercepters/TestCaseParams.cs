﻿using System;
using System.Collections.Generic;
using Yontech.Fat.Logging;

#pragma warning disable SA1649 // File name should match first type name
namespace Yontech.Fat.Interceptors
{
    public class ExecutionStartsParams { }
    public class ExecutionFinishedParams { }

    public class TestClassParams
    {
        public string TestClassFullName { get; set; }
    }

    public class FatTestCasePassed
    {
        public TimeSpan Duration { get; internal set; }
        public List<Log> Logs { get; internal set; } = new List<Log>();
    }

    public class FatTestCaseFailed
    {
        public TimeSpan Duration { get; internal set; }
        public List<Log> Logs { get; internal set; } = new List<Log>();
        public int? BrowserIndexWithCausedError { get; internal set; }
        public Exception Exception { get; internal set; }
    }
}
