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
        public void Happy_flow()
        {
            MockedLoggerFactory mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, new Alfa.Config1());

            var result = runner.Run<Alfa.JsonFileDataTestCases.HappyFlowTests>();

            Assert.Equal(0, result.Failed);

            result.AssertTestHasLog("Test_one_inline_param", LogLevel.Info, "firstName: Razvan");

            result.AssertTestHasLog("Test_multiple_inline_values", LogLevel.Info, "lastName: Dragomir");

            result.AssertTestHasLog("Test_different_inline_types", LogLevel.Info, "age: 20");
            result.AssertTestHasLog("Test_different_inline_types", LogLevel.Info, "isActive: True");

            result.AssertTestHasLog("Test_json_object", LogLevel.Info, "firstName: Razvan");
        }

        [Fact]
        public void When_file_not_exists_Then_displays_error_message()
        {
            MockedLoggerFactory mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, new Alfa.Config1());

            var result = runner.Run<Alfa.JsonFileDataTestCases.FileDoesNotExistTests>();

            Assert.Equal(0, result.Passed);

            result.AssertTestHasLog("Test_file_not_found", LogLevel.Error, "could not be found. Is the this file copied to the output folder? Make sure you added <Content Include");
        }

        [Fact]
        public void When_type_not_supported_Then_displays_error_message()
        {
            MockedLoggerFactory mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, new Alfa.Config1());

            var result = runner.Run<Alfa.JsonFileDataTestCases.TypeNotSupportedTests>();

            Assert.Equal(1, result.Failed);

            result.AssertTestHasLog("Test_type_not_supported", LogLevel.Error, "Not supported type for parameter 'address' in file 'files/persons.json'");
        }

        [Fact]
        public void When_type_mismatched_Then_displays_error_message()
        {
            MockedLoggerFactory mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, new Alfa.Config1());

            var result = runner.Run<Alfa.JsonFileDataTestCases.TypeMismatchTests>();

            Assert.Equal(1, result.Failed);

            result.AssertTestHasLog("Test_type_not_supported", LogLevel.Error, "Type mismatch for parameter 'age' in file 'files/persons.json'");
        }

        [Fact]
        public void When_property_doesnt_exist_Then_error_is_thrown()
        {
            MockedLoggerFactory mockedLoggerFactory = new MockedLoggerFactory();
            var streamProvider = new StreamProvider(mockedLoggerFactory);
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Alfa.Config1).Assembly);

            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, streamProvider, new Alfa.Config1());

            var result = runner.Run<Alfa.JsonFileDataTestCases.PropertyDoesNotExist>();

            Assert.Equal(1, result.Failed);

            result.AssertTestHasLog("Test_property_does_not_exist", LogLevel.Error, "Property 'city' does not exist in file 'files/persons.json'");
        }
    }
}
