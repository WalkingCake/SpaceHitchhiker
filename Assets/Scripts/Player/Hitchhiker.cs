using SpaceHitchhiker.Planets;
using SpaceHitchhiker.Tools;
using UnityEngine;
using SpaceHitchhiker.Abstraction;

namespace SpaceHitchhiker.Player
{
    public class Hitchhiker : AbstractMoveable
    {
        public HitchhikerState State { get; set; }
        public float MaxSpeed => this._maxSpeed;

        public float Acceleration => this._acceleration;

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

        public override void MovePrev()
        {
            base.MovePrev();
            if(this.CurrentOffset is SpinOffset)
                this.UpdateRotationAnimator();
        }


        public void BindToPlanet(Planet planet)
        {

        }


        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;

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
