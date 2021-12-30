using System.Drawing;

namespace Logger.IntermediateLogger
{
	/// <summary>
	/// Represents a level of severity on your application
	/// </summary>
	public class LogLevel
	{
		/// <summary>
		/// Name of the level. It will get cropped to 5 characters.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Level of Severity
		/// </summary>
		public int Severity { get; private set; }
		public Color Foreground { get; private set; }
		public Color Background { get; private set; }

		public LogLevel(string name, int severity, Color foreground, Color background)
		{
			Name = name;
			Severity = severity;
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
		/// <summary>
		/// Useful to log everything
		/// </summary>
		public static readonly LogLevel Trace = new("Trace", 5, Color.DarkGray, Color.Black);
		/// <summary>
		/// Useful to debug info
		/// </summary>
		public static readonly LogLevel Debug = new("Debug", 10, Color.LightGray, Color.Black);
		/// <summary>
		/// Useful information or display of the normal flow of the program
		/// </summary>
		public static readonly LogLevel Info = new("Info", 20, Color.DeepSkyBlue, Color.Black);
		/// <summary>
		/// Useful to log handled scenarios
		/// </summary>
		public static readonly LogLevel Warning = new("Warn", 40, Color.Yellow, Color.Black);
		/// <summary>
		/// Useful to log handled exceptions
		/// </summary>
		public static readonly LogLevel Error = new("Error", 50, Color.DarkOrange, Color.Black);
		/// <summary>
		/// Useful to log unhandled exceptions
		/// </summary>
		public static readonly LogLevel Critical = new("CRIT", 60, Color.Red, Color.Black);

	}
}
