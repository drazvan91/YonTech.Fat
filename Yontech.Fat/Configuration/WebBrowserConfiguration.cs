using System.Collections.Generic;

namespace Yontech.Fat.Configuration
{
    public class WebBrowserConfiguration
    {
        private int _defaultTimeout = 0;

        public List<FatBusyCondition> BusyConditions { get; private set; }
        public int DefaultTimeout
        {
            get
            {
                return _defaultTimeout;
            }
            set
            {
                _defaultTimeout = value;
                this.WebBrowser.DefaultTimeout = value;
            }
        }

        public int FinderTimeout { get; set; }
        internal BaseWebBrowser WebBrowser { get; private set; }

        internal WebBrowserConfiguration(BaseWebBrowser webBrowser)
        {
            WebBrowser = webBrowser;
            BusyConditions = new List<FatBusyCondition>();
        }
    }
}
