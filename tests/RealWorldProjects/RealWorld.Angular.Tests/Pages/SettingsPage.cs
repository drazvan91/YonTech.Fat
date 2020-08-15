using RealWorld.Angular.Tests.PageSections;
using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace RealWorld.Angular.Tests.Pages
{
    public class SettingsPage : FatPage
    {
        public ITextBoxControl ImageUrlTextBox => _.TextBox("[formcontrolname=\"image\"]");
    }
}
