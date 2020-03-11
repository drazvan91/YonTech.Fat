using OpenQA.Selenium;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class CheckboxControl : ButtonControl, ICheckboxControl
    {
        public CheckboxControl(SelectorNode selectorNode, IWebElement webElement, SeleniumWebBrowser webBrowser)
        : base(selectorNode, webElement, webBrowser)
        {
        }

        public bool IsChecked
        {
            get
            {
                EnsureElementExists();
                return WebElement.Selected;
            }
        }

        public void Toggle()
        {
            base.Click();
        }
    }
}
