using System;
using System.Runtime.InteropServices;
using Mono.Unix;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal static class FileSecurityUtil
    {
        public static void SetExecutionRight(string filePath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                SetForUnix(filePath);
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SetForWindows(filePath);
                return;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                SetForUnix(filePath);
                return;
            }

            throw new NotSupportedException("Cannot download driver because Operating System could not be detected");
        }

        private static void SetForWindows(string filePath)
        {

        }

        private static void SetForUnix(string filePath)
        {
            var unixFileInfo = new Mono.Unix.UnixFileInfo(filePath);
            unixFileInfo.FileAccessPermissions =
                 FileAccessPermissions.UserReadWriteExecute
                | FileAccessPermissions.GroupRead
                | FileAccessPermissions.OtherRead;
        }

    }
}