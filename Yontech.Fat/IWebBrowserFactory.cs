namespace Yontech.Fat
{
    internal interface IWebBrowserFactory
    {
        IWebBrowser Create(BrowserFatConfig browserConfig);
    }
}
