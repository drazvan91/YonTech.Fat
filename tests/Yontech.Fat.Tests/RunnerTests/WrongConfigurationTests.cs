using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

namespace Yontech.Fat.Tests.RunnerTests
{
    public class WrongConfigurationTests
    {
        [Fact]
        public void When_assembly_not_registered_Then_throws_exception()
        {
            MockedLoggerFactory mockedLoggerFactory = new MockedLoggerFactory();
            MockedAssemblyDiscoverer assemblyDiscoverer = new MockedAssemblyDiscoverer(typeof(Delta.Config).Assembly);

            FatRunner runner = new FatRunner(assemblyDiscoverer, mockedLoggerFactory, new Alfa.Config1());

            var result = runner.Run<Alfa.TestCases.AlfaTest1>();

            Assert.Equal(0, result.Passed);
            Assert.Equal(1, result.Failed);
        }
    }
}
