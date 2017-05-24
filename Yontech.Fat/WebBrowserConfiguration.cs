using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat
{
    public class WebBrowserConfiguration
    {
        public List<IBusyCondition> BusyConditions { get; private set; }
        public int DefaultTimeout { get; set; }

        public WebBrowserConfiguration()
        {
            DefaultTimeout = 20000;
            BusyConditions = new List<IBusyCondition>();
        }
    }
}
