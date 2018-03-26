using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public interface IWebBrowser:IDisposable
    {
        BrowserType Type { get; }

        IControlFinder ControlFinder { get; }
        IJsExecutor JavaScriptExecutor { get; }
        IIFrameControl IFrameControl { get; }

        WebBrowserConfiguration Configuration { get; }

        void Navigate(string url);
        string Url { get; }
        void Close();

        void WaitForIdle();
        void WaitForIdle(int timout);

        ISnapshot TakeSnapshot();
    }
}
