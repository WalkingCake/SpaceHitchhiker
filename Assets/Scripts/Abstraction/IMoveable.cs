using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Offsets;
using SpaceHitchhiker.Tools;

namespace SpaceHitchhiker.Abstraction
{
    public interface IMoveable
    {
        Offset CurrentOffset { get; }
        void MoveNext();
        void MoveTo(Offset offset);

    }
}
