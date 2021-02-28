using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SpaceHitchhiker.Tools
{
    public static class TexturePainter
    {
        public static Sprite CreateCircle(int radius, Color color, int pixelPerUnit = 1)
        {
            Texture2D tex = new Texture2D(radius * 2 + 1, radius * 2 + 1)
            {
                filterMode = FilterMode.Point
            };
            Color[] colors = new Color[tex.width * tex.height];
            float _radius = radius - 0.5f;
            float sqrRadius = _radius * _radius;

            for(int y = 0; y < tex.height; y++)
            {
                for(int x = 0; x < tex.width; x++)
                {
                    int offsetedY = y - radius;
                    int offsetedX = x - radius;
                    colors[y * tex.width + x] = offsetedX * offsetedX + offsetedY * offsetedY < sqrRadius ? color : Color.clear;
                }
            }
            tex.SetPixels(0, 0, tex.width, tex.height, colors);

            tex.Apply();

            return Sprite.Create(tex, new Rect(Vector2.zero, Vector2.one * (radius * 2 + 1)), Vector2.one * .5f, pixelPerUnit);
        }
    }
}
