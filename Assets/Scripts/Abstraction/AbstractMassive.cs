using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Player;

namespace SpaceHitchhiker.Abstraction
{
    public class AbstractMassive : MonoBehaviour
    {
        protected virtual void FixedUpdate()
        {
            if (this._hitchhiker == null) 
                return;
            float distance = Vector2.Distance(this.transform.position, this._hitchhiker.transform.position);
            Vector2 direction = (this.transform.position - this._hitchhiker.transform.position).normalized;
            this._hitchhiker.RigidbodyHandler.Rigidbody.AddForce(direction * this._acceleration / distance);
        }

        [SerializeField] protected float _acceleration;
        [SerializeField] private Hitchhiker _hitchhiker;
    }
}
