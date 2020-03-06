using System.Collections.Generic;
using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public interface IControlFinder
    {
        ITextBoxControl TextBox(string cssSelector);
        IButtonControl Button(string cssSelector);
        ILinkControl Link(string cssSelector);
        ITextControl Text(string cssSelector);
        IRadioButtonControl RadioButton(string cssSelector);
        ICheckboxControl Checkbox(string cssSelector);
        IGenericControl Generic(string cssSelector);
        TComponent Custom<TComponent>(string cssSelector) where TComponent : FatCustomComponent, new();
        IDropdownControl Dropdown(string cssSelector);

        IEnumerable<ITextControl> TextList(string cssSelector);
        IEnumerable<ITextBoxControl> TextBoxList(string cssSelector);
        IEnumerable<IButtonControl> ButtonList(string cssSelector);
        IEnumerable<ILinkControl> LinkList(string cssSelector);
        IEnumerable<TComponent> CustomList<TComponent>(string cssSelector) where TComponent : FatCustomComponent, new();

        IButtonControl ButtonByXPath(string xPathSelector);
        IGenericControl GenericByXPath(string xPathSelector);
        ITextControl TextByXPath(string xPathSelector);
        ITextBoxControl TextBoxByXPath(string xPathSelector);
    }

}


