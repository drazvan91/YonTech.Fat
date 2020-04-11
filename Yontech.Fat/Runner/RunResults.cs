using System;
using System.Collections.Generic;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;

#pragma warning disable SA1649 // File name should match first type name

namespace Yontech.Fat.Runner
{
    public enum RunTestResultType
    {
        Passed,
        Skipped,
        Failed,
    }

    public class RunTestResult
    {
        public RunTestResultType Result { get; internal set; }
        public FatTestCase TestCase { get; internal set; }
        public Exception Error { get; internal set; }
        public List<Log> Logs { get; internal set; }
        public TimeSpan Duration { get; internal set; }
    }

    public class RunResults
    {
        public int Passed { get; private set; }
        public int Skipped { get; private set; }
        public int Failed { get; private set; }

        public Dictionary<string, RunTestResult> TestResults { get; private set; } = new Dictionary<string, RunTestResult>();

        internal void AddPassed(FatTestCase testCase, TimeSpan duration, List<Log> logs)
        {
            this.Passed++;
            this.TestResults.Add(testCase.FullyQualifiedName, new RunTestResult()
            {
                Result = RunTestResultType.Passed,
                TestCase = testCase,
                Logs = logs,
                Duration = duration,
            });
        }

        internal void AddSkipped(FatTestCase testCase)
        {
            this.Skipped++;
            this.TestResults.Add(testCase.FullyQualifiedName, new RunTestResult()
            {
                Result = RunTestResultType.Skipped,
                TestCase = testCase,
                Logs = new List<Log>(),
            });
        }

        internal void AddFailed(FatTestCase testCase, Exception ex, TimeSpan duration, List<Log> logs)
        {
            this.Failed++;
            this.TestResults.Add(testCase.FullyQualifiedName, new RunTestResult()
            {
                Result = RunTestResultType.Failed,
                TestCase = testCase,
                Logs = logs,
                Duration = duration,
                Error = ex,
            });
        }
    }
}
