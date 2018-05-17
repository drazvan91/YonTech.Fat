using System;
using System.Runtime.Serialization;

namespace Yontech.Fat.Configuration
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(string message) : base(message)
        {
        }

        public InvalidConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
