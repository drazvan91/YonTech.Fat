using System;
using System.Runtime.InteropServices;
using Yontech.Fat.Logging;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal class FirefoxDriverDownloader : DriverDownloader
    {
        private const string MACOS64 = "https://github.com/mozilla/geckodriver/releases/download/v0.26.0/geckodriver-v0.26.0-macos.tar.gz";
        private const string WIN64 = "https://github.com/mozilla/geckodriver/releases/download/v0.26.0/geckodriver-v0.26.0-win64.zip";
        private const string LINUX64 = "https://github.com/mozilla/geckodriver/releases/download/v0.26.0/geckodriver-v0.26.0-linux64.tar.gz";
        private readonly FirefoxVersion _firefoxVersion;

        public FirefoxDriverDownloader(ILoggerFactory loggerFactory, FirefoxVersion version)
            : base(loggerFactory)
        {
            this._firefoxVersion = version;
        }

        protected override string GetDownloadUrl()
        {
            // we dont care yet about the FirefoxVersion
            return GetOsZipName();
        }

        protected override string GetUnzipFilename()
        {
            return "geckodriver";
        }

        private string GetOsZipName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return MACOS64;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return WIN64;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return LINUX64;
            }

            throw new NotSupportedException("Cannot download driver because Operating System could not be detected");
        }
    }
}
