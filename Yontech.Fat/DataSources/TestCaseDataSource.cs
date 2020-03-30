using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yontech.Fat.DataSources
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true)]
    public abstract class TestCaseDataSource : System.Attribute
    {
        public abstract IEnumerable<object[]> GetExecutionArguments(ParameterInfo[] parameters);

        protected virtual bool IsPrimitive(Type parameterType)
        {
            return parameterType == typeof(string) || parameterType == typeof(int) || parameterType == typeof(bool);
        }
    }
}
