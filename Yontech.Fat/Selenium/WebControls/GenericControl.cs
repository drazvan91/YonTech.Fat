using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class GenericControl : BaseSeleniumControl, IGenericControl
    {
        public GenericControl(SelectorNode selectorNode, IWebElement webElement, SeleniumWebBrowser webBrowser)
            : base(selectorNode, webElement, webBrowser)
        {
        }

        public string Text
        {
            get
            {
                EnsureElementExists();
                return WebElement.Text;
            }
        }

        public IControlFinder ControlFinder => _;
        public IControlFinder _ => new SeleniumControlFinder(SelectorNode, WebElement, WebBrowser);

        public IEnumerable<IGenericControl> Find(string cssSelector)
        {
            EnsureElementExists();
            int index = 0;
            foreach (var element in WebElement.FindElements(By.CssSelector(cssSelector)))
            {
                var newNode = new SelectorNode(cssSelector, null, base.SelectorNode);

                yield return new GenericControl(newNode, element, WebBrowser);
                index++;
            }
        }
    }
}
