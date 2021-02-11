using SpaceHitchhiker.Planets;
using SpaceHitchhiker.Tools;
using UnityEngine;
using SpaceHitchhiker.Abstraction;
using System.Collections;

namespace SpaceHitchhiker.Player
{
    public class Hitchhiker : AbstractMoveable
    {
        public HitchhikerState State { get; set; }
        public RigidbodyHandler RigidbodyHandler => this._rigidbodyHandler;

        private void Awake()
        {
            this.State = HitchhikerState.InOrbit;
            this.CurrentOffset = new SpinOffset(this.transform.position);
        }

        public void UpdateRotationAnimator() => this.UpdateAnimator(this.CurrentOffset as SpinOffset);

        public void UpdateAnimator(SpinOffset offset) => this.UpdateAnimator(offset.AddX, offset.AddY, offset.SubX, offset.SubY);

        public void UpdateAnimator(bool addX, bool addY, bool subX, bool subY)
        {
            this._rotationAnimator.SetBool("AddedX", addX);
            this._rotationAnimator.SetBool("AddedY", addY);
            this._rotationAnimator.SetBool("SubX", subX);
            this._rotationAnimator.SetBool("SubY", subY);
        }

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
            this.RigidbodyHandler.Position = this.transform.position;
            this._flight = StartCoroutine(this.Flight(flyDirection, positionSwitchingTime));
        }

        public void BindToPlanet(Planet planet)
        {

        }

        private IEnumerator Flight(Vector2 flyDirection, float positionSwitchingTime)
        {
            FreeHitchhikerOffset path = new FreeHitchhikerOffset(this.transform.position, this._rigidbodyHandler,
                flyDirection * this._rigidbodyHandler.MaxVelocity);
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
        [SerializeField] private HitchhikerMovementInfoCollector _movementInfoCollector;
        [SerializeField] private Animator _rotationAnimator;
    }

    public enum HitchhikerState
    {
        OnPlanet,
        InOrbit,
        Transformating,
        Free
    }
}
