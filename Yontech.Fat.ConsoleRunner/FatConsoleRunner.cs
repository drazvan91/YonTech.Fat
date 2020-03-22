using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.ConsoleRunner
{
    public class FatConsoleRunner
    {
        private readonly ConsoleRunnerInterceptor _interceptor;
        private readonly FatConfig _options;

        public FatConsoleRunner(FatConfig options = null)
        {
            this._interceptor = new ConsoleRunnerInterceptor();

            if (options != null)
            {
                this._options = options;
                AddInterceptor(this._options);
            }
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

        private void AddInterceptor(FatConfig config)
        {
            var interceptors = config.Interceptors?.ToList() ?? new List<FatInterceptor>();
            interceptors.Add(_interceptor);
            config.Interceptors = interceptors;
        }

        private FatRunner CreateRunner()
        {
            var assemblyDiscoverer = new AssemblyDiscoverer();
            if (this._options == null)
            {
                var loggerFactory = new ConsoleLoggerFactory();
                return new FatRunner(assemblyDiscoverer, loggerFactory, AddInterceptor);
            }
            else
            {
                var loggerFactory = new ConsoleLoggerFactory(this._options.LogLevel, this._options.LogLevelConfig);
                return new FatRunner(assemblyDiscoverer, loggerFactory, this._options);
            }
        }
    }
}
