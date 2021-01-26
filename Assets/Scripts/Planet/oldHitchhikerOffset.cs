using System;
using UnityEngine;

namespace SpaceHitchhiker.Planet
{
    [Obsolete]
    public struct oldHitchhikerOffset
    {
        public Vector2 Offset { get; }
        public bool AddedX { get; }
        public bool AddedY { get; }
        public bool SubX { get; }
        public bool SubY { get; }

        public oldHitchhikerOffset(Vector2 offset, bool addedX, bool addedY, bool subX, bool subY)
        {
            Offset = offset;
            AddedX = addedX;
            AddedY = addedY;
            SubX = subX;
            SubY = subY;
        }

    }
}
