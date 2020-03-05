using System;
using System.Runtime.InteropServices;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal class ChromeDriverDownloader : DriverDownloader
    {
        private const string CHROME_79_FOLDER = "http://chromedriver.storage.googleapis.com/80.0.3987.16/";
        private const string CHROME_80_FOLDER = "http://chromedriver.storage.googleapis.com/80.0.3987.16/";
        private const string MACOS64 = "chromedriver_mac64.zip";
        private const string WIN32 = "chromedriver_win32.zip";
        private const string LINUX64 = "chromedriver_linux64.zip";
        private readonly ChromeVersion _chromeVersion;

        public ChromeDriverDownloader(ChromeVersion version)
        {
            this._chromeVersion = version;
        }

        protected override string GetDownloadUrl()
        {
            return GetFolderName() + GetOsZipName();
        }

        private string GetFolderName()
        {
            switch (this._chromeVersion)
            {
                case ChromeVersion.v79: return CHROME_79_FOLDER;
                case ChromeVersion.v80: return CHROME_80_FOLDER;
                default: return CHROME_80_FOLDER;
            }
        }

        private string GetOsZipName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return MACOS64;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return WIN32;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return LINUX64;
            }

            throw new NotSupportedException("Cannot download driver because Operating System could not be detected");
        }
    }
}
