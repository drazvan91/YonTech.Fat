using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using Yontech.Fat.Configuration;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal class InternetExplorerDriverFactory
    {
        public static IWebDriver Create(string driversPath, BrowserStartOptions startOptions)
        {
            var options = CreateOptions(startOptions);
            var driver = new InternetExplorerDriver(driversPath, options);

            if (startOptions.StartMaximized)
            {
                driver.Manage().Window.Maximize();
            }

            return driver;
        }

        private static InternetExplorerOptions CreateOptions(BrowserStartOptions startOptions)
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            if (startOptions.RunHeadless)
            {
                // Internet explorer does not support Headless.
                throw new InvalidConfigurationException($"InternetExplorerDriver does not support {nameof(BrowserStartOptions.RunHeadless)} options.");
            }

            return options;
        }
    }
}
