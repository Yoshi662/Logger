using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
	public static class Logger
	{
		private static object _MessageLock = new();

		public static LoggerConfig Config;

		/// <summary>
		/// Logs into console and file
		/// </summary>
		/// <param name="level">Level of severity</param>
		/// <param name="loginfo">Information to be logged</param>
		public static void Log(LogLevel level, string loginfo, bool flushConsole = false)
		{
			lock (_MessageLock)
			{
				//TODO File Logic Here
				//TODO Add support for events

				if (level.Severity >= Config.MinimumSeverityLevel)
				{
					if (flushConsole) Console.Clear();

					Console.WriteLine($"\u001b[0m{level.GetANSIBackgroundColor()}{level.GetANSIForegroundColor()}" +
					$"[{DateTime.UtcNow:u}][{level.Name,-5}] {loginfo}");
				}
			}
		}
	}

	public class LogLevel
	{
		public string Name { get; private set; }
		public int Severity { get; private set; }
		public bool Highlight { get; private set; }
		public Color Foreground { get; private set; }
		public Color Background { get; private set; }

		public LogLevel(string name, int severity, bool highlight, Color foreground, Color background)
		{
			Name = name;
			Severity = severity;
			Highlight = highlight;
			Foreground = foreground;
			Background = background;
		}

		public string GetANSIForegroundColor()
		{
			return $"\u001b[38;2;{Foreground.R};{Foreground.G};{Foreground.B}m";
		}
		public string GetANSIBackgroundColor()
		{
			return $"\u001b[48;2;{Background.R};{Background.G};{Background.B}m";
		}

		public static readonly LogLevel Trace = new("Trace", 5, false, Color.LightGray, Color.Black);
		public static readonly LogLevel Debug = new("Debug", 10, false, Color.DarkGray, Color.Black);
		public static readonly LogLevel Info = new("Info", 20, false, Color.DeepSkyBlue, Color.Black);
		public static readonly LogLevel Notice = new("Notic", 25, true, Color.Lime, Color.Black);
		public static readonly LogLevel Mark = new("Mark", 30, false, Color.Black, Color.Gold);
		public static readonly LogLevel Warning = new("Warn", 40, false, Color.Yellow, Color.Black);
		public static readonly LogLevel Heading = new("Head", 45, true, Color.Black, Color.CornflowerBlue);
		public static readonly LogLevel Critical = new("Crit", 50, true, Color.Red, Color.Black);
		public static readonly LogLevel Alert = new("Alert", 60, true, Color.Black, Color.DarkRed);
		public static readonly LogLevel Emergency = new("Emerg", 999, true, Color.White, Color.Red);
	}
}
