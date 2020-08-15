using System;
using System.Drawing;
using Yontech.Fat.Configuration;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public interface IWebBrowser : IDisposable
    {
        BrowserType BrowserType { get; }
        int BrowserId { get; }

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

        /// <summary> After closing the tab it will focus on tab index 0. Use SwitchToTab(index) if you need another tab.</summary>
        void CloseCurrentTab();
        void WaitForIdle();
        void WaitForIdle(int timout);
        void WaitForCondition(FatBusyCondition condition);
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

        ISnapshot TakeSnapshot();

        /// <summary> It opens a new tab to the specifed URL but it does not switch the context to it. Use .Focus() for that </summary>
        IWebBrowserTab OpenNewTab(string url);
        IWebBrowserTab CurrentTab { get; }

        /// <summary> Gets the tab with specified tabId. </summary>
        IWebBrowserTab Tab(string tabId);

        /// <summary> Gets the tab with specified tabIndex. TabIndex stards from 0 </summary>
        IWebBrowserTab Tab(int tabIndex);
        IWebBrowserTab[] Tabs { get; }
    }
}
