using System;
using System.Linq;
using Xunit;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

namespace Yontech.Fat.Tests.Extensions
{
    public static class RunResultsExtensions
    {
        public static void AssertResult(this RunResults runResults, int passedTests, int failedTests)
        {
            if (runResults.Passed != passedTests || runResults.Failed != failedTests)
            {
                var allLogs = GetLogs(runResults);
                Assert.True(false, $"The TestClass should have {passedTests} tests passed and {failedTests} failed " +
                    $"but instead it had {runResults.Passed} and {runResults.Failed}. Logs: {allLogs}");
            }
        }

        public static void AssertTestHasLog(this RunResults runResults, string testName, string logMessage)
        {
            var key = runResults.TestResults.Keys.FirstOrDefault(k => k.Contains(testName));
            Assert.False(key == null, $"No test has name {testName}");

            var allLogs = GetLogs(runResults, key);

            var hasLog = runResults.TestResults[key].Logs.Any(l => l.Message.Contains(logMessage));
            Assert.True(hasLog, $"Test should have log '{logMessage}' but instead it had: {allLogs}");
        }

        public static void AssertTestHasLog(this RunResults runResults, string testName, LogLevel logLevel, string logMessage)
        {
            var key = runResults.TestResults.Keys.FirstOrDefault(k => k.Contains(testName));
            Assert.False(key == null, $"No test has name {testName}");

            var allLogs = GetLogs(runResults, key);

            var hasLog = runResults.TestResults[key].Logs.Any(l => l.Message.Contains(logMessage));
            Assert.True(hasLog, $"Test should have log '{logMessage}' but instead it had: {allLogs}");
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

        private static string GetLogs(RunResults runResults, string testName)
        {
            var logs = runResults.TestResults[testName].Logs;
            return string.Join(Environment.NewLine, logs.Select(l => l.Message));
        }

        private static string GetLogs(RunResults runResults)
        {
            var logs = runResults.TestResults.SelectMany(testResult => testResult.Value.Logs);
            return string.Join(Environment.NewLine, logs.Select(l => l.Message));
        }
    }
}
