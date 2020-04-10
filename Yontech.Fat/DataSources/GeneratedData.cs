using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yontech.Fat.Exceptions;

namespace Yontech.Fat.DataSources
{
    public class GeneratedData : TestCaseDataSource
    {
        private readonly Type _type;

        public GeneratedData(Type type)
        {
            this._type = type;
        }

        protected override IEnumerable<object[]> GetExecutionArguments(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var instance = Activator.CreateInstance(_type);
            var items = _type.GetMethod("Generate").Invoke(instance, null) as IEnumerable<object>;

            var requiredIsPrimitive = IsPrimitive(parameters[0].ParameterType);
            var providedIsPrimitive = IsPrimitive(GetProvidedType());

            if (providedIsPrimitive)
            {
                return GetObjectLike(items, parameters[0]);
            }

            if (requiredIsPrimitive)
            {
                return GetInlineParamsLike(items, parameters);
            }

            return GetObjectLike(items, parameters[0]);
        }

        private Type GetProvidedType()
        {
            return _type.GetMethod("Generate").ReturnType.GetGenericArguments()[0];
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
                    if (property == null)
                    {
                        throw new FatException("Cannot find property '{0}' on type '{1}'", param.Name, GetProvidedType().Name);
                    }

                    var value = property.GetValue(item);
                    return value;
                });

                yield return paramValues.ToArray();
            }
        }
    }
}
