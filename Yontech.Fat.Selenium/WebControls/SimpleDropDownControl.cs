using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using OpenQA.Selenium;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class SimpleDropDownControl : BaseSeleniumControl, ISimpleDropDownControl
    {
        IWebElement Select;


        public SimpleDropDownControl(IWebElement webElement, SeleniumWebBrowser webBrowser) : base(webElement, webBrowser)
        {
            Select = webElement;
        }

        public void Click()
        {
            throw new NotImplementedException();
        }

        public void ScrollTo()
        {
            throw new NotImplementedException();
        }

        public void SelectItem(string itemText)
        {

        }

        public void ShouldBeVisible()
        {
            throw new NotImplementedException();
        }

        public void ShouldNotBeVisible()
        {
            throw new NotImplementedException();
        }
    }
}

