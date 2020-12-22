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

            for (var y = 0; y < size; y++)
                for (var x = 0; x < size; x++)
                {
                    /*float xRatio = x / (float)size * 2.0f - 1.0f;
                    float yRatio = y / (float)size * 2.0f - 1.0f;*/
                    float xRatio = x - size * 0.5f;
                    float yRatio = y - size * 0.5f;

                    float dist = Math.Max( Math.Abs(xRatio), Math.Abs(yRatio) ) / (size * 0.5f);

                    fMap[x, y] = Evaluate(dist);
                }

            return fMap;
        }

        private static float Evaluate(float v)
        {
            float b = 5.0f;
            var v2 = v * v;
            var d = (float)Math.Pow((b - b * v), 2);

            return v2 / (v2 + d);
        }
    }
}
