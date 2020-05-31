﻿using Yontech.Fat;
using Yontech.Fat.Configuration;
using Yontech.Fat.Logging;

namespace CreateConsoleFatProject
{
    public class Config : FatConfig
    {
        public Config()
        {
            LogLevel = LogLevel.Debug;
            AddChrome(new ChromeFatConfig()
            {
                RunInBackground = true,
                DriversFolder = "drivers-folder1",
                Version = ChromeVersion.Latest
            });
        }
    }
}
