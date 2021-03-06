﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Yontech.Fat.Configuration;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Selenium.DriverFactories;
using Yontech.Fat.Selenium.WebControls;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumWebBrowser : BaseWebBrowser, IWebBrowser
    {
        private readonly Lazy<SeleniumJsExecutor> _jsExecutorLazy;
        private readonly Lazy<SeleniumControlFinder> _seleniumControlFinderLazy;
        private readonly Lazy<IFrameControl> _frameControlLazy;
        private bool _disposedValue;
        public IWebDriver WebDriver { get; internal set; }

#pragma warning disable SX1309
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "It is a constant initiated at load time")]
        private readonly ChromeNetworkConditions FAST_NETWORK_CONDITION = new ChromeNetworkConditions()
        {
            Latency = TimeSpan.FromMilliseconds(1),
            IsOffline = false,
            DownloadThroughput = 100000,
            UploadThroughput = 100000,
        };
#pragma warning restore SX1309

        public SeleniumWebBrowser(FatExecutionContext fatContext, IWebDriver webDriver, int browserId, BrowserType browserType, IEnumerable<FatBusyCondition> busyConditions)
            : base(fatContext, browserId, browserType)
        {
            this.WebDriver = webDriver;
            this._jsExecutorLazy = new Lazy<SeleniumJsExecutor>(() => new SeleniumJsExecutor(this));
            this._seleniumControlFinderLazy = new Lazy<SeleniumControlFinder>(() => new SeleniumControlFinder(fatContext.LoggerFactory, this));
            this._frameControlLazy = new Lazy<IFrameControl>(() => new IFrameControl(this));

            if (busyConditions != null)
            {
                this.Configuration.BusyConditions.AddRange(busyConditions);
            }

            var chromeDriver = this.WebDriver as CustomChromeDriver;
            if (chromeDriver != null)
            {
                chromeDriver.NetworkConditions = FAST_NETWORK_CONDITION;
            }
        }

        public override IControlFinder ControlFinder => this._seleniumControlFinderLazy.Value;

        public override IJsExecutor JavaScriptExecutor => this._jsExecutorLazy.Value;

        public override IIFrameControl IFrameControl => this._frameControlLazy.Value;

        public override string CurrentUrl => WebDriver.Url;

        public override string Title => WebDriver.Title;

        public override Size Size => WebDriver.Manage().Window.Size;

        public override int DefaultTimeout
        {
            get
            {
                return (int)WebDriver.Manage().Timeouts().PageLoad.TotalMilliseconds;
            }
            set
            {
                var timeouts = WebDriver.Manage().Timeouts();
                if (timeouts != null)
                {
                    timeouts.PageLoad = TimeSpan.FromMilliseconds(value);
                }
            }
        }

        public override void Close()
        {
            if (this.IsRemoteBrowser)
            {
                return;
            }

            var tabs = this.WebDriver.WindowHandles;
            for (int i = tabs.Count - 1; i >= 0; i--)
            {
                var tabDriver = this.WebDriver.SwitchTo().Window(tabs[i]);
                tabDriver.Close();
            }
        }

        public override void CloseCurrentTab()
        {
            WebDriver.Close();
            var tabs = this.WebDriver.WindowHandles;
            this.WebDriver = this.WebDriver.SwitchTo().Window(tabs[0]);
        }

        public override void Navigate(string url)
        {
            WebDriver.Url = url;
            WebDriver.Navigate();
            this.WaitForIdle();
        }

        public override bool AcceptAlert()
        {
            try
            {
                WebDriver.SwitchTo().Alert().Accept();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                this.WaitForIdle();
            }
        }

        public override bool DismissAlert()
        {
            try
            {
                WebDriver.SwitchTo().Alert().Dismiss();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                this.WaitForIdle();
            }
        }

        public override void Refresh()
        {
            WebDriver.Navigate().Refresh();
        }

        public override ISnapshot TakeSnapshot()
        {
            ITakesScreenshot takesScreenshot = WebDriver as ITakesScreenshot;
            if (takesScreenshot != null)
            {
                var shot = takesScreenshot.GetScreenshot();
                return new SeleniumSnapshot(shot);
            }

            IHasCapabilities hasCapability = WebDriver as IHasCapabilities;
            if (hasCapability == null)
            {
                throw new WebDriverException("Driver does not implement ITakesScreenshot or IHasCapabilities");
            }

            if (!hasCapability.Capabilities.HasCapability(CapabilityType.TakesScreenshot) || !(bool)hasCapability.Capabilities.GetCapability(CapabilityType.TakesScreenshot))
            {
                throw new WebDriverException("Driver capabilities do not support taking screenshots");
            }

            MethodInfo method = WebDriver.GetType().GetMethod("Execute", BindingFlags.Instance | BindingFlags.NonPublic);
            object[] screenshot = new object[] { DriverCommand.Screenshot, null };
            Response response = method.Invoke(WebDriver, screenshot) as Response;
            if (response == null)
            {
                throw new WebDriverException("Unexpected failure getting screenshot; response was not in the proper format.");
            }

            return new SeleniumSnapshot(new Screenshot(response.Value.ToString()));
        }

        // This code added to correctly implement the disposable pattern.
        public override void Dispose()
        {
            Dispose(true);
        }

        public override void SwitchToIframe(string iframeId)
        {
            WebDriver.SwitchTo().Frame(iframeId);
        }

        public override void SimulateOfflineConnection()
        {
            var chromeDriver = this.WebDriver as CustomChromeDriver;
            if (chromeDriver == null)
            {
                Logger.Debug("Simulate network conditions is not supported for this Browser");
                return;
            }

            if (chromeDriver.NetworkConditions.IsOffline)
            {
                return;
            }

            chromeDriver.NetworkConditions = new ChromeNetworkConditions()
            {
                Latency = TimeSpan.FromMilliseconds(1),
                IsOffline = true,
                DownloadThroughput = 200,
                UploadThroughput = 200,
            };
            Logger.Debug("Network condition is being simulated as offline");
        }

        public override void SimulateSlowConnection(int delay = 1000)
        {
            var chromeDriver = this.WebDriver as CustomChromeDriver;
            if (chromeDriver == null)
            {
                Logger.Debug("Simulate network conditions is not supported for this Browser");
                return;
            }

            var newLatency = TimeSpan.FromMilliseconds(delay / 2);

            if (chromeDriver.NetworkConditions.Latency == newLatency)
            {
                return;
            }

            chromeDriver.NetworkConditions = new ChromeNetworkConditions()
            {
                Latency = newLatency,
                IsOffline = false,
                DownloadThroughput = 20000,
                UploadThroughput = 20000,
            };

            Logger.Debug("Network condition is being simulated as slow with a delay of '{0}'", delay);
        }

        public override void SimulateFastConnection()
        {
            var chromeDriver = this.WebDriver as CustomChromeDriver;
            if (chromeDriver != null && chromeDriver.NetworkConditions.Latency != FAST_NETWORK_CONDITION.Latency)
            {
                chromeDriver.NetworkConditions = FAST_NETWORK_CONDITION;
                Logger.Debug("Network condition is being simulated as fast with no delay");
            }
        }

        public override void Resize(int width, int height)
        {
            this.WebDriver.Manage().Window.Size = new Size(width, height);
        }

        public override void Fullscreen()
        {
            this.WebDriver.Manage().Window.FullScreen();
        }

        public override void Maximize()
        {
            this.WebDriver.Manage().Window.Maximize();
        }

        public override void Minimize()
        {
            this.WebDriver.Manage().Window.Minimize();
        }

        public override IWebBrowserTab OpenNewTab(string url)
        {
            var oldTabs = this.WebDriver.WindowHandles;

            // TODO: encode URL
            this.JavaScriptExecutor.ExecuteScript("window.open('" + url + "','_blank');");

            var newTabs = this.WebDriver.WindowHandles;
            var newTabId = newTabs.FirstOrDefault(tab => !oldTabs.Contains(tab));

            return new SeleniumWebBrowserTab(this, newTabId);
        }

        public override IWebBrowserTab CurrentTab
        {
            get
            {
                return new SeleniumWebBrowserTab(this, this.WebDriver.CurrentWindowHandle);
            }
        }

        public override IWebBrowserTab[] Tabs
        {
            get
            {
                return this.WebDriver.WindowHandles.Select(window => new SeleniumWebBrowserTab(this, window)).ToArray();
            }
        }

        public override IWebBrowserTab Tab(string tabId)
        {
            var tabs = this.WebDriver.WindowHandles;
            if (!tabs.Contains(tabId))
            {
                throw new FatException("Tab with id {0} cannot be found.", tabId);
            }

            return new SeleniumWebBrowserTab(this, tabId);
        }

        public override IWebBrowserTab Tab(int tabIndex)
        {
            var tabs = this.WebDriver.WindowHandles;
            if (tabs.Count <= tabIndex)
            {
                throw new FatException("Tab with index {0} cannot be found. Only {1} tabs are available", tabIndex, tabs.Count);
            }

            return new SeleniumWebBrowserTab(this, tabs[tabIndex]);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (!this.IsRemoteBrowser)
                    {
                        WebDriver.Dispose();
                    }
                }

                _disposedValue = true;
            }
        }
    }
}
