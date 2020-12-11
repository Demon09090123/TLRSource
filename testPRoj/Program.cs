using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace testPRoj
{
    class Program
    {
        static int _size = 4096;
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var jaggedArray = new float[_size][];

            for (var x = 0; x < _size; x++)
            {
                jaggedArray[x] = new float[_size];
                for (var y = 0; y < _size; y++)
                    jaggedArray[x][y] = (x * y) / 2.16456f;
            }

            var test = 1.3333f;
            for (var x = 0; x < _size; x++)
                for (var y = 0; y < _size; y++)
                    test += jaggedArray[x][y];

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            watch.Reset();
            watch.Start();

            var multiArray = new float[_size, _size];

            for (var x = 0; x < _size; x++)
                for (var y = 0; y < _size; y++)
                    multiArray[x, y] = (x * y) / 2.16456f;

            test = 1.3333f;
            for (var x = 0; x < _size; x++)
                for (var y = 0; y < _size; y++)
                    test += multiArray[x, y];

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);

            Console.Read();
        }
    }
}
