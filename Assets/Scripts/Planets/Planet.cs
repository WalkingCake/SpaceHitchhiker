using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Dialogues;
using SpaceHitchhiker.Player;

namespace SpaceHitchhiker.Planets
{
    public class Planet : MonoBehaviour, IInitializeable<Planet>
    {
        public string Name { get; private set; }

        public DialogueNode PlanetEvent { get; private set; }

        public void Initialize(AbstractRawInfo<Planet> planetRawInfo)
        {
            PlanetRawInfo info = planetRawInfo as PlanetRawInfo;
            
            this.transform.position = info.Position;
            this.gameObject.name = info.Name;

            this.Name = info.Name;
            this.PlanetEvent = EventLibrary.Instance.GetEvent(info.EventID);
            this._planetMovement.Initialize(planetRawInfo);

            this._trigger.radius = info.Diameter / 2;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out RigidbodyHandler rbHandler)
                && rbHandler.Parent is Hitchhiker)
            {
                this._planetMovement.LandToPlanet(rbHandler.Parent as Hitchhiker);
                this._trigger.enabled = false;
            }
        }


        [SerializeField] private CircleCollider2D _trigger;
        [SerializeField] private PlanetMovement _planetMovement;
    }
}
