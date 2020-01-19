using System;
using System.Collections.Generic;
using System.Text;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Selenium.WebControls
{
    public class IFrameControl : IIFrameControl
    {
        internal readonly SeleniumWebBrowser WebBrowser;

        internal IFrameControl(SeleniumWebBrowser webBrowser)
        {
            WebBrowser = webBrowser;
        }
        public void SwitchToIFrame(string iframeId)
        {
            WebBrowser.SwitchToIframe(iframeId);
        }
    }
}
