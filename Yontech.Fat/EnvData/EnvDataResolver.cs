using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Yontech.Fat.Logging;

namespace Yontech.Fat.EnvData
{
    internal class EnvDataResolver
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public EnvDataResolver(ILoggerFactory loggerFactory)
        {
            this._loggerFactory = loggerFactory;
            this._logger = loggerFactory.Create(this);
        }

        public object Resolve(Type envDataType)
        {
            var instance = Activator.CreateInstance(envDataType) as FatEnvData;
            if (instance.FilePath.EndsWith(".json"))
            {
                EnvDataJsonResolver r = new EnvDataJsonResolver(this._loggerFactory);
                r.Resolve(instance);
            }
            else if (instance.FilePath.EndsWith(".txt"))
            {
                EnvDataTextResolver r = new EnvDataTextResolver(this._loggerFactory);
                r.Resolve(instance);
            }
            else
            {
                this._logger.Error("Unknown type of EnvData file. Only .txt and .json files are being supported");
            }

            return instance;
        }
    }
}
