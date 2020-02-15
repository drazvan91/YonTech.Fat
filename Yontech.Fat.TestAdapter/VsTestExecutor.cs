// inspiration: https://github.com/xunit/visualstudio.xunit/blob/master/src/xunit.runner.visualstudio/VsTestRunner.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Runner;
using Yontech.Fat.TestAdapter.Factories;

namespace Yontech.Fat.TestAdapter
{
    [ExtensionUri(Constants.ExecutorUri)]
    public class TestExecutor : ITestExecutor
    {
        public void Cancel()
        {
            throw new NotImplementedException();

        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var stopwatch = Stopwatch.StartNew();
            var logger = new LoggerHelper(frameworkHandle, stopwatch);

            var filter = new TestCaseFilter(runContext, logger, "assembly file name todo", new HashSet<string>());
            var filteredTestCases = tests.Where(dtc => filter.MatchTestCase(dtc)).ToList();

            foreach (var testCase in filteredTestCases)
            {
                frameworkHandle.RecordStart(testCase);
                var testResult = new TestResult(testCase)
                {
                    ComputerName = "todo: some computer", // todo:
                    Outcome = TestOutcome.Passed,
                };

                frameworkHandle.RecordResult(testResult);
            }
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var assemblies = sources.Select(s => Assembly.LoadFile(s)).ToList();
            var testCaseFactory = new TestCaseFactory(Constants.ExecutorUri);
            var simpleFilter = new SimpleTestCaseFilter(runContext, testCaseFactory);

            var options = new FatRunOptions()
            {
                Assemblies = assemblies,
                AutomaticDriverDownload = true,
                Browser = BrowserType.Chrome,
                Filter = simpleFilter,
                Interceptors = new List<FatInterceptor>(){
                    new Interceptor(frameworkHandle, testCaseFactory)
                }
            };

            var fatRunner = new FatRunner();
            fatRunner.Run(options);
        }
    }

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
