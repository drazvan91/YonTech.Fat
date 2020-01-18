using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.WebControls
{
  public interface ITextBoxControl : IWebControl
  {
    string Text { get; }
    string Value { get; }
    void TypeKeys(string keys);
    void TypeKeysSlowly(string keys, int delayBetweenKeys = 300);
    void ClearText();
    void Click();
  }
}
