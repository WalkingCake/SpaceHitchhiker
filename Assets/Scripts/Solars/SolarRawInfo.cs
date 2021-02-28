using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Abstraction;

namespace SpaceHitchhiker.Solars
{
    public class SolarRawInfo : AbstractRawInfo<Solar>
    {
        public float Acceleration { get; }
        public int Radius { get; }
        public SolarRawInfo(float acceleration, int radius)
        {
            this.Acceleration = acceleration;
            this.Radius = radius;
        }
    }
}
