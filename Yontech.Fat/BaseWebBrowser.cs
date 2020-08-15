using System;
using System.Drawing;
using Yontech.Fat.Configuration;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Waiters;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public abstract class BaseWebBrowser : IWebBrowser
    {
        public WebBrowserConfiguration Configuration { get; set; }
        public int BrowserId { get; }
        public BrowserType BrowserType { get; }

        public abstract IControlFinder ControlFinder { get; }
        public abstract IJsExecutor JavaScriptExecutor { get; }
        public abstract IIFrameControl IFrameControl { get; }

        public abstract void Close();
        public abstract void CloseCurrentTab();

        public abstract void Dispose();
        public abstract void Navigate(string url);
        public abstract void Refresh();

        public abstract bool AcceptAlert();
        public abstract bool DismissAlert();
        public abstract string CurrentUrl { get; }
        public abstract string Title { get; }
        public abstract Size Size { get; }
        public abstract int DefaultTimeout { get; set; }

        public abstract ISnapshot TakeSnapshot();
        public abstract void SwitchToIframe(string iframeId);
        public abstract IWebBrowserTab CurrentTab { get; }
        public abstract IWebBrowserTab[] Tabs { get; }

        internal bool IsRemoteBrowser { get; set; }

        protected ILogger Logger { get; }

        protected BaseWebBrowser(FatExecutionContext fatContext, int browserId, BrowserType type)
        {
            this.Logger = fatContext.LoggerFactory.Create(this);
            this.BrowserId = browserId;
            this.BrowserType = type;
            this.Configuration = new WebBrowserConfiguration(this);
        }

        public void WaitForIdle()
        {
            this.WaitForIdle(this.Configuration.DefaultTimeout);
        }

        public void WaitForIdle(int timeout)
        {
            int pollingNumber = 0;

            Waiter.WaitForConditionToBeTrue(() =>
            {
                pollingNumber++;
                foreach (var condition in Configuration.BusyConditions)
                {
                    condition.WaitSessionPollingNumber = pollingNumber;
                    if (condition.IsBusy())
                    {
                        return false;
                    }
                }

                return true;
            }, timeout);
        }

        public void WaitForCondition(FatBusyCondition condition)
        {
            int pollingNumber = 0;

            Waiter.WaitForConditionToBeTrue(() =>
            {
                pollingNumber++;
                condition.WaitSessionPollingNumber = pollingNumber;

                if (condition.IsBusy())
                {
                    return false;
                }

                return true;
            }, this.Configuration.DefaultTimeout);
        }

        public void WaitForConditionToBeTrue(Func<bool> condition)
        {
            Waiter.WaitForConditionToBeTrue(() =>
            {
                return condition();
            }, this.Configuration.DefaultTimeout);
        }

        public void WaitForElementToAppear(string cssSelector, int timeout)
        {
            Waiter.WaitForConditionToBeTrue(() =>
            {
                var element = this.ControlFinder.Generic(cssSelector);
                return element.IsVisible;
            }, timeout);
        }

        public void WaitForElementToAppear(string cssSelector)
        {
            this.WaitForElementToAppear(cssSelector, this.Configuration.DefaultTimeout);
        }

        public abstract void SimulateOfflineConnection();
        public abstract void SimulateSlowConnection(int delay = 1000);
        public abstract void SimulateFastConnection();

        public abstract void Resize(int width, int height);
        public abstract void Fullscreen();
        public abstract void Maximize();
        public abstract void Minimize();
        public abstract IWebBrowserTab OpenNewTab(string url);
        public abstract IWebBrowserTab Tab(string tabId);
        public abstract IWebBrowserTab Tab(int tabIndex);
    }
}
