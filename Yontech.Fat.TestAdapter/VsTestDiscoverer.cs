// inspiration: https://github.com/xunit/visualstudio.xunit/blob/master/src/xunit.runner.visualstudio/VsTestRunner.cs
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Yontech.Fat.Discoverer;
using Yontech.Fat.TestAdapter.Factories;
using Yontech.Fat.TestAdapter.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.TestAdapter
{
    [DefaultExecutorUri(Constants.ExecutorUri)]
    public class VsTestDiscoverer : ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            var testCases = DiscoverTestCases(sources, logger);
            foreach (var testCase in testCases)
            {
                discoverySink.SendTestCase(testCase);
            }
        }

        private IEnumerable<TestCase> DiscoverTestCases(IEnumerable<string> sources, IMessageLogger messageLogger)
        {
            var assemblyDiscoverer = new AssemblyDiscoverer();
            var loggerFactory = new VsTestLoggerFactory(messageLogger);

            var testCaseFactory = new TestCaseFactory(Constants.ExecutorUri);
            var fatDiscoverer = new FatDiscoverer(assemblyDiscoverer, loggerFactory);

            foreach (var source in sources)
            {
                var assembly = Assembly.LoadFile(source);
                var testCases = fatDiscoverer.DiscoverTestCases(assembly);
                foreach (var testCase in testCases)
                {
                    yield return testCaseFactory.Create(testCase);
                }
            }
        }
    }
}
