using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Tools;

namespace SpaceHitchhiker.Player
{
    public class EndlessOffset : Offset
    {
        public Vector2 AxisDelta { get; set; }
        public float MaxVelocity => this._maxVelocity;


        public override Offset Next { 
            get
            {
                if(this._next == null)
                {
                    this._next = new EndlessOffset((this.Vector + this._velocity).RoundVector());
                }
                return this._next;
            }
            set { }
        }
        public override Offset Prev
        {
            get => null; 
            set { }
        }

        public EndlessOffset() : base() { }
        public EndlessOffset(Vector2 vector) : this(vector, Vector2.zero, float.MaxValue, 1f) { }

        public EndlessOffset(Vector2 vector, Vector2 velocity, float maxVelocity, float acceleration) : base(vector)
        {
            this.Vector = vector;
            this._velocity = velocity;
            this._maxVelocity = maxVelocity;
            this._acceleration = acceleration;
        }

        public override IEnumerable<Offset> GetSequance()
        {
            yield return this;
            yield break;
        }

        private Vector2 _velocity;
        private Offset _next;

        private float _maxVelocity;
        private float _acceleration;
    }
}
