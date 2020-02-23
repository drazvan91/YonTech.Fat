using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;
using Yontech.Fat.TestAdapter.Factories;

namespace Yontech.Fat.TestAdapter
{
    internal class Interceptor : FatInterceptor
    {
        private readonly IFrameworkHandle frameworkHandle;
        private readonly TestCaseFactory testCaseFactory;

        public Interceptor(IFrameworkHandle frameworkHandle, TestCaseFactory testCaseFactory)
        {
            this.frameworkHandle = frameworkHandle;
            this.testCaseFactory = testCaseFactory;
        }

        protected override void BeforeTestCase(FatTestCase fatTestCase)
        {
            var testCase = this.testCaseFactory.Create(fatTestCase);
            frameworkHandle.RecordStart(testCase);
        }

        protected override void OnTestCaseFailed(FatTestCase fatTestCase, FatTestCaseFailed failed)
        {
            var testCase = this.testCaseFactory.Create(fatTestCase);
            var testResult = new TestResult(testCase)
            {
                ComputerName = Environment.MachineName,
                Outcome = TestOutcome.Failed,
                Duration = failed.Duration,
                ErrorMessage = failed.Exception.Message,
                ErrorStackTrace = failed.Exception.StackTrace
            };

            this.AddLogs(testResult, failed.Logs);

            frameworkHandle.RecordResult(testResult);
        }

        protected override void OnTestCasePassed(FatTestCase fatTestCase, FatTestCasePassed passed)
        {
            var testCase = this.testCaseFactory.Create(fatTestCase);
            var testResult = new TestResult(testCase)
            {
                ComputerName = Environment.MachineName,
                Outcome = TestOutcome.Passed,
                Duration = passed.Duration,
            };

            this.AddLogs(testResult, passed.Logs);

            frameworkHandle.RecordResult(testResult);
        }

        private void AddLogs(TestResult result, List<Log> logs)
        {
            foreach (var log in logs)
            {
                var category = MapCategory(log.Category);
                result.Messages.Add(new TestResultMessage(category, log.Message));
            }
        }

        private string MapCategory(string fatCategory)
        {
            switch (fatCategory)
            {
                case Log.ERROR: return TestResultMessage.StandardErrorCategory;
                case Log.DEBUG: return TestResultMessage.DebugTraceCategory;
                case Log.INFO: return TestResultMessage.StandardOutCategory;
                case Log.WARNING: return TestResultMessage.StandardOutCategory;
                default: return TestResultMessage.AdditionalInfoCategory;
            }
        }
    }
}