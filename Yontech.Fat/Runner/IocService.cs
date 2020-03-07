using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Runner
{
    internal class IocService
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly FatDiscoverer _discoverer;
        private readonly LogsSink _logsSink;
        private readonly ILoggerFactory _loggerFactory;

        private readonly Func<IWebBrowser> _webBrowserProvider;

        public IocService(FatDiscoverer discoverer, ILoggerFactory loggerFactory, LogsSink logsSink, Func<IWebBrowser> webBrowserProvider)
        {
            this._discoverer = discoverer;
            this._loggerFactory = loggerFactory;
            this._logsSink = logsSink;
            this._webBrowserProvider = webBrowserProvider;

            var serviceCollection = new ServiceCollection();

            var assemblies = AssemblyDiscoverer.DiscoverAssemblies();
            foreach (var assembly in assemblies)
            {
                this.RegisterAssembly(serviceCollection, assembly);
            }

            this._serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public void InjectFatDiscoverableProps(BaseFatDiscoverable fatDiscoverable)
        {
            var browser = this._webBrowserProvider();

            fatDiscoverable.WebBrowser = browser;
            fatDiscoverable.LogsSink = this._logsSink;
            fatDiscoverable.Logger = this._loggerFactory.Create(fatDiscoverable);
        }

        private void RegisterAssembly(ServiceCollection serviceCollection, Assembly assembly)
        {
            var testClasses = _discoverer.FindTestClasses(assembly);

            foreach (var testClass in testClasses)
            {
                serviceCollection.AddTransient(testClass, testClass);
            }

            var fatPages = _discoverer.FindPages(assembly);
            foreach (var page in fatPages)
            {
                serviceCollection.AddSingleton(page);
            }

            var fatPageSections = _discoverer.FindPageSections(assembly);
            foreach (var pageSections in fatPageSections)
            {
                serviceCollection.AddSingleton(pageSections);
            }

            var fatFlows = _discoverer.FindFatFlows(assembly);
            foreach (var flow in fatFlows)
            {
                serviceCollection.AddSingleton(flow);
            }
        }

        internal T GetService<T>(Type type) where T : class
        {
            return GetPropertyInjectedService(type, new HashSet<string>()) as T;
        }

        private object GetPropertyInjectedService(Type type, HashSet<string> injectionContext)
        {
            injectionContext.Add(type.FullName);

            var instance = this._serviceProvider.GetService(type);
            var injectableProperties = this.GetInjectableProperties(type);

            foreach (var prop in injectableProperties)
            {

                if (prop.GetValue(instance) == null)
                {
                    object fatPageProp = null;
                    if (injectionContext.Contains(prop.PropertyType.FullName))
                    {

                        fatPageProp = this._serviceProvider.GetService(prop.PropertyType);
                    }
                    else
                    {
                        fatPageProp = GetPropertyInjectedService(prop.PropertyType, injectionContext);
                    }

                    prop.SetValue(instance, fatPageProp);
                }
            }

            injectionContext.Remove(type.FullName);

            var fatDiscoverable = instance as BaseFatDiscoverable;
            if (fatDiscoverable != null)
            {
                InjectFatDiscoverableProps(fatDiscoverable);
            }

            return instance;
        }

        private readonly static Type[] FAT_TYPES = new Type[]{
            typeof(FatPage),
            typeof(FatPageSection),
            typeof(FatFlow),
            typeof(FatTest)
        };

        private List<PropertyInfo> GetInjectableProperties(Type type)
        {
            var injectableProperties = new List<PropertyInfo>();

            var refType = type;
            while (!FAT_TYPES.Contains(refType))
            {
                var properties = refType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var injectable = properties.Where(prop =>
                {
                    return prop.PropertyType.IsSubclassOf(typeof(FatPage))
                    || prop.PropertyType.IsSubclassOf(typeof(FatPageSection))
                    || prop.PropertyType.IsSubclassOf(typeof(FatFlow));
                });
                injectableProperties.AddRange(injectable);
                refType = refType.BaseType;
            }

            return injectableProperties;
        }
    }
}
