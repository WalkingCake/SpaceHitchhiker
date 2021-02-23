using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Abstraction;
using UnityEngine;


namespace SpaceHitchhiker.Planets
{
    public class PlanetRawInfo : AbstractRawInfo<Planet>
    {
        public string Name { get; }
        public Vector2 Position { get; }
        /// <summary>
        /// Distance from Planet center to Hitchhiker
        /// </summary>
        public float Distance { get; }
        /// <summary>
        /// Hitchhiker path and Camera path after separation angle
        /// </summary>
        public float Angle { get; }

        public PlanetRawInfo(string name, Vector2 position, float distance,
            float angle)
        {
            this.Name = name;
            this.Position = position;
            this.Distance = distance;
            this.Angle = angle;
        }

    }
}
