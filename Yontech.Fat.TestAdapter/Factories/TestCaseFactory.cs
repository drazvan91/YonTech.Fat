using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Yontech.Fat.Discoverer;

namespace Yontech.Fat.TestAdapter.Factories
{
    internal class TestCaseFactory
    {
        public string ExecutorUri { get; }

        public TestCaseFactory(string executorUri)
        {
            this.ExecutorUri = executorUri;
        }

        public TestCase Create(FatTestCase testCase)
        {
            return new TestCase()
            {
                DisplayName = testCase.DisplayName,
                Source = testCase.Method.ReflectedType.Assembly.Location,
                CodeFilePath = testCase.CodeFilePath,
                ExecutorUri = new Uri(Constants.ExecutorUri),
                FullyQualifiedName = testCase.FullyQualifiedName,
                Id = testCase.Id,
                LineNumber = testCase.CodeFileLineNumber,
                // Properties = new List<TestProperty>(), // todo
                // Traits = new TraitCollection()
            };
        }
    }
}
