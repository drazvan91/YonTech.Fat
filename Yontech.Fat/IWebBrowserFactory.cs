namespace Yontech.Fat
{
    public interface IWebBrowserFactory
    {
        IWebBrowser Create(BrowserType browserType);
    }
}
