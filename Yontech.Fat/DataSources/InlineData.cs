using System.Collections.Generic;
using System.Reflection;
using Yontech.Fat.Exceptions;

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
            int paramCount = method.GetParameters().Length;
            if (paramCount != _data.Length)
            {
                throw new FatException("InlineData has defined {0} parameters but {1} are expected", _data.Length, paramCount);
            }

            yield return _data;
        }
    }
}
