using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Yontech.Fat.DataSources
{

    public class JsonFileData : TestCaseDataSource
    {
        private readonly string _filename;

        public JsonFileData(string filename)
        {
            _filename = filename;
        }

        public override IEnumerable<object[]> GetExecutionArguments(ParameterInfo[] parameters)
        {
            var text = File.ReadAllText(this._filename);

            if (parameters.Length == 1 && !IsPrimitive(parameters[0].ParameterType))
            {
                return GetObjectLike(text, parameters[0]);
            }

            return GetInlineParamsLike(text, parameters);
        }

        private IEnumerable<object[]> GetInlineParamsLike(string text, ParameterInfo[] parameters)
        {
            var document = JsonDocument.Parse(text);
            foreach (var item in document.RootElement.EnumerateArray())
            {
                var paramsValues = parameters.Select(param =>
                {
                    var jsonItem = item.EnumerateObject().FirstOrDefault(i => string.Compare(i.Name, param.Name, true) == 0);
                    return convertJsonToType(jsonItem.Value, param.ParameterType);
                });

                yield return paramsValues.ToArray();
            }
        }

        private object convertJsonToType(JsonElement value, Type parameterType)
        {
            if (parameterType == typeof(int))
            {
                return value.GetInt32();
            }
            if (parameterType == typeof(string))
            {
                return value.GetString();
            }
            if (parameterType == typeof(bool))
            {
                return value.GetBoolean();
            }

            throw new Exception("Not supported");
        }

        private IEnumerable<object[]> GetObjectLike(string text, ParameterInfo parameterInfo)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            var array = JsonSerializer.Deserialize(text, parameterInfo.ParameterType.MakeArrayType(), options) as object[];

            foreach (var item in array)
            {
                yield return new object[] { item };
            }

        }
    }
}
