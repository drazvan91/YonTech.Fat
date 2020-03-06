namespace Yontech.Fat.BusyConditions
{
    public class DocumentReadyBusyCondition : IBusyCondition
    {
        public bool IsBusy(IWebBrowser webBrowser)
        {
            var result = (string)webBrowser.JavaScriptExecutor.ExecuteScript("return document.readyState");

            return result != "complete";
        }
    }
}