using System;
using System.Collections.Generic;
using System.Linq;

namespace Yontech.Fat.Runner.Results
{
    public class TestClassRunResult
    {
        public Type Class { get; set; }
        public string Name { get; set; }
        public List<TestCaseRunResult> TestCases { get; set; }


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