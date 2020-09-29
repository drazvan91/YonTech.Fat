using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Tests.Mocks;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.FilterTests
{
    public class SkipTests
    {
        private readonly MockedExecutionContext context;
        private readonly FatRunner runner;

        public SkipTests()
        {
            var config = new Alfa.Config1();
            config.LogLevel = LogLevel.Debug;

            this.context = new MockedExecutionContext(config, typeof(Alfa.Config1).Assembly);
            this.runner = new FatRunner(this.context);
        }

        [Fact]
        public void WHEN_ignore_on_method_THEN_should_skip_test()
        {
            var result = runner.Run<Alfa.TestCases.OneTestSkipped>();

            Assert.Equal(1, result.Passed);
            Assert.Equal(0, result.Failed);
            Assert.Equal(1, result.Skipped);
        }

        [Fact]
        public void WHEN_ignore_on_class_THEN_all_tests_should_be_skipped()
        {
            var result = runner.Run<Alfa.TestCases.EntireClassSkipped>();

            Assert.Equal(0, result.Passed);
            Assert.Equal(0, result.Failed);
            Assert.Equal(2, result.Skipped);
        }

        [Fact]
        public void WHEN_ignore_on_class_THEN_before_all_tests_should_be_skipped()
        {
            var result = runner.Run<Alfa.TestCases.EntireClassSkipped>();

            this.context.MockedLoggerFactory.AssertDoesNotContain(LogLevel.Error, "This should not be called because the class is skipped");
        }
    }
}
