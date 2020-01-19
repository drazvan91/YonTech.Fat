
namespace Yontech.Fat
{
  public abstract class FatPageSection
  {
    internal protected IWebBrowser WebBrowser { get; internal set; }
    internal protected IControlFinder _ => WebBrowser.ControlFinder;
  }
}