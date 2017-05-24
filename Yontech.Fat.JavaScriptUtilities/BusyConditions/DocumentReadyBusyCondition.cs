using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yontech.Fat.Logging;

namespace Yontech.Fat.JavaScriptUtilities.BusyConditions
{
    public class DocumentReadyBusyCondition : IBusyCondition
    {
        private readonly IJsExecutor _javascriptExecutor;

        public DocumentReadyBusyCondition(IWebBrowser webBrowser)
        {
            this._javascriptExecutor = webBrowser.JavaScriptExecutor;
        }

        public bool IsBusy()
        {
            var result = (string)_javascriptExecutor.ExecuteScript("return document.readyState");
            if (result != "complete") {
                TraceLogger.Write("Page status: {0}", result);
            }
            return result != "complete";
        }
    }
}
