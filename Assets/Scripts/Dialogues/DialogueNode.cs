using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHitchhiker.Dialogues
{
    public class DialogueNode
    {
        public string ID { get; }
        public string Name { get; }
        public string Text { get; }
        public Answer[] Answers { get; }
        public bool IsImportant { get; }
        public bool IsStableNode { get; }

        public DialogueNode(string id, string name, string text, Answer[] answers,
            bool isImportant = false, bool isStableNode = false)
        {
            this.ID = id;
            this.Name = name;
            this.Text = text;
            this.Answers = answers;
            this.IsImportant = isImportant;
            this.IsStableNode = isStableNode;
        }
    }
}
