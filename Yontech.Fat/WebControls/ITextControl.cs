using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.WebControls
{
    public interface ITextControl:IWebControl
    {
        string Text { get; }

        void ShouldContainText(string text);
        void ShouldNotContainText(string text);
    }
}
