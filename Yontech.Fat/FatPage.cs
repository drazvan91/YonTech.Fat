
namespace Yontech.Fat
{
  public abstract class FatPage
  {
    protected internal IControlFinder _ => WebBrowser.ControlFinder;
    protected internal IWebBrowser WebBrowser { get; internal set; }
  }
}