using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.WebControls
{
    public interface ICheckboxControl : IWebControl
    {
        bool IsChecked { get; }
        void Toggle();
    }
}
