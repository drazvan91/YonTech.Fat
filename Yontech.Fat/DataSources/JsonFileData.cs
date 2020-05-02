using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Yontech.Fat.Exceptions;

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
            var valueKind = value.ValueKind;

            if (valueKind == JsonValueKind.Undefined)
            {
                throw new FatException($"Property '{paramInfo.Name}' does not exist in file '{this._filename}'");
            }

            try
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

                if (parameterType == typeof(DateTime))
                {
                    return value.GetDateTime();
                }

                throw new FatException($"Not supported type for parameter '{paramInfo.Name}' in file '{this._filename}'");
            }
            catch (Exception ex) when (ex.Message.Contains("The requested operation requires an element of type"))
            {
                // eg: The requested operation requires an element of type 'String', but the target element has type 'Number'
                throw new FatException($"Type mismatch for parameter '{paramInfo.Name}' in file '{this._filename}'");
            }
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
