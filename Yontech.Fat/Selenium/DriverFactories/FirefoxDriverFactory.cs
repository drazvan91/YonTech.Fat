using System;
using System.Collections.Generic;
using System.IO;
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

        public IWebDriver Create(FirefoxFatConfig config, BrowserFatConfig defaultConfig)
        {
            var chromeOptions = CreateOptions(config, defaultConfig);

            var driver = DownloadAndCreateDriver(chromeOptions, config, defaultConfig);

            return driver;
        }

        private IWebDriver DownloadAndCreateDriver(FirefoxOptions firefoxOptions, FirefoxFatConfig config, BrowserFatConfig defaultConfig)
        {
            var location = typeof(SeleniumWebBrowserFactory).Assembly.Location;
            location = Path.GetDirectoryName(location);
            string driverPath = Path.Combine(location, config.DriversFolder ?? defaultConfig.DriversFolder);

            _logger.Info("Looking for drivers at location {0}", location);

            IWebDriver driver = null;
            try
            {
                driver = CreateDriver(driverPath, firefoxOptions);
            }
            catch (DriverServiceNotFoundException)
            {
                if (!(config.AutomaticDriverDownload ?? defaultConfig.AutomaticDriverDownload))
                {
                    throw new FatException($"The driver could not be found at location '{driverPath}'. Use AutomaticDriverDownload = false in config file to let FatFramework download it.");
                }

                new FirefoxDriverDownloader(_loggerFactory, config.Version).Download(driverPath).Wait();

                driver = CreateDriver(driverPath, firefoxOptions);
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

        private FirefoxOptions CreateOptions(FirefoxFatConfig config, BrowserFatConfig defaultConfig)
        {
            var firefoxOptions = new FirefoxOptions();
            return firefoxOptions;
        }
    }
}
