using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class EnvDataTests
    {
        private readonly MockedLoggerFactory mockedLoggerFactory;
        private readonly FatRunner runner;

        public EnvDataTests()
        {
            this.mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            this.runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, new Alfa.Config1());
        }

        [Fact]
        public void Happy_flow()
        {
            var result = runner.Run<Alfa.EnvDataTestCases.HappFlowTests>();

            Assert.Equal(0, result.Failed);
            Assert.Equal(2, result.Passed);

            mockedLoggerFactory.AssertContains(LogLevel.Info, "Alfa.SimpleEnvData loaded from file 'files/simple-env-data.json'");
        }

        [Fact]
        public void When_file_not_found_then_empty_object_is_provided()
        {
            var result = runner.Run<Alfa.EnvDataTestCases.FileNotFoundTests>();

            mockedLoggerFactory.AssertContains(LogLevel.Error, "File 'files/no-file.json' could not be found. Empty Alfa.FileNotFoundEnvData object will be provided");

            Assert.Equal(0, result.Failed);
            Assert.Equal(1, result.Passed);
        }
    }
}
