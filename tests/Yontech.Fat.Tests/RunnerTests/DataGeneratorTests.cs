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
    public class DataGeneratorTests
    {
        private readonly MockedExecutionContext context;
        private readonly FatRunner runner;

        public DataGeneratorTests()
        {
            var config = new Alfa.Config1();
            this.context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);
            this.runner = new FatRunner(context);
        }

        [Fact]
        public void DataGenerator_happy_flow()
        {
            var result = runner.Run<Alfa.GeneratedDataTestCases.HappFlowTests>();

            result.AssertTestHasLog("Test_person_generator", LogLevel.Info, "Person name: Razvan 1");
            result.AssertTestHasLog("Test_person_generator", LogLevel.Info, "Person name: Razvan 3");

            result.AssertTestHasLog("Test_string_generator", LogLevel.Info, "String value: string number 1");
            result.AssertTestHasLog("Test_string_generator", LogLevel.Info, "String value: string number 2");

            Assert.Equal(0, result.Failed);
        }

        [Fact]
        public void DataGenerator_no_property_found()
        {
            var result = runner.Run<Alfa.GeneratedDataTestCases.PropertyNotFoundTests>();

            result.AssertTestHasLog("Test_person_generator", "Cannot find property 'age' on type 'Person'");

            Assert.Equal(1, result.Failed);
        }
    }
}
