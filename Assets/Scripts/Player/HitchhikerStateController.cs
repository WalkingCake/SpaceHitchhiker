using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

namespace SpaceHitchhiker.Player
{
    public class HitchhikerStateController : MonoBehaviour
    {
        public HitchhikerState State
        {
            get => this._state;
            set
            {
                this._state = value;
                this._animator.runtimeAnimatorController = this._animControllers[value];
            }
        }
        private void Awake()
        {
            this._animControllers = new Dictionary<HitchhikerState, AnimatorController>
            {
                { HitchhikerState.OnPlanet, this._bornController },
                { HitchhikerState.InOrbit, this._rotationController },
                { HitchhikerState.Transformating, this._transformationController },
                { HitchhikerState.Free, this._freeController }
            };

        }

        public void UpdateRotationAnimator(bool addX, bool addY, bool subX, bool subY)
        {
            if (this._state != HitchhikerState.InOrbit)
                return;
            this._animator.SetBool("AddedX", addX);
            this._animator.SetBool("AddedY", addY);
            this._animator.SetBool("SubX", subX);
            this._animator.SetBool("SubY", subY);
        }

        private HitchhikerState _state;
        private Dictionary<HitchhikerState, AnimatorController> _animControllers;

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimatorController _rotationController;
        [SerializeField] private AnimatorController _transformationController;
        [SerializeField] private AnimatorController _freeController;
        [SerializeField] private AnimatorController _bornController;
    }
    public enum HitchhikerState
    {
        OnPlanet,
        InOrbit,
        Transformating,
        Free
    }
}
