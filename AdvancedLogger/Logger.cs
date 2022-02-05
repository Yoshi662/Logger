using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
	public class Logger
	{
		private static object _MessageLock = new();
		private const string AnsiReset = "\u001b[0m";
		private bool running = false;

		/// <summary>
		/// Current configuration of the logger. It has a Default Configuration
		/// </summary>
		private LoggerConfig Config;
		private LogEngine Engine;

		private static Logger _instance;

		/// <summary>
		/// Singleton Instance of the Logger
		/// </summary>
		public static Logger Instance
		{
			get
			{
				lock (_MessageLock)
				{
					return _instance ?? (_instance ??= new Logger());
				}
			}
		}


		private Logger()
		{
		}
		/// <summary>
		/// Starts the logger
		/// </summary>
		/// <param name="config">Configuration of the logger, if null or none provided will default to <see cref="LoggerConfig.DefaultConfig"/></param>
		public void Start(LoggerConfig config = null)
		{
			Config = config ?? LoggerConfig.DefaultConfig;
			if (Config.SaveLogToFile)
				Engine = new(Config);
			
			running = true;
		}
		/// <summary>
		/// Saves all to file and 
		/// </summary>
		public void Stop()
		{
			if (Config.SaveLogToFile)
				Engine.WriteAll();

			running = false;
			Engine = null;
			Config = null;
		}

		/// <summary>
		/// Logs a message into the console
		/// </summary>
		/// <param name="level">Level of severity</param>
		/// <param name="loginfo">Information to be logged</param>
		/// <param name="eventID">Event ID </param> //TODO
		/// <param name="flushConsole">If true, clears the console before sending the message</param>
		/// <exception cref="ArgumentException">It gets thrown when rotations are enabled but no rotation setting is set</exception>
		public void Log(LogLevel level, string loginfo, EventID eventID = null, bool flushConsole = false)
		{
			if (!running)
				return;

			eventID ??= new(0, "");

			if (level.Severity >= Config.MinimumSeverityLevel)
			{
				if (flushConsole) { Console.Clear(); Debug.Flush(); }

				string AnsiStart = $"{level.GetANSIBackgroundColor()}{level.GetANSIForegroundColor()}";
				string Datetime = $"[{DateTime.UtcNow:u}]";
				string DebugMsg = $"[{level.Name,-5}]";
				string eventname = eventID.Name.Length > 10 ? eventID.Name[..10] : eventID.Name;
				string EventMsg = $"[{eventname,-10}/{eventID.ID:000}]";

				string LogMessage = $"{Datetime}{DebugMsg}{(Config.UseEvents ? EventMsg : "")}[{(Config.ShowDebugInfo ? GetDebugInfo() : "")}] {loginfo}";

				if (Config.ShowToConsole) Console.WriteLine($"{AnsiReset}{AnsiStart}{LogMessage}{AnsiReset}");
				if (Config.ShowToDebug) 
				Debug.WriteLine($"{LogMessage}");

				if (Config.SaveLogToFile) Engine.Append(LogMessage + "\r\n", level.Severity);
			}

		}

		/// <summary>
		/// Logs an exception into the console
		/// </summary>
		/// <param name="level">Level of severity</param>
		/// <param name="exception">Exception to be logged</param>
		/// <param name="eventID">Event ID </param> //TODO
		/// <param name="flushConsole">If true, clears the console before sending the message</param>
		public void Log(LogLevel level, Exception exception, EventID eventID = null, bool flushConsole = false)
		{
			string output = $"\n{exception.GetType().Name}: {exception.Message}\n{exception.StackTrace}";
			if (exception.InnerException != null)
				output += $"\n{exception.InnerException.GetType().Name}: {exception.InnerException.Message}\n{exception.InnerException.StackTrace}";


			Log(level, output, eventID, flushConsole);
		}

		/// <summary>
		/// It gets info of the caller
		/// </summary>
		/// <returns></returns>
		private string GetDebugInfo()
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

		/// <summary>
		/// Writes all the buffer into the file
		/// </summary>
		public void ForceSave()
		{
			Engine.WriteAll();
		}
	}
}
