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
using Yontech.Fat.TestAdapter.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.TestAdapter
{
    [ExtensionUri(Constants.ExecutorUriString)]
    public class VsTestExecutor : ITestExecutor
    {
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var assemblies = tests.Select(s => Assembly.LoadFile(s.Source)).ToList();

            var testCaseFactory = new TestCaseFactory(Constants.ExecutorUriString);
            var interceptor = new VsTestInterceptor(frameworkHandle, testCaseFactory);
            var filter = new VsTestCaseFilterByFullName(tests.Select(t => t.FullyQualifiedName));

            var execContext = new FatExecutionContext();
            execContext.AssemblyDiscoverer = new AssemblyDiscoverer();
            execContext.LoggerFactory = new VsTestLoggerFactory(frameworkHandle);
            execContext.StreamReaderProvider = new StreamProvider(execContext.LoggerFactory);

            var fatRunner = new FatRunner(execContext, (options) =>
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
            var testCaseFactory = new TestCaseFactory(Constants.ExecutorUriString);
            var simpleFilter = new VsTestCaseFilter(runContext, testCaseFactory);

            var interceptor = new VsTestInterceptor(frameworkHandle, testCaseFactory);

            var execContext = new FatExecutionContext();
            execContext.AssemblyDiscoverer = new AssemblyDiscoverer();
            execContext.LoggerFactory = new VsTestLoggerFactory(frameworkHandle);
            execContext.StreamReaderProvider = new StreamProvider(execContext.LoggerFactory);

            var fatRunner = new FatRunner(execContext, (options) =>
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
