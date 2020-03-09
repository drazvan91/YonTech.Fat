using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Configuration;
using Yontech.Fat.Logging;
using Yontech.Fat.Selenium.DriverFactories;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumWebBrowserFactory : IWebBrowserFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public SeleniumWebBrowserFactory(ILoggerFactory loggerFactory)
        {
            this._loggerFactory = loggerFactory;
            this._logger = loggerFactory.Create(this);
        }

        public IWebBrowser Create(BrowserType browserType)
        {
            return this.Create(browserType, null, null);
        }

        public IWebBrowser Create(BrowserType browserType, BrowserStartOptions startOptions)
        {
            return this.Create(browserType, startOptions, null);
        }

        public IWebBrowser Create(BrowserType browserType, BrowserStartOptions startOptions, IEnumerable<FatBusyCondition> busyConditions)
        {
            this.ValidateStartOptions(browserType, startOptions);

            IWebDriver webDriver = null;
            var location = typeof(SeleniumWebBrowserFactory).Assembly.Location;
            location = Path.GetDirectoryName(location);
            string driversPath = Path.Combine(location, startOptions.DriversFolder);

            _logger.Info("Looking for drivers at location {0}", location);

            switch (browserType)
            {
                case BrowserType.Chrome:
                    webDriver = ChromeDriverFactory.Create(_loggerFactory, driversPath, startOptions, false);
                    break;
                case BrowserType.RemoteChrome:
                    webDriver = ChromeDriverFactory.Create(_loggerFactory, driversPath, startOptions, true);
                    break;
                case BrowserType.InternetExplorer:
                    webDriver = InternetExplorerDriverFactory.Create(driversPath, startOptions);
                    break;
                default:
                    throw new NotSupportedException();
            }

            SeleniumWebBrowser browser = new SeleniumWebBrowser(
                webDriver: webDriver,
                browserType: browserType,
                busyConditions: busyConditions ?? new List<FatBusyCondition>());

            return browser;
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
