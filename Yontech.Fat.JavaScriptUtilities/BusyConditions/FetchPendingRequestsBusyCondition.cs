using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.JavaScriptUtilities.BusyConditions
{
    public class FetchPendingRequestsBusyCondition : IBusyCondition
    {
        private string CreateScript()
        {
            return @"
                (function(w) {
                    var fat = w.fatFramework = w.fatFramework || {};
                    fat.busyConditions = fatFramework.busyConditions || {};

                    if(fat.busyConditions.fetchPendingRequests !== undefined){
                        return;
                    }
                    var oldFetch = window.fetch;
                    var pendingRequests = 0;

                    window.fetch = function(url, options) {
                        pendingRequests++;
                        console.log('[FatFramework] pending requests: '+pendingRequests);
                        return oldFetch(url, options).finally(function(){
                            pendingRequests--;
                            if(pendingRequests<0){
                                pendingRequests = 0;
                            }
                            console.log('[FatFramework] pending requests: '+pendingRequests);
                        });
                    }

                    var isBusy = function() {
                        return pendingRequests > 0;
                    };

                    fat.busyConditions.fetchPendingRequests = {
                        isBusy: isBusy
                    }
                })(window);
                
                return window.fatFramework.busyConditions.fetchPendingRequests.isBusy()
            ";
        }

        public bool IsBusy(IWebBrowser webBrowser)
        {
            var script = this.CreateScript();
            try
            {
                var executor = webBrowser.JavaScriptExecutor;
                var busy = (bool)executor.ExecuteScript(script);
                return busy;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
    }
}
