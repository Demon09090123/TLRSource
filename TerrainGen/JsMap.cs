using Ionic.Zlib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public struct MapData
    {
        public ushort GroundType { get; private set; }
        public ushort ObjectType { get; private set; }
    }
    public struct WorldMap
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public byte[] Data { get; private set; }

        [JsonIgnore]
        public MapData[,] RawData { get; set; }

        public string Export()
        {
            var strData = JsonConvert.SerializeObject(RawData);
            byte[] dataBuffer = Convert.FromBase64String(strData);
            var compressedBuffer = ZlibStream.CompressBuffer(dataBuffer);

            Data = compressedBuffer;

            return JsonConvert.SerializeObject(this);
        }

        public static void WriteToFile(string data, string filePath)
        {
            var dataBuffer = Convert.FromBase64String(data);
            var compressedBuffer = ZlibStream.CompressBuffer(dataBuffer);

            File.WriteAllBytes(filePath, compressedBuffer);
        }

        public static WorldMap Import(byte[] data)
        {
            var mapBuffer = ZlibStream.UncompressBuffer(data);
            var mapDataStr = Convert.ToBase64String(mapBuffer);
            var jsMap = JsonConvert.DeserializeObject<WorldMap>(mapDataStr);
            var dataBuffer = ZlibStream.UncompressBuffer(jsMap.Data);
            var strData = Convert.ToBase64String(dataBuffer);
            var rawData = JsonConvert.DeserializeObject<MapData[,]>(strData);

            jsMap.RawData = rawData;

            return jsMap;
        }
    }
}
