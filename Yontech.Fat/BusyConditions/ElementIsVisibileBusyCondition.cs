using System;
using System.Collections.Generic;
using System.Text;

namespace Yontech.Fat.BusyConditions
{
    public class ElementIsVisibileBusyCondition : IBusyCondition
    {
        private readonly string _elementCssSelector;

        public ElementIsVisibileBusyCondition(string elementCssSelector)
        {
            this._elementCssSelector = elementCssSelector;
        }

        public bool IsBusy(IWebBrowser webBrowser)
        {
            try
            {
                var element = webBrowser.ControlFinder.Generic(this._elementCssSelector);
                return element.IsVisible;
            }
            catch(Exception ex) // sometimes is throws element not attached if IsDisplayed is called right after element becomes hidden
            {
                return true;
            }
        }
    }
}
