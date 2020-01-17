using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.WebControls
{
  public interface ILinkControl : IWebControl
  {
    void Click();
    void Click(int relativeX, int relativeY);
    string Text { get; }
    void ShouldHaveText(string text);
    void ShouldContainText(string text);
    void ShouldNotContainText(string text);
  }
}
