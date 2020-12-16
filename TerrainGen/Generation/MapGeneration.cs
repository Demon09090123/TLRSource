using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TerrainGen.Generation;
using TerrainGen.Generation.Shape;

namespace TerrainGen
{
    public class MapGeneration
    {
        private WorkManager _workManager;

        private OpenSimplexNoise _noise;
        public static Random _random;

        private BiomeGeneration _biomeGeneration;

        private FilterManager _filterManager;
        private Bitmap _shapeBitmap;
        private Bitmap _heightBitmap;
        private Bitmap _moistureBitmap;
/*
        public float[,] _shapeMap;
        public float[,] _heightMap;
        public float[,] _moistureMap;*/

        private int _size;
        private long _seed;
        private float _scale;

        private Action<Bitmap> _onDraw;

        public MapGeneration()
        {
            _workManager = new WorkManager();
            _random = new Random();

            SetSeed();
            _biomeGeneration = new BiomeGeneration();

            _size = 1024;
            _scale = (1024f / _size) * 0.00333f;

            _filterManager = new FilterManager(_size, 4);
            _shapeBitmap = new Bitmap(_size, _size);
            _heightBitmap = new Bitmap(_size, _size);
            _moistureBitmap = new Bitmap(_size, _size);

/*            _shapeMap = new float[_size, _size];
            _heightMap = new float[_size, _size];
            _moistureMap = new float[_size, _size];*/

            _workManager.Start();
        }

        public void AddonDraw(Action<Bitmap> draw) => _onDraw = draw;

        public void Resize(int size)
        {
            _size = size;
            _shapeBitmap = new Bitmap(_size, _size);
            _heightBitmap = new Bitmap(_size, _size);
            _moistureBitmap = new Bitmap(_size, _size);
        }

        public void ResetFilter(int quadCount) => _filterManager.Resize(_size, quadCount);
        
        public void SetSeed(long seed)
        {
            _seed = seed;
            _noise = new OpenSimplexNoise(_seed);
        }
        public void SetSeed() => SetSeed(_random.Next());
        public long GetSeed() => _seed;

        public void AddFilter(int qX, int qY, int type) => 
            _filterManager.AddFilter(qX, qY, type);

        private int _workDone;

        private void processNoise()
        {
            var bitmap = new Bitmap(_size, _size);

            for (int x = 0; x < _size; x++)
                for (int y = 0; y < _size; y++)
                {
                    /*float shape = _shapeMap[x, y];
                    float height = _heightMap[x, y];
                    float moisture = _moistureMap[x, y];*/

                    //var biome = _biomeGeneration.GetBiome(height);
                    //Color color = Color.Black;

                    /*switch (biome.Type)
                    {
                        case BiomeType.Meadow: color = Color.LightGreen; break;
                        case BiomeType.Forest: color = Color.DarkGreen; break;
                        case BiomeType.Desert: color = Color.Khaki; break;
                        case BiomeType.Mountians: color = Color.DarkGray; break;
                        case BiomeType.ScorchLands: color = Color.DarkRed; break;
                        case BiomeType.Void: color = Color.Black; break;
                    }

                    if (shape < .01)
                        color = Color.Black;*/

                    /*if (getRiver(moisture) && terrain < .75)
                        if (terrain <= .08f)
                            color = Color.DarkBlue;
                        else
                            color = Color.Blue;*/

                    var alpha1 = _shapeBitmap.GetPixel(x, y).A;
                    var alpha2 = _heightBitmap.GetPixel(x, y).A;
                    var alpha3 = _moistureBitmap.GetPixel(x, y).A;

                    var totalAlpha = alpha1;

                    bitmap.SetPixel(x, y, Color.FromArgb(totalAlpha, 0, 0, 0));
                }

            if (_onDraw != null)
                _onDraw?.Invoke(bitmap);
        }

        public void Generate()
        {
            _workDone = 0;

            _workManager.QueueWork(new GenerateWork(() =>
            {
                for (var x = 0; x < _size; x++)
                    for (var y = 0; y < _size; y++)
                    {
                        int filter = _filterManager.TotalFilterBitmap.GetPixel(x, y).A;
                        float noiseFilter = (filter * _noise.OctaveNoise(x, y, _scale, 16, 0.5f)) / 2;
                        float shapeAlpha = Math.Min(255, 255 * noiseFilter);

                        _shapeBitmap.SetPixel(x, y, Color.FromArgb(filter, 0, 0, 0));
                    }
            }, callBack));

            Task.Factory.StartNew(() =>
            {
                while (_workDone != 3)
                    Thread.Sleep(50);

                Console.WriteLine("Processing");

                processNoise();
            });
        }

        private void callBack() => _workDone++;
    }


}
