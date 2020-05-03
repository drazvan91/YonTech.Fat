using System;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

namespace Yontech.Fat.EnvData
{
    internal class EnvDataResolver
    {
        private readonly FatExecutionContext _execContext;
        private readonly ILogger _logger;

        public EnvDataResolver(FatExecutionContext execContext)
        {
            this._execContext = execContext;
            this._logger = this._execContext.LoggerFactory.Create(this);
        }

        public object Resolve(Type envDataType)
        {
            var instance = Activator.CreateInstance(envDataType) as FatEnvData;
            if (instance.FilePath.EndsWith(".json"))
            {
                EnvDataJsonResolver r = new EnvDataJsonResolver(this._execContext);
                r.Resolve(instance);
            }
            else if (instance.FilePath.EndsWith(".txt"))
            {
                EnvDataTextResolver r = new EnvDataTextResolver(this._execContext);
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
