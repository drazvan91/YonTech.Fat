using System;
using Xunit;
using Yontech.Fat.Selenium.DriverFactories;
using Yontech.Fat.Logging;
using Yontech.Fat.Tests.Mocks;
using Yontech.Fat.Exceptions;

namespace Yontech.Fat.Tests.DriverFactories
{
    public class FirefoxDriverFactoryTests
    {
        [Fact]
        public void When_driver_does_not_exist_Then_error_is_thrown()
        {
            var downloadFolder = "firefox-" + Guid.NewGuid().ToString();

            var mockFactory = new MockedLoggerFactory();
            var factory = new FirefoxDriverFactory(mockFactory);

            Assert.Throws<FatException>(() =>
            {
                var driver = factory.Create(downloadFolder, new Configuration.BrowserStartOptions()
                {
                    AutomaticDriverDownload = false
                });
            });
        }

        [Fact]
        public void Test_automatic_download()
        {
            var downloadFolder = "firefox-" + Guid.NewGuid().ToString();

            var mockFactory = new MockedLoggerFactory();
            var factory = new FirefoxDriverFactory(mockFactory);

            var driver = factory.Create(downloadFolder, new Configuration.BrowserStartOptions()
            {
                AutomaticDriverDownload = true
            });

            driver.Quit();

            mockFactory.AssertNoErrorOrWarning();
        }
    }
}
