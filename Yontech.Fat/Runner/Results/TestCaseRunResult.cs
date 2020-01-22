using System;
using System.Diagnostics;
using System.Reflection;

namespace Yontech.Fat.Runner.Results
{
    public class TestCaseRunResult
    {
        public MethodInfo Method { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public ResultType Result { get; set; }
        public long Duration { get; set; }
        public string Logs { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }

        public enum ResultType
        {
            NotStarted,
            Skipped,
            Success,
            Error
        }

        public bool IsSkipped()
        {
            return this.Result == ResultType.Skipped;
        }

        public bool HasErrors()
        {
            return this.Result == ResultType.Error;
        }

        public void PrintException()
        {
            var st = new StackTrace(this.Exception, true);
            var fatTestFrame = this.GetFatTestFrame(st);
            if (fatTestFrame != null)
            {
                var fileDetails = $"{fatTestFrame.GetFileName()}:line {fatTestFrame.GetFileLineNumber()}";
                Console.WriteLine(fileDetails);
            }
        }

        private StackFrame GetFatTestFrame(StackTrace st)
        {
            for (int i = 0; i < st.FrameCount; i++)
            {
                var frame = st.GetFrame(i);
                if (frame.HasMethod())
                {
                    var method = frame.GetMethod();
                    if (method.ReflectedType.IsSubclassOf(typeof(FatTest)))
                    {
                        return frame;
                    }
                }
            }

            return null;
        }
    }
}