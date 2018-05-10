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

            var checkAngularExistsScript = $"return window.angular != undefined && angular.element('{selector}').injector() != undefined";
            try
            {
                var angularExists = (bool)this._javascriptExecutor.ExecuteScript(checkAngularExistsScript);
                if (!angularExists)
                    return false;
                var pendingRequestsScript = $"return angular.element('{selector}').injector().get('$http').pendingRequests.length > 0";
                try
                {
                    var executeResult = this._javascriptExecutor.ExecuteScript(pendingRequestsScript);

                    bool busy = (bool)executeResult;
                    return busy;
                }
                catch (Exception ex)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
