using System;
using System.IO;
using System.Reflection;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Logging;

namespace Yontech.Fat.Utils
{
    public class StreamProvider : IStreamProvider
    {
        private readonly ILogger _logger;

        public StreamProvider(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.Create(this);
        }

        public Stream GetStream(string filename, Assembly relativeToAssembly)
        {
            var location = GetFileLocation(filename, relativeToAssembly);
            _logger.Debug("Reading inline parameters from file {0}", location);
            if (!File.Exists(location))
            {
                throw new FatException("File '{0}' could not be found. Is the this file copied to the output folder? Make sure you added <Content Include=\"files\\**\\*\" CopyToOutputDirectory=\"Always\" /> in the .csproj file");
            }

            return new FileStream(location, FileMode.Open);
        }

        public TextReader GetTextReader(string filename, Assembly relativeToAssembly)
        {
            var location = GetFileLocation(filename, relativeToAssembly);
            _logger.Debug("Reading inline parameters from file {0}", location);

            if (!File.Exists(location))
            {
                throw new FatException("File '{0}' could not be found. Is the this file copied to the output folder? Make sure you added <Content Include=\"files\\**\\*\" CopyToOutputDirectory=\"Always\" /> in the .csproj file", filename);
            }

            return new StreamReader(location);
        }

        public string GetFileLocation(string filename, Assembly relativeToAssembly)
        {
            var folderLocation = Path.GetDirectoryName(relativeToAssembly.Location);
            var location = Path.Combine(folderLocation, filename);
            return location;
        }
    }
}
