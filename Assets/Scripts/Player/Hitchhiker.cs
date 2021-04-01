using SpaceHitchhiker.Planets;
using SpaceHitchhiker.Tools;
using UnityEngine;
using SpaceHitchhiker.Abstraction;
using System.Collections;
using SpaceHitchhiker.Offsets;
using SpaceHitchhiker.Player.ShipParts;

namespace SpaceHitchhiker.Player
{
    public class Hitchhiker : AbstractMoveable, IRigidbodyHandlerOwner
    {
        public SpaceShip Info => this._info;

        public HitchhikerState State
        {
            get => this._stateController.State;
            set => this._stateController.State = value;
        }
        
        public RigidbodyHandler RigidbodyHandler => this._rigidbodyHandler;
        
        public CameraMover CameraMover => this._cameraMover;

        public void UpdateRotationAnimator() => this.UpdateAnimator(this.CurrentOffset as SpinOffset);

        public void UpdateAnimator(SpinOffset offset) => this._stateController.UpdateRotationAnimator(offset.AddX, offset.AddY, offset.SubX, offset.SubY);

        public override void MoveNext()
        {
            base.MoveNext();
            if(this.CurrentOffset is SpinOffset)
                this.UpdateRotationAnimator();
        }

        public void Free(Vector2 flyDirection, float positionSwitchingTime)
        {
            this.State = HitchhikerState.Free;
            this.RigidbodyHandler.MovementAllowed = true;
            //this.RigidbodyHandler.Position = this.transform.position;
            this.RigidbodyHandler.transform.position = this.transform.position;
            this._flight = StartCoroutine(this.Flight(flyDirection, positionSwitchingTime));
        }

        public void BindToPlanet(HitchhikerMovement planet)
        {
            this.State = HitchhikerState.OnPlanet;
            this.RigidbodyHandler.MovementAllowed = false;
            this.RigidbodyHandler.Position = planet.transform.position;
            this._movementInfoCollector.ResetMovementInfo();
            if(this._flight != null)
            {
                StopCoroutine(this._flight);
            }
        }

        private IEnumerator Flight(Vector2 flyDirection, float positionSwitchingTime)
        {
            FreeHitchhikerOffset path = new FreeHitchhikerOffset(this.transform.position, this._rigidbodyHandler,
                flyDirection * this._info.MaxVelocity);
            this.MoveTo(path);
            this._cameraMover.MoveTo(path);

            while(true)
            {
                this.RigidbodyHandler.AxisDelta = this._movementInfoCollector.AxisDelta;
                this.MoveNext();
                this._cameraMover.MoveNext();
                yield return new WaitForSeconds(positionSwitchingTime);
            }

            StopCoroutine(_flight);
            yield break;

        }

        private Coroutine _flight;

        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private RigidbodyHandler _rigidbodyHandler;
        [SerializeField] private HitchhikerControl _movementInfoCollector;
        [SerializeField] private SpaceShip _info;
        [SerializeField] private HitchhikerStateController _stateController;
    }


}
