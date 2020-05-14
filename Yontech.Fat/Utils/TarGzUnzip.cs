using System.Diagnostics;
using System.IO;

namespace Yontech.Fat.Utils
{
    public static class TarGzUnzip
    {
        public static void ExtractFileFromTaz(string tazFilePath, string destination)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "tar",
                    Arguments = "-xvf " + tazFilePath + " -C " + destination,
                    RedirectStandardOutput = false,
                    UseShellExecute = true,
                    CreateNoWindow = false,
                },
            };

            process.Start();

            // string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
    }
}