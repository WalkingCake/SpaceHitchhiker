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
        public event Action OnEventChanged;
        public event Action OnDialogueEnded;

        public string EventID => this._currentEvent.ID;
        public string EventName => this._currentEvent.Name;
        public string EventText => this._currentEvent.Text;
        public Answer[] Answers => this._currentEvent.Answers.Where(answer => answer.IsAvailable(this._importantData)).ToArray();

        public Dialogue(DialogueNode startEvent, params DialogueDependency[] dependencies)
        {
            this._currentEvent = startEvent;
            this._importantData = new List<DialogueDependency>(dependencies);
            this._stableNodes = new Stack<DialogueNode>();
        }

        public bool PickAnswer(Answer answer)
        {
            if (answer.IsAvailable(this._importantData))
            {
                if(this._currentEvent.IsImportant)
                {
                    this._importantData.Add(new DialogueDependency(this._currentEvent.ID, answer.ID));
                }

                if(this._currentEvent.IsStableNode)
                {
                    this._stableNodes.Push(this._currentEvent);
                }

                this._currentEvent = EventLibrary.Instance.GetEvent(answer.NextEventID);
                if(this._currentEvent == null)
                {
                    if (this._stableNodes.Count > 0)
                        this._currentEvent = this._stableNodes.Pop();
                    else
                        this.OnDialogueEnded?.Invoke();
                }
                
                this.OnEventChanged?.Invoke();

                return true;
            }
            return false;
        }

        private readonly Stack<DialogueNode> _stableNodes;
        private DialogueNode _currentEvent;
        private readonly List<DialogueDependency> _importantData;
    }
}
