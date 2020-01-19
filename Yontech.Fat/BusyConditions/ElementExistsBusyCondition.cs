using System;
using System.Collections.Generic;
using System.Text;

namespace Yontech.Fat.BusyConditions
{
  public class ElementExistsBusyCondition : IBusyCondition
  {
    private readonly string _elementCssSelector;

    public ElementExistsBusyCondition(string elementCssSelector)
    {
      this._elementCssSelector = elementCssSelector;
      Console.WriteLine(this._elementCssSelector);
    }

    public bool IsBusy(IWebBrowser webBrowser)
    {
      try
      {
        var element = webBrowser.ControlFinder.Generic(this._elementCssSelector);
        return element.Exists;
      }
      catch (Exception) // sometimes is throws element not attached if IsDisplayed is called right after element becomes hidden
      {
        Console.WriteLine(this._elementCssSelector);
        return true;
      }
    }
  }
}
