using System;

namespace Logger.AdvancedLogger
{
	class LoggerExample
	{
		static void Main(string[] args)
		{
			Logger.Config.ShowDebugInfo = true;
			Logger.Config.UseEvents = true;
			Logger.Log(LogLevel.Trace,		"This is an example of a message log with different levels of severity", new EventID(14,"ProgramStarto"));
			Logger.Log(LogLevel.Debug,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Info,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Notice,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Mark,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Warning,	"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Heading,	"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Critical,	"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Alert,		"This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Emergency,	"This is an example of a message log with different levels of severity", new EventID(41, "ProgramEndo"));
			Console.WriteLine("\u001b[0m");
		}
	}
}
