using System;
using System.IO;

namespace Logger.AdvancedLogger
{
	class LoggerExample
	{
		static void Main(string[] args)
		{
			LoggerConfig config = new LoggerConfig()
			{
				ShowToConsole = true,
				SaveLogToFile = false,
				LogRotationMode = LogRotationMode.Date,
				LogFile = "Current.log",
				LogFolder = "Logs",
				ShowDebugInfo = true,
				UseEvents = true,
				WriteFrequency = 5000,
				LogRotationTime = LogRotationTime.Daily,
				MinimumSeverityLevel = 0,
				SaveSeverity = 999
			};
			if (File.Exists(config.LogFolder + "\\" + config.LogFile))
				File.Delete(config.LogFolder + "\\" + config.LogFile);
			
			Logger.Instance.Start(config);
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

		}
	}

}


/*
 LoggerConfig config = new LoggerConfig()
			{
				ShowToConsole = false,
				SaveLogToFile = true,
				LogRotationMode = LogRotationMode.Date,
				LogFile = "Current.log",
				LogFolder = "Logs",
				ShowDebugInfo = true,
				UseEvents = true,
				WriteFrequency = 5000,
				LogRotationTime = LogRotationTime.Daily,
				MinimumSeverityLevel = 0,
				SaveSeverity = 999
			};

			File.Delete(config.LogFolder + "\\" + config.LogFile);
			Logger.Instance.Start(config);
			for (int i = 0; i < 20; i++)
			{
				new System.Threading.Thread(() =>
				{
					var later = DateTime.Now.AddMinutes(1);
					int counter = 0;
					while (DateTime.Now < later)
					{
						Logger.Instance.Log(LogLevel.Trace, "This is a test", new EventID(counter, $"TH_{i}"));
						counter++;
					}
				}).Start();
			}
 */
