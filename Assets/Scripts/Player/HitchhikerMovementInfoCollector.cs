using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Player
{
    public class HitchhikerMovementInfoCollector : MonoBehaviour
    {
        public bool MovementAccepted { get; set; }

        public Vector2 AxisDelta { get; private set; }

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

            this.AxisDelta = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            //Debug.Log(AxisDelta);
            bool up = AxisDelta.y > this._epsilon;
            bool right = AxisDelta.x > this._epsilon;
            bool down = AxisDelta.y < -this._epsilon;
            bool left = AxisDelta.x < -this._epsilon;

            bool movementStateChanged = !(up == this._upPressed && right == this._rightPressed && down == this._downPressed && left == this._leftPressed);

            this._upPressed = up;
            this._downPressed = down;
            this._rightPressed = right;
            this._leftPressed = left;


            //if (this._upPressed != Input.GetKey(this._moveUp))
            //{
            //    this._upPressed = !this._upPressed;
            //    movementStateChanged = true;
            //}
            //if (this._rightPressed != Input.GetKey(this._moveRight))
            //{
            //    this._rightPressed= !this._rightPressed;
            //    movementStateChanged = true;
            //}
            //if (this._downPressed != Input.GetKey(this._moveDown))
            //{
            //    this._downPressed= !this._downPressed;
            //    movementStateChanged = true;
            //}
            //if (this._leftPressed != Input.GetKey(this._moveLeft))
            //{
            //    this._leftPressed = !this._leftPressed;
            //    movementStateChanged = true;
            //}

            if (movementStateChanged)
            {
                this._fireAnimation.AcceptFireActive(this._upPressed, this._rightPressed, this._downPressed, this._leftPressed);
            }
        }

        private bool _upPressed, _rightPressed, _downPressed, _leftPressed;

        [SerializeField] private FireAnimation _fireAnimation;
        [SerializeField] private float _epsilon = 0.1f;

    }
}
