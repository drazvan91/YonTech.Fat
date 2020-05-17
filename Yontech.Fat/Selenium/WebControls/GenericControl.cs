using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;
using Yontech.Fat.Logging;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class GenericControl : BaseSeleniumControl, IGenericControl
    {
        private readonly ILoggerFactory _loggerFactory;

        public GenericControl(ILoggerFactory loggerFactory, SelectorNode selectorNode, IWebElement webElement, SeleniumWebBrowser webBrowser)
            : base(selectorNode, webElement, webBrowser)
        {
            this._loggerFactory = loggerFactory;
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

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "This is an alias to improve readability")]
        public IControlFinder _ => new SeleniumControlFinder(this._loggerFactory, SelectorNode, WebElement, WebBrowser);

        public IEnumerable<IGenericControl> Find(string cssSelector)
        {
            EnsureElementExists();
            int index = 0;
            foreach (var element in WebElement.FindElements(By.CssSelector(cssSelector)))
            {
                var newNode = new SelectorNode(cssSelector, null, base.SelectorNode);

                yield return new GenericControl(this._loggerFactory, newNode, element, WebBrowser);
                index++;
            }
        }
    }
}
