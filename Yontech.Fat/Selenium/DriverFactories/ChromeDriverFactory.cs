using System;
using System.Collections.Generic;
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

        public IWebDriver Create(string driverPath, BrowserStartOptions startOptions, bool useRemote)
        {
            var chromeOptions = useRemote ? CreateRemoteOptions(startOptions) : CreateOptions(startOptions);

            IWebDriver driver = null;
            try
            {
                driver = CreateDriver(driverPath, chromeOptions);
            }
            catch (DriverServiceNotFoundException)
            {
                if (!startOptions.AutomaticDriverDownload)
                {
                    throw new FatException($"The driver could not be found at location '{driverPath}'. Use AutomaticDriverDownload = false in config file to let FatFramework download it.");
                }

                new ChromeDriverDownloader(_loggerFactory, startOptions.ChromeVersion).Download(driverPath).Wait();

                driver = CreateDriver(driverPath, chromeOptions);
            }

            if (driver != null && useRemote == false)
            {
                // this is a hotfix because selenium --start-maximized doesn't work (see below)
                if (startOptions.StartMaximized)
                {
                    driver.Manage().Window.Maximize();
                }
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

        private ChromeOptions CreateRemoteOptions(BrowserStartOptions startOptions)
        {
            return new ChromeOptions()
            {
                DebuggerAddress = startOptions.RemoteDebuggerAddress,
            };
        }

        private ChromeOptions CreateOptions(BrowserStartOptions startOptions)
        {
            startOptions = startOptions ?? new BrowserStartOptions();

            var chromeOptions = new ChromeOptions();

            if (startOptions.RunHeadless)
            {
                chromeOptions.AddArguments("--headless");
            }

            if (startOptions.StartMaximized)
            {
                // this looks like it is not working, might be deprecated by selenium
                chromeOptions.AddArgument("--start-maximized");
            }

            var height = startOptions.InitialSize.Height;
            var width = startOptions.InitialSize.Width;
            if (height > 0 && height > 0)
            {
                chromeOptions.AddArgument($"--window-size={height},{width}");
            }

            if (startOptions.DisablePopupBlocking)
            {
                chromeOptions.AddArgument("--disable-popup-blocking");
            }

            return chromeOptions;
        }
    }
}
