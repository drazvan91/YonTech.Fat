namespace Yontech.Fat
{
    public interface IBusyCondition
    {
        bool IsBusy(IWebBrowser webBrowser);
    }
}