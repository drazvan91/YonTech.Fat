using Yontech.Fat.Logging;

namespace Yontech.Fat.JavaScriptUtilities.BusyConditions
{
    public class DocumentReadyBusyCondition : IBusyCondition
    {
        public bool IsBusy(IWebBrowser webBrowser)
        {
            var result = (string)webBrowser.JavaScriptExecutor.ExecuteScript("return document.readyState");
            if (result != "complete")
            {
                TraceLogger.Write("Page status: {0}", result);
            }

            return result != "complete";
        }
    }
}
