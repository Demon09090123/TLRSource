using System;
using System.Collections.Generic;
using System.Drawing;
using TerrainGen.Generation.Shape;

namespace TerrainGen.Generation
{
    public class FilterManager
    {
        public Bitmap TotalFilterBitmap { get; private set; }
        public int Size { get; private set; }

        private List<FilterMap> _currentFilters;
        private List<Position> _occupiedPosition;
        public FilterManager(int size)
        {
            _currentFilters = new List<FilterMap>();
            _occupiedPosition = new List<Position>();

            Size = size;

            TotalFilterBitmap = new Bitmap(Size, Size);
            using (Graphics gfx = Graphics.FromImage(TotalFilterBitmap))
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0)))
                    gfx.FillRectangle(brush, 0, 0, Size, Size);
        }

        public void Resize(int size)
        {
            Size = size;
            TotalFilterBitmap = Utils.ResizeImage(TotalFilterBitmap, Size, Size);
        }

        public void AddFilter(FilterMap filter)
        {
            var radius = filter.Size / 2;
            if (filter.Size > Size || radius > Size - radius)
                throw new Exception("Filter size is too large for the Bitmap canvas.");
            
            int tries = 0;
            int rX;
            int rY;

            do
            {
                rX = MapGeneration._random.Next(radius, Size - radius);
                rY = MapGeneration._random.Next(radius, Size - radius);

                tries++;
                if (tries % 100 == 0)
                    Console.WriteLine($" {rX} {rY} {filter.Size} {filter.Size / 2} ");
            } while(!isValidPosition(rX, rY, radius));

            filter.SetCenterPosition(new Position(rX, rY));
            _currentFilters.Add(filter);

            var center = filter.CenterPosition;
            var fBitmap = filter.FilterBitmap;

            for (var x = 0; x < filter.Size; x++)
            {
                var totalX = (center.X - radius) + x;
                for (var y = 0; y < filter.Size; y++)
                {
                    var totalY = (center.Y - radius) + y;
                    var fPixel = fBitmap.GetPixel(x, y);
                    var mPixel = TotalFilterBitmap.GetPixel(totalX, totalY);

                    Color color = fPixel;
                    if (mPixel.A > 0)
                        color = Color.FromArgb((fPixel.A + mPixel.A) / 2, 0, 0, 0);

                    TotalFilterBitmap.SetPixel(totalX, totalY, color);
                    _occupiedPosition.Add(new Position(totalX, totalY));
                }
            }
        }

        private bool isValidPosition(int x, int y, int radius)
        {
            if (_currentFilters.Count == 0)
                return true;

            if (_occupiedPosition.Contains(new Position(x, y)))
                return false;

            foreach (var f in _currentFilters)
            {
                var fPos = f.CenterPosition;
                var fRadius = f.Size / 2;

                if (x + radius > fPos.X - fRadius || x - radius < fPos.X + fRadius ||
                    y + radius > fPos.Y - fRadius || y - radius < fPos.Y + fRadius)
                {
                    Console.WriteLine($"{x + radius} > {fPos.X - fRadius} | {x - radius} < {fPos.X + fRadius} | {y + radius} > {fPos.Y - fRadius} | {y - radius} < {fPos.Y + fRadius}");
                    continue;
                }

                return true;
            }

            return false;
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
