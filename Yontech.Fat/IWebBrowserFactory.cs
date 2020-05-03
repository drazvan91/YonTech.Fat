using System.Collections.Generic;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.Configuration;

namespace Yontech.Fat
{
    internal interface IWebBrowserFactory
    {
        IWebBrowser Create();
    }
}
