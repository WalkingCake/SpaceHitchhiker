using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Solars;
using SpaceHitchhiker.Planets;

namespace SpaceHitchhiker.SolarSystems
{
    public class SolarSystemRawInfo : AbstractRawInfo<SolarSystem>
    {
        public SolarRawInfo SolarInfo { get; }
        public PlanetRawInfo[] PlanetsInfo { get; }

        public SolarSystemRawInfo(SolarRawInfo solarInfo, PlanetRawInfo[] planetsInfo)
        {
            this.SolarInfo = solarInfo;
            this.PlanetsInfo = planetsInfo;
        }
    }
}
