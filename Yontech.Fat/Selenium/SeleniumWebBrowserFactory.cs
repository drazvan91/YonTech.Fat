using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using Yontech.Fat.Configuration;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Selenium.DriverFactories;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumWebBrowserFactory : IWebBrowserFactory
    {
        private readonly FatExecutionContext _execContext;
        private readonly ILogger _logger;

        public SeleniumWebBrowserFactory(FatExecutionContext execContext)
        {
            this._execContext = execContext;
            this._logger = _execContext.LoggerFactory.Create(this);
        }

        public IWebBrowser Create(BrowserType browserType)
        {
            var config = this._execContext.Config;
            var startOptions = new BrowserStartOptions()
            {
                RunHeadless = config.RunInBackground,
                StartMaximized = config.StartMaximized,
                InitialSize = config.InitialSize,
                DriversFolder = config.DriversFolder ?? "drivers",
                AutomaticDriverDownload = config.AutomaticDriverDownload,
                RemoteDebuggerAddress = config.RemoteDebuggerAddress,
            };

            this.ValidateStartOptions(browserType, startOptions);

            var location = typeof(SeleniumWebBrowserFactory).Assembly.Location;
            location = Path.GetDirectoryName(location);
            string driversPath = Path.Combine(location, startOptions.DriversFolder);

            _logger.Info("Looking for drivers at location {0}", location);
            IWebDriver webDriver = CreateWebDriver(browserType, driversPath, startOptions);

            SeleniumWebBrowser browser = new SeleniumWebBrowser(
                this._execContext.LoggerFactory,
                webDriver: webDriver,
                browserType: browserType,
                busyConditions: new List<FatBusyCondition>());

            browser.Configuration.DefaultTimeout = config.Timeouts.DefaultTimeout;
            browser.Configuration.FinderTimeout = config.Timeouts.FinderTimeout;

            return browser;
        }

        private IWebDriver CreateWebDriver(BrowserType browserType, string driversPath, BrowserStartOptions startOptions)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new ChromeDriverFactory(this._execContext.LoggerFactory)
                        .Create(driversPath, startOptions, false);
                case BrowserType.RemoteChrome:
                    return new ChromeDriverFactory(this._execContext.LoggerFactory)
                        .Create(driversPath, startOptions, true);
                case BrowserType.Firefox:
                    return new FirefoxDriverFactory(this._execContext.LoggerFactory)
                        .Create(driversPath, startOptions);
                case BrowserType.InternetExplorer:
                    return InternetExplorerDriverFactory.Create(driversPath, startOptions);
                default:
                    throw new FatException($"Browser type {browserType} not supported yet");
            }
        }

        private void ValidateStartOptions(BrowserType browserType, BrowserStartOptions startOptions)
        {
            if (browserType == BrowserType.RemoteChrome)
            {
                if (startOptions.RemoteDebuggerAddress == null)
                {
                    var error = "When using RemoteChrome browser you need to specify DebuggerAddress. Run chrome.exe (in your program files) with --remote-debugging-port=9222 and then set DebuggerAddress=\"localhost:9222\" in FatConfig";
                    _logger.Error(error);
                    throw new Exception(error);
                }
            }

            if (startOptions.InitialSize.IsEmpty == false && startOptions.StartMaximized)
            {
                _logger.Warning("Both StartMaximized and InitialSize have been set and they cannot work together. Only StartMaximized will be taken into consideration");
            }
        }
    }
}
