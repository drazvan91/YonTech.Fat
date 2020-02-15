using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yontech.Fat.Runner.Results;

namespace Yontech.Fat.Runner
{
    public static class RunStrategyFactory
    {
        public static List<TestCollectionRunResult> Create(FatRunOptions runOptions)
        {
            return Create(runOptions.Assemblies);
        }

        public static List<TestCollectionRunResult> Create(IEnumerable<Assembly> assemblies)
        {
            return assemblies.Select(assembly =>
            {
                var collection = new TestCollectionRunResult();
                collection.Assembly = assembly;
                collection.Name = assembly.FullName;

                collection.TestClasses = GetTestClasses(assembly);
                return collection;
            }).ToList();
        }

        private static List<TestClassRunResult> GetTestClasses(Assembly assembly)
        {
            return Discoverer.GetTestClassesForAssembly(assembly).Select(testClass =>
            {
                return new TestClassRunResult()
                {
                    Class = testClass,
                    Name = testClass.Name,
                    TestCases = GetTestCases(testClass)
                };
            }).ToList();
        }

        private static List<TestCaseRunResult> GetTestCases(Type testClass)
        {
            return Discoverer.GetTestCasesForClass(testClass).Select(methodInfo =>
            {
                return new TestCaseRunResult()
                {
                    ShortName = methodInfo.Name,
                    LongName = $"{testClass.FullName}.{methodInfo.Name}",
                    Method = methodInfo,
                    Result = TestCaseRunResult.ResultType.NotStarted,
                };
            }).ToList();
        }
    }
}