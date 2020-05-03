using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Tests.Mocks;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class RunReturnTests
    {
        private readonly MockedExecutionContext context;
        private readonly FatRunner runner;

        public RunReturnTests()
        {
            var config = new Alfa.Config1();
            this.context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);
            this.runner = new FatRunner(context);
        }

        [Fact]
        public void When_all_tests_pass_Then_run_returns_0_failed_And_all_passed()
        {
            var result = runner.Run<Alfa.TestCases.AllTestsPass>();

            Assert.Equal(2, result.Passed);
            Assert.Equal(0, result.Failed);
        }

        [Fact]
        public void When_some_tests_fail_Then_run_returns_non_zero_failed_And_non_zero_passed()
        {
            var result = runner.Run<Alfa.TestCases.TwoTestsFail>();

            Assert.Equal(3, result.Passed);
            Assert.Equal(2, result.Failed);
        }

        [Fact]
        public void When_all_tests_fail_Then_run_returns_zero_passed_And_non_zero_failed()
        {
            var result = runner.Run<Alfa.TestCases.AllTestsFail>();

            Assert.Equal(0, result.Passed);
            Assert.Equal(3, result.Failed);
        }
    }
}
