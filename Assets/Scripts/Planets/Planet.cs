using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Abstraction;

namespace SpaceHitchhiker.Planets
{
    public class Planet : MonoBehaviour, IInitializeable<Planet>
    {
        public string Name { get; private set; }

        public static Planet Create(string name, Vector2 position)
        {
            GameObject planetHandler = new GameObject(name);
            planetHandler.transform.position = position;

            Planet planet = planetHandler.AddComponent<Planet>();
            planet.Name = name;

            return planet;
        }

        public Planet Initialize(AbstractRawInfo<Planet> info)
        {
            throw new NotImplementedException();
        }
    }
}
