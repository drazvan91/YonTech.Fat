using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;
using Yontech.Fat.Tests.Extensions;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class JsonFileTests
    {
        [Fact]
        public void Inline_json_values()
        {
            MockedLoggerFactory mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, new Alfa.Config1());

            var result = runner.Run<Alfa.TestCases.JsonFile>();

            mockedLoggerFactory.PrintAllLogs();

            Assert.Equal(1, result.Passed);
            Assert.Equal(1, result.Failed);

            result.AssertTestHasLog("Test_json_existing_properties", LogLevel.Info, "Value from column1: value1_1");
            result.AssertTestHasLog("Test_json_existing_properties", LogLevel.Info, "Value from column3: value3_1");
            result.AssertTestHasLog("Test_json_existing_properties", LogLevel.Info, "Value from column1: value1_2");
            result.AssertTestHasLog("Test_json_existing_properties", LogLevel.Info, "Value from column3: value3_2");

            result.AssertTestHasLog("Test_json_does_not_exist", LogLevel.Error, "could not be found. Is the this file copied to the output folder? Make sure you added <Content Include");
        }
    }
}
