using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;
using Yontech.Fat.Tests.Extensions;
using Yontech.Fat.Tests.Mocks;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class CsvFileTests
    {
        [Fact]
        public void Inline_csv_values()
        {
            var context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);
            var runner = new FatRunner(context);

            var result = runner.Run<Alfa.TestCases.CsvFile>();

            Assert.Equal(1, result.Passed);
            Assert.Equal(1, result.Failed);

            result.AssertTestHasLog("Test_csv_existing_columns", LogLevel.Info, "Value from column1: value1_1");
            result.AssertTestHasLog("Test_csv_existing_columns", LogLevel.Info, "Value from column3: value3_1");
            result.AssertTestHasLog("Test_csv_existing_columns", LogLevel.Info, "Value from column1: value1_2");
            result.AssertTestHasLog("Test_csv_existing_columns", LogLevel.Info, "Value from column3: value3_2");

            result.AssertTestHasLog("Test_csv_does_not_exist", LogLevel.Error, "could not be found. Is the this file copied to the output folder? Make sure you added <Content Include");
        }

        [Fact]
        public void Object_like_values_success()
        {
            var context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);
            var runner = new FatRunner(context);

            var result = runner.Run<Alfa.TestCases.CsvFileObjectLike>();

            Assert.Equal(1, result.Passed);
            Assert.Equal(0, result.Failed);

            result.AssertTestHasLog("Test_ok", LogLevel.Info, "name: Razvan");
            result.AssertTestHasLog("Test_ok", LogLevel.Info, "age: 21");
        }

        [Fact]
        public void Object_like_column_not_supplied()
        {
            var context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);
            var runner = new FatRunner(context);

            var result = runner.Run<Alfa.TestCases.CsvFileObjectLikeErrors>();

            result.AssertResult(0, 1);

            result.AssertTestHasLog("Test_column_does_not_exist", LogLevel.Error, "Header with name 'SomeColumn' was not found");
        }
    }
}
