// inspiration: https://github.com/xunit/visualstudio.xunit/blob/master/src/xunit.runner.visualstudio/VsTestRunner.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Runner;
using Yontech.Fat.TestAdapter.Factories;

namespace Yontech.Fat.TestAdapter
{
    [DefaultExecutorUri(Constants.ExecutorUri)]
    public class VsTestDiscoverer : ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            var testCases = DiscoverTestCases(sources);
            foreach (var testCase in testCases)
            {
                discoverySink.SendTestCase(testCase);
            }
        }

        public IEnumerable<TestCase> DiscoverTestCases(IEnumerable<string> sources)
        {
            var testCaseFactory = new TestCaseFactory(Constants.ExecutorUri);
            var fatDiscoverer = new FatDiscoverer();

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