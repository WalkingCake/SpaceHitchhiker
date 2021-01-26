using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Player;
using SpaceHitchhiker.Tools;

namespace SpaceHitchhiker.Planet
{
    public class PlanetRotation : MonoBehaviour
    {

        public Vector2 CurrentOffset => this._currentOffset.Vector;
        public float PositionSwitchingTime => this._positionSwitchingTime;
        public float FromPlayerToPlanet => this._distanceFromCenter;

        private void Awake()
        {
            HitchhickerOffset up = new HitchhickerOffset(Vector2.up * this._distanceFromCenter, false, true, false, false);
            HitchhickerOffset right = new HitchhickerOffset(Vector2.right * this._distanceFromCenter, true, false, false, false);
            HitchhickerOffset down = new HitchhickerOffset(Vector2.down * this._distanceFromCenter, false, false, false, true);
            HitchhickerOffset left = new HitchhickerOffset(Vector2.left * this._distanceFromCenter, false, false, true, false);
            
            up.Prev = left;
            up.Next = right;
            right.Prev = up;
            right.Next = down;
            down.Prev = right;
            down.Next = left;
            left.Prev = down;
            left.Next = up;

            VectorConverter.FillIntermidiateVectorsArc(up, right, this._distanceFromCenter);
            VectorConverter.FillIntermidiateVectorsArc(right, down, this._distanceFromCenter);
            VectorConverter.FillIntermidiateVectorsArc(down, left, this._distanceFromCenter);
            VectorConverter.FillIntermidiateVectorsArc(left, up, this._distanceFromCenter);

            foreach(HitchhickerOffset offset in up.GetSequance())
            {
                offset.Vector = VectorConverter.RoundVector(offset.Vector);
            }

            this._currentOffset = up;
            //TODO: remove Debug
            this._testList = this._currentOffset.GetSequance().Select(offset => offset.Vector).ToList();
            this._timer = 0f;
            this._hitchhiker.UpdatePosition((Vector2)this.transform.position + this._currentOffset.Vector,
                this._currentOffset.AddX, this._currentOffset.AddY, this._currentOffset.SubX, this._currentOffset.SubY);
        }
        
        private void OnDrawGizmos()
        {
            if(this._testList != null)
            {
                Gizmos.color = Color.cyan;
                foreach (Vector2 v in this._testList)
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
            Vector2 meetPlace = VectorConverter.RoundVector(
                Vector3.Cross(this.CurrentOffset, Vector3.forward).normalized
                * Mathf.Tan(Mathf.Deg2Rad * this._angle) * this._distanceFromCenter);
            //Debug.Log(this._currentOffset.Vector);
            HitchhickerOffset start = new HitchhickerOffset(this._currentOffset.Vector);
            HitchhickerOffset end = new HitchhickerOffset(this._currentOffset.Vector + meetPlace);

            start.Next = end;
            start.Prev = end;
            end.Next = start;
            end.Prev = start; 

            VectorConverter.FillIntermidiateVectorsStraight(start, end);

            foreach(HitchhickerOffset offset in start.GetSequance())
            {
                offset.Vector = VectorConverter.RoundVector(offset.Vector);
            }

            this._currentOffset = start;
        }

        private void FixedUpdate()
        {
            this._timer += Time.fixedDeltaTime;
            if(this._timer > this._positionSwitchingTime)
            {
                this._timer -= this._positionSwitchingTime;
                this._currentOffset = this._currentOffset.Next;
                this._hitchhiker.UpdatePosition(this._currentOffset, this.transform.position);
            }
        }


        //TODO: Remove debug
        private List<Vector2> _testList;

        private HitchhickerOffset _currentOffset;
        private float _timer;


        [SerializeField] private float _angle = 70f;
        [SerializeField] private float _positionSwitchingTime;
        [SerializeField] private float _distanceFromCenter = 14;
        [SerializeField] private Hitchhiker _hitchhiker;
        [SerializeField] private KeyCode _separateKey = KeyCode.Space;

        private const float SIN_22_5 = 0.3826834324f;
    }
}
