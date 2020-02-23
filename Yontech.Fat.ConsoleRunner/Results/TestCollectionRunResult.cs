using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yontech.Fat.ConsoleRunner.Results
{
    public class TestCollectionRunResult
    {
        public Assembly Assembly { get; }
        public string Name { get; }
        public List<TestClassRunResult> TestClasses { get; }

        public TestCollectionRunResult(Assembly assembly)
        {
            this.Assembly = assembly;
            this.Name = assembly.FullName;
            this.TestClasses = new List<TestClassRunResult>();
        }

        public bool HasErrors()
        {
            return TestClasses.Any(tc => tc.HasErrors());
        }

        public bool IsSkipped()
        {
            return TestClasses.All(tc => tc.IsSkipped());
        }
    }
}
