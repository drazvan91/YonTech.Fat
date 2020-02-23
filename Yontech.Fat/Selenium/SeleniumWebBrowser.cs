using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Reflection;
using Yontech.Fat.WebControls;
using Yontech.Fat.Selenium.WebControls;
using Yontech.Fat.BusyConditions;
using OpenQA.Selenium.Chrome;
using System.Drawing;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumWebBrowser : BaseWebBrowser, IWebBrowser
    {
        public readonly IWebDriver WebDriver;
        private bool _disposedValue;
        private readonly Lazy<SeleniumJsExecutor> _jsExecutorLazy;
        private readonly Lazy<SeleniumControlFinder> _seleniumControlFinderLazy;
        private readonly Lazy<IFrameControl> _frameControlLazy;

        public SeleniumWebBrowser(IWebDriver webDriver, BrowserType browserType, IEnumerable<IBusyCondition> busyConditions) : base(browserType)
        {
            this.WebDriver = webDriver;
            this._jsExecutorLazy = new Lazy<SeleniumJsExecutor>(() => new SeleniumJsExecutor(this));
            this._seleniumControlFinderLazy = new Lazy<SeleniumControlFinder>(() => new SeleniumControlFinder(this));
            this._frameControlLazy = new Lazy<IFrameControl>(() => new IFrameControl(this));

            if (busyConditions != null)
            {
                this.Configuration.BusyConditions.AddRange(busyConditions);
            }
        }

        public override IControlFinder ControlFinder => this._seleniumControlFinderLazy.Value;

        public override IJsExecutor JavaScriptExecutor => this._jsExecutorLazy.Value;

        public override IIFrameControl IFrameControl => this._frameControlLazy.Value;

        public override string CurrentUrl => WebDriver.Url;

        public override string Title => WebDriver.Title;

        public override Size Size => WebDriver.Manage().Window.Size;

        public override void Close()
        {
            WebDriver.Close();
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

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    WebDriver.Dispose();
                }

                _disposedValue = true;
            }
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
            var chromeDriver = this.WebDriver as ChromeDriver;
            if (chromeDriver != null)
            {
                chromeDriver.NetworkConditions = new ChromeNetworkConditions()
                {
                    Latency = TimeSpan.FromMilliseconds(1),
                    IsOffline = true,
                    DownloadThroughput = 200,
                    UploadThroughput = 200
                };
            }
        }

        public override void SimulateSlowConnection(int delay = 1000)
        {
            var chromeDriver = this.WebDriver as ChromeDriver;
            if (chromeDriver != null)
            {
                chromeDriver.NetworkConditions = new ChromeNetworkConditions()
                {
                    Latency = TimeSpan.FromMilliseconds(delay / 2),
                    IsOffline = false,
                    DownloadThroughput = 20000,
                    UploadThroughput = 20000
                };
            }
        }

        public override void SimulateFastConnection()
        {
            var chromeDriver = this.WebDriver as ChromeDriver;
            if (chromeDriver != null)
            {
                chromeDriver.NetworkConditions = new ChromeNetworkConditions()
                {
                    Latency = TimeSpan.FromMilliseconds(1),
                    IsOffline = false,
                    DownloadThroughput = 100000,
                    UploadThroughput = 100000
                };
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
    }
}
