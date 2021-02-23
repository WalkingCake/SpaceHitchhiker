using SpaceHitchhiker.Player;
using SpaceHitchhiker.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Offsets
{
    public class FreeHitchhikerOffset : EndlessOffset
    {
        public override Offset Next 
        {
            get
            {
                if (this._next == null)
                {
                    this._next = new FreeHitchhikerOffset(this._rigidbodyHandler.Position, this._rigidbodyHandler);
                    this.Next = this._next;
                }
                return this._next;
            }
            set => this._next = value; 
        }

        public FreeHitchhikerOffset(Vector2 vector, RigidbodyHandler rigidbodyHandler) : base(vector)
        {
            this._rigidbodyHandler = rigidbodyHandler;
        }

        public FreeHitchhikerOffset(Vector2 vector, RigidbodyHandler rigidbodyHandler, Vector2 velocity) : this(vector, rigidbodyHandler)
        {
            this._rigidbodyHandler.Velocity = velocity;
        }

        public Offset _next;
        private readonly RigidbodyHandler _rigidbodyHandler;
    }
}
