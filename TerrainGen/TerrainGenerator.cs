using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerrainGen
{
    public class TerrainGenerator
    {
        private long _seed;
        public long Seed
        {
            get => _seed;
            set
            {
                _seed = value;
                _noise = new OpenSimplexNoise(_seed);
            }
        }

        private int _size;
        public int Size
        {
            get => _size;
            set
            {
                _size = value;

                if (_heightNoise != null || _terrainNoise != null || _moistureNoise != null)
                {
                    _heightNoise = ResizeArray(_heightNoise, _size, _size);
                    _terrainNoise = ResizeArray(_terrainNoise, _size, _size);
                    _moistureNoise = ResizeArray(_moistureNoise, _size, _size);
                } else
                {
                    _heightNoise = new float[_size, _size];
                    _terrainNoise = new float[_size, _size];
                    _moistureNoise = new float[_size, _size];
                }
                
            }
        }
        protected T[,] ResizeArray<T>(T[,] original, int x, int y)
        {
            T[,] newArray = new T[x, y];
            int minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
            int minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

            for (int i = 0; i < minY; ++i)
                Array.Copy(original, i * original.GetLength(0), newArray, i * newArray.GetLength(0), minX);

            return newArray;
        }


        private OpenSimplexNoise _noise;
        private Random _random;

        private WorkManager _workManager;

        private BiomeGeneration _biomeGeneration;


        private float[,] _terrainNoise;
        private float[,] _heightNoise;
        private float[,] _moistureNoise;

        private float _octave;
        private float _persistence;
        private float _scale;

        private int _taskDone;



        public TerrainGenerator()
        {
            _random = new Random();
            _workManager = new WorkManager();
            _biomeGeneration = new BiomeGeneration();
        }

        public void Load(int size)
        {
            Size = size;
            RandomSeed();
            _scale = (1024f / Size) * 0.0033f;
            _octave = 16;
            _persistence = .5f;

            _workManager.Start();
        }

        public void RandomSeed() => Seed = _random.Next();

        public void GenerateTerrain(Action<Bitmap> draw)
        {
            var radius = Size / 2;
            var halfRadius = radius / 2;
            for (var xQuad = 0; xQuad < 4; xQuad++)
            {
                var xTo = halfRadius * (xQuad + 1);
                var xStart = halfRadius * xQuad;

                for (var yQuad = 0; yQuad < 4; yQuad++)
                {
                    var yTo = halfRadius * (yQuad + 1);
                    var yStart = halfRadius * yQuad;

                    _workManager.QueueWork(doProcess, new Process()
                    {
                        xQuad = xQuad,
                        yQuad = yQuad
                    });
                }
            }

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);

                while (_workManager.CurrentWorkPool != 0)
                    Thread.Sleep(50);

                draw.Invoke(processBitmap());
            });
        }


        private bool getRiver(float moisture)
        {
            if (moisture < .3 && moisture > .27)
                return true;
            return false;
        }


        private Bitmap processBitmap()
        {
            var bitmap = new Bitmap(Size, Size);

            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                {
                    float terrain = _terrainNoise[x, y];
                    float height = _heightNoise[x, y];
                    float moisture =  _moistureNoise[x, y];

                    var biome = _biomeGeneration.GetBiome(terrain, height);
                    Color color = Color.Black;

                    switch(biome.Type)
                    {
                        case BiomeType.Meadow: color = Color.LightGreen; break;
                        case BiomeType.Forest: color = Color.DarkGreen; break;
                        case BiomeType.Desert: color = Color.Khaki; break;
                        case BiomeType.Mountians: color = Color.DarkGray; break;
                        case BiomeType.ScorchLands: color = Color.DarkRed; break;
                        case BiomeType.Void: color = Color.Black; break;
                    }

                    /*if (getRiver(moisture) && terrain < .75)
                        if (terrain <= .08f)
                            color = Color.DarkBlue;
                        else
                            color = Color.Blue;*/

                    bitmap.SetPixel(x, y, color);
                }

            return bitmap;
        }

        private void doProcess(Process state)
        {
            var process = state;

            var radius = Size / 2;
            var halfRadius = radius / 2;
            var xTo = halfRadius * (process.xQuad + 1);
            var xStart = halfRadius * process.xQuad; 
            var yTo = halfRadius * (process.yQuad + 1);
            var yStart = halfRadius * process.yQuad;

            for (var x = xStart; x < xTo; x++)
            {
                var distanceX = radius - Math.Abs(radius - x);
                for (var y = yStart; y < yTo; y++)
                {
                    var distanceY = radius - Math.Abs(radius - y); 
                    float gradient = Math.Min(distanceX, distanceY) / (radius - (Size / 10f));

                    var noise = _noise.OctaveNoise(x, y, _scale, _octave, _persistence);
                    var moistureNoise = _noise.RigidOctaveNoise(x, y, .0025f, 9);

                    _terrainNoise[x, y] = noise * gradient;
                    _heightNoise[x, y] = noise;
                    _moistureNoise[x, y] = moistureNoise;
                }
            }
            Interlocked.Increment(ref _taskDone);
        }
    }
    public class Process
    {
        public int xQuad { get; set; }
        public int yQuad { get; set; }
    }
}
