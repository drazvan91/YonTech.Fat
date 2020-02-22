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
        private readonly FatRunnerOptions _options;

        public FatConsoleRunner(FatRunnerOptions options)
        {
            this._interceptor = new ConsoleRunnerInterceptor();
            this._options = FatRunnerOptions.Clone(options);

            var interceptors = this._options.Interceptors?.ToList() ?? new List<FatInterceptor>();
            interceptors.Add(_interceptor);
            this._options.Interceptors = interceptors;
        }

        public void Run()
        {
            FatRunner runner = new FatRunner(this._options);
            runner.Run();
        }

        public void Run<T>() where T : FatTest
        {
            FatRunner runner = new FatRunner(this._options);
            runner.Run<T>();
        }

        public void Run(IEnumerable<Assembly> assemblies)
        {
            FatRunner runner = new FatRunner(this._options);
            runner.Run(assemblies);
        }

        public void Run(Assembly assembly)
        {
            FatRunner runner = new FatRunner(this._options);
            runner.Run(assembly);
        }
    }
}