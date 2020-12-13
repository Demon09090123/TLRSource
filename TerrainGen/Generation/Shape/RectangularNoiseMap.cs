using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainGen.Generation.Shape
{
    public class RectangularNoiseMap
    {
        public Bitmap NoiseBitmap { get; private set; }

        public int Size { get; private set; }

        private long _seed;
        private float _scale;

        private OpenSimplexNoise _noise;
        public RectangularNoiseMap(long seed, int size)
        {
            _seed = seed;
            Size = size;

            _noise = new OpenSimplexNoise(_seed); 
            
            var scaleFactor = 0.0033f / (1024.0f / Size);
            _scale = (1024.0f / Size) * scaleFactor;

            generateNoiseMap();
        }

        private void generateNoiseMap()
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
                    float noise = _noise.OctaveNoise(x, y, _scale, 16f, 0.5f);

                    int alpha = (int)(255 * gradient * noise);

                    NoiseBitmap.SetPixel(x, y, Color.FromArgb(alpha, 0, 0, 0));
                }
            }
        }
        
    }
}
