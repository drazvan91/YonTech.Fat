using System;
using OpenQA.Selenium;
using Yontech.Fat.Exceptions;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class LinkControl : BaseSeleniumControl, ILinkControl
    {
        public LinkControl(SelectorNode selectorNode, IWebElement webElement, SeleniumWebBrowser webBrowser)
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

        public void ShouldContainText(string text)
        {
            EnsureElementExists();
            if (this.Text.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) < 0)
            {
                throw new FatAssertException($"Element does not contain this text: {text}. It is equal to: {this.Text}");
            }
        }

        public void ShouldHaveText(string text)
        {
            EnsureElementExists();

            if (this.Text != text)
            {
                throw new FatAssertException($"Control contains '{this.Text}' instead of '{text}'");
            }
        }

        public void ShouldNotContainText(string text)
        {
            EnsureElementExists();
            if (this.Text.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) > 0)
            {
                throw new FatAssertException($"Element contains this text: {text} and it shoudn't");
            }
        }
    }
}
