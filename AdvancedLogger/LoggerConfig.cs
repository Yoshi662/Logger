using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
    public class LoggerConfig
    {
        public string LogFile { get; set; }
        public string LogFolder { get; set; }
        public LogLevel LogLevel { get; set; }
        public bool SaveLogToFile { get; set; }
        public LogRotationMode LogRotationMode { get; set; }
        public uint Size { get; set; }
        public LogRotationTime LogRotationTime { get; set; }
        public bool UseEvents { get; set; }
        public bool UseDebugInfo { get; set; }

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
