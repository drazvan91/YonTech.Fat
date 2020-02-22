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

            var options = new FatRunnerOptions()
            {
                AutomaticDriverDownload = true,
                Browser = BrowserType.Chrome,
                Filter = simpleFilter,
                Interceptors = new List<FatInterceptor>(){
                    new Interceptor(frameworkHandle, testCaseFactory)
                }
            };

            var fatRunner = new FatRunner(options);
            fatRunner.Run(assemblies);
        }
    }


}
