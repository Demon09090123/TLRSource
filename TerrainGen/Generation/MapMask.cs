using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TerrainGen.Generation
{
    public struct Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position left, Position right) => left.X == right.X && left.Y == right.Y;
        public static bool operator !=(Position left, Position right) => left.X != right.X || left.Y != right.Y;
        public override bool Equals(object obj)
        {
            var pos = (Position)obj;
            return X == pos.X && Y == pos.Y;
        }
        public override int GetHashCode() => GetHashCode(); // Maybe add quicker hashing
    }
    public class MapMask
    {
        public Dictionary<Position, float> ShapeMask { get; private set; } //Opacity level of overall map
        public List<List<Position>> RegionPosition { get; private set; } //Individual island quad positions

        private Bitmap _mask;

        public MapMask(Bitmap mask)
        {
            ShapeMask = new Dictionary<Position, float>();
            RegionPosition = new List<List<Position>>();

            _mask = mask;
        }

        private void generateMask()
        {
            if (_mask.Width > MapGeneration.QUAD_SIZE || _mask.Height > MapGeneration.QUAD_SIZE)
                Utils.ResizeImage(_mask, MapGeneration.QUAD_SIZE, MapGeneration.QUAD_SIZE);

            for (var x = 0; x < MapGeneration.QUAD_SIZE; x++)
                for (var y = 0; y < MapGeneration.QUAD_SIZE; y++)
                {
                    var pixelAlpha = _mask.GetPixel(x, y).A / 127.5f;
                    ShapeMask.Add(new Position(x, y), pixelAlpha);
                }

            findRegions();
        }

        private void findRegions()
        {
            var exploredRegions = new List<Position>();

            while(exploredRegions.Count < MapGeneration.QUAD_SIZE * MapGeneration.QUAD_SIZE)
            {
                var regionPosition = new List<Position>();

                var initialPosition = ShapeMask.Where(_ => _.Value > 0 && exploredRegions.Contains(_.Key)).FirstOrDefault().Key;
                //var neighbors = getNeighbors(initialPosition);

                loop(initialPosition);

                RegionPosition.Add(regionPosition);

                void loop(Position parent)
                {
                    exploredRegions.Add(parent);
                    regionPosition.Add(parent);

                    var neighbors = getNeighbors(parent);

                    foreach (var pos in neighbors)
                        if (!exploredRegions.Contains(pos))
                            loop(pos);
                }
            }
        }

        private List<Position> getNeighbors(Position pos)
        {
            var neighbors = new List<Position>();
            var x = pos.X;
            var y = pos.Y;

            if (isValidPosition(x, y + 1)) //up
                neighbors.Add(new Position(x, y + 1));
            if (isValidPosition(x + 1, y)) //right
                neighbors.Add(new Position(x + 1, y));
            if (isValidPosition(x, y - 1))
                neighbors.Add(new Position(x, y - 1));
            if (isValidPosition(x - 1, y))
                neighbors.Add(new Position(x - 1, y));

            return neighbors;
        }

        

        private bool isValidPosition(int x, int y)
        {
            if (x < MapGeneration.QUAD_SIZE && x >= 0 && y < MapGeneration.QUAD_SIZE && y >= 0)
                return ShapeMask[new Position(x, y)] > 0;
            return false;
        }
    }
}
