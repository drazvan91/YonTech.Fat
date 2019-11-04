using System.Collections.Generic;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public interface IControlFinder
    {
        ITextBoxControl TextBox(string cssSelector);
        IButtonControl Button(string cssSelector);
        ITextControl Text(string cssSelector);
        IRadioButtonControl RadioButton(string cssSelector);
        ICheckboxControl Checkbox(string cssSelector);
        IGenericControl Generic(string cssSelector);
        IDropdownControl Dropdown(string cssSelector);
        IList<ITextControl> TextList(string cssSelector);
        IList<ITextBoxControl> TextBoxList(string cssSelector);
        IList<IButtonControl> ButtonList(string cssSelector);
    }
}
