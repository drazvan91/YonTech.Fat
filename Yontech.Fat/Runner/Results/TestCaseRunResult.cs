using System;
using System.Collections.Generic;
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
        public TimeSpan Duration { get; set; }
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
            var fatTestFrames = this.GetFatTestFrames(st);
            foreach (var frame in fatTestFrames)
            {
                var fileDetails = $"{frame.GetFileName()}:line {frame.GetFileLineNumber()}";
                Console.WriteLine(fileDetails);
            }
        }

        private IEnumerable<StackFrame> GetFatTestFrames(StackTrace st)
        {
            for (int i = 0; i < st.FrameCount; i++)
            {
                var frame = st.GetFrame(i);
                if (frame.HasMethod())
                {
                    var method = frame.GetMethod();
                    if (method.ReflectedType.IsSubclassOf(typeof(FatFlow)))
                    {
                        yield return frame;
                    }

                    if (method.ReflectedType.IsSubclassOf(typeof(FatTest)))
                    {
                        yield return frame;
                    }
                }
            }
        }
    }
}