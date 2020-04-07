using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class InlineDataTests
    {
        private readonly MockedLoggerFactory mockedLoggerFactory;
        private readonly FatRunner runner;

        public InlineDataTests()
        {
            this.mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            this.runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, new Alfa.Config1());
        }

        [Fact]
        public void Inline_happy_flow()
        {
            var result = runner.Run<Alfa.InlineDataTestCases.HappFlowTests>();

            mockedLoggerFactory.AssertContains(LogLevel.Info, "Test_one_string_inline_data: string value");
            mockedLoggerFactory.AssertContains(LogLevel.Info, "Test_one_number_inline_data: 4");

            mockedLoggerFactory.AssertContains(LogLevel.Info, "Test_multiple_inline_data_values: string 1");
            mockedLoggerFactory.AssertContains(LogLevel.Info, "Test_multiple_inline_data_values: string 3");

            mockedLoggerFactory.AssertContains(LogLevel.Info, "Test_multiple_params: string 1 2 string 3");

            Assert.Equal(0, result.Failed);
        }

        [Fact]
        public void Inline_parameter_type_missmatch()
        {
            var result = runner.Run<Alfa.InlineDataTestCases.ParameterTypeMissmatchTests>();

            mockedLoggerFactory.AssertContains(LogLevel.Error, "Object of type 'System.String' cannot be converted to type 'System.Int32'");

            Assert.Equal(0, result.Passed);
        }

        [Fact]
        public void Inline_parameters_number_missmatch()
        {
            var result = runner.Run<Alfa.InlineDataTestCases.ParametersNumberMissmatchTests>();

            mockedLoggerFactory.AssertContains(LogLevel.Error, "InlineData has defined 1 parameters but 2 are expected");
            mockedLoggerFactory.AssertContains(LogLevel.Error, "InlineData has defined 3 parameters but 2 are expected");

            Assert.Equal(0, result.Passed);
        }
    }
}
