﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat
{
    public interface IWebBrowserFactory
    {
        IWebBrowser Create(BrowserType browserType, string driversFolder);
    }
}
