using SpaceHitchhiker.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace SpaceHitchhiker.Tools
{
    [RequireComponent(typeof(Collider2D))]
    public class Zone : MonoBehaviour
    {
        public event Action OnHitchhikerEnter;
        public event Action OnHitchhikerExit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Hitchhiker hitchhiker))
            {
                this.OnHitchhikerEnter?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Hitchhiker hitchhiker))
            {
                this.OnHitchhikerExit?.Invoke();
            }
        }
    }
}
