using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Yontech.Fat.Discoverer;
using Yontech.Fat.EnvData;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Runner
{
    internal class IocService
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly FatExecutionContext _executionContext;
        private readonly FatDiscoverer _discoverer;
        private readonly LogsSink _logsSink;
        private readonly ILogger _logger;

        public IocService(FatExecutionContext executionContext, FatDiscoverer discoverer, LogsSink logsSink)
        {
            this._executionContext = executionContext;
            this._discoverer = discoverer;
            this._logger = executionContext.LoggerFactory.Create(this);
            this._logsSink = logsSink;

            var serviceCollection = new ServiceCollection();

            var assemblies = executionContext.AssemblyDiscoverer.DiscoverAssemblies();
            foreach (var assembly in assemblies)
            {
                this.RegisterAssembly(serviceCollection, assembly);
            }

            this._serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public void InjectFatDiscoverableProps(BaseFatDiscoverable fatDiscoverable, IWebBrowser browser)
        {
            fatDiscoverable.WebBrowser = browser;
            fatDiscoverable.LogsSink = this._logsSink;
            fatDiscoverable.Logger = this._executionContext.LoggerFactory.Create(fatDiscoverable);
        }

        internal T GetService<T>(Type type, IWebBrowser browser) where T : class
        {
            var scopedService = _serviceProvider.CreateScope();
            var service = GetPropertyInjectedService(scopedService, type, browser, new HashSet<string>()) as T;
            if (service == null)
            {
                throw new FatException("Type '{0}' cound not be found. Have you registered all assemblies?", type.FullName);
            }

            return service;
        }

        private void RegisterAssembly(ServiceCollection serviceCollection, Assembly assembly)
        {
            var testClasses = _discoverer.FindTestClasses(assembly);

            foreach (var testClass in testClasses)
            {
                serviceCollection.AddScoped(testClass, testClass);
            }

            var fatPages = _discoverer.FindPages(assembly);
            foreach (var page in fatPages)
            {
                serviceCollection.AddScoped(page);
            }

            var fatPageSections = _discoverer.FindPageSections(assembly);
            foreach (var pageSections in fatPageSections)
            {
                serviceCollection.AddScoped(pageSections);
            }

            var fatFlows = _discoverer.FindFatFlows(assembly);
            foreach (var flow in fatFlows)
            {
                serviceCollection.AddScoped(flow);
            }

            var fatEnvDatas = _discoverer.FindFatEnvDatas(assembly);
            foreach (var fatEnvData in fatEnvDatas)
            {
                serviceCollection.AddScoped(fatEnvData, (s) =>
                {
                    var envDataResolver = new EnvDataResolver(this._executionContext);
                    return envDataResolver.Resolve(fatEnvData);
                });
            }
        }

        private object GetPropertyInjectedService(IServiceScope serviceScope, Type type, IWebBrowser browser, HashSet<string> injectionContext)
        {
            injectionContext.Add(type.FullName);

            var instance = serviceScope.ServiceProvider.GetService(type);
            var injectableProperties = this.GetInjectableProperties(type);

            foreach (var prop in injectableProperties)
            {
                if (prop.GetValue(instance) == null)
                {
                    object fatPageProp = null;
                    if (injectionContext.Contains(prop.PropertyType.FullName))
                    {
                        fatPageProp = serviceScope.ServiceProvider.GetService(prop.PropertyType);
                    }
                    else
                    {
                        fatPageProp = GetPropertyInjectedService(serviceScope, prop.PropertyType, browser, injectionContext);
                    }

                    prop.SetValue(instance, fatPageProp);
                }
            }

            injectionContext.Remove(type.FullName);

            var fatDiscoverable = instance as BaseFatDiscoverable;
            if (fatDiscoverable != null)
            {
                InjectFatDiscoverableProps(fatDiscoverable, browser);
            }

            return instance;
        }

        private readonly static Type[] BASE_FAT_TYPES = new Type[]
        {
            typeof(FatPage),
            typeof(FatPageSection),
            typeof(FatFlow),
            typeof(FatTest),
            typeof(FatEnvData),
        };

        private bool IsInjectableProperty(PropertyInfo prop)
        {
            return prop.PropertyType.IsSubclassOf(typeof(FatPage))
                || prop.PropertyType.IsSubclassOf(typeof(FatPageSection))
                || prop.PropertyType.IsSubclassOf(typeof(FatFlow))
                || prop.PropertyType.IsSubclassOf(typeof(FatEnvData));
        }

        private List<PropertyInfo> GetInjectableProperties(Type type)
        {
            var injectableProperties = new List<PropertyInfo>();

            var refType = type;
            while (!BASE_FAT_TYPES.Contains(refType))
            {
                var properties = refType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var injectable = properties.Where(IsInjectableProperty);
                injectableProperties.AddRange(injectable);
                refType = refType.BaseType;
            }

            return injectableProperties;
        }
    }
}
