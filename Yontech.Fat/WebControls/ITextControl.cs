using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.WebControls
{
  public interface ITextControl : IWebControl
  {
    void Click();
    void Click(int relativeX, int relativeY);
    string Text { get; }
    void InnerTextShouldBe(string text);
    void ShouldContainText(string text);
    void ShouldNotContainText(string text);
    void SendKeys(string keys);
    void ClearText();
  }
}
