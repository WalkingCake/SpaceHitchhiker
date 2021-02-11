using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Tools;

namespace SpaceHitchhiker.Player
{
    public abstract class EndlessOffset : Offset
    {
        public override abstract Offset Next { get; set; }

        public EndlessOffset() : base() { }

        public EndlessOffset(Vector2 vector) : base(vector) { }

        public override IEnumerable<Offset> GetSequance()
        {
            yield return this;
            yield break;
        }
    }
}
