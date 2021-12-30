using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Logger.IntermediateLogger
{
	public class LoggerConfig
	{
		/// <summary>
		/// Default configuration to the logger. It will show everything in console while not saving anything in disk
		/// </summary>
		public static readonly LoggerConfig DefaultConfig = new()
		{
			LogFile = "Log.txt", //Giving it a name will prevent errors in the future
			LogFolder = "Logs",
			MinimumSeverityLevel = 0,
			SaveLogToFile = false,
			LogRotationMode = LogRotationMode.None,
			UseEvents = false,
			ShowDebugInfo = false,
		};
		/// <summary>
		/// How the most receient or current log will be called
		/// </summary>
		public string LogFile { get; set; }
		/// <summary>
		/// Names for the rotated log names
		/// </summary>
		public string RotatedLogName { get => $"{DateTime.UtcNow:yyyyMMdd_hhmmss}.log"; }
		/// <summary>
		/// Where the logs will be saved
		/// </summary>
		public string LogFolder { get; set; }
		/// <summary>
		/// Minimum level on which the logs will be displayed to both console and files
		/// </summary>
		public int MinimumSeverityLevel { get; set; }
		/// <summary>
		/// Whether or not the log will be written in the disk.
		/// </summary>
		public bool SaveLogToFile { get; set; }
		/// <summary>
		/// Mode for the rotation of files
		/// </summary>
		public LogRotationMode LogRotationMode { get; set; }
		/// <summary>
		/// Reccommended maximum size in bytes for log files.
		/// </summary>
		public uint MaxSize { get; set; }
		/// <summary>
		/// Time between Rotations
		/// </summary>
		public LogRotationTime LogRotationTime { get; set; }
		/// <summary>
		/// Whether or not events will be logged
		/// </summary>
		public bool UseEvents { get; set; }
		/// <summary>
		/// This will show both the calls and the line which this log has been called
		/// </summary>
		public bool ShowDebugInfo { get; set; }

		/// <summary>
		/// Loads a <see cref="LoggerConfig"/> from a file
		/// </summary>
		/// <param name="filepath">Path to the file</param>
		/// <returns>The <see cref="LoggerConfig"/></returns>
		public static LoggerConfig LoadConfig(string filepath) =>
			JsonSerializer.Deserialize<LoggerConfig>(
				File.ReadAllText(filepath)
			);


		/// <summary>
		/// Save current <see cref="LoggerConfig"/> into a file
		/// </summary>
		/// <param name="filepath">Path to the file</param>
		/// <param name="config"><see cref="LoggerConfig"/> to be saved</param>
		public static void SaveConfig(string filepath, LoggerConfig config) =>
			File.WriteAllText(filepath,
				JsonSerializer.Serialize<LoggerConfig>(config,
					new JsonSerializerOptions { WriteIndented = true }
				)
			);
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
