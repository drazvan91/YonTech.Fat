using System;
using System.Threading;
using OpenQA.Selenium;
using Yontech.Fat.Selenium.WebControls;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumJsExecutor : IJsExecutor
    {
        private readonly IJavaScriptExecutor _javascriptExecutor;

        public SeleniumJsExecutor(SeleniumWebBrowser webBrowser)
        {
            this._javascriptExecutor = (IJavaScriptExecutor)webBrowser.WebDriver;
        }

        public object ExecuteScript(string script)
        {
            return _javascriptExecutor.ExecuteScript(script);
        }

        public void ScrollToControl(IWebControl controlToScroll)
        {
            var seleniumControl = controlToScroll as BaseSeleniumControl;
            if (seleniumControl == null)
                throw new NotSupportedException("You can scroll only to SeleniumContorls");

            _javascriptExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", seleniumControl.WebElement);
        }

        public void WaitToFinishAjaxRequests(int timeout = 5000)
        {
            DateTime dateToTimeout = DateTime.Now.AddMilliseconds(timeout);
            while (DateTime.Now < dateToTimeout)
            {
                var ajaxIsComplete = (bool)ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                    return;

                Thread.CurrentThread.Join(100);
            }
        }
    }
}
