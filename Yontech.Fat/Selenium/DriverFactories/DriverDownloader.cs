﻿using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using Yontech.Fat.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Selenium.DriverFactories
{
    internal abstract class DriverDownloader
    {
        private readonly ILogger _logger;

        public DriverDownloader(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.Create(this);
        }

        public async Task Download(string destinationFolder)
        {
            string url = this.GetDownloadUrl();
            await DownloadAndUnzip(url, destinationFolder);
        }

        protected abstract string GetDownloadUrl();

        protected abstract string GetUnzipFilename();

        private async Task DownloadAndUnzip(string url, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            string downloadFile = $"download_{DateTime.Now.Ticks}.";
            downloadFile += url.EndsWith("tar.gz") ? "tar.gz" : "zip";

            string tempFilename = Path.Combine(destination, downloadFile);

            var httpClient = new HttpClient();
            using (var downloadStream = await httpClient.GetStreamAsync(url))
            {
                using (var writer = new FileStream(tempFilename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int len = 0;
                    long megabytes = 0;
                    _logger.Info("Downloading driver: {0}mb", megabytes);

                    DateTime lastPrint = DateTime.Now;
                    while ((len = downloadStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        megabytes += len;
                        if (DateTime.Now.Subtract(lastPrint).TotalMilliseconds > 1000)
                        {
                            lastPrint = DateTime.Now;
                            _logger.Info("Downloading driver: {0}mb", megabytes / (1024 * 1024));
                        }

                        writer.Write(buffer, 0, len);
                    }

                    _logger.Info("Downloading driver finished");

                    writer.Flush();
                }
            }

            if (url.EndsWith(".zip"))
            {
                ZipFile.ExtractToDirectory(tempFilename, destination.TrimEnd('/') + "/");
            }
            else
            {
                TarGzUnzip.ExtractFileFromTaz(tempFilename, destination.TrimEnd('/') + "/");
            }

            if (File.Exists(tempFilename))
            {
                // the file might be already deleted by the unzipper
                File.Delete(tempFilename);
            }

            string unzippedFile = Path.Combine(destination, this.GetUnzipFilename());
            FileSecurityUtil.SetExecutionRight(unzippedFile);
        }
    }
}
