namespace Yontech.Fat
{
    public interface IWebBrowserTab
    {
        IWebBrowserTab Focus();
        void Close();
        string Id { get; }
    }
}
