using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Massive;

namespace SpaceHitchhiker.Solars
{
    public class SolarRawInfo : AbstractRawInfo<Solar>
    {
        public int Radius { get; }

        public WholeMassiveRawInfo MassiveInfo {get;}
        public SolarRawInfo(int radius, WholeMassiveRawInfo massiveInfo)
        {
            this.Radius = radius;
            this.MassiveInfo = massiveInfo;
        }
    }
}
