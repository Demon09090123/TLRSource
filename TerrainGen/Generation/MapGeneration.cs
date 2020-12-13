using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TerrainGen.Generation;

namespace TerrainGen
{
    public class MapGeneration
    {
        private WorkManager _workManager;

        private OpenSimplexNoise _noise;
        private Random _random;

        private BiomeGeneration _biomeGeneration;

        public float[,] _shapeMap;
        public float[,] _heightMap;
        public float[,] _moistureMap;

        private int _size;
        private long _seed;
        private float _scale;

        private Action<Bitmap> _onDraw;

        public MapGeneration(Bitmap mask)
        {
            _workManager = new WorkManager();
            _random = new Random();

            SetSeed();
            _biomeGeneration = new BiomeGeneration();

            _size = 1024;
            _scale = (1024f / _size) * 0.00333f; 
            _shapeMap = new float[_size, _size];
            _heightMap = new float[_size, _size];
            _moistureMap = new float[_size, _size];

            _workManager.Start();
        }

        public void AddonDraw(Action<Bitmap> draw) => _onDraw = draw;

        public void Resize(int size)
        {
            _size = size;
            _shapeMap = Utils.ResizeArray(_shapeMap, _size, _size);
            _heightMap = Utils.ResizeArray(_heightMap, _size, _size);
            _moistureMap = Utils.ResizeArray(_shapeMap, _size, _size);
        }
        public void SetSeed(long seed)
        {
            _seed = seed;
            _noise = new OpenSimplexNoise(_seed);
        }
        public void SetSeed() => SetSeed(_random.Next());
        public long GetSeed() => _seed;

        private int _workDone;
        private int _quadSize;
        private const int _quadLength = 4; // 4 by 4 grid

        private void generateShapeMap()
        {

        }

        private void generateNoiseMap(int startX, int startY, int endX, int endY)
        {

            for (var x = startX; x < endX; x++)
            {
                //int quadX = x / _mapMask.PixelLength;

                for (var y = startY; y < endY; y++)
                {
                    //int quadY = y / _mapMask.PixelLength; 

                    //var quadPos = new Position(quadX, quadY);

                    //var quadAlpha = _mapMask.regionAlpha[x, y];
                    var shapeNoise = _noise.OctaveNoise(x, y, _scale, 8, .5f);
                    var heightNoise = _noise.OctaveNoise(x, y, _scale, 16, .5f);
                    var moistureNoise = _noise.OctaveNoise(x, y, _scale, 3, .5f);

                    _shapeMap[x, y] = shapeNoise;
                    _heightMap[x, y] = heightNoise;
                    _moistureMap[x, y] = moistureNoise;
                }
            }
        }

        private void processNoise()
        {
            var bitmap = new Bitmap(_size, _size);

            for (int x = 0; x < _size; x++)
                for (int y = 0; y < _size; y++)
                {
                    float shape = _shapeMap[x, y];
                    float height = _heightMap[x, y];
                    float moisture = _moistureMap[x, y];

                    var biome = _biomeGeneration.GetBiome(height);
                    Color color = Color.Black;

                    switch (biome.Type)
                    {
                        case BiomeType.Meadow: color = Color.LightGreen; break;
                        case BiomeType.Forest: color = Color.DarkGreen; break;
                        case BiomeType.Desert: color = Color.Khaki; break;
                        case BiomeType.Mountians: color = Color.DarkGray; break;
                        case BiomeType.ScorchLands: color = Color.DarkRed; break;
                        case BiomeType.Void: color = Color.Black; break;
                    }

                    if (shape < .01)
                        color = Color.Black;

                    /*if (getRiver(moisture) && terrain < .75)
                        if (terrain <= .08f)
                            color = Color.DarkBlue;
                        else
                            color = Color.Blue;*/

                    bitmap.SetPixel(x, y, color);
                }

            if (_onDraw != null)
                _onDraw?.Invoke(bitmap);
        }

        public void Generate()
        {
            _workDone = 0;
            _quadSize = _size / _quadLength;

            for (var xQuad = 0; xQuad < _quadLength; xQuad++)
            {
                var startX = xQuad * _quadSize;
                var endX = (xQuad + 1) * _quadSize;

                for (var yQuad = 0; yQuad < _quadLength; yQuad++)
                {
                    var startY = yQuad * _quadSize;
                    var endY = (yQuad + 1) * _quadSize;

                    _workManager.QueueWork(new GenerateWork(() => generateNoiseMap(startX, startY, endX, endY), callBack));
                }
            }

            Task.Factory.StartNew(() =>
            {
                while (_workDone != _quadLength * _quadLength)
                    Thread.Sleep(50);

                processNoise();
            });
        }

        private void callBack() => _workDone++;
    }


}
