using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat
{
    public interface IWebBrowser:IDisposable
    {
        BrowserType Type { get; }

        IControlFinder ControlFinder { get; }
        IJsExecutor JavaScriptExecutor { get; }

        WebBrowserConfiguration Configuration { get; }

        void Navigate(string url);
        void Close();

        void WaitForIdle();
        void WaitForIdle(int timout);

        ISnapshot TakeSnapshot();
    }
}
