using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yontech.Fat.Logging;
using Yontech.Fat.Waiters;

namespace Yontech.Fat
{
    public abstract class BaseWebBrowser : IWebBrowser
    {
        public WebBrowserConfiguration Configuration { get; private set; }
        public BrowserType Type { get; private set; }


        public abstract IControlFinder ControlFinder { get; }
        public abstract IJsExecutor JavaScriptExecutor { get; }
        public abstract void Close();
        public abstract void Dispose();
        public abstract void Navigate(string url);
        public abstract ISnapshot TakeSnapshot();

        public BaseWebBrowser(BrowserType type)
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
                    if (condition.IsBusy())
                    {
                        TraceLogger.Write("Browser is busy: {0}", condition.GetType().ToString());
                        return false;
                    }
                }

                return true;
            }, timeout);
        }
    }
}
