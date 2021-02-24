using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHitchhiker.Dialogues
{
    public class Answer
    {
        public string ID { get; }
        public string Text { get; }
        public string NextEventID { get; }
        public DialogueDependency[] Dependencies { get; }
        
        public Answer(string id, string text, string nextEventID, params DialogueDependency[] dependencies)
        {
            this.ID = id;
            this.Text = text;
            this.NextEventID = nextEventID;
            this.Dependencies = dependencies;
        }

        //public bool IsAvailable(IEnumerable<DialogueDependency> playerData)
        //{
            
        //}

    }

    public struct DialogueDependency
    {
        public Event Event { get; }
        public Answer Answer { get; }
        public DialogueDependency(Event e, Answer a)
        {
            this.Event = e;
            this.Answer = a;
        }
    }
}
