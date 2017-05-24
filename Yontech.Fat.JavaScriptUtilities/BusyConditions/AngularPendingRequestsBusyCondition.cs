using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.JavaScriptUtilities.BusyConditions
{
    public class AngularPendingRequestsBusyCondition : IBusyCondition
    {
        private readonly IJsExecutor _javascriptExecutor;
        private readonly string _ngAppCssSelector;

        public AngularPendingRequestsBusyCondition(IWebBrowser webBrowser, string ngAppCssSelector)
        {
            this._javascriptExecutor = webBrowser.JavaScriptExecutor;
            this._ngAppCssSelector = ngAppCssSelector;
        }

        public bool IsBusy()
        {
            var selector = _ngAppCssSelector.Replace("'", "\"");

            var script = $"return window.angular != undefined && (angular.element('{selector}').injector().get('$http').pendingRequests.length > 0)";
            try
            {
                var busy = (bool)this._javascriptExecutor.ExecuteScript(script);
                return busy;
            }catch(Exception ex)
            {

                return true;
            }
        }
    }
}
