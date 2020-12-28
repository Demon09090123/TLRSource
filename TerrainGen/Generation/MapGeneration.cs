using System;
using System.Drawing;
using System.IO;
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

        private float[,] _filter;

        public MapGeneration()
        {
            _workManager = new WorkManager();

            SetSeed();
            Resize(2048);

            _workManager.Start();
        }

        private void reScale() => _scale = (1024f / _size) * 0.00333f;

        public void AddonDraw(Action<Image> draw) => _onDraw = draw;
        public void Resize(int size)
        {
            _size = size;
            reScale();
            _filter = Filter.FalloutFilter(_size);
            _worldMap = new WorldMap(_size, _size);
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
            saveDialog.InitialDirectory = @"D:\MapData\";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (!saveDialog.CheckFileExists)
                {
                    var f = File.Create(saveDialog.InitialDirectory + "mapData.map");
                    f.Close();
                }

                _worldMap.Export(saveDialog.OpenFile());
            }
        }

        public void Generate() =>
            GenerationData.Generate(_noise, _size, _scale, (data) =>
            {
                var image = new DirBitmap(_size, _size);

                for (var y = 0; y < _size; y++)
                    for (var x = 0; x < _size; x++)
                    {
                        var noise = data.HeightNoise[x, y];
                        var filter = _filter[x, y];
                        var moisture = data.MoistureNoise[x, y];
                        var heat = data.HeatNoise[x, y];
                        var height = filter * noise;

                        var biome = BiomeGeneration.GetBiome(height, moisture, heat);
                        var color = BiomeGeneration.GetBiomeColor(biome);

                        _worldMap.Tiles[x, y] = new JSTile()
                        {
                            GroundType = BiomeGeneration.GetBiomeObjectType(biome)
                        };

                        image.SetPixel(x, y, color);
                    }

                _onDraw.Invoke(image.Bitmap.Clone() as Image);

                data.Dispose();
                image.Dispose();
            });
    }
}
