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
            var f = new float[100, 100];

            for(var x = 0; x < 100; x++)
            {
                for(var y = 0; y < 100; y++)
                {
                    Console.WriteLine(f[x, y]);
                }
            }
        }
    }
}
