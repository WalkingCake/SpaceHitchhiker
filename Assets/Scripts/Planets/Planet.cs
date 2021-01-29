using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Planets
{
    public class Planet : MonoBehaviour
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
    }
}
