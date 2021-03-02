using UnityEngine;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Tools;
using System.Collections;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Offsets;
using System;

namespace SpaceHitchhiker.Planets
{
    public class HitchhikerMovement : MonoBehaviour, IInitializeable<Planet>
    {
        public void Initialize(AbstractRawInfo<Planet> planetRawInfo)
        {
            PlanetRawInfo info = planetRawInfo as PlanetRawInfo;
            this._angle = info.Angle;
            this._distanceFromCenter = info.DistanceToHitchhiker;
            this._bornDeltaTime = info.BornDeltaTime;
            this._spinDeltaTime = info.SpinDeltaTime;
        }

        private void Update()
        {
            if (this._rotationPlanet != null && Input.GetKeyDown(GameSettings.Instance.SeparateKey))
                this._timeToSeparate = true;
        }


        public void LandToPlanet(Hitchhiker hitchhiker)
        {
            hitchhiker.BindToPlanet(this);
            this._parent.SetTriggerActive(false);

            this._rotationPlanet = StartCoroutine(this.RotateAroundPlanet(hitchhiker));
            this._planetOffset = Vector2.zero;

            this._planetMovedAction = () => this.OnPlanetMoved(hitchhiker);
            this._previousPlanetPosition = this.transform.position;
            this._parent.PlanetMovement.OnPositionChanged += this._planetMovedAction;
        }

        private void OnPlanetMoved(Hitchhiker hitchhiker)
        {
            this._planetOffset = (Vector2)this.transform.position - this._previousPlanetPosition;
            this._previousPlanetPosition = this.transform.position;

            //hitchhiker.CurrentOffset.Vector += this._planetOffset;
            hitchhiker.CurrentOffset.AddToSequance(this._planetOffset);
            hitchhiker.MoveTo(hitchhiker.CurrentOffset);

            hitchhiker.CameraMover.CurrentOffset.AddToSequance(this._planetOffset);
            hitchhiker.CameraMover.MoveTo(hitchhiker.CameraMover.CurrentOffset);
        }

        private IEnumerator RotateAroundPlanet(Hitchhiker hitchhiker)
        {

            hitchhiker.CameraMover.MoveTo(new Offset(this.transform.position));
            hitchhiker.MoveTo(new Offset(this.transform.position));
            
            this._timeToSeparate = false;
            
            hitchhiker.State = HitchhikerState.OnPlanet;
            while(!_timeToSeparate)
            {
                yield return this.MoveAndSkip(0f, hitchhiker);
            }
            
            Offset endBornOffset = new Offset(this.transform.position + Vector3.up * this._distanceFromCenter);
            Offset startBornOffset = new Offset(this.transform.position) { Next = endBornOffset };

            VectorConverter.FillIntermidiateVectorsStraight(startBornOffset, endBornOffset);
            startBornOffset.RoundSequance();
            hitchhiker.MoveTo(startBornOffset);

            while(hitchhiker.CurrentOffset.Next != null)
            {
                yield return this.MoveAndSkip(this._bornDeltaTime, hitchhiker);
            }
            SpinOffset spinOffset = SpinOffset.Create(this.transform.position, this._distanceFromCenter);
            hitchhiker.CurrentOffset.Next = spinOffset;
            hitchhiker.State = HitchhikerState.InOrbit;
            
            this._timeToSeparate = false;

            while (!_timeToSeparate)
            {
                yield return this.MoveAndSkip(this._spinDeltaTime, hitchhiker);
            }

            this._timeToSeparate = false;
            this._parent.PlanetMovement.OnPositionChanged -= this._planetMovedAction;

            Vector2 meetPlace= this.SetSyncPath(hitchhiker, hitchhiker.CameraMover);
            hitchhiker.State = HitchhikerState.Transformating;
            hitchhiker.RigidbodyHandler.transform.position = (Vector2)hitchhiker.transform.position + meetPlace;

            while(hitchhiker.CurrentOffset.HasNext || hitchhiker.CameraMover.CurrentOffset.HasNext)
            {
                yield return this.MoveAndSkip(this._spinDeltaTime, hitchhiker, hitchhiker.CameraMover);
            }
            hitchhiker.Free(meetPlace.normalized, this._spinDeltaTime);
            yield return new WaitForEndOfFrame();
            
            this._parent.SetTriggerActive(true);

            StopCoroutine(this._rotationPlanet);
            yield break;
        }

        private WaitForSeconds MoveAndSkip(float time, params IMoveable[] moveables)
        {
            foreach (IMoveable moveable in moveables)
            {
                moveable.MoveNext();
            }
            
            return new WaitForSeconds(time);
        }

        private Vector2 SetSyncPath(Hitchhiker hitchhiker, CameraMover cameraMover)
        {
            Vector2 meetPlace = ((Vector2)(Vector3.Cross(hitchhiker.CurrentOffset.Vector - (Vector2)this.transform.position, Vector3.forward).normalized
                * Mathf.Tan(Mathf.Deg2Rad * this._angle) * this._distanceFromCenter)).RoundVector();

            Offset endHitchhiker = new Offset(hitchhiker.CurrentOffset.Vector + meetPlace);
            Offset startHitchhiker = new Offset(hitchhiker.CurrentOffset.Vector) { Next = endHitchhiker };

            VectorConverter.FillIntermidiateVectorsStraight(startHitchhiker, endHitchhiker);
            startHitchhiker.RoundSequance();

            Offset endCamera = new Offset(hitchhiker.CurrentOffset.Vector + meetPlace);
            Offset startCamera = new Offset(this.transform.position) { Next = endCamera };

            VectorConverter.FillIntermidiateVectorsStraight(startCamera, endCamera);
            startCamera.RoundSequance();

            hitchhiker.MoveTo(startHitchhiker);
            cameraMover.MoveTo(startCamera);
            return meetPlace;
        }

        private bool _timeToSeparate;
        private Coroutine _rotationPlanet;
        private float _angle;
        private float _spinDeltaTime;
        private float _bornDeltaTime;
        private float _distanceFromCenter;
        private Vector2 _planetOffset;
        private Vector2 _previousPlanetPosition;
        private Action _planetMovedAction;

        [SerializeField] private Planet _parent;
    }
}
