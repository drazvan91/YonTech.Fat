using CreateFatProjectWithSamples.PageSections;
using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace CreateFatProjectWithSamples.Pages
{
    public class SignInPage : FatPage
    {
        public ITextControl ErrorMessage => _.Text("app-list-errors .error-messages");
        public ITextControl Title => _.Text(".auth-page h1.text-xs-center");
        public ITextBoxControl EmailTextInput => _.TextBox(".auth-page input[formcontrolname=\"email\"]");
        public ITextBoxControl PasswordTextInput => _.TextBox(".auth-page input[formcontrolname=\"password\"]");
        public IButtonControl SignInButton => _.Button(".auth-page form button");
    }
}
