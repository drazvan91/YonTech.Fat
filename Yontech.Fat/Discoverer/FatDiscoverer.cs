using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yontech.Fat.Discoverer
{
    public class FatDiscoverer
    {
        public IEnumerable<FatTestCollection> DiscoverTestCollections(IEnumerable<Assembly> assemblies)
        {
            return assemblies.Select(assembly => new FatTestCollection(assembly)
            {
                TestClasses = DiscoverTestClasses(assembly).ToList()
            });
        }

        public IEnumerable<FatTestClass> DiscoverTestClasses(Assembly assembly)
        {
            var allTestClasses = this.FindTestClasses(assembly);

            foreach (var type in allTestClasses)
            {
                yield return new FatTestClass(type)
                {
                    TestCases = DiscoverTestCases(type).ToList(),
                };
            }
        }

        private IEnumerable<FatTestCase> DiscoverTestCases(Type testClass)
        {
            var allMethods = testClass.GetMethods();
            var testCases = allMethods.Where(method => method.Name.StartsWith("Test")); // todo: make this configurable

            foreach (var method in testCases)
            {
                yield return new FatTestCase(method);
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
                return type.IsSubclassOf(typeof(FatTest));
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
    }
}