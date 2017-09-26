using System;
using System.Collections.Generic;


namespace Yontech.Fat.WebControls
{
    public interface ISimpleDropDownControl : IWebControl
    {
        void SelectItem(string itemText);
        void Click();
    }

}