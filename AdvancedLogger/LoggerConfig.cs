using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
	public class LoggerConfig
	{
		public static readonly LoggerConfig DefaultConfig = new() {
			LogFile = $"{DateTime.UtcNow:yyyyMMdd_hhmmss}.log",
			LogFolder = "Logs",
			MinimumSeverityLevel = 0,
			SaveLogToFile = false,
			LogRotationMode = LogRotationMode.None,
			UseEvents = false,
			ShowDebugInfo = false,
		};

		public string LogFile { get; set; }
		public string LogFolder { get; set; }
		public int MinimumSeverityLevel { get; set; }
		public bool SaveLogToFile { get; set; }
		public LogRotationMode LogRotationMode { get; set; }
		public uint Size { get; set; }
		public LogRotationTime LogRotationTime { get; set; }
		public bool UseEvents { get; set; }
		/// <summary>
		/// This will show both the calls and the line which this log has been called
		/// </summary>
		public bool ShowDebugInfo { get; set; }

		public LoggerConfig LoadConfig(string path)
		{
			throw new NotImplementedException();
		}

		public void SaveConfig(string path, LoggerConfig config)
		{
			throw new NotImplementedException();
		}
	}

	public enum LogRotationMode
    {
        None, Size, Date
    }
    public enum LogRotationTime
    {
        Daily, Weekly, Monthly
    }
}
