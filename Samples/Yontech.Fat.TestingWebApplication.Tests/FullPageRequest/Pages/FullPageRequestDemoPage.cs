using Yontech.Fat.WebControls;

namespace Yontech.Fat.TestingWebApplication.Tests.FullPageRequest.Pages
{ 
    public class FullPageRequestDemoPage: BasePageHandler
    {
        public FullPageRequestDemoPage(IWebBrowser webBrowser) : base(webBrowser)
        {
        }

        public ITextBoxControl FirstNameTextBox
        {
            get
            {
                return ControlFinder.TextBox("[name=firstName]");
            }
        }

        public ITextBoxControl LastNameTextBox
        {
            get
            {
                return ControlFinder.TextBox("[name=lastName]");
            }
        }

        public IButtonControl ConcatenateButton
        {
            get
            {
                return ControlFinder.Button("#concatenateButton");
            }
        }

        public ITextControl ConcatenatedNameResultSection
        {
            get
            {
                return ControlFinder.Text("#concatenationResult");
            }
        }

        public IButtonControl OneSecondRequestButton
        {
            get
            {
                return ControlFinder.Button("#oneSecondButton");
            }
        }

        public IButtonControl FiveSecondsRequestButton
        {
            get
            {
                return ControlFinder.Button("#fiveSecondsButton");
            }
        }

        public IButtonControl TenSecondsRequestButton
        {
            get
            {
                return ControlFinder.Button("#tenSecondsButton");
            }
        }

        public ITextControl InfoMessageSection
        {
            get
            {
                return ControlFinder.Text("#outputMessage");
            }
        }
    }
}
