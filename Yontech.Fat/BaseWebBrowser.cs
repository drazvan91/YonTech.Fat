using System;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Configuration;
using Yontech.Fat.Logging;
using Yontech.Fat.Waiters;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public abstract class BaseWebBrowser : IWebBrowser
    {
        public WebBrowserConfiguration Configuration { get; private set; }
        public BrowserType Type { get; private set; }


        public abstract IControlFinder ControlFinder { get; }
        public abstract IJsExecutor JavaScriptExecutor { get; }
        public abstract IIFrameControl IFrameControl { get; }

        public abstract void Close();

        public abstract void Dispose();
        public abstract void Navigate(string url);
        public abstract void Refresh();

        public abstract bool AcceptAlert();
        public abstract bool DismissAlert();
        public abstract string CurrentUrl { get; }

        public abstract ISnapshot TakeSnapshot();
        public abstract void SwitchToIframe(string iframeId);

        protected BaseWebBrowser(BrowserType type)
        {
            this.Type = type;
            this.Configuration = new WebBrowserConfiguration();
        }

        public void WaitForIdle()
        {
            this.WaitForIdle(this.Configuration.DefaultTimeout);
        }

        public void WaitForIdle(int timeout)
        {
            Waiter.WaitForConditionToBeTrue(() =>
            {
                foreach (var condition in Configuration.BusyConditions)
                {
                    if (condition.IsBusy(this))
                    {
                        TraceLogger.Write("Browser is busy: {0}", condition.GetType().ToString());
                        return false;
                    }
                }

                return true;
            }, timeout);
        }

        public void WaitForCondition(IBusyCondition condition)
        {
            Waiter.WaitForConditionToBeTrue(() =>
            {
                if (condition.IsBusy(this))
                {
                    TraceLogger.Write("Browser is busy: {0}", condition.GetType().ToString());
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
    }
}
