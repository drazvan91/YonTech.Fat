using System;

namespace Yontech.Fat.JavaScriptUtilities.BusyConditions
{
    public class AngularPendingRequestsBusyCondition : IBusyCondition
    {
        private readonly string _ngAppCssSelector;

        public AngularPendingRequestsBusyCondition(string ngAppCssSelector)
        {
            this._ngAppCssSelector = ngAppCssSelector;
        }

        public bool IsBusy(IWebBrowser webBrowser)
        {
            var selector = _ngAppCssSelector.Replace("'", "\"");

            var checkAngularExistsScript = $"return window.angular != undefined && angular.element('{selector}').injector() != undefined";
            try
            {
                var angularExists = (bool)webBrowser.JavaScriptExecutor.ExecuteScript(checkAngularExistsScript);
                if (!angularExists)
                    return false;
                var pendingRequestsScript = $"return angular.element('{selector}').injector().get('$http').pendingRequests.length > 0";
                try
                {
                    var executeResult = webBrowser.JavaScriptExecutor.ExecuteScript(pendingRequestsScript);
                    bool busy = (bool)executeResult;
                    return busy;
                }
                catch (Exception)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
