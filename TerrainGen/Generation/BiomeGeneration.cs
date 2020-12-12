using System;
using System.Collections.Generic;
using System.Linq;

namespace TerrainGen
{
    public class BiomeGeneration
    {
        private readonly List<Biome> _biomes = new List<Biome>()
        {
            new Biome(BiomeType.Meadow, new Threshold(.78f, 2f), new Threshold(-1, -1)),
            new Biome(BiomeType.Forest, new Threshold(.6f, .78f), new Threshold(.6f, .78f)),
            new Biome(BiomeType.Desert, new Threshold(.4f, .6f), new Threshold(.4f, .6f)),
            new Biome(BiomeType.Mountians, new Threshold(.23f, .4f), new Threshold(.25f, .4f)),
            new Biome(BiomeType.ScorchLands, new Threshold(.08f, .23f), new Threshold(.08f, .1f)),
            new Biome(BiomeType.Void, new Threshold(-2f, .08f), new Threshold(-1, -1))
        };


        public BiomeGeneration()
        {

        }

        public Biome GetBiome(float terrain, float height) //HANDLE HEIGHT LATER
        {
            return _biomes.Where(_ => _.IsValidTerrain(terrain)).FirstOrDefault();
        }
    }

    public struct Threshold
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public Threshold(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public bool CompareTo(float factor) => factor < Max && factor > Min;
        public static Threshold Blank() => new Threshold(-1, -1);
    }

    public enum BiomeType
    {
        //main-Biomes
        Meadow,
        Forest,
        Desert,
        Mountians,
        ScorchLands,
        //sub-Biomes
        Swamp,
        Hills,
        SnowCap,
        LavaLake,
        //void
        Void
    }

    public struct Biome
    {
        public BiomeType Type { get; private set; }
        public Threshold Terrain { get; private set; }
        public Threshold Height { get; private set; }

        public Biome(BiomeType biome, Threshold terrain, Threshold height)
        {
            Type = biome;
            Terrain = terrain;
            Height = height;
        }

        public BiomeType GetBiomeType()
        {
            switch (Type) //HANDLE SUB_BIOMES
            {  
                case BiomeType.Forest: return BiomeType.Forest;
                case BiomeType.Desert: return BiomeType.Desert;
                case BiomeType.Mountians: return BiomeType.Mountians;
                case BiomeType.ScorchLands: return BiomeType.ScorchLands;
                default: return Type;
            }
        }

        public bool IsValidTerrain(float compareTo) => Terrain.CompareTo(compareTo);
        public bool IsValidHeight(float compareTo) => Height.CompareTo(compareTo);
    }
}
