using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Yontech.Fat.Configuration;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal static class InternetExplorerDriverFactory
    {
        /// <summary>
        /// Creates a web driver.
        /// </summary>
        /// <param name="driversPath">Path to the folder containg the web driver.</param>
        /// <param name="startOptions">Null allowed. Providing null falls back to default BrowserStartOptions.</param>
        /// <returns>An instance of Internet Explorer Driver.</returns>
        public static RemoteWebDriver Create(string driversPath, BrowserStartOptions startOptions)
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
            // Initialize default.
            startOptions = startOptions ?? new BrowserStartOptions();
            var options = new InternetExplorerOptions();

            if (startOptions.RunHeadless)
            {
                // Internet explorer does not support Headless.
                throw new InvalidConfigurationException($"InternetExplorerDriver does not support {nameof(BrowserStartOptions.RunHeadless)} options.");
            }

            return options;
        }
    }
}
