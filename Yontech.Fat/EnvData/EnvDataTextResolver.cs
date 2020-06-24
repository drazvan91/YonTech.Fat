using System;
using System.IO;
using System.Linq;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

namespace Yontech.Fat.EnvData
{
    internal class EnvDataTextResolver
    {
        private readonly ILogger _logger;

        public EnvDataTextResolver(FatExecutionContext execContext)
        {
            this._logger = execContext.LoggerFactory.Create(this);
        }

        public void Resolve(FatEnvData instance)
        {
            string filename = instance.FilePath;
            var lines = File.ReadAllLines(instance.FilePath);

            int lineNumber = 1;
            foreach (var line in lines)
            {
                this.ApplyLine(instance, filename, lineNumber, line);
                lineNumber++;
            }

            _logger.Info("{0} loaded from file '{1}'", instance.GetType().FullName, filename);
        }

        private void ApplyLine<T>(T instance, string fileName, int lineNumber, string line)
        {
            if (line.TrimStart().StartsWith("#"))
            {
                // it is a comment
                return;
            }

            int equalSign = line.IndexOf('=');
            if (equalSign < 0)
            {
                _logger.Debug("ignored line {0}: {1}", lineNumber, line);
                return;
            }

            var propertyPaths = line.Substring(0, equalSign).Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            var value = line.Substring(equalSign + 1);

            this.SetProperty<T>(propertyPaths, instance, value.Trim(), fileName, lineNumber);
        }

        private void SetProperty<T>(string[] propertyPaths, object instance, string value, string fileName, int lineNumber)
        {
            var propertyName = propertyPaths[0].Trim();
            var property = instance.GetType().GetProperties().FirstOrDefault(prop => prop.Name == propertyName);
            if (property == null)
            {
                _logger.Warning("Could not find property '{0}' described at {1}:{2}", propertyName, fileName, lineNumber);
                return;
            }

            if (propertyPaths.Length == 1)
            {
                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(instance, value);
                }
                else if (property.PropertyType == typeof(int))
                {
                    if (int.TryParse(value, out int intValue))
                    {
                        property.SetValue(instance, intValue);
                        return;
                    }
                    else
                    {
                        _logger.Warning("Cound not parse to integer the value described at {0}:{1}", fileName, lineNumber);
                        return;
                    }
                }
                else if (property.PropertyType == typeof(bool))
                {
                    if (bool.TryParse(value, out bool boolValue))
                    {
                        property.SetValue(instance, boolValue);
                        return;
                    }
                }
                else
                {
                    _logger.Warning("Unknown type of property at {0}:{1}", fileName, lineNumber);
                }
            }
            else
            {
                throw new NotImplementedException("setting navigation properties not implemented yet");
            }
        }
    }
}
