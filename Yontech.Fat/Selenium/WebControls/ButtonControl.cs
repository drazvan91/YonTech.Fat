using System;
using Yontech.Fat.WebControls;
using OpenQA.Selenium;

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
