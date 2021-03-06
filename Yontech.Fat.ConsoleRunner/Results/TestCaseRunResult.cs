﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Yontech.Fat.Logging;

namespace Yontech.Fat.ConsoleRunner.Results
{
    public class TestCaseRunResult
    {
        public MethodInfo Method { get; }
        public string ShortName
        {
            get
            {
                return Method.Name;
            }
        }

        public string LongName
        {
            get
            {
                return Method.ReflectedType.FullName + "." + ShortName;
            }
        }

        public ResultType Result { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Log> Logs { get; set; }
        public string ErrorMessage { get; set; }
        public int? BrowserIndexWhichCausedError { get; set; }
        public Exception Exception { get; set; }

        public TestCaseRunResult(MethodInfo testCase)
        {
            this.Method = testCase;
        }

        public enum ResultType
        {
            NotStarted,
            Skipped,
            Success,
            Error,
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

        private readonly static Type[] FAT_TYPES = new Type[]
        {
            typeof(FatPage),
            typeof(FatPageSection),
            typeof(FatFlow),
            typeof(FatTest),
            typeof(FatCustomComponent),
        };

        private IEnumerable<StackFrame> GetFatTestFrames(StackTrace st)
        {
            for (int i = 0; i < st.FrameCount; i++)
            {
                var frame = st.GetFrame(i);
                if (frame.HasMethod())
                {
                    /*
                    todo: maybe it is not a good idea to filter the stack trace
                    maybe we should make it configurable
                     yield return frame;
                    */
                    var method = frame.GetMethod();
                    if (FAT_TYPES.Any(fatType => method.ReflectedType.IsSubclassOf(fatType)))
                    {
                        yield return frame;
                    }
                }
            }
        }
    }
}
