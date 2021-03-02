using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Massive;

namespace SpaceHitchhiker.Abstraction
{
    public interface IMassive<T> where T : AbstractMassive<T>
    {
        T Massive { get; }
    }
}
