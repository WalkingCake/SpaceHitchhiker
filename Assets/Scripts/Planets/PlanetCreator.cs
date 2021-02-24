using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Abstraction;

namespace SpaceHitchhiker.Planets
{
    public class PlanetCreator : MonoBehaviour
    {
        private void Start()
        {
            
        }

        public Planet Create(PlanetRawInfo planetRawInfo)
        {
            Planet planet = GameObject.Instantiate(_planetPrefub).GetComponent<Planet>();
            planet.transform.SetParent(this.transform);
            planet.Initialize(planetRawInfo);
            return planet;
        }

        [SerializeField] private GameObject _planetPrefub;
    }
}
