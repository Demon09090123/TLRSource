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

        private 
        public void AddFilter(FilterMap filter)
        {



        }

        public float getScale()
        {
            var scale = 0.0033f / (1024.0f / Size);
            return (1024.0f / Size) * scale;
        }
    }
    public struct Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position left, Position right) => left.X == right.X && left.Y == right.Y;
        public static bool operator !=(Position left, Position right) => left.X != right.X || left.Y != right.Y;
        public override bool Equals(object obj)
        {
            var pos = (Position)obj;
            return X == pos.X && Y == pos.Y;
        }
    }
}
