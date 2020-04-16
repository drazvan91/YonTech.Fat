using System;
using System.Linq;
using Xunit;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

namespace Yontech.Fat.Tests.Extensions
{
    public static class RunResultsExtensions
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

        public static void AssertTestHasPassed(this RunResults runResults, string testName)
        {
            var key = runResults.TestResults.Keys.FirstOrDefault(k => k.Contains(testName));
            Assert.False(key == null, $"No test has name {testName}");

            var testResult = runResults.TestResults[key].Result;
            Assert.True(testResult == RunTestResultType.Passed, $"The test {testName} should have passed");
        }

        public static void AssertTestHasFailed(this RunResults runResults, string testName)
        {
            var key = runResults.TestResults.Keys.FirstOrDefault(k => k.Contains(testName));
            Assert.False(key == null, $"No test has name {testName}");

            var testResult = runResults.TestResults[key].Result;
            Assert.True(testResult == RunTestResultType.Failed, $"The test {testName} should have failed");
        }

        public static void AssertTestWasSkipped(this RunResults runResults, string testName)
        {
            var key = runResults.TestResults.Keys.FirstOrDefault(k => k.Contains(testName));
            Assert.False(key == null, $"No test has name {testName}");

            var testResult = runResults.TestResults[key].Result;
            Assert.True(testResult == RunTestResultType.Skipped, $"The test {testName} should have been skipped");
        }
    }
}
