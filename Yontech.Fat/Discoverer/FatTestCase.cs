using System;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Yontech.Fat.Utils;

namespace Yontech.Fat.Discoverer
{
    public class FatTestCase
    {
        public MethodInfo Method { get; }
        public Guid Id { get; }
        public string FullyQualifiedName { get; }
        public string DisplayName { get; }
        public string CodeFilePath { get; }
        public int CodeFileLineNumber { get; }

        public FatTestCase(MethodInfo method)
        {
            this.Method = method;
            this.FullyQualifiedName = $"{Method.ReflectedType.FullName}.{Method.Name}";
            this.DisplayName = Method.Name;
            this.Id = GuidGenerator.FromString(FullyQualifiedName);

            DiaSession diaSession = new DiaSession(method.ReflectedType.Assembly.Location);
            var navData = diaSession.GetNavigationData(method.ReflectedType.FullName, method.Name);
            if (navData != null)
            {
                this.CodeFilePath = navData.FileName;
                this.CodeFileLineNumber = navData.MinLineNumber;
            }
        }
    }
}
