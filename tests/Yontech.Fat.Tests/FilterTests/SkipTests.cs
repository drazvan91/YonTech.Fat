using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.FilterTests
{
    public class SkipTests
    {
        private readonly MockedLoggerFactory mockedLoggerFactory;
        private readonly FatRunner runner;

        public SkipTests()
        {
            this.mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            var config = new Alfa.Config1();
            config.LogLevel = LogLevel.Debug;

            this.runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, config);
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
    }
}
