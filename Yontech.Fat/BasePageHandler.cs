using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat
{
    public class BasePageHandler
    {
        protected readonly IWebBrowser WebBrowser;
        protected readonly IControlFinder ControlFinder;

        public BasePageHandler(IWebBrowser webBrowser)
        {
            this.WebBrowser = webBrowser;
            this.ControlFinder = webBrowser.ControlFinder;
        }
    }
}
