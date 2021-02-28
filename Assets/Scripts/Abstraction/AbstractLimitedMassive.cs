using SpaceHitchhiker.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Abstraction
{
    public class AbstractLimitedMassive : AbstractMassive
    {
        private void Awake()
        {
            this._hitchhikerInZone = false;
            this._zone.OnHitchhikerEnter += this.OnHitchhikerEnter;
            this._zone.OnHitchhikerExit += this.OnHitchhikerExit;

        }

        protected override void FixedUpdate()
        {
            if(this._hitchhikerInZone)
                base.FixedUpdate();
        }

        private void OnHitchhikerEnter()
        {
            this._hitchhikerInZone = true;
        }
        
        private void OnHitchhikerExit()
        {
            this._hitchhikerInZone = false;
        }

        private bool _hitchhikerInZone;
        [SerializeField] private Zone _zone;
    }
}
