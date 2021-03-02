using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Tools;
using SpaceHitchhiker.Massive;

namespace SpaceHitchhiker.Solars
{
    public class Solar : MonoBehaviour, IInitializeable<Solar>, IMassive<WholeMassive>
    {
        public WholeMassive Massive => this._massive;

        //TODO: add some features later
        public void Initialize(AbstractRawInfo<Solar> info)
        {
            SolarRawInfo solarInfo = info as SolarRawInfo;
            this._spriteRenderer.sprite = TexturePainter.CreateCircle(solarInfo.Radius, this._color);
            this._massive.Initialize(solarInfo.MassiveInfo);
        }

        [SerializeField] private Color _color;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private WholeMassive _massive;
    }
}
