using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Dialogues;

namespace SpaceHitchhiker.Planets
{
    public class Planet : MonoBehaviour, IInitializeable<Planet>
    {
        public string Name { get; private set; }

        public Dialogues.Event PlanetEvent { get; private set; }

        public void Initialize(AbstractRawInfo<Planet> planetRawInfo)
        {
            PlanetRawInfo info = planetRawInfo as PlanetRawInfo;
            
            this.transform.position = info.Position;
            this.gameObject.name = info.Name;

            this.Name = info.Name;
            this.PlanetEvent = EventLibrary.Instance.GetEvent(info.EventID);
            this._planetMovement.Initialize(planetRawInfo);
        }

        [SerializeField] private PlanetMovement _planetMovement;
    }
}
