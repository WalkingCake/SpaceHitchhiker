using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Abstraction;

namespace SpaceHitchhiker.Massive
{
    public class LimitedMassiveRawInfo : AbstractRawInfo<LimitedMassive>
    {
        public float Acceleration { get; }
        public float Radius { get; }

        public LimitedMassiveRawInfo(float acceleration, float radius)
        {
            this.Acceleration = acceleration;
            this.Radius = radius;
        }
    }

    public class WholeMassiveRawInfo : AbstractRawInfo<WholeMassive>
    {
        public float Acceleration { get; }

        public WholeMassiveRawInfo(float acceleration)
        {
            this.Acceleration = acceleration;
        }
    }
}
