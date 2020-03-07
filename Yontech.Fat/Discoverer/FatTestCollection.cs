using System;
using System.Collections.Generic;
using System.Reflection;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Discoverer
{
    public class FatTestCollection
    {
        public Assembly Assembly { get; }
        public string Name { get; }
        public List<FatTestClass> TestClasses { get; set; }

        public FatTestCollection(Assembly assembly)
        {
            this.Assembly = assembly;
            this.Name = this.Assembly.FullName;
            this.TestClasses = new List<FatTestClass>();
        }

    }
}
