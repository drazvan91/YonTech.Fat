﻿using System;
using Xunit;
using Yontech.Fat.Selenium.DriverFactories;
using Yontech.Fat.Tests.Mocks;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Configuration;

namespace Yontech.Fat.Tests.DriverFactories
{
    public class FirefoxDriverFactoryTests
    {
        [Fact]
        public void When_driver_does_not_exist_Then_error_is_thrown()
        {
            var mockFactory = new MockedLoggerFactory();
            var factory = new FirefoxDriverFactory(mockFactory);

            var config = new FirefoxFatConfig()
            {
                DriversFolder = "firefox-" + Guid.NewGuid().ToString(),
                AutomaticDriverDownload = false
            };

            Assert.Throws<FatException>(() =>
            {
                var driver = factory.Create(config, new BrowserFatConfig());
            });
        }

        [Fact]
        public void Test_automatic_download()
        {

            var mockFactory = new MockedLoggerFactory();
            var factory = new FirefoxDriverFactory(mockFactory);

            var config = new FirefoxFatConfig()
            {
                DriversFolder = "firefox-" + Guid.NewGuid().ToString(),
                AutomaticDriverDownload = true
            };

            var driver = factory.Create(config, new BrowserFatConfig());

            driver.Quit();

            mockFactory.AssertNoErrorOrWarning();
        }

        [Fact]
        public void Test_run_in_the_background()
        {

            var mockFactory = new MockedLoggerFactory();
            var factory = new FirefoxDriverFactory(mockFactory);

            var config = new FirefoxFatConfig()
            {
                AutomaticDriverDownload = true,
                RunInBackground = true
            };

            var driver = factory.Create(config, new BrowserFatConfig());

            driver.Quit();

            mockFactory.AssertNoErrorOrWarning();
        }
    }
}
