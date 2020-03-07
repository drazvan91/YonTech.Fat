using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yontech.Fat.DataSources
{

    public class GeneratedData : TestCaseDataSource
    {
        private readonly Type _type;

        public GeneratedData(Type type)
        {
            this._type = type;
        }

        public override IEnumerable<object[]> GetExecutionArguments(ParameterInfo[] parameters)
        {
            var instance = Activator.CreateInstance(_type);
            var items = _type.GetMethod("Generate").Invoke(instance, null) as IEnumerable<object>;

            if (parameters.Length == 1 && !IsPrimitive(parameters[0].ParameterType))
            {
                return GetObjectLike(items, parameters[0]);
            }

            return GetInlineParamsLike(items, parameters);
        }

        private IEnumerable<object[]> GetObjectLike(IEnumerable<object> items, ParameterInfo parameterInfo)
        {
            foreach (var item in items)
            {
                yield return new object[] { item };
            }
        }

        private IEnumerable<object[]> GetInlineParamsLike(IEnumerable<object> items, ParameterInfo[] parameters)
        {
            foreach (var item in items)
            {
                var paramValues = parameters.Select(param =>
                {
                    var property = item.GetType().GetProperties().FirstOrDefault(prop => string.Compare(prop.Name, param.Name, true) == 0);
                    var value = property.GetValue(item);
                    return value;
                });

                yield return paramValues.ToArray();
            }
        }
    }
}
