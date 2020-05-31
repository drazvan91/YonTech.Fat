using System;
using System.Runtime.InteropServices;
using Yontech.Fat.Logging;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal class ChromeDriverDownloader : DriverDownloader
    {
        private const string CHROME_74_FOLDER = "http://chromedriver.storage.googleapis.com/74.0.3729.6/";
        private const string CHROME_79_FOLDER = "http://chromedriver.storage.googleapis.com/79.0.3945.16/";
        private const string CHROME_80_FOLDER = "http://chromedriver.storage.googleapis.com/80.0.3987.16/";
        private const string CHROME_81_FOLDER = "https://chromedriver.storage.googleapis.com/81.0.4044.138/";
        private const string CHROME_83_FOLDER = "https://chromedriver.storage.googleapis.com/83.0.4103.39/";
        private const string CHROME_84_FOLDER = "https://chromedriver.storage.googleapis.com/84.0.4147.30/";
        private const string MACOS64 = "chromedriver_mac64.zip";
        private const string WIN32 = "chromedriver_win32.zip";
        private const string LINUX64 = "chromedriver_linux64.zip";
        private readonly ChromeVersion _chromeVersion;

        public ChromeDriverDownloader(ILoggerFactory loggerFactory, ChromeVersion version)
            : base(loggerFactory)
        {
            this._chromeVersion = version;
        }

        protected override string GetDownloadUrl()
        {
            return GetFolderName() + GetOsZipName();
        }

        protected override string GetUnzipFilename()
        {
            return "chromedriver";
        }

        private string GetFolderName()
        {
            switch (this._chromeVersion)
            {
                case ChromeVersion.v74: return CHROME_74_FOLDER;
                case ChromeVersion.v79: return CHROME_79_FOLDER;
                case ChromeVersion.v80: return CHROME_80_FOLDER;
                case ChromeVersion.v81: return CHROME_81_FOLDER;
                case ChromeVersion.v83: return CHROME_83_FOLDER;
                case ChromeVersion.v84: return CHROME_84_FOLDER;
                default: return CHROME_84_FOLDER;
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
