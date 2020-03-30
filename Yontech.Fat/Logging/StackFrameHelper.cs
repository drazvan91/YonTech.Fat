using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Yontech.Fat.Logging
{
    public static class StackFrameHelper
    {
        private readonly static Type[] FAT_TYPES = new Type[]
        {
            typeof(FatPage),
            typeof(FatPageSection),
            typeof(FatFlow),
            typeof(FatTest),
            typeof(FatCustomComponent),
        };

        public static string GetJoinedStackLines(Exception exception, bool includeInnerExceptions, bool includeFatStackFrames = true)
        {
            var lines = GetStackLines(exception, includeInnerExceptions, includeFatStackFrames);
            return string.Join(Environment.NewLine, lines);
        }

        public static IEnumerable<string> GetStackLines(Exception exception, bool includeInnerExceptions, bool includeFatStackFrames = true)
        {
            if (includeInnerExceptions && exception.InnerException != null)
            {
                foreach (var line in GetStackLines(exception.InnerException, includeInnerExceptions, includeFatStackFrames))
                {
                    yield return line;
                }

                yield return "---";
            }

            var st = new StackTrace(exception, true);
            var fatTestFrames = GetFatTestFrames(st);
            foreach (var frame in fatTestFrames)
            {
                var fileDetails = $"{frame.GetFileName()}:line {frame.GetFileLineNumber()}";
                yield return fileDetails;
            }
        }

        private static IEnumerable<StackFrame> GetFatTestFrames(StackTrace st, bool includeFatStackFrames = true)
        {
            for (int i = 0; i < st.FrameCount; i++)
            {
                var frame = st.GetFrame(i);
                if (frame.HasMethod())
                {
                    if (includeFatStackFrames)
                    {
                        yield return frame;
                    }
                    else
                    {
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
}
