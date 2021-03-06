﻿using System;
using Xunit;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Tests.Mocks;

namespace Yontech.Fat.Tests.FatDiscovererTests
{
    public class FatConfigTests
    {
        [Fact]
        public void When_one_config_Then_should_happy_case()
        {
            var context = new MockedExecutionContext(typeof(Delta.Config).Assembly);
            FatDiscoverer discoverer = new FatDiscoverer(context);
            var config = discoverer.DiscoverConfig();

            Assert.Equal("delta_drivers", config.BrowserConfig.DriversFolder);
            context.MockedLoggerFactory.AssertNoErrorOrWarning();
        }

        [Fact]
        public void When_multiple_configs_Then_should_pick_one_And_log_warning()
        {
            var context = new MockedExecutionContext(typeof(Alfa.Config1).Assembly);

            FatDiscoverer discoverer = new FatDiscoverer(context);
            var config = discoverer.DiscoverConfig();

            Assert.Equal("alfa1_drivers", config.BrowserConfig.DriversFolder);
            context.MockedLoggerFactory.AssertContains(LogLevel.Warning, "Multiple FatConfig files have been found. The one with the shortest name has been chosen:");
        }

        [Fact]
        public void When_no_config_Then_should_return_null_And_log_warning()
        {
            var context = new MockedExecutionContext(typeof(Beta.Class1).Assembly);

            FatDiscoverer discoverer = new FatDiscoverer(context);
            var config = discoverer.DiscoverConfig();

            Assert.Null(config);
            context.MockedLoggerFactory.AssertContains(LogLevel.Debug, "No FatConfig has been found");
        }
    }
}
