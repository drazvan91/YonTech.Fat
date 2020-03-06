using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

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
                this._options = FatConfig.Clone(options);
                AddInterceptor(this._options);
            }
        }

        public void Run()
        {
            CreateRunner().Run();
        }

        public void Run<T>() where T : FatTest
        {
            CreateRunner().Run<T>();
        }

        public void Run(IEnumerable<Assembly> assemblies)
        {
            CreateRunner().Run(assemblies);
        }

        public void Run(Assembly assembly)
        {
            CreateRunner().Run(assembly);
        }

        private void AddInterceptor(FatConfig config)
        {
            var interceptors = config.Interceptors?.ToList() ?? new List<FatInterceptor>();
            interceptors.Add(_interceptor);
            config.Interceptors = interceptors;
        }

        private FatRunner CreateRunner()
        {
            if (this._options == null)
            {
                // todo: this is a bug because it does not take into consideration any FatConfig file
                var defaultConfig = new FatConfig();
                var loggerFactory = new ConsoleLoggerFactory(defaultConfig.LogLevel, defaultConfig.LogLevelConfig);
                return new FatRunner(loggerFactory, AddInterceptor);
            }
            else
            {
                var loggerFactory = new ConsoleLoggerFactory(this._options.LogLevel, this._options.LogLevelConfig);
                return new FatRunner(loggerFactory, this._options);
            }
        }
    }
}
