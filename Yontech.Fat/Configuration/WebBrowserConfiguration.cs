using System.Collections.Generic;

namespace Yontech.Fat.Configuration
{
    public class WebBrowserConfiguration
    {
        public List<FatBusyCondition> BusyConditions { get; private set; }
        public int DefaultTimeout { get; set; }

        public WebBrowserConfiguration()
        {
            DefaultTimeout = 20000;
            BusyConditions = new List<FatBusyCondition>();
        }
    }
}
