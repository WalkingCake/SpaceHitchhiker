using SpaceHitchhiker.Planet;
using SpaceHitchhiker.Tools;
using UnityEngine;

namespace SpaceHitchhiker.Player
{
    public class Hitchhiker : MonoBehaviour
    {
        
        public void UpdatePosition(HitchhickerOffset offset, Vector2 basePosition)
        {
            this.UpdatePosition(basePosition + offset.Vector, offset.AddX, offset.AddY, offset.SubX, offset.SubY);
        }

        public void UpdatePosition(Vector2 offset, bool addX, bool addY, bool subX, bool subY)
        {
            this.transform.position = offset;
            this._animator.SetBool("AddedX", addX);
            this._animator.SetBool("AddedY", addY);
            this._animator.SetBool("SubX", subX);
            this._animator.SetBool("SubY", subY);
        }

        [SerializeField] private Animator _animator;
    }
}
