using Xunit;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class RunnerLogsTests
    {
        [Fact]
        public void When_run_it_logs_the_fat_framework_version()
        {
            MockedLoggerFactory mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            var config = new Alfa.Config1();
            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);

            var result = runner.Run<Alfa.TestCases.AllTestsPass>();
            mockedLoggerFactory.AssertContains(LogLevel.Info, "Using Fat Framework version");
        }
    }
}
