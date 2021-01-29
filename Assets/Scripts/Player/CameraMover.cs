using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Tools;

namespace SpaceHitchhiker.Player
{
    public class CameraMover : AbstractMoveable
    {
        public override void MoveTo(Offset offset)
        {
            base.MoveTo(offset);
            this.transform.position += this._indent;
        }

        [SerializeField] private Vector3 _indent;
    }
}
