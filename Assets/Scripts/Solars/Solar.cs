using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Tools;

namespace SpaceHitchhiker.Solars
{
    public class Solar : AbstractMassive, IInitializeable<Solar>
    {
        //TODO: add some features later
        public void Initialize(AbstractRawInfo<Solar> info)
        {
            SolarRawInfo solarInfo = info as SolarRawInfo;
            this._acceleration = solarInfo.Acceleration;
            this._spriteRenderer.sprite = TexturePainter.CreateCircle(solarInfo.Radius, this._color);
        }

        [SerializeField] private Color _color;
        [SerializeField] private SpriteRenderer _spriteRenderer;
    }
}
