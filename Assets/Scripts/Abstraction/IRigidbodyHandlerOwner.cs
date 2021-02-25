using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Player;

namespace SpaceHitchhiker.Abstraction
{
    public interface IRigidbodyHandlerOwner
    {
        RigidbodyHandler RigidbodyHandler { get; }
    }
}
