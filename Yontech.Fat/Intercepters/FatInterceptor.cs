using System;
using Yontech.Fat.Discoverer;

namespace Yontech.Fat.Interceptors
{
    public abstract class FatInterceptor
    {
        protected internal virtual void OnExecutionStarts(ExecutionStartsParams startsParams) { }
        protected internal virtual void OnExecutionFinished(ExecutionFinishedParams finishedParams) { }
        protected internal virtual void BeforeTestCase(FatTestCase testCase) { }
        protected internal virtual void BeforeTestClass(TestClassParams testClass) { }

        protected internal virtual void OnTestCasePassed(FatTestCase testCase, FatTestCasePassed passed) { }
        protected internal virtual void OnTestCaseFailed(FatTestCase testCase, FatTestCaseFailed failed) { }
        protected internal virtual void OnTestCaseSkipped(FatTestCase testCase) { }

        protected internal virtual void AfterTestClass(TestClassParams testClass) { }
    }
}