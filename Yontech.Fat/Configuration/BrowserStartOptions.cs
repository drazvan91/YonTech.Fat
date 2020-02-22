namespace Yontech.Fat.Configuration
{
    internal class BrowserStartOptions
    {
        // Set default start options in constructor.
        public bool RunHeadless { get; set; }
        public bool StartMaximized { get; set; }
        public bool DisablePopupBlocking { get; set; }
        public string DriversFolder { get; set; }
        public bool AutomaticDriverDownload { get; set; }
    }
}
