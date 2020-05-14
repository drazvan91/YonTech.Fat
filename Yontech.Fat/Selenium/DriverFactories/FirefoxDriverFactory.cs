using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using Yontech.Fat.Configuration;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Logging;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal class FirefoxDriverFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public FirefoxDriverFactory(ILoggerFactory loggerFactory)
        {
            this._loggerFactory = loggerFactory;
            this._logger = loggerFactory.Create(this);
        }

        public IWebDriver Create(string driverPath, BrowserStartOptions startOptions)
        {
            var firefoxOptions = CreateOptions(startOptions);

            IWebDriver driver = null;
            try
            {
                driver = CreateDriver(driverPath, firefoxOptions);
            }
            catch (DriverServiceNotFoundException)
            {
                if (!startOptions.AutomaticDriverDownload)
                {
                    throw new FatException($"The driver could not be found at location '{driverPath}'. Use AutomaticDriverDownload = false in config file to let FatFramework download it.");
                }

                new FirefoxDriverDownloader(_loggerFactory, FirefoxVersion.Latest).Download(driverPath).Wait();

                driver = CreateDriver(driverPath, firefoxOptions);
            }

            if (driver != null)
            {
                // this is a hotfix because selenium --start-maximized doesn't work (see below)
                if (startOptions.StartMaximized)
                {
                    driver.Manage().Window.Maximize();
                }
            }

            return driver;
        }

        private IWebDriver CreateDriver(string driverPath, FirefoxOptions firefoxOptions)
        {
            int servicePort = 5556;

            while (servicePort < 6000)
            {
                try
                {
                    return CreateDriverForPort(servicePort, driverPath, firefoxOptions);
                }
                catch (OpenQA.Selenium.WebDriverException ex) when (
                    ex.Message.Contains("Cannot start the driver service") ||
                    ex.Message.Contains("A exception with a null response was thrown sending an HTTP request"))
                {
                    servicePort++;
                }
                catch
                {
                    throw;
                }
            }

            throw new FatException("No free port found for web driver to start");
        }

        private IWebDriver CreateDriverForPort(int servicePort, string driverPath, FirefoxOptions firefoxOptions)
        {
            _logger.Info($"Create and start ChromeDriverService with URI: http://127.0.0.1:{servicePort}");

            var webDriver = new FirefoxDriver(driverPath, firefoxOptions);
            _logger.Debug("ChromeDriver connected to service");
            return webDriver;
        }

        private FirefoxOptions CreateOptions(BrowserStartOptions startOptions)
        {
            startOptions = startOptions ?? new BrowserStartOptions();

            var firefoxOptions = new FirefoxOptions();

            if (startOptions.RunHeadless)
            {
                firefoxOptions.AddArguments("--headless");
            }

            if (startOptions.StartMaximized)
            {
                // this looks like it is not working, might be deprecated by selenium
                firefoxOptions.AddArgument("--start-maximized");
            }

            var height = startOptions.InitialSize.Height;
            var width = startOptions.InitialSize.Width;
            if (height > 0 && height > 0)
            {
                firefoxOptions.AddArgument($"--window-size={height},{width}");
            }

            if (startOptions.DisablePopupBlocking)
            {
                firefoxOptions.AddArgument("--disable-popup-blocking");
            }

            return firefoxOptions;
        }
    }
}
