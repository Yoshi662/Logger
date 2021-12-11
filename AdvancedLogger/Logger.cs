using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
	public static class Logger
	{
		private static object _MessageLock = new();

		/// <summary>
		/// Current configuration of the logger. It has a Default Configuration
		/// </summary>
		public static LoggerConfig Config = LoggerConfig.DefaultConfig;

		//TODO add docs to LoggerConfig
		//TODO? Add tests
		//TODO? Use a Singleton on this class so I can use a StreamWriter instead of a File.Append to greatly improve performance

		/// <summary>
		/// Logs a message into the console
		/// </summary>
		/// <param name="level">Level of severity</param>
		/// <param name="loginfo">Information to be logged</param>
		/// <param name="eventID">Event ID </param> //TODO
		/// <param name="flushConsole">If true, clears the console before sending the message</param>
		/// <exception cref="ArgumentException">It gets thrown when rotations are enabled but no rotation setting is set</exception>
		public static void Log(LogLevel level, string loginfo, EventID eventID = null, bool flushConsole = false)
		{
			eventID ??= new(0, "");

			lock (_MessageLock) //Locking this part of the code saves us from using a singleton pattern (ok no. but it helps)
			{
				if (level.Severity >= Config.MinimumSeverityLevel)
				{
					if (flushConsole) Console.Clear();

					string AnsiStart = $"{level.GetANSIBackgroundColor()}{level.GetANSIForegroundColor()}";
					string Datetime = $"[{DateTime.UtcNow:u}]";
					string DebugMsg = $"[{level.Name,-5}]";
					string eventname = eventID.Name.Length > 10 ? eventID.Name[..10] : eventID.Name;
					string EventMsg = $"[{eventname,-10}/{eventID.ID:000}]";
					string CallerMethod = GetDebugInfo();
					string AnsiReset = "\u001b[0m";

					string LogMessage = $"{Datetime}{DebugMsg}{(Config.UseEvents ? EventMsg : "")}[{(Config.ShowDebugInfo ? CallerMethod : "")}] {loginfo}";

					Console.WriteLine($"{AnsiReset}{AnsiStart}{LogMessage}{AnsiReset}");

					if (Config.SaveLogToFile) SaveToFile(LogMessage + "\r\n");
				}
			}
		}

		/// <summary>
		/// Logs an exception into the console
		/// </summary>
		/// <param name="level">Level of severity</param>
		/// <param name="exception">Exception to be logged</param>
		/// <param name="eventID">Event ID </param> //TODO
		/// <param name="flushConsole">If true, clears the console before sending the message</param>
		public static void Log(LogLevel level, Exception exception, EventID eventID = null, bool flushConsole = false)
		{
			string output = $"\n{exception.GetType().Name}: {exception.Message}\n{exception.StackTrace}";
			if (exception.InnerException != null)
				output += $"\n{exception.InnerException.GetType().Name}: {exception.InnerException.Message}\n{exception.InnerException.StackTrace}";
			

			Log(level, output, eventID,	flushConsole);
		}


		private static void SaveToFile(string s)
		{
			string logpath = $"{Config.LogFolder}\\{Config.LogFile}";
			FileInfo fileinfo = new(logpath);
			
			bool NeedsRotation = false;

			if (Config.LogRotationMode == LogRotationMode.Size)
			{
				if (Config.Size == 0) throw new ArgumentException("You have selected a rotation mode by size, yet size is zero");

				NeedsRotation = fileinfo.Length >= Config.Size;
			}

			if (Config.LogRotationMode == LogRotationMode.Date)
			{
				switch (Config.LogRotationTime)
				{
					case LogRotationTime.Daily:
						NeedsRotation = fileinfo.CreationTimeUtc.AddMinutes(5) <= DateTime.UtcNow; //TODO change this on release
						break;

					case LogRotationTime.Weekly:
						NeedsRotation = fileinfo.CreationTimeUtc.AddDays(7) <= DateTime.UtcNow;
						break;

					case LogRotationTime.Monthly:
						NeedsRotation = fileinfo.CreationTimeUtc.AddMonths(1) <= DateTime.UtcNow;
						break;
				}
			}
			if (!Directory.Exists(Config.LogFolder))
				Directory.CreateDirectory(Config.LogFolder);
			

			File.AppendAllText(logpath, s);

			if (fileinfo.Exists && NeedsRotation)
			{
				fileinfo.CreationTimeUtc = DateTime.UtcNow;
				File.Move(logpath, $"{Config.LogFolder}\\{Config.RotatedLogName}");
			}
		}
		/// <summary>
		/// It gets info of the caller
		/// </summary>
		/// <returns></returns>
		private static string GetDebugInfo()
		{
			string output = "";

			StackTrace stack = new(true);
			int stackframes = stack.GetFrames().Length;
			for (int i = 1; i < stackframes; i++) //Frame 0 is this Method, and Frame 1 is Log()
			{
				output += stack.GetFrame(i).GetMethod().Name + (i == stackframes - 1 ? "()" : ".");
			}
			output += $"l:{stack.GetFrame(2).GetFileLineNumber():0000}"; //Frame 2 is always the caller of Log()

			return output;
		}
	}
}
