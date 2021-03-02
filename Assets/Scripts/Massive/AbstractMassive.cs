using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Abstraction;

namespace SpaceHitchhiker.Massive
{
    public abstract class AbstractMassive<T> : MonoBehaviour, IInitializeable<T>
        where T : AbstractMassive<T>
    {
        public abstract void Initialize(AbstractRawInfo<T> info);
        private void Awake()
        {
            this._rigidbodies = new HashSet<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            foreach (Rigidbody2D rb in this._rigidbodies)
            {
                this.ApplyForce(rb);
            }
        }

        private void ApplyForce(Rigidbody2D rb)
        {
            this._distance = Vector2.Distance(this.transform.position, rb.position);
            this._direction = ((Vector2)this.transform.position - rb.position).normalized;
            rb.AddForce(_direction * this._acceleration / _distance);
        }


        private float _distance;
        private Vector2 _direction;
        protected HashSet<Rigidbody2D> _rigidbodies;

        protected float _acceleration;
    }
}
