using System;

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
            var lines = StackFrameHelper.GetJoinedStackLines(exception, includeInnerExceptions, includeFatStackFrames);

            PrintRed(lines);
            Console.WriteLine();
        }
    }
}
