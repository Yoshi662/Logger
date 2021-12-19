using System;

namespace Logger.AdvancedLogger
{
	class LoggerExample
	{
		static void Main(string[] args)
		{

			Logger.Config.ShowDebugInfo = true;
			Logger.Config.UseEvents = true;
			Logger.Config.SaveLogToFile = false;

			Logger.Log(LogLevel.Trace, "This is an example of a message log with different levels of severity", new EventID(14, "ProgramStarto"));
			Logger.Log(LogLevel.Debug, "This is an example of a message log with different levels of severity", new EventID(101, "LOG_DEBUG"));
			Logger.Log(LogLevel.Info, "This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Notice, "This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Mark, "This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Warning, "This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Heading, "This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Error, "This is an example of a message log with different levels of severity", new EventID(101, "BAD EXCEPTION"));
			Logger.Log(LogLevel.Critical, "This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Alert, "This is an example of a message log with different levels of severity");
			Logger.Log(LogLevel.Emergency, "This is an example of a message log with different levels of severity", new EventID(41, "ProgramEndo"));

			try
			{
				var s = "";
				var e = s[1];
			}
			catch (Exception e)
			{
				Logger.Log(LogLevel.Critical, e);
			}

			a();

			Console.WriteLine("\u001b[0m");
		}
		private static void a() { b(); }
		private static void b() { c(); }
		private static void c() { d(); }
		private static void d() { Logger.Log(LogLevel.Notice, "This is an example of a path",  new EventID(234, "BIG_PATH")); }

	}
}
