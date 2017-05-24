using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.WebControls
{
    public interface IGenericControl : IWebControl
    {
        string Text { get; }

        void Click();
        IEnumerable<IGenericControl> Find(string cssSelector);
        string GetAttribute(string attributeName);
    }
}
