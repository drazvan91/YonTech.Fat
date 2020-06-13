using System;
using System.Linq;
using Alfa.TestCases;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Tests.Mocks;

namespace Yontech.Fat.Tests.FatDiscovererTests
{
    public class FatWarmupDiscovererTests
    {
        [Fact]
        public void When_one_warmup_Then_should_happy_case()
        {
            var context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);
            FatDiscoverer discoverer = new FatDiscoverer(context);
            var warmups = discoverer.FindFatWarmups();

            Assert.Equal(warmups.Count(), 1);
        }

        [Fact]
        public void When_multiple_warmups_Then_should_return_all()
        {
            var context = new MockedExecutionContext(typeof(Beta.Class1).Assembly);
            FatDiscoverer discoverer = new FatDiscoverer(context);
            var warmups = discoverer.FindFatWarmups();

            Assert.Equal(warmups.Count(), 2);
        }

        [Fact]
        public void When_no_config_Then_should_return_empty_And_log_info()
        {
            var context = new MockedExecutionContext(typeof(Gama.Config).Assembly);
            FatDiscoverer discoverer = new FatDiscoverer(context);
            var warmups = discoverer.FindFatWarmups();

            Assert.Equal(warmups.Count(), 0);
        }

        [Fact]
        public void Success()
        {
            var config = new Alfa.Config1();
            var context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);
            var runner = new FatRunner(context);
            var results = runner.Run<AlfaTest1>();
            Assert.Equal(results.Failed, 0);

            context.MockedLoggerFactory.AssertContains(LogLevel.Info, "Warming up Alfa project for browser Chrome");
            context.MockedLoggerFactory.AssertContains(LogLevel.Info, "Warming up Alfa project for browser Firefox");
        }

        [Fact]
        public void When_no_warmup_Then_log()
        {
            var config = new Alfa.Config1();
            var context = new MockedExecutionContext(typeof(Gama.Config).Assembly);
            var runner = new FatRunner(context);
            var results = runner.Run<Gama.TestCases.GamaTest1>();

            context.MockedLoggerFactory.AssertContains(LogLevel.Info, "No warmup configured");
        }
    }
}
