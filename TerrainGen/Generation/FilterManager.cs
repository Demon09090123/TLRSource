using System;
using System.Drawing;
using TerrainGen.Generation.Shape;

namespace TerrainGen.Generation
{
    public class FilterManager
    {
        public Bitmap TotalFilterBitmap { get; private set; }
        public int Size { get; private set; }
        public int Quad { get; private set; }

        public FilterMap[,] _currentFilters;
        public FilterManager(int size, int quad)
        {
            Size = size;
            Quad = quad;
            _quadSize = Size / (int)Math.Sqrt(quad);
            TotalFilterBitmap = new Bitmap(Size, Size);
            blankFill();
        }

        private int _quadSize = 0;
        public void Resize(int size, int quadCount)
        {
            TotalFilterBitmap.Dispose();

            Size = size;
            Quad = quadCount;
            _quadSize = Size / (int)Math.Sqrt(Quad);

            TotalFilterBitmap = new Bitmap(Size, Size);
            _currentFilters = new FilterMap[Quad, Quad];
            blankFill();
        }

        private void blankFill()
        {
            using (Graphics gfx = Graphics.FromImage(TotalFilterBitmap))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0)))
                    gfx.FillRectangle(brush, 0, 0, Size, Size);
        }
        public void AddFilter(int qX, int qY, int filterType)
        {
            var rx = qX * _quadSize;
            var ry = qY * _quadSize;

            for (var x = 0; x < _quadSize; x++)
            {
                float distX = Math.Abs(x - _quadSize * 0.5f);
                for (var y = 0; y < _quadSize; y++)
                {
                    float distY = Math.Abs(y - _quadSize * 0.5f);
                    float delta = Math.Max(distX, distY) / (_quadSize * 0.5f - 10.0f);
                    int gradient =  (int)(1.0f - delta * delta);

                    TotalFilterBitmap.SetPixel(rx + x, ry + y, Color.FromArgb(gradient, Color.Black));
                }
            }
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
