using Yontech.Fat.WebControls;

namespace Yontech.Fat
{
    public class BasePageHandler
    {
        protected readonly IWebBrowser WebBrowser;
        protected readonly IControlFinder ControlFinder;
        protected readonly IIFrameControl IFrame;        

        public BasePageHandler(IWebBrowser webBrowser)
        {
            this.WebBrowser = webBrowser;
            this.ControlFinder = webBrowser.ControlFinder;
            this.IFrame = webBrowser.IFrameControl;
        }
    }
}
