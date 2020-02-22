using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Interceptors;
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
                ComputerName = "todo: some computer", // todo:
                Outcome = TestOutcome.Failed,
                Duration = failed.Duration,
                ErrorMessage = failed.Exception.Message,
                ErrorStackTrace = failed.Exception.StackTrace
            };

            frameworkHandle.RecordResult(testResult);
        }
        protected override void OnTestCasePassed(FatTestCase fatTestCase, FatTestCasePassed passed)
        {
            var testCase = this.testCaseFactory.Create(fatTestCase);
            var testResult = new TestResult(testCase)
            {
                ComputerName = "todo: some computer", // todo:
                Outcome = TestOutcome.Passed,
                Duration = passed.Duration,
            };

            frameworkHandle.RecordResult(testResult);
        }
    }
}