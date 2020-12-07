using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Entities
{
    public partial class Player
    {
        public const int MaxLevel = 20;
        public const int EXPPerFame = 2000;

        public static int GetNextLevelEXP(int level)
        {
            return 50 + (level - 1) * 100;
        }

        public static int GetLevelEXP(int level)
        {
            if (level == 1) return 0;
            return 50 * (level - 1) + (level - 2) * (level - 1) * 50;
        }

        public static int GetNextClassQuestFame(int fame)
        {
            for (int i = 0; i < Stars.Length; i++)
            {
                if (fame >= Stars[i] && i == Stars.Length - 1)
                    return 0;
                if (fame < Stars[i])
                    return Stars[i];
            }
            return -1;
        }

        public Entity Quest;

        public void InitLevel(CharacterModel character)
        {
            if (character.Experience != 0) EXP = character.Experience;
            NextLevelEXP = GetNextLevelEXP(Level);
            GainEXP(0);
        }

        public bool GainEXP(int exp)
        {
            EXP += exp;

            bool levelledUp = false;
            if (EXP - GetLevelEXP(Level) >= NextLevelEXP && Level < MaxLevel)
            {
                levelledUp = true;
                Level++;
                NextLevelEXP = GetNextLevelEXP(Level);
                StatDesc[] stats = Resources.Type2Player[Type].Stats;
                for (int i = 0; i < stats.Length; i++)
                {
                    int min = stats[i].MinIncrease;
                    int max = stats[i].MaxIncrease;
                    Stats[i] += MathUtils.NextInt(min, max);
                }

                HP = Stats[0];
                MP = Stats[1];

                if (Level == 20)
                {
                    byte[] text = GameServer.Text("", 0, -1, 0, "", $"{Name} achieved level 20");
                    foreach (Player player in Parent.Players.Values)
                        player.Client.Send(text);
                }

                RecalculateEquipBonuses();
            }

            TrySetSV(StatType.EXP, EXP - GetLevelEXP(Level));
            return levelledUp;
        }
    }
}
