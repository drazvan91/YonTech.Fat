using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.WebControls
{
    public interface IButtonControl : IWebControl
    {
        string Text { get; }
        void Click();
        
    }
}
