using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class RunReturnTests
    {
        MockedLoggerFactory mockedLoggerFactory;
        StreamProvider streamProvider;

        public RunReturnTests()
        {
            mockedLoggerFactory = new MockedLoggerFactory();
            streamProvider = new StreamProvider(mockedLoggerFactory);
        }

        [Fact]
        public void When_all_tests_pass_Then_run_returns_0_failed_And_all_passed()
        {

            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            var config = new Alfa.Config1();
            config.LogLevel = LogLevel.Debug;
            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);

            var result = runner.Run<Alfa.TestCases.AllTestsPass>();

            Assert.Equal(2, result.Passed);
            Assert.Equal(0, result.Failed);
        }

        [Fact]
        public void When_some_tests_fail_Then_run_returns_non_zero_failed_And_non_zero_passed()
        {
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            var config = new Alfa.Config1();
            config.LogLevel = LogLevel.Debug;
            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);

            var result = runner.Run<Alfa.TestCases.TwoTestsFail>();

            Assert.Equal(3, result.Passed);
            Assert.Equal(2, result.Failed);
        }

        [Fact]
        public void When_all_tests_fail_Then_run_returns_zero_passed_And_non_zero_failed()
        {
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            var config = new Alfa.Config1();
            config.LogLevel = LogLevel.Debug;
            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);

            var result = runner.Run<Alfa.TestCases.AllTestsFail>();

            Assert.Equal(0, result.Passed);
            Assert.Equal(3, result.Failed);
        }
    }
}
