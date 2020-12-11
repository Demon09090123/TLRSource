using System;
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

                if (_heightNoise != null || _biomeNoise != null || _moistureNoise != null)
                {
                    _heightNoise = ResizeArray(_heightNoise, _size, _size);
                    _biomeNoise = ResizeArray(_biomeNoise, _size, _size);
                    _moistureNoise = ResizeArray(_moistureNoise, _size, _size);
                } else
                {
                    _heightNoise = new float[_size, _size];
                    _biomeNoise = new float[_size, _size];
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


        private float[,] _heightNoise; 
        private float[,] _biomeNoise;
        private float[,] _moistureNoise;

        private float _octave;
        private float _persistence;
        private float _scale;

        private int _taskDone;

        public TerrainGenerator()
        {
            _random = new Random();
        }

        public void Load(int size)
        {
            Size = size;
            RandomSeed();
            _scale = (1024f / Size) * 0.0033f;
            _octave = 16;
            _persistence = .5f;
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

                    ThreadPool.QueueUserWorkItem(new WaitCallback(doProcess), new Process()
                    {
                        xQuad = xQuad,
                        yQuad = yQuad
                    });
                }
            }

            Task.Factory.StartNew(() =>
            {
                var timer = new Stopwatch();
                timer.Start();

                while (_taskDone < 16)
                    Thread.Sleep(50); //retarded lock system
                _taskDone = 0;

                draw.Invoke(processBitmap());

                timer.Stop();
                Console.WriteLine("Completed: " + timer.ElapsedMilliseconds + " MS");
            });
        }

        private Color ApplyMoisture(Color clr, float moistureNoise)
        {
            if (moistureNoise < .35)
                return ControlPaint.Light(clr, .3f);
            else if (moistureNoise < .7)
                return ControlPaint.Light(clr, .1f);
            else
                return ControlPaint.Dark(clr, .2f);
        }

        private Bitmap processBitmap()
        {
            var bitmap = new Bitmap(Size, Size);

            Color color = Color.White;
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                {
                    float noise = _heightNoise[x, y];
                    float biome = _biomeNoise[x, y];
                    float moisture = _moistureNoise[x, y];

                    if (noise < .08)
                        color = Color.Black; //Border
                    else if (noise < .1)
                        color = Color.PaleVioletRed;
                    else if (noise > .9)
                        color = Color.Green; //Center
                    else
                    {
                        //Process rest
                        if (noise < .25)
                            color = ApplyMoisture(Color.Gray, biome);
                        else if (noise < .4)
                            color = ApplyMoisture(Color.Red, biome);
                        else if (noise < .7)
                            color = ApplyMoisture(Color.Orange, biome);
                        else
                            color = Color.DarkGreen;

                        if (moisture > 1.15)
                              color = Color.Blue;
                    }

                    bitmap.SetPixel(x, y, color);
                }

            return bitmap;
        }

        private void doProcess(object state)
        {
            var process = (Process)state;

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
                    float gradient = Math.Min(distanceX, distanceY) / (radius - 200.0f);

                    var noise = _noise.OctaveNoise(x, y, _scale, _octave, _persistence);
                    var biomeNoise = _noise.OctaveNoise(x, y, .01f, _octave, _persistence);
                    var riverNoise = _noise.RigidOctaveNoise(x, y, .0275f, 2);

                    _heightNoise[x, y] = noise * gradient;
                    _biomeNoise[x, y] = biomeNoise;
                    _moistureNoise[x, y] = riverNoise;
                }
            }
            Interlocked.Increment(ref _taskDone);
        }

    }

    public struct Process
    {
        public int xQuad { get; set; }
        public int yQuad { get; set; }
    }
}
