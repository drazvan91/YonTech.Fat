using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yontech.Fat.ConsoleRunner.Results;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Interceptors;

namespace Yontech.Fat.ConsoleRunner
{
    internal class ConsoleRunnerInterceptor : FatInterceptor
    {
        private List<TestCollectionRunResult> _collections;

        protected override void OnExecutionStarts(ExecutionStartsParams startsParams)
        {
            this._collections = new List<TestCollectionRunResult>();
        }

        protected override void OnExecutionFinished(ExecutionFinishedParams finishedParams)
        {
            var reporter = new ConsoleReporter();

            reporter.Report(_collections);
        }

        protected override void BeforeTestCase(FatTestCase fatTestCase)
        {
        }

        protected override void OnTestCaseFailed(FatTestCase fatTestCase, FatTestCaseFailed failed)
        {
            var collection = fatTestCase.Method.ReflectedType.Assembly;
            var testClass = fatTestCase.Method.ReflectedType;
            var testCase = fatTestCase.Method;

            var testResult = AddResult(collection, testClass, testCase);
            testResult.Duration = failed.Duration;
            testResult.ErrorMessage = failed.Exception.Message;
            testResult.Exception = failed.Exception;
            testResult.Result = TestCaseRunResult.ResultType.Error;
            testResult.Logs = failed.Logs;
        }

        protected override void OnTestCasePassed(FatTestCase fatTestCase, FatTestCasePassed passed)
        {
            var collection = fatTestCase.Method.ReflectedType.Assembly;
            var testClass = fatTestCase.Method.ReflectedType;
            var testCase = fatTestCase.Method;

            var testResult = AddResult(collection, testClass, testCase);
            testResult.Duration = passed.Duration;
            testResult.Result = TestCaseRunResult.ResultType.Success;
            testResult.Logs = passed.Logs;
        }

        private TestCaseRunResult AddResult(Assembly collection, Type testClass, MethodInfo testCase)
        {
            var collResult = _collections.FirstOrDefault(c => c.Assembly == collection);
            if (collResult == null)
            {
                collResult = new TestCollectionRunResult(collection);
                _collections.Add(collResult);
            }

            var tcResult = collResult.TestClasses.FirstOrDefault(tc => tc.Class == testClass);
            if (tcResult == null)
            {
                tcResult = new TestClassRunResult(testClass);
                collResult.TestClasses.Add(tcResult);
            }

            var testResult = new TestCaseRunResult(testCase);
            tcResult.TestCases.Add(testResult);

            return testResult;
        }
    }
}
