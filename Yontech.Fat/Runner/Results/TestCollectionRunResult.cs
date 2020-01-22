using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yontech.Fat.Runner.Results
{
    public class TestCollectionRunResult
    {
        public Assembly Assembly { get; set; }
        public string Name { get; set; }
        public List<TestClassRunResult> TestClasses { get; set; }


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