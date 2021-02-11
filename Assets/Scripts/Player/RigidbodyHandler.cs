﻿using SpaceHitchhiker.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Player
{
    public class RigidbodyHandler : MonoBehaviour
    {
        public Vector2 AxisDelta { private get;  set; }

        public bool MovementAllowed
        {
            get => this._movementAllowed;
            set
            {
                this._movementAllowed = value;
                this.gameObject.SetActive(value);
                if(value)
                {
                    this.Velocity = Vector2.zero;
                    this._rigidbody.WakeUp();
                }
            }
        }
        private void Awake()
        {
            this.Velocity = Vector2.zero;
            this.MovementAllowed = false;
        }
        public Vector2 Position
        {
            get => this._rigidbody.position.RoundVector();
            set
            {
                this._rigidbody.MovePosition(value);
                this._rigidbody.velocity = Vector2.zero;
                this._rigidbody.angularVelocity = 0f;
            }
        }
        public Vector2 Velocity
        {
            get => this._rigidbody.velocity;
            set => this._rigidbody.velocity = value;
        }
        public float MaxVelocity => this._maxVelocity;
        
        private void FixedUpdate()
        {
            if (this.MovementAllowed)
            {
                this._rigidbody.AddForce(this.AxisDelta * this._acceleration, ForceMode2D.Force);
                if (this._rigidbody.velocity.magnitude > this._maxVelocity)
                    this._rigidbody.velocity = this._rigidbody.velocity.normalized * this._maxVelocity;
            }
        }

        private bool _movementAllowed;

        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _acceleration;
        [SerializeField] private Hitchhiker _hitchhiker;
        [SerializeField] private Rigidbody2D _rigidbody;
    }
}