using System;

namespace testPRoj
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 50;
            float radius = size / 2.0f;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    float xRatio = Math.Abs((x / radius) - 1.0f);
                    float yRatio = Math.Abs((y / radius) - 1.0f);

                    float dist = Math.Max(Math.Abs(xRatio), Math.Abs(yRatio));

                    Console.Write(dist + "|");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
    
}
