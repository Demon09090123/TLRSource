using Ionic.Zlib;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testPRoj
{
    class Program
    {
        static void Main(string[] args)
        {
            var testLock = new object();
            var test2Lock = new object();
            var rand = new Random();

            Task.Factory.StartNew(() =>
            {
                var tt = 1.0;
                for (var x = 0; x < 100; x++)
                {
                    lock (testLock)
                    {
                        tt *= rand.NextDouble() * (x + 1);
                    }
                }
                Console.WriteLine($"Time up! {tt}");
            });
            Task.Factory.StartNew(() =>
            {
                var tt = 1.0;
                for (var x = 0; x < 100; x++)
                {
                    lock (test2Lock)
                    {
                        tt *= rand.NextDouble() * (x + 1);
                    }
                }
                Console.WriteLine($"Time up! {tt}");
            });
            Task.Factory.StartNew(() =>
            {
                lock (testLock)
                {
                    lock (test2Lock)
                    {
                        Console.WriteLine("Unlocked!");
                    }
                }
            });
            Console.ReadLine();

        }
    }
}
