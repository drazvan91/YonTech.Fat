using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yontech.Fat.Filters;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Discoverer
{
    public class FatDiscoverer
    {
        private readonly IAssemblyDiscoverer _assemblyDiscoverer;
        private readonly ILogger _logger;

        public FatDiscoverer(FatExecutionContext executionContext)
        {
            this._assemblyDiscoverer = executionContext.AssemblyDiscoverer;
            this._logger = executionContext.LoggerFactory.Create(this);
        }

        public IEnumerable<FatTestCollection> DiscoverTestCollections(ITestCaseFilter filter = null)
        {
            var assemblies = this._assemblyDiscoverer.DiscoverAssemblies();
            return DiscoverTestCollections(assemblies, filter);
        }

        public IEnumerable<FatTestCollection> DiscoverTestCollections(IEnumerable<Assembly> assemblies, ITestCaseFilter filter = null)
        {
            foreach (var assembly in assemblies)
            {
                var testCollection = DiscoverTestCollection(assembly, filter);
                if (testCollection != null)
                {
                    yield return testCollection;
                }
            }
        }

        public FatConfig DiscoverConfig()
        {
            var configTypes = this._assemblyDiscoverer.DiscoverAssemblies()
             .SelectMany(a => FindFatConfigs(a))
             .OrderBy(c => c.FullName.Length).ToList();

            if (configTypes.Count == 0)
            {
                _logger.Debug("No FatConfig has been found");
                return null;
            }

            var configType = configTypes.First();

            if (configTypes.Count > 1)
            {
                _logger.Warning("Multiple FatConfig files have been found. The one with the shortest name has been chosen: {0}", configType.FullName);
            }

            var config = Activator.CreateInstance(configType) as FatConfig;
            return config;
        }

        public FatTestCollection DiscoverTestCollection<TFatTest>(ITestCaseFilter filter = null) where TFatTest : FatTest
        {
            var testCases = DiscoverTestCases(typeof(TFatTest), filter).ToList();
            if (testCases.Any())
            {
                var testClasses = new List<FatTestClass>()
                {
                    new FatTestClass(typeof(TFatTest))
                    {
                        TestCases = testCases,
                    },
                };

                return new FatTestCollection(typeof(TFatTest).Assembly)
                {
                    TestClasses = testClasses,
                };
            }

            return null;
        }

        public FatTestCollection DiscoverTestCollection(Assembly assembly, ITestCaseFilter filter = null)
        {
            var testClasses = DiscoverTestClasses(assembly, filter).ToList();
            if (testClasses.Any())
            {
                return new FatTestCollection(assembly)
                {
                    TestClasses = testClasses,
                };
            }

            return null;
        }

        public IEnumerable<FatTestClass> DiscoverTestClasses(Assembly assembly, ITestCaseFilter filter = null)
        {
            var allTestClasses = this.FindTestClasses(assembly);

            foreach (var type in allTestClasses)
            {
                var testCases = DiscoverTestCases(type, filter).ToList();
                if (testCases.Any())
                {
                    yield return new FatTestClass(type)
                    {
                        TestCases = testCases,
                    };
                }
            }
        }

        public IEnumerable<FatTestCase> DiscoverTestCases(Assembly assembly)
        {
            var allTestClasses = this.FindTestClasses(assembly);
            return allTestClasses.SelectMany(testClass => DiscoverTestCases(testClass));
        }

        public IEnumerable<Type> FindTestClasses(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            return allTypes.Where(type =>
            {
                return type.IsSubclassOf(typeof(FatTest)) && type.IsAbstract == false;
            });
        }

        public IEnumerable<Type> FindPages(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            return allTypes.Where(type => type.IsSubclassOf(typeof(FatPage)));
        }

        public IEnumerable<Type> FindPageSections(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            return allTypes.Where(type => type.IsSubclassOf(typeof(FatPageSection)));
        }

        public IEnumerable<Type> FindFatFlows(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            return allTypes.Where(type => type.IsSubclassOf(typeof(FatFlow)));
        }

        public IEnumerable<Type> FindFatEnvDatas(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            return allTypes.Where(type => type.IsSubclassOf(typeof(FatEnvData)));
        }

        public IEnumerable<Type> FindFatConfigs(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            return allTypes.Where(type => type.IsSubclassOf(typeof(FatConfig)) && type != typeof(DefaultFatConfig));
        }

        private IEnumerable<FatTestCase> DiscoverTestCases(Type testClass, ITestCaseFilter filter = null)
        {
            var allMethods = testClass.GetMethods();
            var testCases = allMethods.Where(method => method.Name.StartsWith("Test")); // todo: make this configurable

            foreach (var method in testCases)
            {
                var testCase = new FatTestCase(method);
                if (filter?.ShouldExecuteTestCase(testCase) ?? true)
                {
                    yield return testCase;
                }
            }
        }
    }
}
