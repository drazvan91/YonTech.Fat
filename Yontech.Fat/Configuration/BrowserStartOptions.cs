using System.Drawing;

namespace Yontech.Fat.Configuration
{
    public class BrowserStartOptions
    {
        // Set default start options in constructor.
        public bool RunHeadless { get; set; }
        public bool StartMaximized { get; set; }
        public bool DisablePopupBlocking { get; set; }
        public Size ModifyScreenResolution { get; set; }
    }
}
