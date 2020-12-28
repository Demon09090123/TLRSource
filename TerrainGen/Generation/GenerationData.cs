using System;
using System.Threading.Tasks;

namespace TerrainGen.Generation
{
    public struct GenerationData : IDisposable
    {
        public float[,] HeightNoise { get; set; }
        public float[,] HeatNoise { get; set; }
        public float[,] MoistureNoise { get; set; }

        public GenerationData(int size)
        {
            HeightNoise = new float[size, size];
            MoistureNoise = new float[size, size];
            HeatNoise = new float[size, size];
        }

        public static void Generate(OpenSimplexNoise noise, int size, float scale, Action<GenerationData> callBack)
        {
            var data = new GenerationData(size);

            float heightMin = float.MaxValue;
            float heightMax = float.MinValue;
            float heatMin = float.MaxValue;
            float heatMax = float.MinValue;
            float moistureMin = float.MaxValue;
            float moistureMax = float.MinValue;

            float radius = size / 2;

            int heightOffset = MapGeneration._random.Next(0, size * 10);
            int heatOffset = MapGeneration._random.Next(0, size * 10);
            int moistureOffset = MapGeneration._random.Next(0, size * 10);

            //generate each noise in a parallel loop
            ParallelLoopResult result = Parallel.For(0, size, y =>
            {
                for (var x = 0; x < size; x++)
                {
                    var heightNoise = noise.Noise(x + heightOffset - radius, y + heightOffset - radius, scale, 16);
                    var heatNoise = noise.Noise(x + heatOffset - radius, y + heatOffset - radius, scale * 2f, 9);
                    var moistureNoise = noise.Noise(x + moistureOffset - radius, y + moistureOffset - radius, scale * 2f, 9);

                    if (heightNoise > heightMax)
                        heightMax = heightNoise;
                    else if (heightNoise < heightMin)
                        heightMin = heightNoise;
                    data.HeightNoise[x, y] = heightNoise;

                    if (heatNoise > heatMax)
                        heatMax = heatNoise;
                    else if (heatNoise < heatMin)
                        heatMin = heatNoise;
                    data.HeatNoise[x, y] = heatNoise;

                    if (moistureNoise > moistureMax)
                        moistureMax = moistureNoise;
                    else if (moistureNoise < moistureMin)
                        moistureMin = moistureNoise;
                    data.MoistureNoise[x, y] = moistureNoise;
                }
            });

            Task.Factory.StartNew(() =>
            {
                //wait
                while (!result.IsCompleted) { Task.Delay(500); }

                //inLerp and clamp the values
                for (var y = 0; y < size; y++)
                    for (var x = 0; x < size; x++)
                    {
                        var heightNoise = data.HeightNoise[x, y];
                        var heatNoise = data.HeatNoise[x, y];
                        var moistureNoise = data.MoistureNoise[x, y];

                        data.HeightNoise[x, y] = Utils.clamp((heightNoise - heightMin) / (heightMax - heightMin));
                        data.HeatNoise[x, y] = Utils.clamp((heatNoise - heatMin) / (heatMax - heatMin));
                        data.MoistureNoise[x, y] = Utils.clamp((moistureNoise - moistureMin) / (moistureMax - moistureMin));
                    }

                callBack.Invoke(data);
            });
        }

        public void Dispose()
        {
            HeightNoise = null;
            HeatNoise = null;
            MoistureNoise = null;
        }
    }
}
