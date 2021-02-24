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
        
        public Answer(string id, string text, string nextEventID, params DialogueDependency[] dependencies)
        {
            this.ID = id;
            this.Text = text;
            this.NextEventID = nextEventID;
            this._dependencies = dependencies;
        }

        public bool IsAvailable(IEnumerable<DialogueDependency> playerData)
        {
            foreach(DialogueDependency thisDep in this._dependencies)
            {
                bool accepted = false;
                foreach(DialogueDependency playerDep in playerData)
                {
                    if(playerDep == thisDep)
                    {
                        accepted = true;
                        break;
                    }
                }
                if (!accepted)
                    return false;
            }
            return true;
        }

        private DialogueDependency[] _dependencies;

    }

    public struct DialogueDependency
    {
        public string EventID { get; }
        public string AnswerID { get; }
        public DialogueDependency(string eventID, string answerID)
        {
            this.EventID = eventID;
            this.AnswerID = answerID;
        }

        public static bool operator ==(DialogueDependency left, DialogueDependency right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DialogueDependency left, DialogueDependency right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return this.EventID.GetHashCode() + this.AnswerID.GetHashCode() * 997;
        }

        public override bool Equals(object obj)
        {
            if (obj is DialogueDependency dd)
            {
                return dd.AnswerID == this.AnswerID && this.EventID == dd.EventID;
            }
            return base.Equals(obj);
        }
    }
}
