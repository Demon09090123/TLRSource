using System;
using System.Collections.Generic;
using System.Drawing;
using TerrainGen.Generation;

namespace TerrainGen
{
    public class MapGeneration
    {
        public static Random _random;

        private WorkManager _workManager;
        private OpenSimplexNoise _noise;

        private int _size;
        private long _seed;
        private float _scale;

        private Action<Image> _onDraw;

        public MapGeneration()
        {
            _workManager = new WorkManager();
            _random = new Random();

            SetSeed();
            Resize(4096);

            _workManager.Start();
        }

        private void reScale() => _scale = (1024f / _size) * 0.00333f;

        public void AddonDraw(Action<Image> draw) => _onDraw = draw;
        public void Resize(int size)
        {
            _size = size;
            reScale();
            _filter = Filter.FalloutFilter(_size);
        }
        public void SetSeed(long seed)
        {
            _seed = seed;
            _noise = new OpenSimplexNoise(_seed);
        }
        public void SetSeed() => SetSeed(_random.Next());
        public long GetSeed() => _seed;

        private float[,] _filter;

        private bool _generated = false;

        public void Generate()
        {
            _workManager.QueueWork(new GenerateWork(() =>
            {
                var bitmap = new DirBitmap(_size, _size);

                var noiseMap = _noise.Noise2D(_size, _size, _scale, 16);
                var moistureMap = _noise.Noise2D(_size, _size, _scale * 2f, 6);
                var heatMap = _noise.Noise2D(_size, _size, _scale * 2f, 6);
                var filterMap = _filter;

                for (var y = 0; y < _size; y++)
                {
                    for (var x = 0; x < _size; x++)
                    {
                        var noise = noiseMap[x, y];
                        var filter = filterMap[x, y];
                        var moisture = moistureMap[x, y];
                        var heat = heatMap[x, y];

                        var height = filter * noise;

                        Color color = BiomeGeneration.GetBiomeColor(
                            BiomeGeneration.GetBiome(height, moisture, heat));

                        bitmap.SetPixel(x, y, color);
                    }
                }

                _generated = true;

                _onDraw.Invoke(bitmap.Bitmap.Clone() as Image);

                bitmap.Dispose();
                noiseMap = null;
                moistureMap = null;
                heatMap = null;

            }, callBack));
        }

        private void callBack()
        {
            Console.WriteLine("Map Generated!");
        }
    }
}
