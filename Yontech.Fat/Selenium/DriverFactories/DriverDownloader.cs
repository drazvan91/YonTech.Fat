using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal abstract class DriverDownloader
    {
        public async Task Download(string destinationFolder)
        {
            string url = this.GetDownloadUrl();
            await DownloadAndUnzip(url, destinationFolder);
        }

        private async Task DownloadAndUnzip(string url, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            var httpClient = new HttpClient();
            var downloadStream = await httpClient.GetStreamAsync(url);

            string tempFilename = Path.Combine(destination, $"download_{DateTime.Now.Ticks}.zip");
            var writer = new FileStream(tempFilename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

            byte[] buffer = new byte[1024 * 1024];
            int len = 0;
            long megabytes = 0;
            Console.WriteLine("Downloading driver: {0}mb", megabytes);

            DateTime lastPrint = DateTime.Now;
            while ((len = downloadStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                megabytes += len;
                if (DateTime.Now.Subtract(lastPrint).TotalMilliseconds > 1000)
                {
                    lastPrint = DateTime.Now;
                    Console.WriteLine("Downloading driver: {0}mb", megabytes / (1024 * 1024));
                }

                writer.Write(buffer, 0, len);
            }
            Console.WriteLine("Downloading driver finished");

            writer.Flush();

            ZipFile.ExtractToDirectory(tempFilename, destination.TrimEnd('/') + "/");
            File.Delete(tempFilename);

            string unzippedFile = Path.Combine(destination, $"chromedriver");
            FileSecurityUtil.SetExecutionRight(unzippedFile);
        }

        protected abstract string GetDownloadUrl();
    }
}