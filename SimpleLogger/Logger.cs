using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.SimpleLogger
{
    public static class Logger
    {
        private static object _MessageLock = new();

        /// <summary>
        /// File where the log will be written
        /// </summary>
        public static string Logfile = @"log.txt";

        /// <summary>
        /// Minimum set of severty where info will be displayed
        /// </summary>
        public static LogLevel LogLevel = LogLevel.Info;

        /// <summary>
        /// Logs into console and file
        /// </summary>
        /// <param name="loglevel">Level of severity</param>
        /// <param name="loginfo">Information to be logged</param>
        public static void Log(LogLevel loglevel, string loginfo, bool flushConsole = false)
        {
            lock (_MessageLock)
            {
                if (!File.Exists(Logfile))
                    File.Create(Logfile);

                if (flushConsole)
                {
                    Console.Clear();
                }

                if (loglevel >= Logger.LogLevel)
                {
                    string sLog = loglevel.ToString().PadRight(5);
                    ChangeConsoleColor(loglevel);
                    string msg = $"[{DateTime.Now:u}]";
                    Console.Write(msg);
                    Console.Write($"[{sLog}]");
                    Console.ResetColor();
                    Console.Write(" " + loginfo + "\r\n");

                    File.AppendAllText(Logfile, $"[{DateTime.Now:u}][{sLog}]: {loginfo}\r\n");
                }
            }
        }


        private static void ChangeConsoleColor(LogLevel l)
        {
            switch (l)
            {
                case LogLevel.Trace:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case LogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case LogLevel.Crit:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
            }
        }
    }

    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warn,
        Crit
    }
}
