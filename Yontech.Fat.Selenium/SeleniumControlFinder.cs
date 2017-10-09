using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using Yontech.Fat.Selenium.WebControls;
using Yontech.Fat.Exceptions;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumControlFinder : IControlFinder
    {
        private readonly SeleniumWebBrowser webBrowser;

        public SeleniumControlFinder(SeleniumWebBrowser webBrowser)
        {
            this.webBrowser = webBrowser;
        }

        private IWebElement FindElement(By selector)
        {
            var elements = webBrowser.WebDriver.FindElements(selector);
            if (elements.Count > 1)
            {
                throw new MultipleWebControlsFoundException();
            }

            return elements.FirstOrDefault();
        }

        public IButtonControl Button(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new ButtonControl(element, webBrowser);
        }

        public ITextBoxControl TextBox(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new TextBoxControl(element, webBrowser);
        }

        public ITextControl Text(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new TextControl(element, webBrowser);
        }

        public IClassicDropdownControl ClasicDropdown(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new ClassicDropDownControl(element, webBrowser);
        }

        public IGenericControl Generic(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new GenericControl(element, webBrowser);
        }
    }
}
