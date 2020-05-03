using Xunit;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Tests.Mocks;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class RunnerLogsTests
    {
        private readonly MockedExecutionContext context;
        private readonly FatRunner runner;

        public RunnerLogsTests()
        {
            var config = new Alfa.Config1();
            this.context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);
            this.runner = new FatRunner(context);
        }

        [Fact]
        public void When_run_it_logs_the_fat_framework_version()
        {
            var result = runner.Run<Alfa.TestCases.AllTestsPass>();
            context.MockedLoggerFactory.AssertContains(LogLevel.Info, "Using Fat Framework version");
        }
    }
}
