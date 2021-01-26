using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool MovementAccepted { get; set; }

        private void Awake()
        {
            this._upPressed = false;
            this._rightPressed = false;
            this._downPressed = false;
            this._rightPressed = false;
            this.MovementAccepted = true;
        }

        private void Update()
        {
            if (!this.MovementAccepted)
                return;

            bool movementStateChanged = false;
            if (this._upPressed != Input.GetKey(this._moveUp))
            {
                this._upPressed = !this._upPressed;
                movementStateChanged = true;
            }
            if (this._rightPressed != Input.GetKey(this._moveRight))
            {
                this._rightPressed= !this._rightPressed;
                movementStateChanged = true;
            }
            if (this._downPressed != Input.GetKey(this._moveDown))
            {
                this._downPressed= !this._downPressed;
                movementStateChanged = true;
            }
            if (this._leftPressed != Input.GetKey(this._moveLeft))
            {
                this._leftPressed = !this._leftPressed;
                movementStateChanged = true;
            }

            if(movementStateChanged)
            {
                this._fireAnimation.AcceptFireActive(this._upPressed, this._rightPressed, this._downPressed, this._leftPressed);
            }
        }

        private bool _upPressed, _rightPressed, _downPressed, _leftPressed;

        [SerializeField] private FireAnimation _fireAnimation;

        [SerializeField] private KeyCode _moveUp;
        [SerializeField] private KeyCode _moveRight;
        [SerializeField] private KeyCode _moveDown;
        [SerializeField] private KeyCode _moveLeft;

    }
}
