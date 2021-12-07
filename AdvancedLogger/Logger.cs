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

		public static LoggerConfig Config = LoggerConfig.DefaultConfig;

		//TODO Add documentation and comments
		//TODO Add Log(LogLevel level, Exception e, Eventid...

		/// <summary>
		/// Logs into console and file
		/// </summary>
		/// <param name="level">Level of severity</param>
		/// <param name="loginfo">Information to be logged</param>
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
					SaveToFile(LogMessage);
				}
			}
		}

		//TODO add logic for file rotation
		private static void SaveToFile(string s) => File.AppendAllText(Config.LogFile, s);

		private static string GetDebugInfo(){
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
