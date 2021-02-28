using SpaceHitchhiker.Planets;
using SpaceHitchhiker.Solars;
using SpaceHitchhiker.SolarSystems;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new SolarRawInfo(0.2f, 20),
                new PlanetRawInfo[]
                {
                    new PlanetRawInfo("Plyuk", new Vector2(60, 0), 18f, 70f, 13, 0.05f, 0.2f, "event0"),
                    new PlanetRawInfo("Nibiru", new Vector2(100, 0), 20f, 70f, 15, 0.05f, 0.2f, "event0")
                }
                );
        }

        private SolarSystemRawInfo _info;

        private static HardCodeLoader _instance;

    }
}
