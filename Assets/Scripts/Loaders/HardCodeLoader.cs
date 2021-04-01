using SpaceHitchhiker.Planets;
using SpaceHitchhiker.Solars;
using SpaceHitchhiker.SolarSystems;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Massive;

namespace SpaceHitchhiker.Loaders
{
    public class HardCodeLoader : ILoader
    {
        public static ILoader Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new HardCodeLoader();
                }
                return _instance;
            }
        }

        public SolarSystemRawInfo GetSolarSystemInfo() => this._info;

        private HardCodeLoader()
        {
            this._info = new SolarSystemRawInfo(
                new SolarRawInfo(40, 15f, new WholeMassiveRawInfo(15f)),
                new PlanetRawInfo[]
                {
                    new PlanetRawInfo("Plyuk", 80f, 18f, 70f, 13, 0.05f, 0.2f, 0.1f, "event0", new LimitedMassiveRawInfo(12f, 60f)),
                    new PlanetRawInfo("Nibiru", 120f, 20f, 70f, 15, 0.02f, 0.15f, 0.5f, "event0", new LimitedMassiveRawInfo(12f, 60f)),
                    new PlanetRawInfo("Kaleo", 170f, 15f, 70f, 9, 0.02f, 0.15f, 0.8f, "event0", new LimitedMassiveRawInfo(12f, 50f))
                }
                );
        }

        private SolarSystemRawInfo _info;

        private static HardCodeLoader _instance;

    }
}
