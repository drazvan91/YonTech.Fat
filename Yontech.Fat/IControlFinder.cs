using Yontech.Fat.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat
{
    public interface IControlFinder
    {
        ITextBoxControl TextBox(string cssSelector);
        IButtonControl Button(string cssSelector);
        ITextControl Text(string cssSelector);
        IGenericControl Generic(string cssSelector);
    }
}
