using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Tools;

namespace SpaceHitchhiker.Dialogues
{
    public class Dialogue
    {
        public Action OnEventChanged;

        public string EventID => this._currentEvent.ID;
        public string EventName => this._currentEvent.Name;
        public string EventText => this._currentEvent.Text;
        public Answer[] Answers => this._currentEvent.Answers.Where(answer => answer.IsAvailable(this._importantData)).ToArray();

        public Dialogue(Event startEvent, params DialogueDependency[] dependencies)
        {
            this._currentEvent = startEvent;
            this._importantData = new List<DialogueDependency>(dependencies);
        }

        public bool PickAnswer(Answer answer)
        {
            if (answer.IsAvailable(this._importantData))
            {
                this._currentEvent = EventLibrary.Instance.GetEvent(answer.NextEventID);
                this.OnEventChanged?.Invoke();
                return true;
            }
            return false;
        }

        private Event _currentEvent;
        private readonly List<DialogueDependency> _importantData;
    }
}
