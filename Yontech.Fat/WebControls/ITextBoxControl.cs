using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.WebControls
{
    public interface ITextBoxControl : IWebControl
    {
        string Text { get; }
        void ClearAndTypeKeys(string keys);
        void ClearAndTypeKeysSlowly(string keys, int delayBetweenKeys = 300);
        void TypeKeys(string keys);
        void TypeKeysSlowly(string keys, int delayBetweenKeys = 300);
        void ClearText();

        // todo: make this deprecated
        void SendKeys(string keys);
    }
}
