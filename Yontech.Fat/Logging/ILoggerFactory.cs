using System;

namespace Yontech.Fat.Logging
{
    public interface ILoggerFactory
    {
        ILogger Create<T>(T forObject);
    }
}
