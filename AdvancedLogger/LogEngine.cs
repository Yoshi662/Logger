using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
	/// <summary>
	/// Class used to handle the "heavy load" of writing into disk and/or RotateFiles
	/// </summary>
	internal class LogEngine
	{
		uint milisbetweenupdates = 5000;
		string buffer = "";
		public LogEngine(LoggerConfig config)
		{
			//Checks to the config
		}

		public void Append(string input)
		{
			buffer += input;
		}

		private void WriteAll(){
			throw new NotImplementedException();	
		}

		private void Rotate(){
			throw new NotImplementedException();
		}

		private void CompressFiles(){
			throw new NotImplementedException();
		}
	}
}
