using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat
{
    public interface IJsExecutor
    {
        object ExecuteScript(string script);
        void ScrollToControl(IWebControl controlToScroll);
    }
}
