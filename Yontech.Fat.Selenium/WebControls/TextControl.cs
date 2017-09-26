using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class TextControl : BaseSeleniumControl, ITextControl
    {

        public TextControl(IWebElement webElement, SeleniumWebBrowser webBrowser)
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

        public void InnerTextShouldBe(string text)
        {
            EnsureElementExists();

            if (this.Text != text)
            {
                throw new Exception($"Control contains '{this.Text}' instead of '{text}'");
            }
        }

        public void ShouldContainText(string text  )
        {
            EnsureElementExists();
            if (this.Text.IndexOf(text,StringComparison.CurrentCultureIgnoreCase) < 0)
            {
                throw new Exception($"Element does not contain this text: {text}");
            }
        }

        public void ShouldNotContainText(string text)
        {
            EnsureElementExists();
            if (this.Text.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) > 0)
            {
                throw new Exception($"Element contains this text: {text} and it shoudn't");
            }
        }
    }
}
