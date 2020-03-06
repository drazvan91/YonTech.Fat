using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat
{
    public enum BrowserType
    {
        Chrome,
        InternetExplorer,
        Firefox,
        Opera
    }

    public enum ChromeVersion : int
    {
        Latest = 0,
        v79 = 79,
        v80 = 80,
    }
}
