using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class GenericControl : BaseSeleniumControl, IGenericControl
    {
        public GenericControl(IWebElement webElement, SeleniumWebBrowser webBrowser)
            : base(webElement, webBrowser)
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

        public void Click()
        {
            EnsureElementExists();
            WebElement.Click();
        }

        public IEnumerable<IGenericControl> Find(string cssSelector)
        {
            EnsureElementExists();
            foreach (var element in WebElement.FindElements(By.CssSelector(cssSelector)))
            {
                yield return new GenericControl(element, WebBrowser);
            }
        }

        public string GetAttribute(string attributeName)
        {
            EnsureElementExists();
            return WebElement.GetAttribute(attributeName);
        }
    }
}
