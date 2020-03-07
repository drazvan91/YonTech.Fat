using System.Collections.Generic;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public interface IControlFactory
    {
        ITextBoxControl TextBox(IGenericControl genericControl);
        IButtonControl Button(IGenericControl genericControl);
        ILinkControl Link(IGenericControl genericControl);
        ITextControl Text(IGenericControl genericControl);
        IRadioButtonControl RadioButton(IGenericControl genericControl);
        ICheckboxControl Checkbox(IGenericControl genericControl);
        IDropdownControl Dropdown(IGenericControl genericControl);
    }
}
