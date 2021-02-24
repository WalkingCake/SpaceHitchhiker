using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHitchhiker.Dialogues
{
    public class EventLibrary
    {
        public static EventLibrary Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new EventLibrary();
                }
                return _instance;
            }
        }

        private EventLibrary()
        {
            this._defaultEvent = new Event("event0", "Test Event",
                "This event was created for testing event system",
                new Answer[] { new Answer("answer0", "Test Answer", "event0") });
            this._events = new Dictionary<string, Event>()
            {
                { this._defaultEvent.ID, this._defaultEvent }
            };
        }

        public Event GetEvent(string id)
        {
            if(this._events.TryGetValue(id, out Event e))
            {
                return e;
            }
            return this._defaultEvent;
        }

        private readonly Dictionary<string, Event> _events;
        private readonly Event _defaultEvent;

        public static EventLibrary _instance;
    }
}
