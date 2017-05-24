using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yontech.Fat.Logging;

namespace Yontech.Fat.JavaScriptUtilities.BusyConditions
{
    public class JQueryActiveRequestsBusyCondition : IBusyCondition
    {
        private readonly IJsExecutor _javascriptExecutor;

        public JQueryActiveRequestsBusyCondition(IWebBrowser webBrowser)
        {
            this._javascriptExecutor = webBrowser.JavaScriptExecutor;
        }

        public bool IsBusy()
        {
            try
            {
                TraceLogger.Write("aici");
                var result = _javascriptExecutor.ExecuteScript("return $.active > 0");
                return (bool)result;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
