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
        private readonly ChromeDriverFactory _chromeDriverFactory;
        private readonly FirefoxDriverFactory _firefoxDriverFactory;
        private readonly ILogger _logger;

        public SeleniumWebBrowserFactory(FatExecutionContext execContext)
        {
            this._execContext = execContext;
            this._chromeDriverFactory = new ChromeDriverFactory(this._execContext.LoggerFactory);
            this._firefoxDriverFactory = new FirefoxDriverFactory(this._execContext.LoggerFactory);

            this._logger = _execContext.LoggerFactory.Create(this);
        }

        public IWebBrowser Create(BrowserFatConfig browserConfig)
        {
            var config = this._execContext.Config;

            IWebDriver webDriver = CreateWebDriver(browserConfig);

            SeleniumWebBrowser browser = new SeleniumWebBrowser(
                this._execContext.LoggerFactory,
                webDriver: webDriver,
                browserType: browserConfig.BrowserType,
                busyConditions: new List<FatBusyCondition>());

            browser.Configuration.DefaultTimeout = config.Timeouts.DefaultTimeout;
            browser.Configuration.FinderTimeout = config.Timeouts.FinderTimeout;

            return browser;
        }

        private IWebDriver CreateWebDriver(BrowserFatConfig browserConfig)
        {
            var chromeConfig = browserConfig as ChromeFatConfig;
            if (chromeConfig != null)
            {
                return _chromeDriverFactory.Create(chromeConfig, this._execContext.Config.DefaultBrowserConfig);
            }

            var remoteChromeConfig = browserConfig as RemoteChromeFatConfig;
            if (remoteChromeConfig != null)
            {
                return _chromeDriverFactory.Create(remoteChromeConfig, this._execContext.Config.DefaultBrowserConfig);
            }

            var firefoxConfig = browserConfig as FirefoxFatConfig;
            if (firefoxConfig != null)
            {
                return _firefoxDriverFactory.Create(firefoxConfig, this._execContext.Config.DefaultBrowserConfig);
            }

            throw new FatException($"Browser type unknown");
        }
    }
}
