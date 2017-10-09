using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class ClassicDropDownControl : BaseSeleniumControl, IClassicDropdownControl
    {
        private readonly SelectElement _dropdown;

        #region Constructor
        public ClassicDropDownControl(IWebElement webElement, SeleniumWebBrowser webBrowser) : base(webElement, webBrowser)
        {
            _dropdown = new SelectElement(webElement);
        }
        #endregion

        #region Methods
        public void SelectItemByText(string text)
        {
            _dropdown.SelectByText(text);
        }
        #endregion
    }
}

