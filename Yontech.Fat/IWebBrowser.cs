using System;
using System.Drawing;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Configuration;
using Yontech.Fat.WebBrowser;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public interface IWebBrowser : IDisposable
    {
        BrowserType Type { get; }

        IControlFinder ControlFinder { get; }
        IJsExecutor JavaScriptExecutor { get; }
        IIFrameControl IFrameControl { get; }
        Size Size { get; }
        string CurrentUrl { get; }
        string Title { get; }
        WebBrowserConfiguration Configuration { get; }

        void Navigate(string url);
        void Refresh();
        bool AcceptAlert();
        bool DismissAlert();
        void Close();
        void WaitForIdle();
        void WaitForIdle(int timout);
        void WaitForCondition(IBusyCondition condition);
        void WaitForConditionToBeTrue(Func<bool> condition);
        void WaitForElementToAppear(string cssSelector);
        void WaitForElementToAppear(string cssSelector, int timeout);

        void SimulateOfflineConnection();
        void SimulateSlowConnection(int latency = 1000);
        void SimulateFastConnection();

        void Resize(int width, int height);
        void Fullscreen();
        void Maximize();
        void Minimize();

        IWebBrowserStorage LocalStorage { get; }
        IWebBrowserStorage SessionStorage { get; }

        ISnapshot TakeSnapshot();
    }
}
