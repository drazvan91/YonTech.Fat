using System;

namespace Yontech.Fat.BusyConditions
{
    public class PendingRequestsBusyCondition : FatBusyCondition
    {
        private string _script = @"
            function initPendingRequestsBusy() {
                if(window.fatData && window.fatData.pendingRequests!== undefined) return;

                window.fatData = window.fatData || {};
                window.fatData.pendingRequests = window.fatData.pendingRequests || 0;

                window.fatOperations = {
                requestStarted: () => { 
                    window.fatData.pendingRequests = window.fatData.pendingRequests+1; 
                    console.log('pending requests: '+window.fatData.pendingRequests);
                },
                requestFinished: () => {
                    window.fatData.pendingRequests = Math.max(window.fatData.pendingRequests-1, 0); 
                    console.log('pending requests: '+window.fatData.pendingRequests);
                }
                };

                let oldOpen = XMLHttpRequest.prototype.open;
                XMLHttpRequest.prototype.open = function(args) { 
                    fatOperations.requestStarted(); 
                    this.onloadend = fatOperations.requestFinished; 

                    oldOpen.apply(this, arguments); 
                };

                let oldFetch = window.fetch;
                window.fetch = (args) => {
                    fatOperations.requestStarted(); 
                    let result = oldFetch(args);
                    result.finally(fatOperations.requestFinished);
                    return result;
                };
            }; 

            initPendingRequestsBusy();

            return window.fatData.pendingRequests;
            ";

        private long _lastPendingRequestsValue = 0;
        protected internal override bool IsBusy()
        {
            try
            {
                var pendingRequests = (long)WebBrowser.JavaScriptExecutor.ExecuteScript(this._script);
                if (pendingRequests != _lastPendingRequestsValue)
                {
                    LogDebug("Pending requests: {0}  (wait iteration number: {1})", pendingRequests, WaitSessionPollingNumber);
                }

                _lastPendingRequestsValue = pendingRequests;
                return pendingRequests > 0;
            }
            catch (Exception ex)
            {
                LogDebug("An exception was thrown {0}", ex.Message);
                return false;
            }
        }
    }
}
