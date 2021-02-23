using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHitchhiker.Abstraction
{
    public abstract class AbstractRawInfo<T> where T : IInitializeable<T>
    {
        public Type Type => typeof(T);
    }
}
