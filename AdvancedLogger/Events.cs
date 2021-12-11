using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
	/// <summary>
	/// Events are used to quick search though log files
	/// </summary>
	public class EventID
	{
		/// <summary>
		/// Numeric ID used to identify events
		/// </summary>
		public int ID { get; private set; }
		/// <summary>
		/// Name of the event. Keep in mind that this name will be cropped to 10 characters on the log
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Creates a new <see cref="EventID"/>
		/// </summary>
		public EventID(int id, string name)
		{
			ID = id;
			Name = name;
		}
	}
	/// <summary>
	/// Static class used to hold events for your application
	/// </summary>
	public static class Events
	{
		// Example of an event
		// public static EventID Queue { get; } = new EventID(1, "PROGRAM_START");
	}
}
