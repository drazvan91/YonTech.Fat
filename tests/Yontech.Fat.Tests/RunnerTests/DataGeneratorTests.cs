using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class DataGeneratorTests
    {
        private readonly MockedLoggerFactory mockedLoggerFactory;
        private readonly FatRunner runner;

        public DataGeneratorTests()
        {
            this.mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            this.runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, new Alfa.Config1());
        }

        [Fact]
        public void DataGenerator_happy_flow()
        {
            var result = runner.Run<Alfa.GeneratedDataTestCases.HappFlowTests>();

            mockedLoggerFactory.AssertContains(LogLevel.Info, "Person name: Razvan 1");
            mockedLoggerFactory.AssertContains(LogLevel.Info, "Person name: Razvan 3");

            mockedLoggerFactory.AssertContains(LogLevel.Info, "String value: string number 1");
            mockedLoggerFactory.AssertContains(LogLevel.Info, "String value: string number 2");

            Assert.Equal(0, result.Failed);
        }

        [Fact]
        public void DataGenerator_no_property_found()
        {
            var result = runner.Run<Alfa.GeneratedDataTestCases.PropertyNotFoundTests>();

            mockedLoggerFactory.PrintAllLogs();
            mockedLoggerFactory.AssertContains(LogLevel.Error, "Cannot find property 'age' on type 'Person'");

            Assert.Equal(1, result.Failed);
        }
    }
}
