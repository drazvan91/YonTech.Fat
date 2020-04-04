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

        protected override IEnumerable<object[]> GetExecutionArguments(MethodInfo method)
        {
            using (var reader = StreamReaderProvider.GetStream(this._filename, method.ReflectedType.Assembly))
            {
                var parameters = method.GetParameters();

                if (parameters.Length == 1 && !IsPrimitive(parameters[0].ParameterType))
                {
                    return GetObjectLike(reader, parameters[0]);
                }

                return GetInlineParamsLike(reader, parameters);
            }
        }

        private IEnumerable<object[]> GetInlineParamsLike(Stream stream, ParameterInfo[] parameters)
        {
            var document = JsonDocument.Parse(stream);
            foreach (var item in document.RootElement.EnumerateArray())
            {
                var paramsValues = parameters.Select(param =>
                {
                    var jsonItem = item.EnumerateObject().FirstOrDefault(i => string.Compare(i.Name, param.Name, true) == 0);
                    return ConvertJsonToType(jsonItem.Value, param.ParameterType);
                });

                yield return paramsValues.ToArray();
            }
        }

        private object ConvertJsonToType(JsonElement value, Type parameterType)
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

        private IEnumerable<object[]> GetObjectLike(Stream stream, ParameterInfo parameterInfo)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            var type = parameterInfo.ParameterType.MakeArrayType();

            var array = JsonSerializer.DeserializeAsync(stream, type, options).Result as object[];

            foreach (var item in array)
            {
                yield return new object[] { item };
            }
        }
    }
}
