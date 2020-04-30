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
            var parameters = method.GetParameters();

            if (parameters.Length == 1 && !IsPrimitive(parameters[0].ParameterType))
            {
                return GetObjectLike(parameters[0], method);
            }

            return GetInlineParamsLike(parameters, method);
        }

        private IEnumerable<object[]> GetInlineParamsLike(ParameterInfo[] parameters, MethodInfo method)
        {
            using (var stream = StreamReaderProvider.GetStream(this._filename, method.ReflectedType.Assembly))
            {
                var document = JsonDocument.Parse(stream);
                foreach (var item in document.RootElement.EnumerateArray())
                {
                    var paramsValues = parameters.Select(param =>
                    {
                        var jsonItem = item.EnumerateObject().FirstOrDefault(i => string.Compare(i.Name, param.Name, true) == 0);
                        return ConvertJsonToType(jsonItem.Value, param);
                    });

                    yield return paramsValues.ToArray();
                }
            }
        }

        private object ConvertJsonToType(JsonElement value, ParameterInfo paramInfo)
        {
            var parameterType = paramInfo.ParameterType;

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

            throw new Exception($"Not supported type for parameter '{paramInfo.Name}'");
        }

        private IEnumerable<object[]> GetObjectLike(ParameterInfo parameterInfo, MethodInfo method)
        {
            using (var stream = StreamReaderProvider.GetStream(this._filename, method.ReflectedType.Assembly))
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
}
