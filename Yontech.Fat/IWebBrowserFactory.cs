using Yontech.Fat.Configuration;

namespace Yontech.Fat
{
    internal interface IWebBrowserFactory
    {
        IWebBrowser Create(BaseBrowserFatConfig browserConfig);
    }
}
