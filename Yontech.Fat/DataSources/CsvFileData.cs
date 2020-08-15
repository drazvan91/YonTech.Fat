using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvHelper;
using Yontech.Fat.Exceptions;

namespace Yontech.Fat.DataSources
{
    public class CsvFileData : TestCaseDataSource
    {
        private readonly string _filename;

        public CsvFileData(string filename)
        {
            _filename = filename;
        }

        protected override IEnumerable<object[]> GetExecutionArguments(MethodInfo method)
        {
            var parameters = method.GetParameters();
            if (parameters.Length == 1 && !IsPrimitive(parameters[0].ParameterType))
            {
                return GetObjectLike(method, parameters[0]);
            }

            return GetInlineParamsLike(method, parameters);
        }

        private IEnumerable<object[]> GetInlineParamsLike(MethodInfo method, ParameterInfo[] parameters)
        {
            using (var reader = StreamReaderProvider.GetTextReader(this._filename, method.ReflectedType.Assembly))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) =>
                {
                    return header.ToLower();
                };

                foreach (var record in csv.GetRecords<dynamic>())
                {
                    var recordProps = (IDictionary<string, object>)record;
                    var paramsValues = parameters.Select(param =>
                    {
                        return ConvertDynamicToType((string)recordProps[param.Name.ToLower()], param.ParameterType);
                    });

                    yield return paramsValues.ToArray();
                }
            }
        }

        private object ConvertDynamicToType(string value, Type parameterType)
        {
            if (parameterType == typeof(int))
            {
                return int.Parse(value);
            }

            if (parameterType == typeof(string))
            {
                return value;
            }

            if (parameterType == typeof(bool))
            {
                return bool.Parse(value);
            }

            throw new FatException("Not supported");
        }

        private IEnumerable<object[]> GetObjectLike(MethodInfo method, ParameterInfo parameterInfo)
        {
            using (var reader = StreamReaderProvider.GetTextReader(this._filename, method.ReflectedType.Assembly))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) =>
                {
                    return header.ToLower();
                };

                foreach (var record in csv.GetRecords(parameterInfo.ParameterType))
                {
                    yield return new object[] { record };
                }
            }
        }
    }
}
