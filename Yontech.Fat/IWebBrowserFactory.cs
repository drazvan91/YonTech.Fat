using System.Collections.Generic;
using Yontech.Fat.Configuration;

namespace Yontech.Fat
{
    public interface IWebBrowserFactory
    {
        IWebBrowser Create(BrowserType browserType);
        IWebBrowser Create(BrowserType browserType, BrowserStartOptions startOptions);
        IWebBrowser Create(BrowserType browserType, BrowserStartOptions startOptions, IEnumerable<IBusyCondition> busyConditions);
    }
}
