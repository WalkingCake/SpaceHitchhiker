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
        public string EventID => this._currentEvent.ID;
        public string EventName => this._currentEvent.Name;
        public string EventText => this._currentEvent.Text;
        public Answer[] Answers => this._currentEvent.Answers;


        public static Dialogue Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Dialogue(GameSettings.Instance.StartEvent);
                }
                return _instance;
            }
        }

        private Dialogue(Event startEvent, params DialogueDependency[] dependencies)
        {
            this._currentEvent = startEvent;
            this._importantData = new List<DialogueDependency>(dependencies);
        }

        //public bool PickAnswer(Answer answer)
        //{
            
        //}

        private static Dialogue _instance;
        private Event _currentEvent;
        private readonly List<DialogueDependency> _importantData;
    }
}
