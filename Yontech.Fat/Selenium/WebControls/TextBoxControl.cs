using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Yontech.Fat.Selenium.WebControls
{
  internal class TextBoxControl : BaseSeleniumControl, ITextBoxControl
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

    public string Value
    {
      get
      {
        EnsureElementExists();
        return WebElement.GetAttribute("value");
      }
    }

    public void ClearText()
    {
      EnsureElementExists();
      // todo: investigate why this is needed, 
      // also, if needed, make sure it works also on MacOs
      WebElement.SendKeys(Keys.Control + "a");
      WebElement.SendKeys(Keys.Backspace);
      WebElement.Clear();
    }

    public new void Click()
    {
      WebElement.Click();
    }

    public void TypeKeys(string keys)
    {
      EnsureElementExists();
      WebElement.SendKeys(keys);
      base.WebBrowser.WaitForIdle();
    }

    public void TypeKeysSlowly(string keys, int delayBetweenKeys = 300)
    {
      EnsureElementExists();
      foreach (var key in keys)
      {
        WebElement.SendKeys(key.ToString());
        Thread.Sleep(delayBetweenKeys);
      }
      base.WebBrowser.WaitForIdle();
    }
  }
}
