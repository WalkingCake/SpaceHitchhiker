using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Tools;
using SpaceHitchhiker.Massive;

namespace SpaceHitchhiker.Solars
{
    public class Solar : MonoBehaviour, IInitializeable<Solar>, IMassive<WholeMassive>
    {
        public WholeMassive Massive => this._massive;

        //TODO: add some features later
        public void Initialize(AbstractRawInfo<Solar> info)
        {
            SolarRawInfo solarInfo = info as SolarRawInfo;
            this._touchVelocity = solarInfo.TouchVelocity;
            this._collider.radius = solarInfo.Radius;
            this._spriteRenderer.sprite = TexturePainter.CreateCircle(solarInfo.Radius, this._color);
            this._massive.Initialize(solarInfo.MassiveInfo);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<RigidbodyHandler>(out RigidbodyHandler rbHandler))
            {
                rbHandler.Velocity = this._touchVelocity * (rbHandler.transform.position - this.transform.position).normalized;
                if(rbHandler.Parent is IHitable)
                {
                    (rbHandler.Parent as IHitable).Hit();
                }
            }
        }

        private float _touchVelocity;

        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private Color _color;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private WholeMassive _massive;
    }
}
