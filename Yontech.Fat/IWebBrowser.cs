using System;
using Yontech.Fat.Configuration;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public interface IWebBrowser:IDisposable
    {
        BrowserType Type { get; }

        IControlFinder ControlFinder { get; }
        IJsExecutor JavaScriptExecutor { get; }
        IIFrameControl IFrameControl { get; }
        string CurrentUrl { get; }
        WebBrowserConfiguration Configuration { get; }

        void Navigate(string url);
        void Close();
        void WaitForIdle();
        void WaitForIdle(int timout);

        ISnapshot TakeSnapshot();
    }
}
