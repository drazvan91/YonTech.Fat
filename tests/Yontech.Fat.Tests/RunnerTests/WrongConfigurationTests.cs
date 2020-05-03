using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Tests.Mocks;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class WrongConfigurationTests
    {
        private readonly MockedExecutionContext context;
        private readonly FatRunner runner;

        public WrongConfigurationTests()
        {
            var config = new Alfa.Config1();
            this.context = new MockedExecutionContext(typeof(Delta.Config).Assembly);
            this.runner = new FatRunner(context);
        }

        [Fact]
        public void When_assembly_not_registered_Then_throws_exception()
        {
            var result = runner.Run<Alfa.TestCases.AlfaTest1>();

            Assert.Equal(0, result.Passed);
            Assert.Equal(1, result.Failed);
        }
    }
}
