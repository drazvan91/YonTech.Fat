using System;
using System.Collections.Generic;

namespace Yontech.Fat.WebControls
{
    public interface IGenericControl : IWebControl
    {
        string Text { get; }
        IControlFinder ControlFinder { get; }
        IEnumerable<IGenericControl> Find(string cssSelector);
    }
}
