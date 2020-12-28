namespace TerrainGen.Editor
{
    public struct EditorTile 
    {
        public JSTile Data { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override int GetHashCode() => (X << 16 ^ Y);
    }
    public enum EditType
    {
        Ground,
        Object,
        Region
    }
    public class MapEditor
    {
        private WorldMap _mapData;

        private int _width;
        private int _height;

        private EditorTile[,] _tiles;

        public MapEditor(int width, int height)
        {
            Resize(width, height);
        }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;
            _tiles = new EditorTile[_width, _height];

            for (var y = 0; y < _height; y++)
                for (var x = 0; x < _width; x++)
                    _tiles[x, y] = new EditorTile()
                    {
                        X = x,
                        Y = y,
                        Data = new JSTile()
                        {
                            GroundType = ushort.MinValue,
                            ObjectType = ushort.MinValue,
                            Region = Region.None
                        }
                    };
        }

        public void Clear()
        {
            for (var y = 0; y < _height; y++)
                for (var x = 0; x < _width; x++)
                    _tiles[x, y].Data = new JSTile()
                    {
                        GroundType = ushort.MinValue,
                        ObjectType = ushort.MinValue,
                        Region = Region.None,
                    };
        }

        public void RemoveData(int x, int y, EditType type)
        {
            if (x < 0 && x >= _width && y < 0 && y >= _height)
                return;

            var tile = _tiles[x, y];

            switch (type)
            {
                case EditType.Ground:
                    {
                        _tiles[x, y].Data = new JSTile()
                        {
                            GroundType = ushort.MinValue,
                            ObjectType = tile.Data.ObjectType,
                            Region = tile.Data.Region
                        };
                    } break;
                case EditType.Object:
                    {
                        _tiles[x, y].Data = new JSTile()
                        {
                            GroundType = tile.Data.GroundType,
                            ObjectType = ushort.MinValue,
                            Region = tile.Data.Region
                        };
                    } break;
                case EditType.Region:
                    {
                        _tiles[x, y].Data = new JSTile()
                        {
                            GroundType = tile.Data.GroundType,
                            ObjectType = tile.Data.ObjectType,
                            Region = Region.None
                        };
                    } break;
            }
        }

        public void SetData(int x, int y, ushort data, EditType type)
        {
            if (x < 0 && x >= _width && y < 0 && y >= _height)
                return;

            var tile = _tiles[x, y];

            switch(type)
            {
                case EditType.Ground:
                    {
                        _tiles[x, y].Data = new JSTile()
                        {
                            GroundType = data,
                            ObjectType = tile.Data.ObjectType,
                            Region = tile.Data.Region
                        };
                    } break;
                case EditType.Object:
                    {
                        _tiles[x, y].Data = new JSTile()
                        {
                            GroundType = tile.Data.GroundType,
                            ObjectType = data,
                            Region = tile.Data.Region
                        };
                    } break;
                case EditType.Region:
                    {
                        _tiles[x, y].Data = new JSTile()
                        {
                            GroundType = tile.Data.GroundType,
                            ObjectType = tile.Data.ObjectType,
                            Region = (Region)data
                        };
                    } break;
            }
        }
    }
}
