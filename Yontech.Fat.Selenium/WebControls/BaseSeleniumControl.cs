using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yontech.Fat.Exceptions;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class BaseSeleniumControl : IWebControl
    {
        protected readonly internal IWebElement WebElement;
        protected readonly internal SeleniumWebBrowser WebBrowser;

        public BaseSeleniumControl(IWebElement webElement, SeleniumWebBrowser webBrowser)
        {
            this.WebElement = webElement;
            this.WebBrowser = webBrowser;
        }

        public virtual void ScrollTo()
        {
            EnsureElementExists();
            WebBrowser.JavaScriptExecutor.ScrollToControl(this);
        }

        protected void EnsureElementExists()
        {
            if(WebElement == null)
            {
                throw new WebControlNotFoundException("Element not found");
            }
        }

        public void ShouldBeVisible()
        {
            EnsureElementExists();

            var remoteWebElem = WebElement as RemoteWebElement;
            if(remoteWebElem == null || !remoteWebElem.Displayed)
            {
                // todo
                throw new WebControlNotFoundException("Element not visible");
            }
        }

        public void ShouldNotBeVisible()
        {
            var remoteWebElem = WebElement as RemoteWebElement;
            if (remoteWebElem != null && remoteWebElem.Displayed)
            {
                throw new Exception("Element is visible and it shouldn't");  
            }
        }
    }
}
