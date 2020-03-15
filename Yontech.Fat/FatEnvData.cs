using System.Collections.Generic;
using System.Drawing;
using Yontech.Fat.Interceptors;
using Yontech.Fat.Logging;
using Yontech.Fat.Runner;

namespace Yontech.Fat
{
    public abstract class FatEnvData
    {
        internal string FilePath { get; private set; }

        public FatEnvData(string filePath)
        {
            this.FilePath = filePath;
        }

    }
}
