using System;
using System.Collections.Generic;
using System.Reflection;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Discoverer
{
    public class FatTestClass
    {
        public Type Class { get; }
        public string Name { get; }
        public List<FatTestCase> TestCases { get; set; }

        public FatTestClass(Type @class)
        {
            this.Class = @class;
            this.Name = this.Class.FullName;
            this.TestCases = new List<FatTestCase>();
        }

    }
}