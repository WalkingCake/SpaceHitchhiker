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
        public Vector2 AxisDelta { get; private set; }

        private void Awake()
        {
            this.ResetMovementInfo();
        }

        public void ResetMovementInfo()
        {
            this._upPressed = false;
            this._rightPressed = false;
            this._downPressed = false;
            this._rightPressed = false;
            this._fireAnimation.AcceptFireActive(false, false, false, false);
        }

        private void Update()
        {
            if (!this._rbHandler.MovementAllowed)
                return;

            this.AxisDelta = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            bool up = AxisDelta.y > this._epsilon;
            bool right = AxisDelta.x > this._epsilon;
            bool down = AxisDelta.y < -this._epsilon;
            bool left = AxisDelta.x < -this._epsilon;

            bool movementStateChanged = !(up == this._upPressed && right == this._rightPressed && down == this._downPressed && left == this._leftPressed);

            this._upPressed = up;
            this._downPressed = down;
            this._rightPressed = right;
            this._leftPressed = left;

            if (movementStateChanged)
            {
                this._fireAnimation.AcceptFireActive(this._upPressed, this._rightPressed, this._downPressed, this._leftPressed);
            }
        }

        private bool _upPressed, _rightPressed, _downPressed, _leftPressed;

        [SerializeField] private FireAnimation _fireAnimation;
        [SerializeField] private RigidbodyHandler _rbHandler;
        [SerializeField] private float _epsilon = 0.1f;

    }
}
