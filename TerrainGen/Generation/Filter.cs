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
                    var dX = Math.Abs(x / (float)size * 2.0f - 1.0f);
                    var dY = Math.Abs(y / (float)size * 2.0f - 1.0f);
                    var dist = Math.Max(dX, dY);

                    fMap[x, y] = Evaluate(dist);
                }

            return fMap;
        }

        private static float Evaluate(float v)
        {
            float b = 1.0f;
            var v2 = v * v;
            var d = (float)Math.Pow((b - b * v), 2);

            return v2 / (v2 + d);
        }
    }
}
