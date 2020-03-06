using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Yontech.Fat.Logging
{
    public class ConsolePrinter
    {
        public void PrintNormal(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(format, args);
            Console.ResetColor();
        }

        public void PrintGray(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(format, args);
            Console.ResetColor();
        }

        public void PrintPurple(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(format, args);
            Console.ResetColor();
        }

        public void PrintRed(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(format, args);
            Console.ResetColor();
        }

        public void PrintYellow(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(format, args);
            Console.ResetColor();
        }

        public void PrintGreen(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(format, args);
            Console.ResetColor();
        }

        public void PrintBackgroundGray(string format, params object[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(format, args);
            Console.ResetColor();
        }
        public void PrintBackgroundGreen(string format, params object[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(format, args);
            Console.ResetColor();
        }

        public void PrintBackgroundYellow(string format, params object[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(format, args);
            Console.ResetColor();
        }

        public void PrintBackgroundRed(string format, params object[] args)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(format, args);
            Console.ResetColor();
        }

        public void Enter()
        {
            Console.WriteLine();
        }

        public void PrintException(Exception exception, bool includeInnerExceptions, bool includeFatStackFrames = true)
        {
            var st = new StackTrace(exception, true);
            var fatTestFrames = this.GetFatTestFrames(st);
            foreach (var frame in fatTestFrames)
            {
                var fileDetails = $"{frame.GetFileName()}:line {frame.GetFileLineNumber()}";
                PrintRed(fileDetails);
            }
        }

        private readonly static Type[] FAT_TYPES = new Type[]{
            typeof(FatPage),
            typeof(FatPageSection),
            typeof(FatFlow),
            typeof(FatTest),
            typeof(FatCustomComponent),
        };

        private IEnumerable<StackFrame> GetFatTestFrames(StackTrace st, bool includeFatStackFrames = true)
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
