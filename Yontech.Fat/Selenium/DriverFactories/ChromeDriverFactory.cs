using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Yontech.Fat.Configuration;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Logging;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal class ChromeDriverFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public ChromeDriverFactory(ILoggerFactory loggerFactory)
        {
            this._loggerFactory = loggerFactory;
            this._logger = loggerFactory.Create(this);
        }

        public IWebDriver Create(ChromeFatConfig config, BrowserFatConfig defaultConfig)
        {
            var chromeOptions = CreateOptions(config, defaultConfig);

            var driver = DownloadAndCreateDriver(chromeOptions, config, defaultConfig);

            if (driver != null)
            {
                // this is a hotfix because selenium --start-maximized doesn't work (see below)
                if (config.StartMaximized ?? defaultConfig.StartMaximized)
                {
                    driver.Manage().Window.Maximize();
                }
            }

            return driver;
        }

        public IWebDriver Create(RemoteChromeFatConfig config, BrowserFatConfig defaultConfig)
        {
            var chromeOptions = CreateRemoteOptions(config, defaultConfig);

            var driver = DownloadAndCreateDriver(chromeOptions, config, defaultConfig);

            return driver;
        }

        private IWebDriver DownloadAndCreateDriver(ChromeOptions chromeOptions, BaseChromeFatConfig config, BrowserFatConfig defaultConfig)
        {
            var location = typeof(SeleniumWebBrowserFactory).Assembly.Location;
            location = Path.GetDirectoryName(location);
            string driverPath = Path.Combine(location, config.DriversFolder ?? defaultConfig.DriversFolder);

            _logger.Info("Looking for drivers at location {0}", location);

            IWebDriver driver = null;
            try
            {
                driver = CreateDriver(driverPath, chromeOptions);
            }
            catch (DriverServiceNotFoundException)
            {
                if (!(config.AutomaticDriverDownload ?? defaultConfig.AutomaticDriverDownload))
                {
                    throw new FatException($"The driver could not be found at location '{driverPath}'. Use AutomaticDriverDownload = false in config file to let FatFramework download it.");
                }

                var downloader = new ChromeDriverDownloader(_loggerFactory, config.Version);
                downloader
                    .Download(driverPath)
                    .Wait();

                driver = CreateDriver(driverPath, chromeOptions);
            }

            return driver;
        }

        private IWebDriver CreateDriver(string driverPath, ChromeOptions chromeOptions)
        {
            int servicePort = 5556;

            while (servicePort < 6000)
            {
                try
                {
                    return CreateDriverForPort(servicePort, driverPath, chromeOptions);
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

        private IWebDriver CreateDriverForPort(int servicePort, string driverPath, ChromeOptions chromeOptions)
        {
            _logger.Info($"Create and start ChromeDriverService with URI: http://127.0.0.1:{servicePort}");
            // for some reason if we use ChromeDriver instead of RemoteWebDriver there is a performance issue on
            // initial load, we believe that is because of dotnetcore
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverPath);
            service.Port = servicePort;
            service.Start();
            _logger.Debug("ChromeDriverService started");

            var webDriver = new CustomChromeDriver(new Uri($"http://127.0.0.1:{servicePort}"), chromeOptions, service);
            _logger.Debug("ChromeDriver connected to service");
            return webDriver;

            // this is how it should be done.
            // var webDriver = new ChromeDriver(driverPath, chromeOptions);
            // return webDriver;
        }

        private ChromeOptions CreateRemoteOptions(RemoteChromeFatConfig remoteConfig, BrowserFatConfig defaultConfig)
        {
            return new ChromeOptions()
            {
                DebuggerAddress = remoteConfig.RemoteDebuggerAddress,
            };
        }

        private ChromeOptions CreateOptions(ChromeFatConfig config, BrowserFatConfig defaultConfig)
        {
            var chromeOptions = new ChromeOptions();

            if (config.RunInBackground ?? defaultConfig.RunInBackground)
            {
                chromeOptions.AddArguments("--headless");
            }

            if (config.StartMaximized ?? defaultConfig.StartMaximized)
            {
                // this looks like it is not working, might be deprecated by selenium
                chromeOptions.AddArgument("--start-maximized");
            }

            var height = config.InitialSize?.Height ?? defaultConfig.InitialSize.Height;
            var width = config.InitialSize?.Width ?? defaultConfig.InitialSize.Width;
            if (height > 0 && height > 0)
            {
                chromeOptions.AddArgument($"--window-size={height},{width}");
            }

            if (config.DisablePopupBlocking ?? defaultConfig.DisablePopupBlocking)
            {
                chromeOptions.AddArgument("--disable-popup-blocking");
            }

            return chromeOptions;
        }
    }
}
