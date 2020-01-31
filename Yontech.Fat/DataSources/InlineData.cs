using System.Collections.Generic;

namespace Yontech.Fat.DataSources
{

    public class InlineData : TestCaseDataSource
    {
        private object[] _data;

        public InlineData(params object[] arguments)
        {
            _data = arguments;
        }

        public override IEnumerable<object[]> GetExecutionArguments()
        {
            yield return _data;
        }
    }
}