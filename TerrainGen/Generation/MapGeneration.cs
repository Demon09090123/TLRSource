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
        public const int QUAD_SIZE = 32;

        private OpenSimplexNoise _noise;
        private Random _random;
        private MapMask _mapMask;

        public float[,] _shapeMap;
        public float[,] _heightMap;
        public float[,] _moistureMap;

        private int _size;
        private int _seed;
        private float _scale;


        public MapGeneration(Bitmap mask, int size = 1024)
        {
            _random = new Random();

            _seed = _random.Next();
            _size = size;
            _scale = (1024f / _size) * 0.0033f;

            _noise = new OpenSimplexNoise(_seed);
            _mapMask = new MapMask(mask);
        }

        private void resizeNoise()
        {
            _shapeMap = new float[_size, _size];
            _heightMap = new float[_size, _size];
            _moistureMap = new float[_size, _size];
        }

        
        private void generateMap()
        {
            for (var xQuad = 0; xQuad < QUAD_SIZE; xQuad++)
            {
                var startX = xQuad * QUAD_SIZE;
                var endX = (xQuad + 1) * QUAD_SIZE;

                for (var yQuad = 0; yQuad < QUAD_SIZE; yQuad++)
                {
                    var startY = yQuad * QUAD_SIZE;
                    var endY = (yQuad + 1) * QUAD_SIZE;

                    generateShape(startX, endX, startY, endY);
                }
            }
        }

        private void generateShape(int startX, int endX, int startY, int endY)
        {
            var quadX = startX / QUAD_SIZE;
            var quadY = startY / QUAD_SIZE;

            for (var x = startX; x < endX; x++)
            {
                for (var y = startY; y < endY; y++)
                {
                    var quadPos = new Position(quadX, quadY);

                    var quadAlpha = _mapMask.ShapeMask[quadPos];
                    var shapeNoise = _noise.OctaveNoise(x, y, _scale, 8, .5f);

                    _shapeMap[x, y] = quadAlpha * shapeNoise;

                    ThreadPool.QueueUserWorkItem(new WaitCallback())
                }
            }
        }

        private void generateHeight()
        {
        }
    }


}
