﻿using System.Drawing;

namespace Yontech.Fat.Configuration
{
    internal class BrowserStartOptions
    {
        public bool RunHeadless { get; set; }
        public bool StartMaximized { get; set; }
        public Size InitialSize { get; set; }
        public bool DisablePopupBlocking { get; set; }
        public string DriversFolder { get; set; }
        public bool AutomaticDriverDownload { get; set; }
        public string RemoteDebuggerAddress { get; set; }
        public ChromeVersion ChromeVersion { get; set; }
    }
}
