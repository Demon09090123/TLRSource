using System;
using System.Collections.Generic;
using System.Linq;

namespace TerrainGen
{
    public enum BiomeType
    {
        GrassLand,
        Desert,
        WetLand,
        HighLand,
        Swamp,
        Mountian,
        Snow,
        None
    }
    public class BiomeGeneration
    {
        public struct Biome
        {
            public float Weight { get; set; }
            public float Height { get; set; }
            public float Moisture { get; set; }
            public BiomeType Type { get; set; }

            public Biome(float weight, float maxHeight, float maxMoisture, BiomeType type)
            {
                Weight = weight;
                Height = maxHeight;
                Moisture = maxMoisture;
                Type = type;
            }

            public bool Validate(float height, float moisture)
            {
                var maxHeight = Height;
                var maxMoisture = Moisture;

                return height <= maxHeight && moisture <= maxMoisture;
            }
        }

        private const int SIZE = 6;

        private static readonly BiomeType[,] _biomes = new BiomeType[SIZE, SIZE - 1]
            {   //.2f                   .4f                  .6f                 .8                  .1                 1f      
                { BiomeType.Desert,  BiomeType.Desert,    BiomeType.HighLand, BiomeType.Mountian, BiomeType.Mountian}, // .0
                { BiomeType.Desert,  BiomeType.GrassLand,    BiomeType.HighLand, BiomeType.Mountian, BiomeType.Mountian}, // .2
                { BiomeType.WetLand, BiomeType.GrassLand, BiomeType.GrassLand, BiomeType.Mountian, BiomeType.Mountian}, //.4
                { BiomeType.WetLand,   BiomeType.GrassLand,  BiomeType.HighLand, BiomeType.Mountian, BiomeType.Snow}, // .6
                { BiomeType.WetLand,   BiomeType.WetLand,   BiomeType.HighLand, BiomeType.Snow,     BiomeType.Snow}, // .8
                { BiomeType.Swamp,     BiomeType.Swamp,     BiomeType.Snow,     BiomeType.Snow,     BiomeType.Snow} // 1
            };

        private static List<Biome> _biomesList;

        public static void Load()
        {
            _biomesList = new List<Biome>();

            Console.WriteLine(_biomes.GetLength(0)); //6
            Console.WriteLine(_biomes.GetLength(1)); //5

            for (var y = 0; y < _biomes.GetLength(0); y++)
            {
                var maxHeight = (y + 1) * .2f;
                for (var x = 0; x < _biomes.GetLength(1); x++)
                {
                    var maxMoisture = x * .2f;

                    _biomesList.Add(new Biome(0, maxHeight, maxMoisture, _biomes[y, x]));
                }
            }
            _biomesList.Reverse();

            Console.WriteLine("POL");
        }

        public static BiomeType GetBiome(float h, float m) //height, moisture
        {
            var type = BiomeType.None;

            if (h >= .1f)
            {
                foreach (var biome in _biomesList)
                    if (biome.Validate(h, m))
                        type = biome.Type;
            }

            return type;
        }
    }
}
