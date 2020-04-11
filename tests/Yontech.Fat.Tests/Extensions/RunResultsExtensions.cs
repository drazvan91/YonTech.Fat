using System;
using System.Linq;
using Xunit;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

namespace Yontech.Fat.Tests.Exceptions
{
    public static class RunResultsExceptions
    {
        public static void AssertTestHasLog(this RunResults runResults, string testName, string logMessage)
        {
            var key = runResults.TestResults.Keys.FirstOrDefault(k => k.Contains(testName));
            Assert.False(key == null, $"No test has name {testName}");

            var hasLog = runResults.TestResults[key].Logs.Any(l => l.Message.Contains(logMessage));
            Assert.True(hasLog, $"Test should have log '{logMessage}'");
        }

        public static void AssertTestHasLog(this RunResults runResults, string testName, LogLevel logLevel, string logMessage)
        {
            var key = runResults.TestResults.Keys.FirstOrDefault(k => k.Contains(testName));
            Assert.False(key == null, $"No test has name {testName}");

            var hasLog = runResults.TestResults[key].Logs.Any(l => l.Message.Contains(logMessage));
            Assert.True(hasLog, $"Test should have log '{logMessage}'");
        }
    }
}
