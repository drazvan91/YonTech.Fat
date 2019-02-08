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

        public void ClearText()
        {
            EnsureElementExists();
            WebElement.Clear();

            // this is a hack for some cases where the element has autocomplete
            // todo: fix this for Material UI Text Fields
            int tries = 0;
            while (tries < 100)
            {
                WebElement.SendKeys(Keys.Backspace);
                WebElement.SendKeys(Keys.Delete);
                tries++;
            }
        }

        public new void Click()
        {
            WebElement.Click();
        }

        public void SendKeys(string keys)
        {
            EnsureElementExists();
            WebElement.Clear();
            WebElement.SendKeys(keys);
        }
    }
}
