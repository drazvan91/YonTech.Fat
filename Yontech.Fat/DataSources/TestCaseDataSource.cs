using System;
using System.Collections.Generic;
using System.Reflection;
using Yontech.Fat.Logging;
using Yontech.Fat.Utils;

namespace Yontech.Fat.DataSources
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true)]
    public abstract class TestCaseDataSource : System.Attribute
    {
        protected ILogger Logger { get; private set; }
        protected IStreamProvider StreamReaderProvider { get; private set; }

        internal IEnumerable<object[]> GetExecutionArguments(ILoggerFactory loggerFactory, IStreamProvider readerProvider, MethodInfo method)
        {
            this.Logger = loggerFactory.Create(this);
            this.StreamReaderProvider = readerProvider;

            return GetExecutionArguments(method);
        }

        protected abstract IEnumerable<object[]> GetExecutionArguments(MethodInfo method);

        protected virtual bool IsPrimitive(Type parameterType)
        {
            return parameterType == typeof(string) || parameterType == typeof(int) || parameterType == typeof(bool);
        }
    }
}
