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
                    float maxWidth = Size * 0.5f;
                    float delta = dist / maxWidth;
                    float gradient = delta * delta;

                    float alpha = 255.0f * (1.0f - gradient);

                    FilterBitmap.SetPixel(x, y, Color.FromArgb((int)alpha, 0, 0, 0));
                }
            }
        }
        
    }
}
