using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.AdvancedLogger
{
    public class EventID
    {
        public int ID { get; private set; }
        public string Name { get; private set; }

        public EventID(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }

    public static class Events
    {
    //Example of an event
        public static EventID Queue { get; } = new EventID(1, "PROGRAM_START");
    }
}
