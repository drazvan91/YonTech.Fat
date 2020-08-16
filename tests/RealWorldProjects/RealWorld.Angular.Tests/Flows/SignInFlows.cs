using System;
using RealWorld.Angular.Tests.Data;
using RealWorld.Angular.Tests.Pages;
using RealWorld.Angular.Tests.PageSections;
using Yontech.Fat;

namespace RealWorld.Angular.Tests.Flows
{
    public class SignInFlows : FatFlow
    {
        SignInPage signInPage { get; set; }
        HeaderSection headerSection { get; set; }
        SettingsPage settingsPage { get; set; }

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

        public void SignOutIfLoggedIn()
        {
            if (headerSection.SignInLink.IsVisible)
            {
                return;
            }

            headerSection.SettingsLink.Click();
            settingsPage.LogoutButton.Click();
        }
    }
}
