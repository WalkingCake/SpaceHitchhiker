using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Player
{
    public class FireAnimation : MonoBehaviour
    {
        public void AcceptFireActive(bool up, bool right, bool down, bool left)
        {
            this._upFireAnim.SetBool("FireActive", down);
            this._rightFireAnim.SetBool("FireActive", left);
            this._downFireAnim.SetBool("FireActive", up);
            this._leftFireAnim.SetBool("FireActive", right);
        }

        [SerializeField] private Animator _upFireAnim;
        [SerializeField] private Animator _rightFireAnim;
        [SerializeField] private Animator _downFireAnim;
        [SerializeField] private Animator _leftFireAnim;
    }
}
