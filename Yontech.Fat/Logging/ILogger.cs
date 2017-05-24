using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.Logging
{
    public interface ILogger
    {
        void Write(string message, params string[] args);
    }
}
