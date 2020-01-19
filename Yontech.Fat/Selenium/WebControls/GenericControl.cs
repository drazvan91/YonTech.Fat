using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class GenericControl : BaseSeleniumControl, IGenericControl
    {
        public GenericControl(IWebElement webElement, SeleniumWebBrowser webBrowser)
            : base(webElement, webBrowser)
        {
        }

        public string Text
        {
            get
            {
                EnsureElementExists();
                return WebElement.Text;
            }
        }

        public IControlFinder ControlFinder => new SeleniumControlFinder(WebElement, WebBrowser);

        public bool WaitForClickable()
        {
            return WaitForClickable(Constants.CLICK_WAITING_TIMEOUT);
        }

        public bool WaitForClickable(TimeSpan timeout)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(WebBrowser.WebDriver, timeout);
                wait.Until(ExpectedConditions.ElementToBeClickable(this.WebElement));

                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public IEnumerable<IGenericControl> Find(string cssSelector)
        {
            EnsureElementExists();
            foreach (var element in WebElement.FindElements(By.CssSelector(cssSelector)))
            {
                yield return new GenericControl(element, WebBrowser);
            }
        }

        public string GetAttribute(string attributeName)
        {
            EnsureElementExists();
            return WebElement.GetAttribute(attributeName);
        }
    }
}
