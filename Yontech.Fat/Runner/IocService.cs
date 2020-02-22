using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Yontech.Fat.Discoverer;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Runner
{
    public class IocService
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly FatDiscoverer _discoverer;
        private readonly Func<IWebBrowser> _webBrowserProvider;

        public IocService(FatDiscoverer discoverer, Func<IWebBrowser> webBrowserProvider)
        {
            this._discoverer = discoverer;
            this._webBrowserProvider = webBrowserProvider;

            var serviceCollection = new ServiceCollection();

            var assemblies = AssemblyDiscoverer.DiscoverAssemblies();
            foreach (var assembly in assemblies)
            {
                this.RegisterAssembly(serviceCollection, assembly);
            }

            this._serviceProvider = serviceCollection.BuildServiceProvider();
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

            var browser = this._webBrowserProvider();

            var fatTest = instance as FatTest;
            if (fatTest != null)
            {
                fatTest.WebBrowser = browser;
            }

            var sectionPage = instance as FatPageSection;
            if (sectionPage != null)
            {
                sectionPage.WebBrowser = browser;
            }

            var page = instance as FatPage;
            if (page != null)
            {
                page.WebBrowser = browser;
            }

            var flow = instance as FatFlow;
            if (flow != null)
            {
                flow.WebBrowser = browser;
            }

            return instance;
        }

        private IEnumerable<PropertyInfo> GetInjectableProperties(Type type)
        {
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            return properties.Where(prop =>
            {
                return prop.PropertyType.IsSubclassOf(typeof(FatPage))
          || prop.PropertyType.IsSubclassOf(typeof(FatPageSection))
          || prop.PropertyType.IsSubclassOf(typeof(FatFlow));
            });
        }
    }
}