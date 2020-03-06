namespace Yontech.Fat.BusyConditions
{
  public interface IBusyCondition
  {
    bool IsBusy(IWebBrowser webBrowser);
  }
}