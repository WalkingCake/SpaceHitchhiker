using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Tools;
using UnityEngine;

namespace SpaceHitchhiker.Abstraction
{
    public abstract class AbstractMoveable : MonoBehaviour, IMoveable
    {
        public virtual Offset CurrentOffset { get; protected set; }

        public virtual void MoveNext()
        {
            if (this.CurrentOffset.Next != null)
                this.MoveTo(this.CurrentOffset.Next);
        }


        public virtual void MoveTo(Offset offset)
        {
            this.CurrentOffset = offset;
            this.transform.position = offset.Vector;
        }
    }
}
