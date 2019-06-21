using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class DropdownControl : BaseSeleniumControl, IDropdownControl
    {
        private readonly SelectElement _dropdown;

        public DropdownControl(IWebElement webElement, SeleniumWebBrowser webBrowser) : base(webElement, webBrowser)
        {
            _dropdown = new SelectElement(webElement);
        }

        public bool IsOpen => throw new System.NotImplementedException();

        public string ToggleText => throw new System.NotImplementedException();

        public string SelectedItem => throw new System.NotImplementedException();

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetItems()
        {
            throw new System.NotImplementedException();
        }

        public void Open()
        {
            base.Click();
        }

        public void SelectItem(string itemText)
        {
            _dropdown.SelectByText(itemText);
        }

        public void SelectItem(int index)
        {
            throw new System.NotImplementedException();
        }

        public void ToggleTextShouldBe(string text)
        {
            EnsureElementExists();
            this.WebBrowser.WaitForIdle();
            var selectedOptionText = _dropdown.SelectedOption.Text;

            if (!selectedOptionText.Equals(text))
            {
                throw new Exception($"Control contains '{selectedOptionText}' instead of '{text}'");
            }
        }
    }
}
