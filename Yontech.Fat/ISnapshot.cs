﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat
{
    public interface ISnapshot
    {
        void SaveAsFile(string path);
    }
}
