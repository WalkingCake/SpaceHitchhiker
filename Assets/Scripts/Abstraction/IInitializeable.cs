using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHitchhiker.Abstraction
{
    public interface IInitializeable<T> where T : IInitializeable<T>
    {
        T Initialize(AbstractRawInfo<T> info);
    }
}
