using System;

namespace Yontech.Fat.Interceptors
{
    public class ExecutionStartsParams { }
    public class ExecutionFinishedParams { }


    public class TestClassParams
    {
        public string TestClassFullName { get; set; }
    }
    public class TestCaseParams
    {
        public string TestClassFullName { get; set; }
        public string TestCaseName { get; set; }
    }
    public class TestCaseResultParams : TestCaseParams
    {
        public TimeSpan Duration { get; set; }
    }

    public class FatTestCasePassed
    {
        public TimeSpan Duration { get; set; }

    }

    public class FatTestCaseFailed
    {
        public TimeSpan Duration { get; set; }
        public Exception Exception { get; internal set; }

    }



    public class OnTestCasePassedParams : TestCaseResultParams
    {
    }

    public class OnTestCaseFailedParams : TestCaseResultParams
    {
        public Exception ErrorMessage { get; internal set; }
    }

    public class OnTestCaseSkippedParams : TestCaseResultParams
    {
    }
}