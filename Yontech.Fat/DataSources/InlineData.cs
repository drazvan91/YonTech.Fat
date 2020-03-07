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

        public override IEnumerable<object[]> GetExecutionArguments(ParameterInfo[] parameters)
        {
            yield return _data;
        }
    }
}
