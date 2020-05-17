using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Tests.Mocks;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class RunWithDefaultConfigTests
    {
        private readonly MockedExecutionContext context;
        private readonly FatRunner runner;

        public RunWithDefaultConfigTests()
        {
            var config = new Alfa.Config1();
            this.context = new MockedExecutionContext(config, typeof(Beta.Class1).Assembly);
            this.runner = new FatRunner(context);
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
