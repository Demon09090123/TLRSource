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
        private BiomeGeneration _biomeGeneration;

        private int _size;
        private long _seed;
        private float _scale;

        private Action<Bitmap> _onDraw;

        public MapGeneration()
        {
            _filters = new List<Filter>();
            _workManager = new WorkManager();
            _random = new Random();

            SetSeed();
            _biomeGeneration = new BiomeGeneration();

            _size = 1024;
            _scale = (1024f / _size) * 0.00333f;

            _workManager.Start();
        }

        public void AddonDraw(Action<Bitmap> draw) => _onDraw = draw;

        public void Resize(int size) => _size = size;
        public void SetSeed(long seed)
        {
            _seed = seed;
            _noise = new OpenSimplexNoise(_seed);
        }
        public void SetSeed() => SetSeed(_random.Next());
        public long GetSeed() => _seed;

        private List<Filter> _filters;
        public void AddFilter(int gridX, int gridY, int size)
        {
            int mapScale = _size / 200;
            int filterSize = size * mapScale;
            int filterX = gridX * mapScale;
            int filterY = gridY * mapScale;

            _filters.Add(new Filter(Filter.FalloutFilter(filterSize), filterX, filterY));
        }

        public void Generate()
        {
            _workManager.QueueWork(new GenerateWork(() =>
            {
                var bitmap = new DirBitmap(_size, _size);
                var noiseMap = _noise.Noise2D(_size, _size, _scale, 16);
                var filterMap = new float[_size, _size];

                foreach(var filter in _filters)
                {
                    var w = filter.FilterMap.GetLength(0);
                    var h = filter.FilterMap.GetLength(1);
                    for (var y = 0; y < h; y++)
                        for (var x = 0; x < w; x++)
                        {
                            var mapX = filter.mapX + x;
                            var mapY = filter.mapY + y;

                            filterMap[mapX, mapY] = filter.FilterMap[x, y];
                        }
                }

                for (var y = 0; y < _size; y++)
                {
                    for (var x = 0; x < _size; x++)
                    {
                        var n = noiseMap[x, y];
                        var f = filterMap[x, y];
                        int alpha = 0;

                        if (f > 0.0f)
                            alpha = (int)(Utils.clamp(n + (1.0f - f)) * 255.0f);

                        bitmap.SetPixel(x, y, Color.FromArgb(alpha, 0, 0, 0));
                    }
                }

                _onDraw.Invoke(bitmap.Bitmap);
            }, callBack));
        }

        private void callBack()
        {
            Console.WriteLine("Generated!");
        }
    }
}
