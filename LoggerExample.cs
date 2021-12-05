using System;

namespace Logger.AdvancedLogger
{
    class LoggerExample
	{
		static void Main(string[] args)
		{
			Logger.Config = new LoggerConfig {
				LogFile = "log.txt",
				LogFolder = "logs",
				MinimumSeverityLevel = LogLevel.Trace.Severity,
				LogRotationMode = LogRotationMode.None,
				Size =	0,
				LogRotationTime = LogRotationTime.Monthly,
				SaveLogToFile = false,
				ShowCallerMethod = false,
				ShowLineNumber = false, 
				UseEvents = false,
			};

			Logger.Log(LogLevel.Trace,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Debug,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Info,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Notice,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Mark,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Warning,	"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Heading,	"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Critical,	"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Alert,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Emergency, "This is an example of a message log with different levels of severity");
			Console.WriteLine("\u001b[0m");
		}
	}
}
