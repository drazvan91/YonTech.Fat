using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using System;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class TextBoxControl : BaseSeleniumControl, ITextBoxControl
    {
        public TextBoxControl(IWebElement webElement, SeleniumWebBrowser webBrowser)
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

        public string Value
        {
            get
            {
                EnsureElementExists();
                return WebElement.GetAttribute("value");
            }
        }

        public void ClearText()
        {
            EnsureElementExists();
            WebElement.SendKeys(Keys.Control + "a");
            WebElement.SendKeys(Keys.Backspace);
        }

        public new void Click()
        {
            WebElement.Click();
        }

        public void SendKeys(string keys)
        {
            EnsureElementExists();
            WebElement.SendKeys(Keys.Control + "a");
            WebElement.SendKeys(Keys.Backspace);
            WebElement.SendKeys(keys);
        }
    }
}
