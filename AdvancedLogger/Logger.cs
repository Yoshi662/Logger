using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
    public class Logger
    {
        private static object _MessageLock = new();

        private static readonly LoggerConfig Config;

        /// <summary>
        /// Logs into console and file
        /// </summary>
        /// <param name="loglevel">Level of severity</param>
        /// <param name="loginfo">Information to be logged</param>
        public static void Log(LogLevel loglevel, string loginfo, bool flushConsole = false)
        {
            lock (_MessageLock)
            {
                if (!File.Exists(Config.LogFile))
                    File.Create(Config.LogFile);

                if (flushConsole)
                {
                    Console.Clear();
                }

                if (loglevel >= Config.LogLevel)
                {

                }
            }
        }


        private static void ChangeConsoleColor(LogLevel l)
        {
            throw new NotImplementedException();
        }
    }

    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Notice,
        Mark,
        Warning,
        Heading,
        Critical,
        Alert,
        Emergency
    }
}
