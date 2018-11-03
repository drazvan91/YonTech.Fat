using System;
using Yontech.Fat.Logging;

namespace Yontech.Fat.JavaScriptUtilities.BusyConditions
{
    public class JQueryActiveRequestsBusyCondition : IBusyCondition
    {
        public bool IsBusy(IWebBrowser webBrowser)
        {
            try
            {
                TraceLogger.Write("aici");
                var result = webBrowser.JavaScriptExecutor.ExecuteScript("return $.active > 0");
                return (bool)result;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
