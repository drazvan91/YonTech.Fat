using System;
using Yontech.Fat.WebControls;
using OpenQA.Selenium;

namespace Yontech.Fat.Selenium.WebControls
{
  internal class ButtonControl : BaseSeleniumControl, IButtonControl
  {

    public ButtonControl(IWebElement webElement, SeleniumWebBrowser webBrowser)
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

    public void RageClick(int numberOfClicks)
    {
      EnsureElementExists();
      this.ScrollTo();

      for (int i = 0; i < numberOfClicks; i++)
      {
        base.WebElement.Click();
      }

      base.WebBrowser.WaitForIdle();
    }
  }
}
