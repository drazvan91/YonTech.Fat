using System;
using System.Collections.Generic;
using System.Linq;
using Yontech.Fat.ConsoleRunner.Results;
using Yontech.Fat.Logging;

namespace Yontech.Fat.ConsoleRunner
{
    public class ConsoleReporter
    {
        public void Report(List<TestCollectionRunResult> collections)
        {
            Console.WriteLine("Execution results:");
            foreach (var collection in collections)
            {
                foreach (var testClass in collection.TestClasses)
                {
                    Console.WriteLine();

                    PrintPurple("### {0}", testClass.Name);
                    Console.WriteLine();

                    foreach (var testCase in testClass.TestCases)
                    {
                        this.PrintTestCase(testCase);
                    }
                }
            }

            Console.WriteLine();

            PrintNormal("---------------------------------------------------");
            Console.WriteLine();

            this.PrintSummary(collections);
            Console.WriteLine();
        }

        private void PrintSummary(List<TestCollectionRunResult> collections)
        {
            Console.WriteLine();

            var executedCollections = collections.Where(c => c.IsSkipped() == false).ToList();
            var failedCollections = executedCollections.Where(c => c.HasErrors()).ToList();

            PrintNormal("Test Collections: ");
            PrintGreen(" {0} passed", executedCollections.Count - failedCollections.Count);
            PrintNormal(",");
            PrintRed(" {0} failed", failedCollections.Count);
            PrintNormal(", {0} total", collections.Count);
            Console.WriteLine();

            var allTestClasses = collections.SelectMany(collection => collection.TestClasses).ToList();
            var executedTestClasses = allTestClasses.Where(c => c.IsSkipped() == false).ToList();
            var failedTestClasses = executedTestClasses.Where(c => c.HasErrors()).ToList();

            PrintNormal("Test Classes:     ");
            PrintGreen(" {0} passed", executedTestClasses.Count - failedTestClasses.Count);
            PrintNormal(",");
            PrintRed(" {0} failed", failedTestClasses.Count);
            PrintNormal(", {0} total", allTestClasses.Count);
            Console.WriteLine();

            var allTestCases = allTestClasses.SelectMany(collection => collection.TestCases).ToList();
            var executedTestCases = allTestCases.Where(c => c.IsSkipped() == false).ToList();
            var failedTestCasses = executedTestCases.Where(c => c.HasErrors()).ToList();

            PrintNormal("Test Cases:       ");
            PrintGreen(" {0} passed", executedTestCases.Count - failedTestCasses.Count);
            PrintNormal(",");
            PrintRed(" {0} failed", failedTestCasses.Count);
            PrintNormal(", {0} total", allTestCases.Count);
            Console.WriteLine();
        }

        private void PrintTestCase(TestCaseRunResult testCase)
        {
            switch (testCase.Result)
            {
                case TestCaseRunResult.ResultType.Error:
                    PrintBackgroundRed(" FAIL ");
                    break;
                case TestCaseRunResult.ResultType.Skipped:
                    PrintBackgroundYellow(" SKIP ");
                    break;
                case TestCaseRunResult.ResultType.Success:
                    PrintBackgroundGreen(" PASS ");
                    break;
            }

            PrintNormal("  {0}  ", testCase.ShortName);
            if (testCase.Result == TestCaseRunResult.ResultType.Error && testCase.BrowserIndexWhichCausedError != null)
            {
                PrintGray("(browser={0}) ", testCase.BrowserIndexWhichCausedError);
            }

            PrintGray("in {0}ms", testCase.Duration.TotalMilliseconds);

            Console.WriteLine();

            if (testCase.Logs.Any())
            {
                Console.WriteLine("Logs:");
                foreach (var log in testCase.Logs)
                {
                    var browserIdInfo = log.BrowserId.HasValue ? $" [{log.BrowserId}]" : "";
                    switch (log.Category)
                    {
                        case Log.ERROR:
                            PrintRed("    {1} {2} - ERROR: {0}", log.Message, log.TimeSpan, browserIdInfo);
                            Console.WriteLine();
                            break;

                        case Log.WARNING:
                            PrintYellow("    {1} {2} - Warning: {0}", log.Message, log.TimeSpan, browserIdInfo);
                            Console.WriteLine();
                            break;

                        case Log.INFO:
                            PrintNormal("    {1} {2} - Info: {0}", log.Message, log.TimeSpan, browserIdInfo);
                            Console.WriteLine();
                            break;

                        case Log.DEBUG:
                            PrintGray("    {1} {2} - Debug: {0}", log.Message, log.TimeSpan, browserIdInfo);
                            Console.WriteLine();
                            break;
                    }
                }
            }

            if (testCase.HasErrors())
            {
                PrintRed(" > ");
                PrintNormal(testCase.ErrorMessage);
                Console.WriteLine();
                PrintRed(" > ");
                testCase.PrintException();
            }
        }

        private void PrintNormal(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(format, args);
            Console.ResetColor();
        }

        private void PrintGray(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(format, args);
            Console.ResetColor();
        }

        private void PrintPurple(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(format, args);
            Console.ResetColor();
        }

        private void PrintRed(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(format, args);
            Console.ResetColor();
        }

        private void PrintYellow(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(format, args);
            Console.ResetColor();
        }

        private void PrintGreen(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(format, args);
            Console.ResetColor();
        }

        private void PrintBackgroundGreen(string format, params object[] args)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(format, args);
            Console.ResetColor();
        }

        private void PrintBackgroundYellow(string format, params object[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(format, args);
            Console.ResetColor();
        }

        private void PrintBackgroundRed(string format, params object[] args)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(format, args);
            Console.ResetColor();
        }
    }
}
