using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yontech.Fat.Interceptors;
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
                return new FatRunner(AddInterceptor);
            }
            else
            {
                return new FatRunner(this._options);
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
    }
}