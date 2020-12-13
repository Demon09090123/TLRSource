using System;
using System.Collections.Generic;
using System.Drawing;

namespace TerrainGen.Generation.Shape
{
    public class RectangularFilterMap : FilterMap
    {
        public RectangularFilterMap(int size)
            : base(size)
        {
        }

        protected override void generateNoiseMap()
        {
            for (int x = 0; x < Size; x++)
            {
                float distX = Math.Abs(x - Size * 0.5f);
                for (int y = 0; y < Size; y++)
                {
                    float distY = Math.Abs(y - Size * 0.5f);
                    float dist = Math.Max(distX, distY);
                    float maxWidth = Size * 0.5f - 10.0f;
                    float delta = dist / maxWidth;
                    float gradient = delta * delta;

                    int alpha = (int)(255.0f * gradient);

                    NoiseBitmap.SetPixel(x, y, Color.FromArgb(alpha, 0, 0, 0));
                }
            }
        }
        
    }
}
