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
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.TestAdapter.Factories;

namespace Yontech.Fat.TestAdapter
{
    [ExtensionUri(Constants.ExecutorUri)]
    public class VsTestExecutor : ITestExecutor
    {
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var assemblies = tests.Select(s => Assembly.LoadFile(s.Source)).ToList();

            var testCaseFactory = new TestCaseFactory(Constants.ExecutorUri);
            var interceptor = new VsTestInterceptor(frameworkHandle, testCaseFactory);
            var filter = new VsTestCaseFilterByFullName(tests.Select(t => t.FullyQualifiedName));

            var loggerFactory = new ConsoleLoggerFactory();

            var fatRunner = new FatRunner(loggerFactory, (options) =>
            {
                options.Filter = filter;
                var interceptors = options.Interceptors?.ToList() ?? new List<FatInterceptor>();
                interceptors.Add(interceptor);

                options.Interceptors = interceptors;
            });

            fatRunner.Run();
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var assemblies = sources.Select(s => Assembly.LoadFile(s)).ToList();
            var testCaseFactory = new TestCaseFactory(Constants.ExecutorUri);
            var simpleFilter = new VsTestCaseFilter(runContext, testCaseFactory);

            var interceptor = new VsTestInterceptor(frameworkHandle, testCaseFactory);

            var loggerFactory = new ConsoleLoggerFactory();

            var fatRunner = new FatRunner(loggerFactory, (options) =>
            {
                options.Filter = simpleFilter;
                var interceptors = options.Interceptors?.ToList() ?? new List<FatInterceptor>();
                interceptors.Add(interceptor);

                options.Interceptors = interceptors;
            });

            fatRunner.Run(assemblies);
        }
    }
}
