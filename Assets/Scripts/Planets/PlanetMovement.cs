using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Tools;
using System.Collections;
using SpaceHitchhiker.Abstraction;
using SpaceHitchhiker.Offsets;

namespace SpaceHitchhiker.Planets
{
    public class PlanetMovement : MonoBehaviour, IInitializeable<Planet>
    {
        public void Initialize(AbstractRawInfo<Planet> planetRawInfo)
        {
            PlanetRawInfo info = planetRawInfo as PlanetRawInfo;
            this._angle = info.Angle;
            this._distanceFromCenter = info.Distance;
            this._bornDeltaTime = info.BornDeltaTime;
            this._spinDeltaTime = info.SpinDeltaTime;
        }

        private void Start()
        {
            //TODO: Remove it later. It's for debug
            if(this._hitchhiker != null)
                this.LandToPlanet(this._hitchhiker);
        }

        private void Update()
        {
            if (this._rotationPlanet != null && Input.GetKeyDown(GameSettings.Instance.SeparateKey))
                this._timeToSeparate = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out RigidbodyHandler rbHandler))
            {
                this.LandToPlanet(rbHandler.Parent);
            }
        }

        private void LandToPlanet(Hitchhiker hitchhiker)
        {
            this._trigger.enabled = false;
            hitchhiker.BindToPlanet(this);

            this._rotationPlanet = StartCoroutine(this.RotateAroundPlanet(hitchhiker));
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
            this._timeToSeparate = false;

            SpinOffset spinOffset = SpinOffset.Create(this.transform.position, this._distanceFromCenter);
            
            Offset endBornOffset = new Offset(this.transform.position + Vector3.up * this._distanceFromCenter) { Next = spinOffset };
            Offset startBornOffset = new Offset(this.transform.position) { Next = endBornOffset };

            VectorConverter.FillIntermidiateVectorsStraight(startBornOffset, endBornOffset);
            hitchhiker.MoveTo(startBornOffset);

            while(hitchhiker.CurrentOffset != spinOffset)
            {
                yield return this.MoveAndSkip(this._bornDeltaTime, hitchhiker);
            }

            //this._hitchhiker.MoveTo(spinOffset);
            hitchhiker.State = HitchhikerState.InOrbit;

            while (!_timeToSeparate)
            {
                yield return this.MoveAndSkip(this._spinDeltaTime, hitchhiker);
            }

            this._timeToSeparate = false;

            Vector2 meetPlace= this.SetSyncPath(hitchhiker, hitchhiker.CameraMover);
            hitchhiker.State = HitchhikerState.Transformating;
            hitchhiker.RigidbodyHandler.transform.position = (Vector2)hitchhiker.transform.position + meetPlace;

            while(hitchhiker.CurrentOffset.HasNext || hitchhiker.CameraMover.CurrentOffset.HasNext)
            {
                yield return this.MoveAndSkip(this._spinDeltaTime, hitchhiker, hitchhiker.CameraMover);
            }

            hitchhiker.Free(meetPlace.normalized, this._spinDeltaTime);
            yield return new WaitForEndOfFrame();

            this._trigger.enabled = true;
            StopCoroutine(this._rotationPlanet);
            yield break;
        }

        private WaitForSeconds MoveAndSkip(float time, params IMoveable[] moveables)
        {
            foreach (IMoveable moveable in moveables)
                moveable.MoveNext();
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

        [SerializeField] private float _angle = 70f;
        [SerializeField] private float _spinDeltaTime;
        [SerializeField] private float _bornDeltaTime;
        [SerializeField] private float _distanceFromCenter = 14;

        [SerializeField] private CircleCollider2D _trigger;
        [SerializeField] private Hitchhiker _hitchhiker;
    }
}
