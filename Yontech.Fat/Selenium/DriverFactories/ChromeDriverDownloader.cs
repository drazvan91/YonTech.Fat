using System;
using System.Runtime.InteropServices;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal class ChromeDriverDownloader : DriverDownloader
    {
        private const string CHROME_MACOS64 = "https://chromedriver.storage.googleapis.com/79.0.3945.36/chromedriver_mac64.zip";
        private const string CHROME_WIN32 = "https://chromedriver.storage.googleapis.com/80.0.3987.16/chromedriver_win32.zip";
        private const string CHROME_LINUX64 = "https://chromedriver.storage.googleapis.com/80.0.3987.16/chromedriver_linux64.zip";

        protected override string GetDownloadUrl()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return CHROME_MACOS64;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return CHROME_WIN32;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return CHROME_LINUX64;
            }

            throw new NotSupportedException("Cannot download driver because Operating System could not be detected");
        }
    }
}