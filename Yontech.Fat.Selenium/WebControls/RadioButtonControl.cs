using OpenQA.Selenium;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class RadioButtonControl : ButtonControl, IRadioButtonControl
    {
        public RadioButtonControl(IWebElement webElement, SeleniumWebBrowser webBrowser) : base(webElement, webBrowser)
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
    }
}
