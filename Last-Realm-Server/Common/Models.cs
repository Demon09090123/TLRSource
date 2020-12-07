using Last_Realm_Server.Utils;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Last_Realm_Server.Common
{
    public interface IDatabaseInfo
    {
        XElement Export(bool appExport = true);
    }

    public abstract class DatabaseModel : IDatabaseInfo
    {
        public XElement Data;
        public readonly string Path;
        public DatabaseModel(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                Path = Database.CombineKeyPath(key);
                Reload();
            }
        }

        public void Reload()
        {
            if (File.Exists(Path))
                Data = XElement.Parse(File.ReadAllText(Path));
        }

        public void Save()
        {
            Data = Export(false);
            File.WriteAllText(Path, Data.ToString());
        }

        public bool IsNull => Data == null;

        public abstract void Load();
        public abstract XElement Export(bool appExport = true);
    }

    public class CharacterModel : DatabaseModel
    {
        public int Experience;
        public int Level;
        public int ClassType;
        public int HP;
        public int MP;
        public int[] Stats;
        public int[] Inventory;
        public int[] ItemDatas;
        public int Tex1;
        public int Tex2;
        public int SkinType;
        public int HealthPotions;
        public int MagicPotions;
        public bool HasBackpack;

        public CharacterModel(int accountId) : base($"char.{accountId}") 
        {
        }

        public override void Load()
        {
            Level = Data.ParseInt("Level");
            Experience = Data.ParseInt("Experience");
            ClassType = Data.ParseInt("ClassType");
            HP = Data.ParseInt("HP");
            MP = Data.ParseInt("MP");
            Stats = Data.ParseIntArray("Stats", ",");
            Inventory = Data.ParseIntArray("Inventory", ",");
            ItemDatas = Data.ParseIntArray("ItemDatas", ",");
            Tex1 = Data.ParseInt("Tex1");
            Tex2 = Data.ParseInt("Tex2");
            SkinType = Data.ParseInt("SkinType");
            HealthPotions = Data.ParseInt("HealthPotions");
            MagicPotions = Data.ParseInt("MagicPotions");
            HasBackpack = Data.ParseBool("HasBackpack");
        }

        public XElement ExportFame()
        {
            XElement data = new XElement("Char");
            data.Add(new XElement("ObjectType", ClassType));
            data.Add(new XElement("Level", Level));
            data.Add(new XElement("Exp", Experience));
            data.Add(new XElement("Equipment", string.Join(",", Inventory)));
            data.Add(new XElement("ItemDatas", string.Join(",", ItemDatas)));
            data.Add(new XElement("MaxHitPoints", Stats[0]));
            data.Add(new XElement("HitPoints", HP));
            data.Add(new XElement("MaxMagicPoints", Stats[1]));
            data.Add(new XElement("MagicPoints", MP));
            data.Add(new XElement("Attack", Stats[2]));
            data.Add(new XElement("MagicPower", Stats[3]));
            data.Add(new XElement("PhysicalDefense", Stats[4]));
            data.Add(new XElement("MagicDefense", Stats[5]));
            data.Add(new XElement("Speed", Stats[6]));
            data.Add(new XElement("Dexterity", Stats[7]));
            data.Add(new XElement("HpRegen", Stats[8]));
            data.Add(new XElement("MpRegen", Stats[9]));
            data.Add(new XElement("Tex1", Tex1));
            data.Add(new XElement("Tex2", Tex2));
            data.Add(new XElement("Texture", SkinType));
            return data;
        }

        public override XElement Export(bool appExport = true)
        {
            XElement data = new XElement("Char");
            if (appExport) //char/list export
            {
                data.Add(new XElement("ObjectType", ClassType));
                data.Add(new XElement("Level", Level));
                data.Add(new XElement("Exp", Experience));
                data.Add(new XElement("Equipment", string.Join(",", Inventory)));
                data.Add(new XElement("ItemDatas", string.Join(",", ItemDatas)));
                

                var statElement = new XElement("Stats");
                statElement.Add(new XElement("MaxHitPoints", Stats[0]));
                statElement.Add(new XElement("MaxMagicPoints", Stats[1]));
                statElement.Add(new XElement("Attack", Stats[2]));
                statElement.Add(new XElement("MagicPower", Stats[3]));
                statElement.Add(new XElement("PhysicalDefense", Stats[4]));
                statElement.Add(new XElement("MagicDefense", Stats[5]));
                statElement.Add(new XElement("Speed", Stats[6]));
                statElement.Add(new XElement("Dexterity", Stats[7]));
                statElement.Add(new XElement("HpRegen", Stats[8]));
                statElement.Add(new XElement("MpRegen", Stats[9]));
                data.Add(statElement);

                data.Add(new XElement("Tex1", Tex1));
                data.Add(new XElement("Tex2", Tex2));
                data.Add(new XElement("Texture", SkinType));
            }
            else //database export
            {
                data.Add(new XElement("Level", Level));
                data.Add(new XElement("Experience", Experience));
                data.Add(new XElement("ClassType", ClassType));
                data.Add(new XElement("HP", HP));
                data.Add(new XElement("MP", MP));
                data.Add(new XElement("Stats", string.Join(",", Stats)));
                data.Add(new XElement("Inventory", string.Join(",", Inventory)));
                data.Add(new XElement("ItemDatas", string.Join(",", ItemDatas)));
                data.Add(new XElement("Tex1", Tex1));
                data.Add(new XElement("Tex2", Tex2));
                data.Add(new XElement("SkinType", SkinType));
                data.Add(new XElement("HealthPotions", HealthPotions));
                data.Add(new XElement("MagicPotions", MagicPotions));
                data.Add(new XElement("HasBackpack", HasBackpack));
            }
            return data;
        }
    }

    public class AccountModel : DatabaseModel
    {
        public const int MaxDeadCharsStored = 20;

        public readonly int Id; //Taken from database.
        public readonly string Name; //Taken from database.

        public List<int> OwnedSkins;
        public bool Ranked;
        public bool Muted;
        public bool Banned;
        public string GuildName;
        public int GuildRank;
        public StatsInfo Stats;
        public bool Connected;
        public int RegisterTime;
        public List<int> LockedIds;
        public List<int> IgnoredIds;
        public bool AllyShots;
        public bool AllyDamage;
        public bool Effects;
        public bool Sounds;
        public bool Notifications;

        public AccountModel() : base(null) { }
        public AccountModel(int key) : base($"account.{key}")
        {
            Id = key;

            if (Data != null)
                Name = Database.UsernameFromId(key);
        }

        public override void Load()
        {
            OwnedSkins = Data.ParseIntList("OwnedSkins", ",", new List<int>());
            Ranked = Data.ParseBool("Ranked");
            Muted = Data.ParseBool("Muted");
            Banned = Data.ParseBool("Banned");
            GuildName = Data.ParseString("GuildName");
            GuildRank = Data.ParseInt("GuildRank");
            Connected = Data.ParseBool("Connected");
            RegisterTime = Data.ParseInt("RegisterTime");
            LockedIds = Data.ParseIntList("LockedIds", ",", new List<int>());
            IgnoredIds = Data.ParseIntList("IgnoredIds", ",", new List<int>());
            AllyShots = Data.ParseBool("AllyShots", true);
            AllyDamage = Data.ParseBool("AllyDamage", true);
            Effects = Data.ParseBool("Effects", true);
            Sounds = Data.ParseBool("Sounds", true);
            Notifications = Data.ParseBool("Notifications", true);


            Stats = new StatsInfo
            {
                TotalCredits = Data.Element("Stats").ParseInt("TotalCredits"),
                Credits = Data.Element("Stats").ParseInt("Credits")
            };
        }

        public override XElement Export(bool appExport = true)
        {
            XElement data = new XElement("Account");
            data.Add(new XElement("AccountId", Id));

            if (appExport)
            {
                data.Add(new XElement("Name", Name));
                data.Add(new XElement("Guild", new XElement("Name", GuildName), new XElement("Rank", GuildRank)));
            }
            else
            {
                data.Add(new XElement("OwnedSkins", string.Join(",", OwnedSkins)));
                data.Add(new XElement("Ranked", Ranked));
                data.Add(new XElement("Muted", Muted));
                data.Add(new XElement("Banned", Banned));
                data.Add(new XElement("GuildName", GuildName));
                data.Add(new XElement("GuildRank", GuildRank));
                data.Add(new XElement("RegisterTime", RegisterTime));
                data.Add(new XElement("LockedIds", string.Join(",", LockedIds)));
                data.Add(new XElement("IgnoredIds", string.Join(",", IgnoredIds)));
                data.Add(new XElement("AllyShots", AllyShots));
                data.Add(new XElement("AllyDamage", AllyDamage));
                data.Add(new XElement("Effects", Effects));
                data.Add(new XElement("Sounds", Sounds));
                data.Add(new XElement("Notifications", Notifications));
            }

            data.Add(Stats.Export(appExport));

            return data;
        }
    }

    public class StatsInfo : IDatabaseInfo
    {
        public int Credits;
        public int TotalCredits;

        public XElement Export(bool appExport = true)
        {
            XElement data = new XElement("Stats");
            data.Add(new XElement("TotalCredits", TotalCredits));
            data.Add(new XElement("Credits", Credits));
            return data;
        }
    }
}
