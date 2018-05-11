using OpenQA.Selenium;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class CheckboxControl : ButtonControl, ICheckboxControl
    {
        public CheckboxControl(IWebElement webElement, SeleniumWebBrowser webBrowser) : base(webElement, webBrowser)
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
