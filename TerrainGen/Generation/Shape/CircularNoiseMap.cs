using System;
using System.Drawing;

namespace TerrainGen.Generation.Shape
{
    public class CircularNoiseMap
    {
        public Bitmap NoiseBitmap { get; private set; }
        public int Radius { get; private set; }
        public int  Size { get; private set; }

        private OpenSimplexNoise _noise;
        private long _seed;
        private float _scale;

        public CircularNoiseMap(long seed, int size)
        {
            Radius = size / 2;
            Size = size;
            _seed = seed;

            _noise = new OpenSimplexNoise(_seed);
            NoiseBitmap = new Bitmap(Size, Size);

            var scaleFactor = 0.0033f / (1024.0f / Size);
            _scale = (1024.0f / Size) * scaleFactor;

            generateNoiseMap();
        }

        private void generateNoiseMap()
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
                    float noise = _noise.OctaveNoise(x, y, _scale, 16.0f, 0.5f);

                    int alpha = (int)(gradient * noise * 255);

                    NoiseBitmap.SetPixel(x, y, Color.FromArgb(alpha, 0, 0, 0));
                }
            }
        }
    }
}
