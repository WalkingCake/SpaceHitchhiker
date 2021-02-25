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
            this._defaultEvent = new DialogueNode("event0", "Test Event",
                "This event was created for testing event system",
                new Answer[] { new Answer("answer0", "Test Answer", "event0") });
            this._events = new Dictionary<string, DialogueNode>()
            {
                { this._defaultEvent.ID, this._defaultEvent }
            };
        }

        public DialogueNode GetEvent(string id)
        {
            if(this._events.TryGetValue(id, out DialogueNode e))
            {
                return e;
            }
            return this._defaultEvent;
        }

        private readonly Dictionary<string, DialogueNode> _events;
        private readonly DialogueNode _defaultEvent;

        public static EventLibrary _instance;
    }
}
