using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Planet
{
    public class HitchhickerOffset
    {
        public HitchhickerOffset Prev { get; set; }
        public HitchhickerOffset Next { get; set; }
        public Vector2 Vector { get; set; }
        
        public bool AddX { get; }
        public bool AddY { get; }
        public bool SubX { get; }
        public bool SubY { get; }

        public HitchhickerOffset(Vector2 vector)
        {
            this.Vector = vector;
            this.AddX = false;
            this.AddY = false;
            this.SubX = false;
            this.SubY = false;
        }

        public HitchhickerOffset(Vector2 vector, bool addX, bool addY, bool subX, bool subY)
        {
            this.Vector = vector;
            this.AddX = addX;
            this.AddY = addY;
            this.SubX = subX;
            this.SubY = subY;
        }

        public override string ToString()
        {
            return this.Vector.ToString();
        }

        public IEnumerable<HitchhickerOffset> GetSequance()
        {
            HitchhickerOffset current = this.Next;
            while(current != this)
            {
                yield return current.Prev;
                current = current.Next;
            }
            yield return current.Prev;
            yield break;
        }
    }
}
