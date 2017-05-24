using Yontech.Fat.WebControls;
using OpenQA.Selenium;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class TextBoxControl:BaseSeleniumControl, ITextBoxControl
    {
        public TextBoxControl(IWebElement webElement, SeleniumWebBrowser webBrowser)
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

        public void ClearText()
        {
            EnsureElementExists();
            WebElement.Clear();
        }

        public void SendKeys(string keys)
        {
            EnsureElementExists();
            WebElement.SendKeys(keys);
        }
    }
}
