using System.Drawing;

namespace TerrainGen
{
    public enum BiomeType
    {
        None,
        Beach,
        Plains,
        Forest,
        Desert,
        ScorchLand,
        WetLand,
        HighLand,
        Swamp,
        Mountian,
        Snow
    }

    public class BiomeGeneration
    {
        private static BiomeType getLowBiomes(float m, float h)
        {
            if (m < .45f && h > .6f)
                return BiomeType.Desert;
            else if (m > .7f && h > .2f)
                return BiomeType.WetLand;

            return BiomeType.Plains;
        }

        private static BiomeType getMidBiomes(float m, float h)
        {
            if (m < .45f && h > .7f)
                return BiomeType.ScorchLand;
            else if (m > .7f && h > .5f)
                return BiomeType.Swamp;
            return BiomeType.Forest;
        }

        private static BiomeType getHighBiomes(float m, float h)
        {
            if (m < .45f && h > .7f)
                return BiomeType.ScorchLand;
            else if (m > .65f && h > .5f)
                return BiomeType.Swamp;
            else if (m > .5f && h < .35f)
                return BiomeType.Snow;
            return BiomeType.HighLand;
        }

        private static BiomeType getMountianBiomes(float m, float h)
        {
            if (h < .2f && m > .2f)
                return BiomeType.Snow;
            return BiomeType.Mountian;
        }

        public static BiomeType GetBiome(float height, float moisture, float heat) //height, moisture, heat
        {
            if (height > .005f)
            {
                if (height < .008)
                    return BiomeType.Beach;
                else if (height < .13f)
                    return getLowBiomes(moisture, heat);
                else if (height < .35f)
                    return getMidBiomes(moisture, heat);
                else if (height < .57f)
                    return getHighBiomes(moisture, heat);
                else if (height <= 1f)
                    return getMountianBiomes(moisture, heat);
            }
            return BiomeType.None;
        }

        public static ushort GetBiomeObjectType(BiomeType t)
        {
            switch (t)
            {
                case BiomeType.Beach:
                    return 0xbd;
                case BiomeType.Plains:
                    return 0x46;
                case BiomeType.Desert:
                    return 0xf7;
                case BiomeType.WetLand:
                    return 0x57;
                case BiomeType.Forest:
                    return 0x48;
                case BiomeType.HighLand:
                    return 0x47;
                case BiomeType.ScorchLand:
                    return 0xf6;
                case BiomeType.Swamp:
                    return 0x56;
                case BiomeType.Snow:
                    return 0xb9;
                case BiomeType.Mountian:
                    return 0x60;
                case BiomeType.None:
                    return 0xbc;
            }
            return 0xbc;
        }

        public static Color GetBiomeColor(BiomeType t)
        {
            switch(t)
            {
                case BiomeType.Beach:
                    return Color.LightYellow;
                case BiomeType.Plains:
                    return Color.ForestGreen;
                case BiomeType.Desert:
                    return Color.Yellow;
                case BiomeType.WetLand:
                    return Color.SlateBlue;
                case BiomeType.Forest:
                    return Color.Green;
                case BiomeType.HighLand:
                    return Color.DarkOliveGreen;
                case BiomeType.ScorchLand:
                    return Color.DarkOrange;
                case BiomeType.Swamp:
                    return Color.DarkSlateBlue;
                case BiomeType.Snow:
                    return Color.White;
                case BiomeType.Mountian:
                    return Color.Gray;
                case BiomeType.None:
                    return Color.Black;
            }
            return Color.Black;
        }
    }
}
