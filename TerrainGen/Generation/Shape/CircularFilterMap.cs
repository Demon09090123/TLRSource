using System;
using System.Drawing;

namespace TerrainGen.Generation.Shape
{
    public class CircularFilterMap : FilterMap
    {
        public int Radius { get; private set; }

        public CircularFilterMap(int size)
            : base(size)
        {
            Radius = size / 2;
        }

        protected override void generateNoiseMap()
        {
            for (int x = 0; x < Size; x++)
            {
                float distX = Math.Abs(x - Radius);
                for (int y = 0; y < Size; y++)
                {
                    float distY = Math.Abs(y - Radius);
                    float dist = (float)Math.Sqrt(distX * distX + distY * distY);
                    float maxWidth = Size * 0.5f - 10.0f;
                    float delta = dist / maxWidth;
                    float gradient = delta * delta;

                    int alpha = (int)(gradient * 255);

                    NoiseBitmap.SetPixel(x, y, Color.FromArgb(alpha, 0, 0, 0));
                }
            }
        }
    }
}
