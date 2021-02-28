using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Dialogues;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Tools;

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

            this._trigger.radius = info.Radius;
            this._spriteRenderer.sprite = TexturePainter.CreateCircle(info.Radius, this._color);


        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out RigidbodyHandler rbHandler)
                && rbHandler.Parent is Hitchhiker)
            {
                this._planetMovement.LandToPlanet(rbHandler.Parent as Hitchhiker);
            }
        }

        public void SetTriggerActive(bool value)
        {
            this._trigger.enabled = value;
        }


        [SerializeField] private CircleCollider2D _trigger;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _color;
        [SerializeField] private PlanetMovement _planetMovement;
    }
}
