using Ionic.Zlib;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TerrainGen.Generation;

namespace TerrainGen
{
    public class MapGeneration
    {
        public static readonly Random _random = new Random();

        private WorkManager _workManager;
        private OpenSimplexNoise _noise;
        private WorldMap _worldMap;

        private int _size;
        private long _seed;
        private float _scale;

        private Action<Image> _onDraw;

        private JSTile[,] _mapData;
        private float[,] _filter;
        private float[,] _noiseMap;
        private float[,] _moistureMap;
        private float[,] _heatMap;

        public MapGeneration()
        {
            _workManager = new WorkManager();

            SetSeed();
            Resize(1024);

            _workManager.Start();
        }

        private void reScale() => _scale = (1024f / _size) * 0.00333f;

        public void AddonDraw(Action<Image> draw) => _onDraw = draw;
        public void Resize(int size)
        {
            _size = size;
            reScale();
            _filter = Filter.FalloutFilter(_size);
            _worldMap = new WorldMap(_size);
        }
        public void SetSeed(long seed)
        {
            _seed = seed;
            _noise = new OpenSimplexNoise(_seed);
        }

        public void SetSeed() => SetSeed(_random.Next());
        public long GetSeed() => _seed;

        public void SaveToFile()
        {
            var saveDialog = new SaveFileDialog();

            saveDialog.FileName = "mapData";
            saveDialog.DefaultExt = "map";
            saveDialog.InitialDirectory = @"C:\Users\mangz\OneDrive\Documents\mapData\";

            var f = File.Create(saveDialog.InitialDirectory + "mapData.map");
            f.Close();

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using(var stream = saveDialog.OpenFile())
                {
                    var dataBuffer = Encoding.ASCII.GetBytes(_worldMap.Export());
                    var compressedBuffer = ZlibStream.CompressBuffer(dataBuffer);

                    stream.Write(compressedBuffer, 0, compressedBuffer.Length);
                }
            }
        }

        public void Generate()
        {
            var workNeeded = 3;
            var workDone = 0;

            _noiseMap = new float[_size, _size];
            _moistureMap = new float[_size, _size];
            _heatMap = new float[_size, _size];
            _mapData = new JSTile[_size, _size];

            _workManager.QueueWork(new GenerateWork(() => _noiseMap = _noise.Noise2D(_size, _size, _scale, 16), callBack));
            _workManager.QueueWork(new GenerateWork(() => _moistureMap = _noise.Noise2D(_size, _size, _scale * 2f, 6), callBack));
            _workManager.QueueWork(new GenerateWork(() => _heatMap = _noise.Noise2D(_size, _size, _scale * 2f, 6), callBack));

            void callBack()
            {
                workDone++;

                if (workDone == workNeeded)
                {
                    var bitmap = new DirBitmap(_size, _size);

                    _workManager.QueueWork(new GenerateWork(() =>
                    {
                        for (var y = 0; y < _size; y++)
                            for (var x = 0; x < _size; x++)
                            {
                                var noise = _noiseMap[x, y];
                                var filter = _filter[x, y];
                                var moisture = _moistureMap[x, y];
                                var heat = _heatMap[x, y];
                                var height = filter * noise;

                                var biome = BiomeGeneration.GetBiome(height, moisture, heat);

                                Color color = BiomeGeneration.GetBiomeColor(biome);

                                _mapData[x, y] = new JSTile()
                                {
                                    GroundType = BiomeGeneration.GetBiomeObjectType(biome)
                                };

                                bitmap.SetPixel(x, y, color);
                            }

                        _onDraw.Invoke(bitmap.Bitmap.Clone() as Image);

                    }, () =>
                    {
                        string strData = JsonConvert.SerializeObject(_mapData);
                        byte[] dataBuffer = Encoding.ASCII.GetBytes(strData);
                        var compressedBuffer = ZlibStream.CompressBuffer(dataBuffer);

                        _worldMap.Data = compressedBuffer;

                        _noiseMap = null;
                        _moistureMap = null;
                        _heatMap = null;
                        _mapData = null;
                        bitmap.Dispose();
                    }));
                }
            }
        }
    }
}
