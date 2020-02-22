using System;
using System.Collections.Generic;
using System.Linq;

namespace Yontech.Fat.ConsoleRunner.Results
{
    public class TestClassRunResult
    {
        public Type Class { get; }
        public string Name { get; }
        public List<TestCaseRunResult> TestCases { get; }

        public TestClassRunResult(Type testClass)
        {
            this.Class = testClass;
            this.Name = testClass.Name;
            this.TestCases = new List<TestCaseRunResult>();
        }

        public bool HasErrors()
        {
            return TestCases.Any(tc => tc.Result == TestCaseRunResult.ResultType.Error);
        }

        public bool IsSkipped()
        {
            return TestCases.All(tc => tc.Result == TestCaseRunResult.ResultType.Skipped);
        }
    }
}