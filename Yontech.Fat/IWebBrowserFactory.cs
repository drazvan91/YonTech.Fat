namespace Yontech.Fat
{
    internal interface IWebBrowserFactory
    {
        IWebBrowser Create(BrowserType browserType);
    }
}
