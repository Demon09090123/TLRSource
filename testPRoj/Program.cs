using System;
using System.Threading.Tasks;

namespace testPRoj
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[100, 100];
            Parallel.For(0, 100, x =>
            {
                for (var y = 0; y < 100; y++)
                    arr[x, y] = x * y;
            });


            Console.ReadLine();
        }
    }
    
}
