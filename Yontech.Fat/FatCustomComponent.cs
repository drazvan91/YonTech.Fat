
using OpenQA.Selenium;
using Yontech.Fat.Selenium.WebControls;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
  public abstract class FatCustomComponent
  {
    internal IWebElement _webElement { get; set; }
    internal protected IWebBrowser WebBrowser { get; internal set; }
    internal protected IGenericControl Container { get; internal set; }
    internal protected IControlFinder _ => Container.ControlFinder;
  }
}