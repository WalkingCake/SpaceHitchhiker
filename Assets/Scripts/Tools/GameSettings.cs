using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
        public float MaxVelocity { get; }
        public float Acceleration { get; }


        private GameSettings()
        {
            this.SeparateKey = KeyCode.Space;
            this.MaxVelocity = 10f;
            this.Acceleration = 7f;

        }

        private static GameSettings _instance;
    }
}
