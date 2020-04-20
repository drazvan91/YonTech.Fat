using System;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Yontech.Fat.Discoverer;

namespace Yontech.Fat.TestAdapter.Factories
{
    internal class TestCaseFactory
    {
        const string ELLIPSIS = "...";
        const int MAXIMUM_DISPLAY_NAME_LENGTH = 447;

        public string ExecutorUri { get; }

        public TestCaseFactory(string executorUri)
        {
            this.ExecutorUri = executorUri;
        }

        public TestCase Create(FatTestCase testCase)
        {
            var tc = new TestCase()
            {
                DisplayName = Escape(testCase.DisplayName),
                Source = testCase.Method.ReflectedType.Assembly.Location,
                CodeFilePath = testCase.CodeFilePath,
                ExecutorUri = Constants.ExecutorUri,
                FullyQualifiedName = testCase.FullyQualifiedName,
                Id = testCase.Id,
                LineNumber = testCase.CodeFileLineNumber,
                // Properties = new List<TestProperty>(), // todo
            };

            var labelTraits = testCase.GetCascadedAttributes()
                .OfType<FatLabel>()
                .Select(label => new Trait("Label", label.Name));
            tc.Traits.AddRange(labelTraits);

            return tc;
        }

        private static string Escape(string value)
        {
            if (value == null)
                return string.Empty;

            return Truncate(value.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t"));
        }

        static string Truncate(string value)
        {
            if (value.Length <= MAXIMUM_DISPLAY_NAME_LENGTH)
                return value;

            return value.Substring(0, MAXIMUM_DISPLAY_NAME_LENGTH - ELLIPSIS.Length) + ELLIPSIS;
        }
    }
}
