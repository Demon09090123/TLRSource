using System.IO;
using System.Text;

namespace TerrainGen
{
    public enum Region : byte
    {
        None,
        Spawn,
        Regen,
        Blocks_Sight,
        Note,
        Enemy_1,
        Enemy_2,
        Enemy_3,
        Enemy_4,
        Enemy_5,
        Enemy_6,
        Decoration_1,
        Decoration_2,
        Decoration_3,
        Decoration_4,
        Decoration_5,
        Decoration_6,
        Trigger_1,
        Callback_1,
        Trigger_2,
        Callback_2,
        Trigger_3,
        Callback_3,
        Trigger_4,
        Callback_4,
        Store_1,
        Store_2,
        Store_3,
        Store_4
    }
    public struct JSTile
    {
        public ushort GroundType { get; set; }
        public ushort ObjectType { get; set; }
        public Region Region;
    }
    public struct WorldMap
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public JSTile[,] Tiles { get; set; }

        public WorldMap(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new JSTile[Width, Height];
        }

        public void Export(Stream stream)
        {
            using (var br = new BinaryWriter(stream, Encoding.UTF8))
            {
                br.Write(Width);
                br.Write(Height);

                for (var x = 0; x < Width; x++)
                {
                    for (var y = 0; y < Height; y++)
                    {
                        var jsmap = Tiles[x, y];
                        br.Write(jsmap.GroundType);
                        br.Write(jsmap.ObjectType);
                        br.Write((byte)jsmap.Region);
                    }
                }
            }
        }

        public static WorldMap Import(FileStream stream)
        {
            using (var rdr = new BinaryReader(stream, Encoding.UTF8))
            {
                var data = new WorldMap(rdr.ReadInt32(), rdr.ReadInt32());

                for (var x = 0; x < data.Width; x++)
                    for (var y = 0; y < data.Height; y++)
                        data.Tiles[x, y] = new JSTile()
                        {
                            GroundType = rdr.ReadUInt16(),
                            ObjectType = rdr.ReadUInt16(),
                            Region = (Region)rdr.ReadByte()
                        };

                return data;
            }
        }
    }
}
