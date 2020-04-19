using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class RunWithDefaultConfigTests
    {
        private readonly MockedLoggerFactory mockedLoggerFactory;
        private readonly FatRunner runner;

        public RunWithDefaultConfigTests()
        {
            mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Beta.Class1).Assembly);

            runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, (options) =>
            {
            });
        }

        [Fact]
        public void When_run_with_default_settings_Then_happy_flow()
        {
            var result = runner.Run();

            Assert.Equal(0, result.Failed);
            Assert.Equal(1, result.Passed);
        }
    }
}
