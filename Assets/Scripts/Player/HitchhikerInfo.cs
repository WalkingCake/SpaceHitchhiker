using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Dialogues;

namespace SpaceHitchhiker.Player
{
    public class HitchhikerInfo : MonoBehaviour 
    {
        public float MaxVelocity => this._maxVelocity;

        public float Acceleration => this._acceleration;
        
        public int HP => this._hp;

        public int MaxHP => this._maxHP;

        public Dialogue Dialogue { get; private set; }

        private void Awake()
        {
            this.Dialogue = new Dialogue(EventLibrary.Instance.GetEvent(this._eventID));
        }

        [SerializeField] private string _eventID;
        [SerializeField] private int _hp = 3;
        [SerializeField] private int _maxHP = 3;
        [SerializeField] private float _maxVelocity = 10f;
        [SerializeField] private float _acceleration = 7f;
    }
}
