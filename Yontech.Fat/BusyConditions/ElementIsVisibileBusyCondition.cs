using System;
using System.Collections.Generic;
using System.Text;

namespace Yontech.Fat.BusyConditions
{
    public class ElementIsVisibileBusyCondition : IBusyCondition
    {
        private readonly IControlFinder _controlFinder;
        private readonly string _elementCssSelector;

        public ElementIsVisibileBusyCondition(IWebBrowser webBrowser, string elementCssSelector)
        {
            this._controlFinder = webBrowser.ControlFinder;
            this._elementCssSelector = elementCssSelector;
        }

        public bool IsBusy()
        {
            try
            {
                var element = _controlFinder.Generic(this._elementCssSelector);
                return element.IsVisible;
            }
            catch(Exception ex) // sometimes is throws element not attached if IsDisplayed is called right after element becomes hidden
            {
                return true;
            }
        }
    }
}
