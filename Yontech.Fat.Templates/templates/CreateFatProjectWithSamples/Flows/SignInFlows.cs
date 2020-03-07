using CreateFatProjectWithSamples.Data;
using CreateFatProjectWithSamples.Pages;
using Yontech.Fat;

namespace CreateFatProjectWithSamples.Flows
{
    public class SignInFlows : FatFlow
    {
        SignInPage signInPage { get; set; }

        public void Login(UserData user)
        {
            this.Login(user.Email, user.Password);
        }

        public void Login(string username, string password)
        {
            signInPage.EmailTextInput.ClearText();
            signInPage.EmailTextInput.TypeKeys(username);

            signInPage.PasswordTextInput.ClearText();
            signInPage.PasswordTextInput.TypeKeys(password);

            signInPage.SignInButton.Click();
        }
    }
}
