using System;

namespace Yontech.Fat.BusyConditions
{
    public class ElementExistsBusyCondition : FatBusyCondition
    {
        private readonly string _elementCssSelector;

        public ElementExistsBusyCondition(string elementCssSelector)
        {
            this._elementCssSelector = elementCssSelector;
        }

        protected internal override bool IsBusy()
        {
            try
            {
                var element = WebBrowser.ControlFinder.Generic(this._elementCssSelector);

                if (element.Exists == false)
                {
                    LogDebug("Element with selector '{0}' does not exist yet");
                }

                return element.Exists;
            }
            catch (Exception ex) // sometimes is throws element not attached if IsDisplayed is called right after element becomes hidden
            {
                LogDebug("Element with selector '{0}' threw exception {1}", ex.Message);
                return true;
            }
        }
    }
}
