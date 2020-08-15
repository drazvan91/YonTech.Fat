using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace RealWorld.Angular.Tests.Pages
{
    public class SettingsPage : FatPage
    {
        public ITextBoxControl ImageUrlTextBox => _.TextBox("[formcontrolname=\"image\"]");

        public IButtonControl LogoutButton => _.Button(".btn.btn-outline-danger");
    }
}
