using System;
using System.IO;
using Newtonsoft.Json;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

namespace Yontech.Fat.EnvData
{
    internal class EnvDataJsonResolver
    {
        private readonly ILogger _logger;

        public EnvDataJsonResolver(FatExecutionContext execContext)
        {
            this._logger = execContext.LoggerFactory.Create(this);
        }

        public void Resolve(FatEnvData instance)
        {
            string filename = instance.FilePath;
            this.TryFillFromFile(instance, filename, true);

            string overrideFilename = instance.FilePath.Replace(".json", ".override.json");
            this.TryFillFromFile(instance, overrideFilename, false);
        }

        private void TryFillFromFile(object instance, string filename, bool mandatory)
        {
            if (!File.Exists(filename))
            {
                if (mandatory)
                {
                    _logger.Error("File '{0}' could not be found. Empty {1} object will be provided", filename, instance.GetType().FullName);
                    return;
                }
            }

            try
            {
                var fileContent = File.ReadAllText(filename);
                JsonConvert.PopulateObject(fileContent, instance);
                _logger.Info("{0} loaded from file '{1}'", instance.GetType().FullName, filename);
            }
            catch (Exception ex)
            {
                _logger.Warning(ex.Message);
            }
        }
    }
}
