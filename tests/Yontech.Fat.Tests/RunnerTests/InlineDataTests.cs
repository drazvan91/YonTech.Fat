using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Tests.Extensions;
using Yontech.Fat.Tests.Mocks;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class InlineDataTests
    {
        private readonly MockedExecutionContext context;
        private readonly FatRunner runner;

        public InlineDataTests()
        {
            var config = new Alfa.Config1();
            this.context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);
            this.runner = new FatRunner(context);
        }

        [Fact]
        public void Inline_happy_flow()
        {
            var result = runner.Run<Alfa.InlineDataTestCases.HappFlowTests>();

            result.AssertTestHasLog("Test_one_string_inline_data", LogLevel.Info, "data: string value");
            result.AssertTestHasLog("Test_one_number_inline_data", LogLevel.Info, "data: 4");

            result.AssertTestHasLog("Test_multiple_inline_data_values", LogLevel.Info, "data: string 1");
            result.AssertTestHasLog("Test_multiple_inline_data_values", LogLevel.Info, "data: string 3");

            result.AssertTestHasLog("Test_multiple_params", LogLevel.Info, "test: string 1 2 string 3");

            Assert.Equal(0, result.Failed);
        }

        [Fact]
        public void Inline_parameter_type_missmatch()
        {
            var result = runner.Run<Alfa.InlineDataTestCases.ParameterTypeMissmatchTests>();

            result.AssertTestHasLog("Test_type_missmatch_inline_data", LogLevel.Error, "Object of type 'System.String' cannot be converted to type 'System.Int32'");

            Assert.Equal(0, result.Passed);
        }

        [Fact]
        public void Inline_parameters_number_missmatch()
        {
            var result = runner.Run<Alfa.InlineDataTestCases.ParametersNumberMissmatchTests>();

            result.AssertTestHasLog("Test_fewer_parameters", LogLevel.Error, "InlineData has defined 1 parameters but 2 are expected");
            result.AssertTestHasLog("Test_more_parameters", LogLevel.Error, "InlineData has defined 3 parameters but 2 are expected");

            Assert.Equal(0, result.Passed);
        }
    }
}
