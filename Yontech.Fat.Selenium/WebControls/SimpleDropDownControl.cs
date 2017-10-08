using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using OpenQA.Selenium;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class SimpleDropDownControl : BaseSeleniumControl, IDropdownControl
    {
        IWebElement Select;


        public SimpleDropDownControl(IWebElement webElement, SeleniumWebBrowser webBrowser) : base(webElement, webBrowser)
        {
            Select = webElement;
        }

        public bool IsOpen => throw new NotImplementedException();

        public string ToggleText => throw new NotImplementedException();

        public string SelectedItem => throw new NotImplementedException();

        public void Close()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetItems()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void SelectItem(string itemText)
        {
            throw new NotImplementedException();
        }

        public void SelectItem(int index)
        {
            throw new NotImplementedException();
        }

        public void ToggleTextShouldBe(string text)
        {
            throw new NotImplementedException();
        }
    }
}

