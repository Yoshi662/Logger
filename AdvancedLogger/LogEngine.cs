using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
	/// <summary>
	/// Class used to handle the "heavy load" of writing into disk and/or RotateFiles
	/// </summary>
	internal class LogEngine
	{
		private object _MessageLock = new();


		private StringBuilder Buffer = new();
		private LoggerConfig Config;
		private FileInfo FileInfo;
		string LogPath;

		public LogEngine(LoggerConfig config)
		{
			Config = config;

			LogPath = $"{config.LogFolder}\\{config.LogFile}";
			FileInfo = new(LogPath);

			if (!Directory.Exists(Config.LogFolder))
				Directory.CreateDirectory(Config.LogFolder);

			new Thread(() =>
			{
				while (true)
				{
					Thread.Sleep(Config.WriteFrequency);

					bool NeedsRotation = false;

					if (Config.LogRotationMode == LogRotationMode.Size)
					{
						if (Config.MaxSize == 0) throw new ArgumentException("You have selected a rotation mode by size, yet size is zero");
						FileInfo.Refresh();
						NeedsRotation = FileInfo.Length >= Config.MaxSize;
					}

					if (Config.LogRotationMode == LogRotationMode.Date)
					{
						switch (Config.LogRotationTime)
						{
							case LogRotationTime.Daily:
								NeedsRotation = FileInfo.CreationTimeUtc.AddDays(1) <= DateTime.UtcNow;
								break;

							case LogRotationTime.Weekly:
								NeedsRotation = FileInfo.CreationTimeUtc.AddDays(7) <= DateTime.UtcNow;
								break;

							case LogRotationTime.Monthly:
								NeedsRotation = FileInfo.CreationTimeUtc.AddMonths(1) <= DateTime.UtcNow;
								break;
						}

						if (FileInfo.CreationTimeUtc.Year == 1601)
							NeedsRotation = false;
					}


					WriteAll();

					if (NeedsRotation)
						Rotate();

				}
			}).Start();
		}

		/// <summary>
		/// Saves the log info the current buffer
		/// </summary>
		/// <param name="input">text made by the logger</param>
		public void Append(string input, int severity)
		{
			lock (Buffer)
			{
				Buffer.Append(input);
				if (severity >= Config.SaveSeverity)
				{
					WriteAll();
				}
			}
		}
		/// <summary>
		/// Forces to write the current buffer into the disk
		/// </summary>
		public void WriteAll()
		{
			lock (Buffer)
			{
				if (Buffer.Length > 0)
				{
					File.AppendAllText(LogPath, Buffer.ToString());
					Buffer.Clear();
				}
			}
		}

		private void Rotate()
		{
			//We assume the file is created
			FileInfo.CreationTimeUtc = DateTime.UtcNow;
			File.Move(LogPath, $"{Config.LogFolder}\\{Config.RotatedLogName}");
		}

		//TODO
		private void CompressFiles()
		{
			throw new NotImplementedException();
		}
	}
}
