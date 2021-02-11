using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Tools;

namespace SpaceHitchhiker.Planets
{
    public class SpinOffset : Offset
    {
        public bool AddX { get; }
        public bool AddY { get; }
        public bool SubX { get; }
        public bool SubY { get; }

        public static SpinOffset Create(Vector3 startPoint, float radius)
        {
            SpinOffset up = new SpinOffset(Vector2.up * radius, false, true, false, false);
            SpinOffset right = new SpinOffset(Vector2.right * radius, true, false, false, false);
            SpinOffset down = new SpinOffset(Vector2.down * radius, false, false, false, true);
            SpinOffset left = new SpinOffset(Vector2.left * radius, false, false, true, false);

            up.Next = right;
            right.Next = down;
            down.Next = left;
            left.Next = up;

            VectorConverter.FillIntermidiateVectorsArc(up, right, radius);
            VectorConverter.FillIntermidiateVectorsArc(right, down, radius);
            VectorConverter.FillIntermidiateVectorsArc(down, left, radius);
            VectorConverter.FillIntermidiateVectorsArc(left, up, radius);

            up.AddToSequance(startPoint);

            up.RoundSequance();

            return up;
        }

        public SpinOffset() : this(Vector2.zero) { }

        public SpinOffset(Vector2 vector) : this(vector, false, false, false, false) { }

        public SpinOffset(Vector2 vector, bool addX, bool addY, bool subX, bool subY) : base(vector)
        {
            this.AddX = addX;
            this.AddY = addY;
            this.SubX = subX;
            this.SubY = subY;
        }

    }
}
