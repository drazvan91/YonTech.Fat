using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using CsvHelper;

namespace Yontech.Fat.DataSources
{

    public class CsvFileData : TestCaseDataSource
    {
        private readonly string _filename;

        public CsvFileData(string filename)
        {
            _filename = filename;
        }

        public override IEnumerable<object[]> GetExecutionArguments(ParameterInfo[] parameters)
        {
            if (parameters.Length == 1 && !IsPrimitive(parameters[0].ParameterType))
            {
                return GetObjectLike(parameters[0]);
            }

            return GetInlineParamsLike(parameters);
        }

        private IEnumerable<object[]> GetInlineParamsLike(ParameterInfo[] parameters)
        {
            using (var reader = new StreamReader(this._filename))
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
                        return convertDynamicToType((string)recordProps[param.Name.ToLower()], param.ParameterType);
                    });
                    yield return paramsValues.ToArray();
                }
            }
        }

        private object convertDynamicToType(string value, Type parameterType)
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

            throw new Exception("Not supported");
        }

        private IEnumerable<object[]> GetObjectLike(ParameterInfo parameterInfo)
        {
            using (var reader = new StreamReader(this._filename))
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
