using System;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Configuration;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
  public interface IWebBrowser : IDisposable
  {
    BrowserType Type { get; }

    IControlFinder ControlFinder { get; }
    IJsExecutor JavaScriptExecutor { get; }
    IIFrameControl IFrameControl { get; }
    string CurrentUrl { get; }
    WebBrowserConfiguration Configuration { get; }

    void Navigate(string url);
    void Refresh();
    bool AcceptAlert();
    bool DismissAlert();
    void Close();
    void WaitForIdle();
    void WaitForIdle(int timout);
    void WaitForCondition(IBusyCondition condition);
    void WaitForElementToAppear(string cssSelector);
    void WaitForElementToAppear(string cssSelector, int timeout);

    ISnapshot TakeSnapshot();
  }
}
