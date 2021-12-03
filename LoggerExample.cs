using System;

namespace Logger
{
    class LoggerExample
	{
		static void Main(string[] args)
		{
			Logger.Logfile = "YourFile.log";
			Logger.LogLevel = LogLevel.Trace;

			Logger.Log(LogLevel.Trace, "This Log is working");
			Logger.Log(LogLevel.Debug, "And At this point is not breaking");
			Logger.Log(LogLevel.Info, "It's Easy to use and implement into your applications." +
											"\n\t\t\t\tPlus it works using CMD Standard colouring.");
			Logger.Log(LogLevel.Warn, "Although if your application is bigger you might want a more advanced logger.");
			Logger.Log(LogLevel.Crit, "And I hope you don't have to see this");	
		}
	}
}
