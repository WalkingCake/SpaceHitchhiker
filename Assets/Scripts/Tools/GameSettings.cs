using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Dialogues;

namespace SpaceHitchhiker.Tools
{
    public class GameSettings
    {
        public static GameSettings Instance
        {
            get
            {
                if (GameSettings._instance == null)
                    GameSettings._instance = new GameSettings();
                return GameSettings._instance;
            }
        }

        public KeyCode SeparateKey { get; }

        public Dialogues.DialogueNode StartEvent { get; }


        private GameSettings()
        {
            this.SeparateKey = KeyCode.Space;
        }

        private static GameSettings _instance;
    }
}
