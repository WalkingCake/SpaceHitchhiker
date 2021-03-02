using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Solars;
using SpaceHitchhiker.Planets;
using SpaceHitchhiker.Loaders;
using SpaceHitchhiker.Player;

namespace SpaceHitchhiker.SolarSystems
{
    public class SolarSystem : MonoBehaviour, IInitializeable<SolarSystem>
    {
        public Solar Solar { get; private set; }
        public Planet[] Planets { get; private set; }

        private void Start()
        {
            ILoader loader = HardCodeLoader.Instance;
            this.Initialize(loader.GetSolarSystemInfo());
        }

        public void Initialize(AbstractRawInfo<SolarSystem> info)
        {
            SolarSystemRawInfo systemInfo = info as SolarSystemRawInfo;

            this.Solar = GameObject.Instantiate(_solarPrefab).GetComponent<Solar>();
            this.Solar.transform.SetParent(this.transform);
            this.Solar.Initialize(systemInfo.SolarInfo);
            this.Solar.Massive.Add(this._hitchhiker.RigidbodyHandler);

            PlanetRawInfo[] planetsInfo = systemInfo.PlanetsInfo;
            this.Planets = new Planet[planetsInfo.Length];
            for(int i = 0; i < this.Planets.Length; i++)
            {
                Planet planet = GameObject.Instantiate(this._planetPrefab).GetComponent<Planet>();
                planet.transform.SetParent(this.transform);
                planet.Initialize(planetsInfo[i]);
                this.Planets[i] = planet;
            }

            this.Planets[0].Land(this._hitchhiker);
        }

        [SerializeField] private GameObject _solarPrefab;
        [SerializeField] private GameObject _planetPrefab;
        [SerializeField] private Hitchhiker _hitchhiker;
    }
}
