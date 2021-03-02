using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Massive
{
    public class LimitedMassive : AbstractMassive<LimitedMassive>, IInitializeable<LimitedMassive>
    {
        public override void Initialize(AbstractRawInfo<LimitedMassive> info)
        {
            LimitedMassiveRawInfo massiveInfo = info as LimitedMassiveRawInfo;
            this._acceleration = massiveInfo.Acceleration;
            this._zone.Radius = massiveInfo.Radius;
        }

        private void Awake()
        {
            this._zone.OnRigidbodyEnter += this.OnHitchhikerEnter;
            this._zone.OnRigidbodyExit += this.OnHitchhikerExit;
            this._rigidbodies = new HashSet<Rigidbody2D>();
        }

        private void OnHitchhikerEnter(RigidbodyHandler rbHandler)
        {
            this._rigidbodies.Add(rbHandler.Rigidbody);
        }
        
        private void OnHitchhikerExit(RigidbodyHandler rbHandler)
        {
            this._rigidbodies.Remove(rbHandler.Rigidbody);
        }

        [SerializeField] private Zone _zone;
    }
}
