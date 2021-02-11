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

namespace SpaceHitchhiker.Planets
{
    public class PlanetRotation : MonoBehaviour
    {
        public float PositionSwitchingTime => this._positionSwitchingTime;
        public float FromPlayerToPlanet => this._distanceFromCenter;

        private void Awake()
        {
            SpinOffset hitchhikerOffset = SpinOffset.Create(this.transform.position, this._distanceFromCenter);

            this._timer = 0f;
            this._hitchhiker.MoveTo(hitchhikerOffset);
            this._cameraMover.MoveTo(new Offset(this.transform.position));
            this._rotationPlanet = StartCoroutine(this.RotateAroundPlanet());
            //this._hitchhiker.UpdateAnimator((Vector2)this.transform.position + this._currentOffset.Vector,
            //    this._currentOffset.AddX, this._currentOffset.AddY, this._currentOffset.SubX, this._currentOffset.SubY);
        }
        
        private void OnDrawGizmos()
        {
            if(this._cameraMover.CurrentOffset != null && this._hitchhiker.CurrentOffset != null)
            {
                Gizmos.color = Color.cyan;
                List<Vector2> vectors = new List<Vector2>();
                vectors.AddRange(this._cameraMover.CurrentOffset.GetSequance().Select(offset => offset.Vector));
                vectors.AddRange(this._hitchhiker.CurrentOffset.GetSequance().Select(offset => offset.Vector));
                foreach (Vector2 v in vectors)
                    Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3)v);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(this._separateKey))
                this._timeToSeparate = true;
                    //this.SeparateFromThePlanet();
        }

        private IEnumerator RotateAroundPlanet()
        {
            while(!_timeToSeparate)
            {
                yield return this.MoveAndSkip(this._hitchhiker);
            }

            this._timeToSeparate = false;

            Vector2 meetPlace= this.SetSyncPath();

            while(this._hitchhiker.CurrentOffset.HasNext || this._cameraMover.CurrentOffset.HasNext)
            {
                yield return this.MoveAndSkip(this._hitchhiker, this._cameraMover);
            }

            this._hitchhiker.RigidbodyHandler.MovementAllowed = true;
            this._hitchhiker.RigidbodyHandler.Position = this._hitchhiker.transform.position;
            
            StopCoroutine(this._rotationPlanet);
            this._hitchhiker.Free(meetPlace.normalized, this._positionSwitchingTime);

            yield break;
        }

        private WaitForSeconds MoveAndSkip(params IMoveable[] moveables)
        {
            foreach (IMoveable moveable in moveables)
                moveable.MoveNext();
            return new WaitForSeconds(this._positionSwitchingTime);
        }

        private Vector2 SetSyncPath()
        {
            Vector2 meetPlace = ((Vector2)(Vector3.Cross(this._hitchhiker.CurrentOffset.Vector - (Vector2)this.transform.position, Vector3.forward).normalized
                * Mathf.Tan(Mathf.Deg2Rad * this._angle) * this._distanceFromCenter)).RoundVector();

            Offset endHitchhiker = new SpinOffset(this._hitchhiker.CurrentOffset.Vector + meetPlace);
            Offset startHitchhiker = new SpinOffset(this._hitchhiker.CurrentOffset.Vector) { Next = endHitchhiker };

            VectorConverter.FillIntermidiateVectorsStraight(startHitchhiker, endHitchhiker);
            startHitchhiker.RoundSequance();

            Offset startCamera = new Offset(this.transform.position);
            Offset endCamera = new Offset(this._hitchhiker.CurrentOffset.Vector + meetPlace);

            startCamera.Next = endCamera;

            VectorConverter.FillIntermidiateVectorsStraight(startCamera, endCamera);
            startCamera.RoundSequance();

            this._hitchhiker.MoveTo(startHitchhiker);
            this._cameraMover.MoveTo(startCamera);
            return meetPlace;
        }

        private void SeparateFromThePlanet()
        {
            Vector2 meetPlace = ((Vector2)(Vector3.Cross(this._hitchhiker.CurrentOffset.Vector - (Vector2)this.transform.position, Vector3.forward).normalized
                * Mathf.Tan(Mathf.Deg2Rad * this._angle) * this._distanceFromCenter)).RoundVector();
            //Debug.Log(this._currentOffset.Vector);
            SpinOffset startHitchhiker = new SpinOffset(this._hitchhiker.CurrentOffset.Vector);
            SpinOffset endHitchhiker = new SpinOffset(this._hitchhiker.CurrentOffset.Vector + meetPlace);

            startHitchhiker.Next = endHitchhiker;

            VectorConverter.FillIntermidiateVectorsStraight(startHitchhiker, endHitchhiker);
            
            startHitchhiker.RoundSequance();

            this._hitchhiker.RigidbodyHandler.MovementAllowed = true;
            this._hitchhiker.RigidbodyHandler.Position = endHitchhiker.Vector;
            FreeHitchhikerOffset afterSync = new FreeHitchhikerOffset(endHitchhiker.Vector, this._hitchhiker.RigidbodyHandler, 
                meetPlace.normalized * this._hitchhiker.RigidbodyHandler.MaxVelocity / 2);


            endHitchhiker.Next = afterSync;

            Offset startCamera = new Offset(this.transform.position);
            Offset endCamera = new Offset(this._hitchhiker.CurrentOffset.Vector + meetPlace);

            startCamera.Next = endCamera;

            VectorConverter.FillIntermidiateVectorsStraight(startCamera, endCamera);
            startCamera.RoundSequance();

            this._hitchhiker.MoveTo(startHitchhiker);
            this._cameraMover.MoveTo(startCamera);
        }

        //private void FixedUpdate()
        //{
        //    this._timer += Time.fixedDeltaTime;

        //    while(this._timer > this._positionSwitchingTime)
        //    {
        //        this._timer -= this._positionSwitchingTime;
        //        this._hitchhiker.MoveNext();
        //        this._cameraMover.MoveNext();
        //    }
        //}

        private float _timer;
        private bool _timeToSeparate;
        private Coroutine _rotationPlanet;

        [SerializeField] private float _angle = 70f;
        [SerializeField] private float _positionSwitchingTime;
        [SerializeField] private float _distanceFromCenter = 14;
        [SerializeField] private Hitchhiker _hitchhiker;
        [SerializeField] private KeyCode _separateKey = KeyCode.Space;
        [SerializeField] private CameraMover _cameraMover;
    }
}
