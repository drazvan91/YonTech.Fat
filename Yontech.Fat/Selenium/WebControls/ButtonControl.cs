using System;
using OpenQA.Selenium;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class ButtonControl : BaseSeleniumControl, IButtonControl
    {
        public ButtonControl(SelectorNode selectorNode, IWebElement webElement, SeleniumWebBrowser webBrowser)
          : base(selectorNode, webElement, webBrowser)
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
    }
}
