﻿using System;
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
        private readonly ISearchContext _elementScope;
        private readonly SeleniumWebBrowser _webBrowser;

        public SeleniumControlFinder(SeleniumWebBrowser webBrowser) : this(webBrowser.WebDriver, webBrowser) { }

        public SeleniumControlFinder(ISearchContext elementScope, SeleniumWebBrowser webBrowser)
        {
            this._elementScope = elementScope;
            this._webBrowser = webBrowser;
        }

        private IWebElement FindElement(By selector)
        {
            var elements = this._elementScope.FindElements(selector);
            if (elements.Count > 1)
            {
                throw new MultipleWebControlsFoundException();
            }

            return elements.FirstOrDefault();
        }

        public IButtonControl Button(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new ButtonControl(element, _webBrowser);
        }

        public IRadioButtonControl RadioButton(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new RadioButtonControl(element, _webBrowser);
        }

        public ICheckboxControl Checkbox(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new CheckboxControl(element, _webBrowser);
        }

        public ITextBoxControl TextBox(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new TextBoxControl(element, _webBrowser);
        }

        public ITextControl Text(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new TextControl(element, _webBrowser);
        }

        public IDropdownControl Dropdown(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new DropdownControl(element, _webBrowser);
        }

        public IGenericControl Generic(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new GenericControl(element, _webBrowser);
        }

        public IList<ITextControl> TextList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            var textControlElements = new List<ITextControl>();
            foreach (var el in elements)
            {
                textControlElements.Add(new TextControl(el, _webBrowser));
            }
            return textControlElements;
        }

        public IList<ITextBoxControl> TextBoxList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            var textBoxControlElements = new List<ITextBoxControl>();
            foreach (var el in elements)
            {
                textBoxControlElements.Add(new TextBoxControl(el, _webBrowser));
            }

            return textBoxControlElements;
        }

        public IList<IButtonControl> ButtonList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            var buttonControlElements = new List<IButtonControl>();
            foreach (var el in elements)
            {
                buttonControlElements.Add(new ButtonControl(el, _webBrowser));
            }

            return buttonControlElements;
        }
    }
}