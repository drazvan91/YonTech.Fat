using System;
using System.Collections.Generic;

namespace Yontech.Fat.WebControls
{
    public interface IGenericControl : IWebControl
    {
        string Text { get; }
        void Click();
        bool WaitForClickable();
        bool WaitForClickable(TimeSpan timeout);
        IControlFinder ControlFinder { get; }
        IEnumerable<IGenericControl> Find(string cssSelector);
        string GetAttribute(string attributeName);
    }
}
