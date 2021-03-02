using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Player;

namespace SpaceHitchhiker.Massive
{
    public class WholeMassive : AbstractMassive<WholeMassive>, IInitializeable<WholeMassive>
    {
        public override void Initialize(AbstractRawInfo<WholeMassive> info)
        {
            WholeMassiveRawInfo massiveInfo = info as WholeMassiveRawInfo;
            this._acceleration = massiveInfo.Acceleration;
        }

        public void Add(RigidbodyHandler rbHandler)
        {
            this._rigidbodies.Add(rbHandler.Rigidbody);
        }

        public void Remove(RigidbodyHandler rbHandler)
        {
            this._rigidbodies.Remove(rbHandler.Rigidbody);
        }
    }
}
