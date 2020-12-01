using Last_Realm_Server.Game;
using Last_Realm_Server.Game.Entities;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Last_Realm_Server.Common
{
    //XML/Text files combined storage system
    public static class Database
    {
        private const int MaxLegends = 20;

        private const int MaxInvalidLoginAttempts = 5;
        private static Dictionary<string, byte> InvalidLoginAttempts;

        private const int MaxRegisteredAccounts = 1;
        private static Dictionary<string, byte> RegisteredAccounts;

        private const int ResetCooldown = 60000 * 5; //5 minutes
        private static int ResetTime;

        private const int CharSlotPrice = 2000; //Fame
        private const int SkinPrice = 1000; //Credits

        public static void Init()
        {
            InvalidLoginAttempts = new Dictionary<string, byte>();
            RegisteredAccounts = new Dictionary<string, byte>();
            if (!Directory.Exists(Settings.DatabaseDirectory))
                Directory.CreateDirectory(Settings.DatabaseDirectory);

            CreateKey("nextAccId", "0", true);
        }

        public static void Tick()
        {
            if (Environment.TickCount - ResetTime >= ResetCooldown)
            {
#if DEBUG
                Program.Print(PrintType.Debug, "Database reset");
#endif
                RegisteredAccounts.Clear();
                InvalidLoginAttempts.Clear();
                ResetTime = Environment.TickCount;
            }
        }

        private static void AddRegisteredAccount(string ip)
        {
            if (RegisteredAccounts.ContainsKey(ip))
                RegisteredAccounts[ip]++;
            else RegisteredAccounts[ip] = 1;
        }

        private static void AddInvalidLoginAttempt(string ip)
        {
            if (InvalidLoginAttempts.ContainsKey(ip))
                InvalidLoginAttempts[ip]++;
            else InvalidLoginAttempts[ip] = 1;
        }

        private static void CreateKey(string path, string contents, bool global = false)
        {
            string combined = CombineKeyPath(path, global);
            if (!File.Exists(combined))
                File.WriteAllText(combined, contents);
        }

        public static void DeleteKey(string path, bool global = false)
        {
            File.Delete(CombineKeyPath(path, global));
        }

        private static void SetKey(string path, string contents, bool global = false)
        {
            File.WriteAllText(CombineKeyPath(path, global), contents);
        }

        private static void SetKeyLines(string path, string[] contents, bool global = false)
        {
            File.WriteAllLines(CombineKeyPath(path, global), contents);
        }

        private static string GetKey(string path, bool global = false)
        {
            string combined = CombineKeyPath(path, global);
            if (!File.Exists(combined))
                return null;
            return File.ReadAllText(combined);
        }

        private static string[] GetKeyLines(string path, bool global = false)
        {
            string combined = CombineKeyPath(path, global);
            if (!File.Exists(combined))
                return null;
            return File.ReadAllLines(combined);
        }

        public static string CombineKeyPath(string path, bool global = false)
        {
            return $"{Settings.DatabaseDirectory}/{(global ? "@" : "")}{path}.file";
        }

        public static bool CanRegisterAccount(string ip)
        {
            if (RegisteredAccounts.TryGetValue(ip, out byte attempts) && attempts >= MaxRegisteredAccounts)
                return false;
            return true;
        }

        public static bool CanAttemptLogin(string ip)
        {
            if (InvalidLoginAttempts.TryGetValue(ip, out byte attempts) && attempts >= MaxInvalidLoginAttempts)
                return false;
            return true;
        }

        public static AccountModel GuestAccount()
        {
            return new AccountModel() 
            {
                Stats = new StatsInfo() {  },
                OwnedSkins = new List<int>(),
                LockedIds = new List<int>(),
                IgnoredIds = new List<int>(), 
                HasCharacter = false
            };
        }

        public static int GetStars(AccountModel acc)
        {
            return 5;
        }

        public static bool IsValidPassword(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            if (input.Length < 9) return false;
            return true;
        }

        public static bool IsValidUsername(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            if (input.Length < 1 || input.Length > 12) return false;
            return Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");
        }

        public static int IdFromUsername(string username)
        {
            string value = GetKey($"login.username.{username}");
            return string.IsNullOrWhiteSpace(value) ? -1 : int.Parse(value);
        }

        public static string UsernameFromId(int id)
        {
            string value = GetKey($"login.id.{id}");
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        public static RegisterStatus RegisterAccount(string username, string password, string ip)
        {
            if (!CanRegisterAccount(ip))
                return RegisterStatus.TooManyRegisters;

            if (!IsValidUsername(username))
                return RegisterStatus.InvalidUsername;

            if (!IsValidPassword(password))
                return RegisterStatus.InvalidPassword;

            if (IdFromUsername(username) != -1)
                return RegisterStatus.UsernameTaken;

            int id = int.Parse(GetKey("nextAccId", true));
            string salt = MathUtils.GenerateSalt();
            SetKey("nextAccId", (id + 1).ToString(), true);

            SetKey($"login.username.{username}", id.ToString());
            SetKey($"login.id.{id}", username);
            SetKey($"login.hash.{id}", (password + salt).ToSHA1());
            SetKey($"login.salt.{id}", salt);

            AccountModel acc = new AccountModel(id)
            {
                Stats = new StatsInfo
                {
                    TotalCredits = 0,
                    Credits = 0
                },

                OwnedSkins = new List<int>(),
                Ranked = false,
                Muted = false,
                Banned = false,
                GuildName = null,
                GuildRank = 0,
                Connected = false,
                LockedIds = new List<int>(),
                IgnoredIds = new List<int>(),
                AllyDamage = true,
                AllyShots = true,
                Effects = true,
                Sounds = true,
                Notifications = true,
                HasCharacter = false,
                RegisterTime = UnixTime()
            };

            acc.Save();
            AddRegisteredAccount(ip);
            return RegisterStatus.Success;
        }

        public static int UnixTime()
        {
            return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static bool IsAccountInUse(AccountModel acc)
        {
            bool accountInUse = acc.Connected && Manager.GetClient(acc.Id) != null;
            if (!accountInUse && acc.Connected)
            {
                acc.Connected = false;
                acc.Save();
            }
            return accountInUse;
        }

        public static AccountModel Verify(string username, string password, string ip)
        {
            if (!CanAttemptLogin(ip))
                return null;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            int id = IdFromUsername(username);
            if (id == -1) return null;

            string hash = GetKey($"login.hash.{id}");
            string match = (password + GetKey($"login.salt.{id}")).ToSHA1();

            AccountModel acc = hash.Equals(match) ? new AccountModel(id) : null;
            if (acc == null) AddInvalidLoginAttempt(ip);
            else acc.Load();
            return acc;
        }

        public static bool ChangePassword(AccountModel acc, string newPassword) 
        {
#if DEBUG
            if (acc == null)
                throw new Exception("Undefined account");
#endif
            if (!IsValidPassword(newPassword))
                return false;

            string salt = MathUtils.GenerateSalt();
            SetKey($"login.hash.{acc.Id}", (newPassword + salt).ToSHA1());
            SetKey($"login.salt.{acc.Id}", salt);
            return true;
        }

        public static bool BuySkin(AccountModel acc, int skinType)
        {
#if DEBUG
            if (acc == null)
                throw new Exception("Undefined account");
#endif
            if (!Resources.Type2Skin.ContainsKey((ushort)skinType))
                return false;
            if (acc.OwnedSkins.Contains(skinType))
                return false;
            if (acc.Stats.Credits < SkinPrice)
                return false;
            acc.Stats.Credits -= SkinPrice;
            acc.OwnedSkins.Add(skinType);
            acc.Save();
            return true;
        }

        public static CharacterModel LoadCharacter(AccountModel acc)
        {
            CharacterModel character = new CharacterModel(acc.Id);
            if (!character.IsNull)
                character.Load();
            return character;
        }

        public static void SaveCharacter(CharacterModel character)
        {
            character.Save();
        }
        
        public static CharacterModel CreateCharacter(AccountModel acc, int classType, int skinType)
        {
#if DEBUG
            if (acc == null)
                throw new Exception("Account is null.");
#endif
            if (!Resources.Type2Player.TryGetValue((ushort)classType, out PlayerDesc player))
                return null;

            CharacterModel character = new CharacterModel(acc.Id)
            {
                ClassType = classType,
                Level = 1,
                Experience = 0,
                Inventory = player.Equipment.ToArray(),
                ItemDatas = player.ItemDatas.ToArray(),
                Stats = player.StartingValues.ToArray(),
                HP = player.StartingValues[0],
                MP = player.StartingValues[1],
                Tex1 = 0,
                Tex2 = 0,
                SkinType = skinType,
                HasBackpack = false,
                HealthPotions = Player.MaxPotions,
                MagicPotions = Player.MaxPotions,
            };

            acc.HasCharacter = true;
            character.Save();
            acc.Save();
            return character;
        }

    }
}
