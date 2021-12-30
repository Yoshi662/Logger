using System;

namespace Logger.AdvancedLogger
{
	class LoggerExample
	{
		static void Main(string[] args)
		{
				Logger.Instance.Config.ShowDebugInfo = true;
				Logger.Instance.Config.UseEvents = true;
				Logger.Instance.Config.SaveLogToFile = false;
				Logger.Instance.Log(LogLevel.Trace, "This is an example of a message log with different levels of severity", new EventID(14, "ProgramStarto"));
				Logger.Instance.Log(LogLevel.Debug, "This is an example of a message log with different levels of severity", new EventID(101, "LOG_DEBUG"));
				Logger.Instance.Log(LogLevel.Info, "This is an example of a message log with different levels of severity");
				Logger.Instance.Log(LogLevel.Notice, "This is an example of a message log with different levels of severity");
				Logger.Instance.Log(LogLevel.Mark, "This is an example of a message log with different levels of severity");
				Logger.Instance.Log(LogLevel.Warning, "This is an example of a message log with different levels of severity");
				Logger.Instance.Log(LogLevel.Heading, "This is an example of a message log with different levels of severity");
				Logger.Instance.Log(LogLevel.Error, "This is an example of a message log with different levels of severity", new EventID(101, "BAD EXCEPTION"));
				Logger.Instance.Log(LogLevel.Critical, "This is an example of a message log with different levels of severity");
				Logger.Instance.Log(LogLevel.Alert, "This is an example of a message log with different levels of severity");
				Logger.Instance.Log(LogLevel.Emergency, "This is an example of a message log with different levels of severity", new EventID(41, "ProgramEndo"));

				try
				{
					var s = "";
					var e = s[1];
				}
				catch (Exception e)
				{
					Logger.Instance.Log(LogLevel.Critical, e);
				}

				a();

				Console.WriteLine("\u001b[0m"); 
			
		}
		private static void a() { b(); }
		private static void b() { c(); }
		private static void c() { d(); }
		private static void d() { Logger.Instance.Log(LogLevel.Notice, "This is an example of a path", new EventID(234, "BIG_PATH")); }

	}
}
