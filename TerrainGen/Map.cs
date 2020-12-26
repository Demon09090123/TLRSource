using Ionic.Zlib;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace TerrainGen
{
    public enum Region
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
        public string Key;
    }
    public struct WorldMap : IDisposable
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public byte[] Data { get; set; }


        public WorldMap(int size)
        {
            Width = size;
            Height = size;
            Data = null;
        }

        public string Export() => JsonConvert.SerializeObject(this);

        public void Dispose()
        {
            Data = null;
        }

        public static WorldMap Import(byte[] data)
        {
            var mapBuffer = ZlibStream.UncompressBuffer(data);
            var mapDataStr = Encoding.ASCII.GetString(mapBuffer);
            var jsMap = JsonConvert.DeserializeObject<WorldMap>(mapDataStr);

            return jsMap;
        }
    }
}
