using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Tools;

namespace SpaceHitchhiker.Planets
{
    public class PlanetRotation : MonoBehaviour
    {
        public float PositionSwitchingTime => this._positionSwitchingTime;
        public float FromPlayerToPlanet => this._distanceFromCenter;

        private void Awake()
        {
            SpinOffset hitchhikerOffset = SpinOffset.Create(this.transform.position, this._distanceFromCenter);

            this._testList = hitchhikerOffset.GetSequance().Select(offset => offset.Vector).ToList();
            this._timer = 0f;
            this._hitchhiker.MoveTo(hitchhikerOffset);
            this._cameraMover.MoveTo(new Offset(this.transform.position));
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
                this.SeparateFromThePlanet();
        }

        private void SeparateFromThePlanet()
        {
            Vector2 meetPlace = ((Vector2)(Vector3.Cross(this._hitchhiker.CurrentOffset.Vector - (Vector2)this.transform.position, Vector3.forward).normalized
                * Mathf.Tan(Mathf.Deg2Rad * this._angle) * this._distanceFromCenter)).RoundVector();
            //Debug.Log(this._currentOffset.Vector);
            SpinOffset startHitchhiker = new SpinOffset(this._hitchhiker.CurrentOffset.Vector);
            SpinOffset endHitchhiker = new SpinOffset(this._hitchhiker.CurrentOffset.Vector + meetPlace);

            startHitchhiker.Next = endHitchhiker;
            endHitchhiker.Prev = startHitchhiker; 

            VectorConverter.FillIntermidiateVectorsStraight(startHitchhiker, endHitchhiker);
            
            startHitchhiker.RoundSequance();

            EndlessOffset afterSync = new EndlessOffset(endHitchhiker.Vector,
                meetPlace.normalized * this._hitchhiker.Acceleration, this._hitchhiker.MaxSpeed, this._hitchhiker.Acceleration);

            endHitchhiker.Next = afterSync;

            Offset startCamera = new Offset(this.transform.position);
            Offset endCamera = new Offset(this._hitchhiker.CurrentOffset.Vector + meetPlace);

            startCamera.Next = endCamera;
            endCamera.Prev = startCamera;

            VectorConverter.FillIntermidiateVectorsStraight(startCamera, endCamera);
            startCamera.RoundSequance();

            this._hitchhiker.MoveTo(startHitchhiker);
            this._cameraMover.MoveTo(startCamera);
        }

        private void FixedUpdate()
        {
            this._timer += Time.fixedDeltaTime;

            while(this._timer > this._positionSwitchingTime)
            {
                this._timer -= this._positionSwitchingTime;
                this._hitchhiker.MoveNext();
                this._cameraMover.MoveNext();
            }
        }


        //TODO: Remove debug
        private List<Vector2> _testList;
        private float _timer;

        [SerializeField] private float _angle = 70f;
        [SerializeField] private float _positionSwitchingTime;
        [SerializeField] private float _distanceFromCenter = 14;
        [SerializeField] private Hitchhiker _hitchhiker;
        [SerializeField] private KeyCode _separateKey = KeyCode.Space;
        [SerializeField] private CameraMover _cameraMover;
    }
}
