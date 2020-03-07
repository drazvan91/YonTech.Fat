namespace Yontech.Fat.BusyConditions
{
    public class DocumentReadyBusyCondition : FatBusyCondition
    {
        protected internal override bool IsBusy()
        {
            var result = (string)WebBrowser.JavaScriptExecutor.ExecuteScript("return document.readyState");

            if (result != "complete")
            {
                LogDebug("Document state is {0}", result);
            }

            return result != "complete";
        }
    }
}
