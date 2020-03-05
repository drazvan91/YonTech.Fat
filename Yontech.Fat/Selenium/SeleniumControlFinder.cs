using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using Yontech.Fat.Selenium.WebControls;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Waiters;

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
            IWebElement webElement = null;
            Waiter.WaitForConditionToBeTrueOrTimeout(() =>
            {
                var elements = this._elementScope.FindElements(selector);
                if (elements.Count > 1)
                {
                    throw new MultipleWebControlsFoundException();
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
            }, _webBrowser.Configuration.DefaultTimeout);
            return webElement;
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

        public ILinkControl Link(string cssSelector)
        {
            var element = FindElement(By.CssSelector(cssSelector));
            return new LinkControl(element, _webBrowser);
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

        public IEnumerable<ITextControl> TextList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            var textControlElements = new List<ITextControl>();
            foreach (var el in elements)
            {
                textControlElements.Add(new TextControl(el, _webBrowser));
            }
            return textControlElements;
        }

        public IEnumerable<ITextBoxControl> TextBoxList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            var textBoxControlElements = new List<ITextBoxControl>();
            foreach (var el in elements)
            {
                textBoxControlElements.Add(new TextBoxControl(el, _webBrowser));
            }

            return textBoxControlElements;
        }

        public IEnumerable<IButtonControl> ButtonList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            var buttonControlElements = new List<IButtonControl>();
            foreach (var el in elements)
            {
                buttonControlElements.Add(new ButtonControl(el, _webBrowser));
            }

            return buttonControlElements;
        }

        public IEnumerable<ILinkControl> LinkList(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            foreach (var el in elements)
            {
                yield return new LinkControl(el, _webBrowser);
            }
        }

        public TComponent Custom<TComponent>(string cssSelector) where TComponent : FatCustomComponent, new()
        {
            var element = FindElement(By.CssSelector(cssSelector));

            var custom = new TComponent();
            custom.WebBrowser = this._webBrowser;
            custom.Container = new GenericControl(element, this._webBrowser);
            return custom;
        }

        public IEnumerable<TComponent> CustomList<TComponent>(string cssSelector) where TComponent : FatCustomComponent, new()
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            foreach (var element in elements)
            {
                var custom = new TComponent();
                custom.WebBrowser = this._webBrowser;
                custom.Container = new GenericControl(element, this._webBrowser);
                yield return custom;
            }
        }

        public IButtonControl ButtonByXPath(string xPathSelector)
        {
            var element = FindElement(By.XPath(xPathSelector));
            return new ButtonControl(element, _webBrowser);
        }

        public ITextControl TextByXPath(string xPathSelector)
        {
            var element = FindElement(By.XPath(xPathSelector));
            return new TextControl(element, _webBrowser);
        }

        public ITextBoxControl TextBoxByXPath(string xPathSelector)
        {
            var element = FindElement(By.XPath(xPathSelector));
            return new TextBoxControl(element, _webBrowser);
        }

        public IGenericControl GenericByXPath(string xPathSelector)
        {
            var element = FindElement(By.XPath(xPathSelector));
            return new GenericControl(element, _webBrowser);
        }

        public bool Exists(string cssSelector)
        {
            var elements = this._elementScope.FindElements(By.CssSelector(cssSelector));
            return elements.Count > 0;
        }
    }
}
