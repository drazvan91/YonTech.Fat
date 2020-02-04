using System;

namespace Yontech.Fat.Interceptors
{
    public abstract class FatInterceptor
    {
        protected internal virtual void OnExecutionStarts(ExecutionStartsParams startsParams) { }
        protected internal virtual void OnExecutionFinished(ExecutionFinishedParams finishedParams) { }
        protected internal virtual void BeforeTestCase(TestCaseParams testCase) { }
        protected internal virtual void BeforeTestClass(TestClassParams testClass) { }

        protected internal virtual void OnTestCasePassed(OnTestCasePassedParams passedTest) { }
        protected internal virtual void OnTestCaseFailed(OnTestCaseFailedParams failedTest) { }
        protected internal virtual void OnTestCaseSkipped(OnTestCaseSkippedParams skippedTest) { }

        protected internal virtual void AfterTestClass(TestClassParams testClass) { }
    }
}