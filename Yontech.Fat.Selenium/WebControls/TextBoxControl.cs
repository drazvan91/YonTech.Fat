using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using System;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class TextBoxControl:BaseSeleniumControl, ITextBoxControl
    {
        public TextBoxControl(IWebElement webElement, SeleniumWebBrowser webBrowser)
            : base(webElement, webBrowser)
        {
        }

        public bool IsDisplayed
        {
            get
            {
                if (WebElement == null)
                {
                    return false;
                }
                return WebElement.Displayed;
            }
        }

        public string Text
        {
            get
            {
                EnsureElementExists();
                return WebElement.Text;
            }
        }

        public void ClearText()
        {
            EnsureElementExists();
            WebElement.Clear();
        }

        public void Click()
        {
            WebElement.Click();
        }

        public void SendKeys(string keys)
        {
            EnsureElementExists();
            WebElement.SendKeys(keys);            
        }
    }
}
