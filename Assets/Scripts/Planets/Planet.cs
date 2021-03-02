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
using SpaceHitchhiker.Massive;

namespace SpaceHitchhiker.Planets
{
    public class Planet : MonoBehaviour, IInitializeable<Planet>, IMassive<LimitedMassive>
    {
        public string Name { get; private set; }
        public DialogueNode PlanetEvent { get; private set; }
        public LimitedMassive Massive => this._massive;

        public PlanetMovement PlanetMovement => this._planetMovement;

        public void Initialize(AbstractRawInfo<Planet> planetRawInfo)
        {
            PlanetRawInfo info = planetRawInfo as PlanetRawInfo;
            
            this.gameObject.name = info.Name;

            this.Name = info.Name;
            this.PlanetEvent = EventLibrary.Instance.GetEvent(info.EventID);
            this._hitchhikerMovement.Initialize(planetRawInfo);

            this._trigger.radius = info.Radius;
            this._spriteRenderer.sprite = TexturePainter.CreateCircle(info.Radius, this._color);

            this._massive.Initialize(info.MassiveInfo);

            this._planetMovement.Initialize(planetRawInfo);
        }

        public void SetTriggerActive(bool value)
        {
            this._trigger.enabled = value;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out RigidbodyHandler rbHandler)
                && rbHandler.Parent is Hitchhiker)
            {
                this._hitchhikerMovement.LandToPlanet(rbHandler.Parent as Hitchhiker);
            }
        }

        public void Land(Hitchhiker hitchhiker)
        {
            this._hitchhikerMovement.LandToPlanet(hitchhiker);
        }


        [SerializeField] private CircleCollider2D _trigger;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _color;
        [SerializeField] private HitchhikerMovement _hitchhikerMovement;
        [SerializeField] private PlanetMovement _planetMovement;
        [SerializeField] private LimitedMassive _massive;
    }
}
