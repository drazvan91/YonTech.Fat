using System;

namespace Yontech.Fat.BusyConditions
{
    public class ElementIsVisibileBusyCondition : FatBusyCondition
    {
        private readonly string _elementCssSelector;

        public ElementIsVisibileBusyCondition(string elementCssSelector)
        {
            this._elementCssSelector = elementCssSelector;
        }

        protected internal override bool IsBusy()
        {
            try
            {
                var element = WebBrowser.ControlFinder.Generic(this._elementCssSelector);

                if (element.IsVisible == false)
                {
                    LogDebug("Element with selector '{0}' is not visible yet");
                }

                return element.IsVisible;
            }
            catch (Exception ex) // sometimes is throws element not attached if IsDisplayed is called right after element becomes hidden
            {
                LogDebug("Element with selector '{0}' threw exception {1}", ex.Message);
                return true;
            }
        }
    }
}
