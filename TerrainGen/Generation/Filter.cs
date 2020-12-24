using System;

namespace TerrainGen.Generation
{
    public struct Filter
    {
        public readonly float[,] FilterMap;
        public readonly int mapX;
        public readonly int mapY;

        public Filter(float[,] filterMap, int mapX, int mapY)
        {
            this.FilterMap = filterMap;
            this.mapX = mapX;
            this.mapY = mapY;
        }

        public static float[,] FalloutFilter(int size)
        {
            var fMap = new float[size, size];

            float radius = size / 2.0f;

            for (var y = 0; y < size; y++)
                for (var x = 0; x < size; x++)
                {
                    float xRatio = Math.Abs((x / radius) - 1.0f);
                    float yRatio = Math.Abs((y / radius) - 1.0f);
                     
                    float dist = Math.Max( Math.Abs(xRatio), Math.Abs(yRatio));

                    fMap[x, y] = 1.0f - (float)Evaluate(dist);
                }

            return fMap;
        }

        private static double Evaluate(float v)
        {
            float b = 1.4f;
            var v2 = Math.Pow(v, 3);
            var d = Math.Pow((b - b * v), 3);

            return v2 / (v2 + d);
        }
    }
}
