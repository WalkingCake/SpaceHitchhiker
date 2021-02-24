using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHitchhiker.Dialogues
{
    public class Event
    {
        public string ID { get; }
        public string Name { get; }
        public string Text { get; }
        public Answer[] Answers { get; }

        public Event(string id, string name, string text, Answer[] answers)
        {
            this.ID = id;
            this.Name = name;
            this.Text = text;
            this.Answers = answers;
        }
    }
}
