using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.WebControls
{
    public interface IButtonControl : IWebControl
    {
        string Text { get; }
        bool IsDisplayed { get; }
        void Click();
        
    }
}
