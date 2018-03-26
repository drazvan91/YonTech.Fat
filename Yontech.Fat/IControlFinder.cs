using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public interface IControlFinder
    {
        ITextBoxControl TextBox(string cssSelector);
        IButtonControl Button(string cssSelector);
        ITextControl Text(string cssSelector);
        IRadioButtonControl RadioButton(string cssSelector);
        IClassicDropdownControl ClasicDropdown(string cssSelector);
        IGenericControl Generic(string cssSelector);
    }
}
