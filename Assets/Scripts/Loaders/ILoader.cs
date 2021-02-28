using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.SolarSystems;

namespace SpaceHitchhiker.Loaders
{
    public interface ILoader
    {
        SolarSystemRawInfo GetSolarSystemInfo();
    }
}
