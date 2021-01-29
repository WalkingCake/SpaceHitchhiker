using UnityEngine;
using SpaceHitchhiker.Player;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SpaceHitchhiker.Planets
{
    [Obsolete]
    public class oldPlanetRotation : MonoBehaviour
    {
        public oldHitchhikerOffset HitchhikerOffset => this._borderPixels[this._positionIndex];
        
        private void Awake()
        {
            this._halfPixelDistance = 0.5f / this._renderer.sprite.pixelsPerUnit;
            this._timer = 0;
            this._positionIndex = 0;
            this._distanceToPlayer = this._renderer.sprite.rect.width
                / this._renderer.sprite.pixelsPerUnit / 2;
            this._borderPixels = this.SortOffset(this.GetBorderPixels());
        }

        private oldHitchhikerOffset[] GetBorderPixels()
        {
            List<oldHitchhikerOffset> borderPixels = new List<oldHitchhikerOffset>();
            Texture2D texture = this._renderer.sprite.texture;
            Vector2 center = this._renderer.sprite.rect.center;
            
            for(int y = Mathf.RoundToInt(this._renderer.sprite.rect.position.y);
                y < Mathf.RoundToInt(this._renderer.sprite.rect.position.y + this._renderer.sprite.rect.height);
                y++)
            {
                for (int x = Mathf.RoundToInt(this._renderer.sprite.rect.position.x);
                x < Mathf.RoundToInt(this._renderer.sprite.rect.position.x + this._renderer.sprite.rect.width);
                x++)
                {
                    if(texture.GetPixel(x, y).a > float.Epsilon
                        && (x == Mathf.RoundToInt(this._renderer.sprite.rect.position.x)
                        || x == Mathf.RoundToInt(this._renderer.sprite.rect.position.x + this._renderer.sprite.rect.width)
                        || y == Mathf.RoundToInt(this._renderer.sprite.rect.position.y)
                        || y == Mathf.RoundToInt(this._renderer.sprite.rect.position.y + this._renderer.sprite.rect.height)
                        || texture.GetPixel(x - 1, y).a < float.Epsilon
                        || texture.GetPixel(x + 1, y).a < float.Epsilon
                        || texture.GetPixel(x, y - 1).a < float.Epsilon
                        || texture.GetPixel(x, y + 1).a < float.Epsilon
                        ))
                    {
                        Vector2 vector = (new Vector2(x, y) - center) / this._renderer.sprite.pixelsPerUnit
                            + this._halfPixelDistance * Vector2.one;

                        Vector2 additional = Vector2.zero;

                        float cos = vector.x / this._distanceToPlayer;
                        float sin = vector.y / this._distanceToPlayer;

                        bool addX = false, addY = false, subX = false, subY = false;

                        if (Mathf.Abs(cos) > SIN_22_5)
                        {
                            additional.x = Mathf.Sign(vector.x);
                            addX = additional.x > 0;
                            subX = additional.x < 0;

                        }
                        
                        if (Mathf.Abs(sin) > SIN_22_5)
                        {
                            additional.y = Mathf.Sign(vector.y);
                            addY = additional.y > 0;
                            subY = additional.y < 0;

                        }
                        vector += new Vector2(Mathf.Ceil(cos * this._distanceToPlanet), Mathf.Ceil(sin * this._distanceToPlanet));
                        //vector += this._halfPixelDistance * additional;

                        borderPixels.Add(new oldHitchhikerOffset(vector, addX, addY, subX, subY));
                    }
                }
            }
            return borderPixels.ToArray();

        }

        public oldHitchhikerOffset[] SortOffset(oldHitchhikerOffset[] offsets)
        {
            HashSet<oldHitchhikerOffset> offsetSet = new HashSet<oldHitchhikerOffset>(offsets);
            Stack<oldHitchhikerOffset> sortedOffsets = new Stack<oldHitchhikerOffset>();
            sortedOffsets.Push(offsetSet.First());
            offsetSet.Remove(sortedOffsets.Peek());

            while(offsetSet.Count > 0)
            {
                float minDistance = float.MaxValue;
                oldHitchhikerOffset nextOffset = new oldHitchhikerOffset();
                foreach(oldHitchhikerOffset offset in offsetSet)
                {
                    float distance = Vector2.Distance(offset.Offset, sortedOffsets.Peek().Offset);
                    if (distance < minDistance)
                    {
                        nextOffset = offset;
                        minDistance = distance;
                    }
                }
                sortedOffsets.Push(nextOffset);
                offsetSet.Remove(sortedOffsets.Peek());
            }
            return sortedOffsets.ToArray();
        }

        //private void OnDrawGizmos()
        //{
        //    if (this._borderPixels != null)
        //    {

        //        Gizmos.color = Color.red;
        //        foreach (HitchhikerOffset direction in this._borderPixels)
        //        {
        //            Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3)direction.Offset);
        //        }
        //    }
        //}

        private void FixedUpdate()
        {
            this._timer += Time.fixedDeltaTime;
            if(this._timer > this._positionSwitchingTime)
            {
                this._timer -= this._positionSwitchingTime;
                
                this._positionIndex++;
                if(this._positionIndex >= this._borderPixels.Length)
                {
                    this._positionIndex = 0;
                }
                //this._hitchhiker.UpdatePosition(this.HitchhikerOffset);
            }    
        }

        private int _positionIndex;
        private float _timer;
        private float _distanceToPlayer;
        private oldHitchhikerOffset[] _borderPixels;
        private float _halfPixelDistance;
        
        private const float SIN_22_5 = 0.3826834324f;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Hitchhiker _hitchhiker;
        [SerializeField] private float _positionSwitchingTime;
        [SerializeField] private float _distanceToPlanet;
    }
}