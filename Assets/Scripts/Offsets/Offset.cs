﻿using System.Collections.Generic;
using UnityEngine;

namespace SpaceHitchhiker.Offsets
{
    public class Offset
    {
        public virtual Offset Next { get; set; }
        public virtual bool HasNext => this.Next != null;
        public virtual Vector2 Vector { get; set; }

        public Offset() : this(Vector2.zero) { }

        public Offset(Vector2 vector)
        {
            this.Vector = vector;
        }

        public override string ToString() => this.Vector.ToString();

        public virtual IEnumerable<Offset> GetSequance()
        {
            Offset current = this;
            while(current != null)
            {
                yield return current;
                current = current.Next;
                if (current == this)
                    break;
            }
            yield break; 
        }
    }
}
