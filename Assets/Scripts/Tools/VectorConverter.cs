using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SpaceHitchhiker.Planet;

namespace SpaceHitchhiker.Tools
{
    public static class VectorConverter
    {
        public static void FillIntermidiateVectorsStraight(HitchhickerOffset first, HitchhickerOffset second)
        {
            if(VectorConverter.GetPixelBetweenStraight(first.Vector, second.Vector, out Vector2 result))
            {
                HitchhickerOffset newOffset = new HitchhickerOffset(result)
                {
                    Prev = first,
                    Next = second
                };

                first.Next = newOffset;
                second.Prev = newOffset;

                VectorConverter.FillIntermidiateVectorsStraight(first, newOffset);
                VectorConverter.FillIntermidiateVectorsStraight(newOffset, second);
            }
        }

        public static void FillIntermidiateVectorsArc(HitchhickerOffset first, HitchhickerOffset second, float distance)
        {
            if (VectorConverter.GetPixelBetweenArc(first.Vector, second.Vector, distance, out Vector2 result))
            {

                float cos = result.x / distance;
                float sin = result.y / distance;

                bool addX = false, addY = false, subX = false, subY = false;

                if (Mathf.Abs(cos) > SIN_22_5)
                {
                    float sign = Mathf.Sign(result.x);
                    addX = sign > 0;
                    subX = sign < 0;

                }

                if (Mathf.Abs(sin) > SIN_22_5)
                {
                    float sign = Mathf.Sign(result.y);
                    addY = sign > 0;
                    subY = sign < 0;
                }

                HitchhickerOffset newOffset = new HitchhickerOffset(result, addX, addY, subX, subY)
                {
                    Prev = first,
                    Next = second
                };
                first.Next = newOffset;
                second.Prev = newOffset;
                VectorConverter.FillIntermidiateVectorsArc(first, newOffset, distance);
                VectorConverter.FillIntermidiateVectorsArc(newOffset, second, distance);
            }
        }

        public static bool GetPixelBetweenStraight(Vector2 first, Vector2 second, out Vector2 result)
        {
            result = (first + second) / 2;
            return Vector2.Distance(first, result) > 0.5f && Vector2.Distance(first, result) > 0.5f;
        }

        public static bool GetPixelBetweenArc(Vector2 firstDirection, Vector2 secondDirection, float distance, out Vector2 result)
        {
            result = (firstDirection + secondDirection).normalized * distance;
            return Vector2.Distance(firstDirection, result) > 0.5f && Vector2.Distance(secondDirection, result) > 0.5f;
        }

        public static Vector2 RoundVector(Vector2 vec)
        {
            vec.x = Mathf.Round(vec.x);
            vec.y = Mathf.Round(vec.y);
            return vec;
        }

        private const float SIN_22_5 = 0.3826834324f;
    }
}
