using System.Drawing;

namespace Logger.AdvancedLogger
{
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

		public static readonly LogLevel Trace = new("Trace", 5, false, Color.DarkGray, Color.Black);
		public static readonly LogLevel Debug = new("Debug", 10, false, Color.LightGray, Color.Black);
		public static readonly LogLevel Info = new("Info", 20, false, Color.DeepSkyBlue, Color.Black);
		public static readonly LogLevel Notice = new("Notic", 25, true, Color.Lime, Color.Black);
		public static readonly LogLevel Mark = new("Mark", 30, false, Color.Black, Color.Gold);
		public static readonly LogLevel Warning = new("Warn", 40, false, Color.Yellow, Color.Black);
		public static readonly LogLevel Heading = new("Head", 45, true, Color.Black, Color.CornflowerBlue);
		public static readonly LogLevel Critical = new("CRIT", 50, true, Color.Red, Color.Black);
		public static readonly LogLevel Alert = new("ALERT", 60, true, Color.Black, Color.DarkRed);
		public static readonly LogLevel Emergency = new("EMERG", 999, true, Color.White, Color.Red);
	}
}
