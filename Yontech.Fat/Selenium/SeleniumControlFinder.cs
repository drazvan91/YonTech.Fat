using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Selenium.WebControls;
using Yontech.Fat.Waiters;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumControlFinder : IControlFinder
    {
        private readonly SelectorNode _selectorNode;
        private readonly ISearchContext _elementScope;
        private readonly SeleniumWebBrowser _webBrowser;

        public SeleniumControlFinder(SeleniumWebBrowser webBrowser)
            : this(null, webBrowser.WebDriver, webBrowser)
        {
        }

        public SeleniumControlFinder(SelectorNode selectorNode, ISearchContext elementScope, SeleniumWebBrowser webBrowser)
        {
            this._selectorNode = selectorNode;
            this._elementScope = elementScope;
            this._webBrowser = webBrowser;
        }

        public IButtonControl Button(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            var newNode = new SelectorNode(cssSelector, null, this._selectorNode);
            return new ButtonControl(newNode, element, _webBrowser);
        }

        public IRadioButtonControl RadioButton(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            var newNode = new SelectorNode(cssSelector, null, this._selectorNode);
            return new RadioButtonControl(newNode, element, _webBrowser);
        }

        public ICheckboxControl Checkbox(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            var newNode = new SelectorNode(cssSelector, null, this._selectorNode);
            return new CheckboxControl(newNode, element, _webBrowser);
        }

        public ITextBoxControl TextBox(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            var newNode = new SelectorNode(cssSelector, null, this._selectorNode);
            return new TextBoxControl(newNode, element, _webBrowser);
        }

        public ITextControl Text(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            var newNode = new SelectorNode(cssSelector, null, this._selectorNode);
            return new TextControl(newNode, element, _webBrowser);
        }

        public ILinkControl Link(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            var newNode = new SelectorNode(cssSelector, null, this._selectorNode);
            return new LinkControl(newNode, element, _webBrowser);
        }

        public IDropdownControl Dropdown(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            var newNode = new SelectorNode(cssSelector, null, this._selectorNode);
            return new DropdownControl(newNode, element, _webBrowser);
        }

        public IGenericControl Generic(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            var newNode = new SelectorNode(cssSelector, null, this._selectorNode);
            return new GenericControl(newNode, element, _webBrowser);
        }

        public IEnumerable<ITextControl> TextList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            int index = 0;
            foreach (var el in elements)
            {
                var newNode = new SelectorNode(cssSelector, index, this._selectorNode);
                yield return new TextControl(newNode, el, _webBrowser);
                index++;
            }
        }

        public IEnumerable<ITextBoxControl> TextBoxList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            int index = 0;
            foreach (var el in elements)
            {
                var newNode = new SelectorNode(cssSelector, index, this._selectorNode);
                yield return new TextBoxControl(newNode, el, _webBrowser);

                index++;
            }
        }

        public IEnumerable<IButtonControl> ButtonList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            int index = 0;
            foreach (var el in elements)
            {
                var newNode = new SelectorNode(cssSelector, index, this._selectorNode);
                yield return new ButtonControl(newNode, el, _webBrowser);

                index++;
            }
        }

        public IEnumerable<ILinkControl> LinkList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            int index = 0;
            foreach (var el in elements)
            {
                var newNode = new SelectorNode(cssSelector, index, this._selectorNode);
                yield return new LinkControl(newNode, el, _webBrowser);

                index++;
            }
        }

        public TComponent Custom<TComponent>(string cssSelector) where TComponent : FatCustomComponent, new()
        {
            var element = FindElement(By.CssSelector(cssSelector));
            if (element == null)
            {
                throw new FatException($"element with selector '{cssSelector}' not found");
            }

            var newNode = new SelectorNode(cssSelector, null, this._selectorNode);

            var custom = new TComponent();
            custom.WebBrowser = this._webBrowser;
            custom.Container = new GenericControl(newNode, element, this._webBrowser);
            return custom;
        }

        public IEnumerable<TComponent> CustomList<TComponent>(string cssSelector) where TComponent : FatCustomComponent, new()
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            int index = 0;
            foreach (var element in elements)
            {
                var newNode = new SelectorNode(cssSelector, index, this._selectorNode);

                var custom = new TComponent();
                custom.WebBrowser = this._webBrowser;
                custom.Container = new GenericControl(newNode, element, this._webBrowser);
                yield return custom;

                index++;
            }
        }

        public IButtonControl ButtonByXPath(string xPathSelector)
        {
            var element = FindElement(By.XPath(xPathSelector));
            var newNode = new SelectorNode(xPathSelector, null, this._selectorNode);

            return new ButtonControl(newNode, element, _webBrowser);
        }

        public ITextControl TextByXPath(string xPathSelector)
        {
            var element = FindElement(By.XPath(xPathSelector));
            var newNode = new SelectorNode(xPathSelector, null, this._selectorNode);
            return new TextControl(newNode, element, _webBrowser);
        }

        public ITextBoxControl TextBoxByXPath(string xPathSelector)
        {
            var element = FindElement(By.XPath(xPathSelector));
            var newNode = new SelectorNode(xPathSelector, null, this._selectorNode);
            return new TextBoxControl(newNode, element, _webBrowser);
        }

        public IGenericControl GenericByXPath(string xPathSelector)
        {
            var element = FindElement(By.XPath(xPathSelector));
            var newNode = new SelectorNode(xPathSelector, null, this._selectorNode);
            return new GenericControl(newNode, element, _webBrowser);
        }

        public bool Exists(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            return elements.Count > 0;
        }

        private IWebElement FindElement(By selector)
        {
            IWebElement webElement = null;
            Waiter.WaitForConditionToBeTrueOrTimeout(() =>
            {
                this._webBrowser.WaitForIdle();
                var elements = this._elementScope.FindElements(selector);
                if (elements.Count > 1)
                {
                    SelectorNode newNode = new SelectorNode(selector.ToString(), 0, this._selectorNode);
                    throw new MultipleWebControlsFoundException(newNode);
                }

                webElement = elements.FirstOrDefault();

                if (webElement == null)
                {
                    return false;
                }

                try
                {
                    return webElement.Displayed;
                }
                catch (Exception ex) when (ex.Message.Contains("element is not attached to the page document"))
                {
                    return false;
                }
            }, _webBrowser.Configuration.FinderTimeout);
            return webElement;
        }
    }
}
