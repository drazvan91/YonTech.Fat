using System.Collections.Generic;
using System.Reflection;

namespace Yontech.Fat.DataSources
{
    public class InlineData : TestCaseDataSource
    {
        private object[] _data;

        public InlineData(params object[] arguments)
        {
            _data = arguments;
        }

        protected override IEnumerable<object[]> GetExecutionArguments(MethodInfo method)
        {
            yield return _data;
        }
    }
}
