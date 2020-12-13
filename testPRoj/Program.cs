using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace testPRoj
{
    public class Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var pos = obj as Position;
            return X == pos.X && Y == pos.Y;
        }

        public override int GetHashCode() => GetHashCode();

        public static bool operator ==(Position left, Position right) => left.X == right.X && left.Y == right.Y;

        public static bool operator !=(Position left, Position right) => left.X != right.X || left.Y != right.Y;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var _testBitmap = new Bitmap(1024, 1024);

            for (var x = 0; x < 1024; x++)
                for (var y = 0; y < 1024; y++)
                    _testBitmap.SetPixel(x, y, Color.Black);

            long mem = GC.GetTotalMemory(true);

            Console.WriteLine("Memory Used ~ " + GC.GetTotalMemory(true));

            _testBitmap.Dispose();

            Console.WriteLine("Memory Cleared ~ " + GC.GetTotalMemory(true));

            var arr = new float[1024, 1024];

            for (var x = 0; x < 1024; x++)
                for (var y = 0; y < 1024; y++)
                    arr[x, y] = 1024 * 2.5f * x;

            Console.WriteLine("Memory Used ~ " + GC.GetTotalMemory(true));

            Console.Read();
        }
    }
}
