using SpaceHitchhiker.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace SpaceHitchhiker.Tools
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Zone : MonoBehaviour
    {
        public event Action<RigidbodyHandler> OnRigidbodyEnter;
        public event Action<RigidbodyHandler> OnRigidbodyExit;

        public float Radius
        {
            get => this._collider.radius;
            set => this._collider.radius = value;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out RigidbodyHandler rbHandler))
            {
                this.OnRigidbodyEnter?.Invoke(rbHandler);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out RigidbodyHandler rbHandler))
            {
                this.OnRigidbodyExit?.Invoke(rbHandler);
            }
        }

        [SerializeField] private CircleCollider2D _collider;
    }
}
