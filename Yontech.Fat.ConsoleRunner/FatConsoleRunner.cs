using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yontech.Fat.Configuration;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.ConsoleRunner
{
    public class FatConsoleRunner
    {
        private readonly ConsoleRunnerInterceptor _interceptor;
        private readonly FatConfig _config;

        public FatConsoleRunner(FatConfig config = null)
        {
            this._interceptor = new ConsoleRunnerInterceptor();
            this._config = config;
        }

        public RunResults Run()
        {
            return CreateRunner().Run();
        }

        public RunResults Run<T>() where T : FatTest
        {
            return CreateRunner().Run<T>();
        }

        public RunResults Run(IEnumerable<Assembly> assemblies)
        {
            return CreateRunner().Run(assemblies);
        }

        public RunResults Run(Assembly assembly)
        {
            return CreateRunner().Run(assembly);
        }

        private FatRunner CreateRunner()
        {
            var execContext = new FatExecutionContext();
            execContext.AssemblyDiscoverer = new AssemblyDiscoverer();
            execContext.LoggerFactory = new ConsoleLoggerFactory();
            execContext.StreamReaderProvider = new StreamProvider(execContext.LoggerFactory);
            return new FatRunner(execContext, (config) =>
            {
                var interceptors = config.Interceptors?.ToList() ?? new List<FatInterceptor>();
                interceptors.Add(_interceptor);
                config.Interceptors = interceptors;

                execContext.LoggerFactory.LogLevel = config.LogLevel;
                execContext.LoggerFactory.LogLevelConfig = config.LogLevelConfig;
            });
        }
    }
}
