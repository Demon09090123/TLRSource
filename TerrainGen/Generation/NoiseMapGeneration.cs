using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainGen.Generation.Shape;

namespace TerrainGen.Generation
{
    public class NoiseMapGeneration
    {
        public Bitmap TotalNoiseBitmap { get; private set; }
        public int Size { get; private set; }

        private List<FilterMap> _currentFilters;

        private OpenSimplexNoise _simplexNoise;

        private long _seed;
        public NoiseMapGeneration(long seed, int size)
        {
            _currentFilters = new List<FilterMap>();

            _seed = seed;
            Size = size;

            TotalNoiseBitmap = new Bitmap(Size, Size);
            _simplexNoise = new OpenSimplexNoise(_seed);
        }

        public void AddFilter(FilterMap filter)
        {

        }
        public float getScale()
        {
            var scale = 0.0033f / (1024.0f / Size);
            return (1024.0f / Size) * scale;
        }
    }
}
