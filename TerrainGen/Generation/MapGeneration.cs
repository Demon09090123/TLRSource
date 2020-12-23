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

        private Action<Bitmap> _onDraw;

        public MapGeneration()
        {
            _workManager = new WorkManager();
            _random = new Random();

            SetSeed();

            _size = 1024;
            _scale = (1024f / _size) * 0.00333f;

            _workManager.Start();
            _filter = new Filter(Filter.FalloutFilter(_size), 0, 0);
            BiomeGeneration.Load();
        }

        public void AddonDraw(Action<Bitmap> draw) => _onDraw = draw;
        public void Resize(int size)
        {
            _size = size;
            _filter = new Filter(Filter.FalloutFilter(_size), 0, 0);
        }
        public void SetSeed(long seed)
        {
            _seed = seed;
            _noise = new OpenSimplexNoise(_seed);
        }
        public void SetSeed() => SetSeed(_random.Next());
        public long GetSeed() => _seed;

        private Filter _filter;

        public void Generate()
        {
            _workManager.QueueWork(new GenerateWork(() =>
            {
                var bitmap = new DirBitmap(_size, _size);
                var noiseMap = _noise.Noise2D(_size, _size, _scale, 16);
                var moistureMap = _noise.Noise2D(_size, _size, _scale * 3f, 16);
                var filterMap = _filter.FilterMap;

                for (var y = 0; y < _size; y++)
                {
                    for (var x = 0; x < _size; x++)
                    {
                        var n = noiseMap[x, y];
                        var f = filterMap[x, y];
                        var m = moistureMap[x, y];

                        var h = n * f;

                        var biome = BiomeGeneration.GetBiome(h, m);

                        Color color = Color.Black;

                        switch(biome)
                        {
                            case BiomeType.Desert: color = Color.LightYellow; break;
                            case BiomeType.GrassLand: color = Color.Green; break;
                            case BiomeType.WetLand: color = Color.SlateBlue; break;
                            case BiomeType.HighLand: color = Color.DarkGreen; break;
                            case BiomeType.Swamp: color = Color.PaleGreen; break;
                            case BiomeType.Mountian: color = Color.Gray; break;
                            case BiomeType.Snow: color = Color.White; break;
                            case BiomeType.None: color = Color.Black; break;
                        }

                        bitmap.SetPixel(x, y, color);
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
