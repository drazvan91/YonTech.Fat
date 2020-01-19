using System;

namespace Yontech.Fat.BusyConditions
{
  public class PendingRequestsBusyCondition : IBusyCondition
  {
    private string script = @"
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
          this.onload = fatOperations.requestFinished; 
          oldOpen.apply(this, arguments); 
        };
      }; 

      initPendingRequestsBusy();

      return window.fatData.pendingRequests;
    ";
    public bool IsBusy(IWebBrowser webBrowser)
    {
      try
      {
        var pendingRequests = (Int64)webBrowser.JavaScriptExecutor.ExecuteScript(this.script);
        // Console.WriteLine("Pending: " + pendingRequests);
        return pendingRequests > 0;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }
    }
  }
}
