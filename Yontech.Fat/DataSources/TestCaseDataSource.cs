using System.Collections.Generic;

namespace Yontech.Fat.DataSources
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true)]
    public abstract class TestCaseDataSource : System.Attribute
    {
        public abstract IEnumerable<object[]> GetExecutionArguments();
    }
}