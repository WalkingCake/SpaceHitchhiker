using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Offsets;
using UnityEngine;

namespace SpaceHitchhiker.Planets
{
    public class PlanetMovement : AbstractMoveable, IInitializeable<Planet>
    {
        public void Initialize(AbstractRawInfo<Planet> info)
        {
            PlanetRawInfo planetInfo = info as PlanetRawInfo;
            this.MoveTo(SpinOffset.Create(Vector3.zero, planetInfo.DistanceFromSolar));
            this._positionSwitchingTime = planetInfo.PositionSwitchingTime;
        }

        public void FixedUpdate()
        {
            this._timer += Time.fixedDeltaTime;
            while(this._timer >= this._positionSwitchingTime)
            {
                this._timer -= this._positionSwitchingTime;
                this.MoveNext();
            }
        }

        private float _timer;
        private float _positionSwitchingTime;
    }
}
