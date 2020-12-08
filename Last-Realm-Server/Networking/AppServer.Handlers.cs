using Last_Realm_Server.Common;
using Last_Realm_Server.Game.Entities;
using System.Collections.Specialized;
using System.Net;
using System.Xml.Linq;

namespace Last_Realm_Server.Networking
{
    public static partial class AppServer
    {
        private static byte[] CharList(HttpListenerContext context, NameValueCollection query)
        {
            XElement data = new XElement("Chars");

            bool accountInUse = false;
            string username = query["username"];
            string password = query["password"];

            _listenEvent.Reset();
            Program.PushWork(() =>
            {
                AccountModel acc = Database.Verify(username, password, GetIPFromContext(context));
                if (!(accountInUse = Database.IsAccountInUse(acc)))
                    if (acc != null)
                    {
                        data.Add(acc.Export());
                        data.Add(new XElement("OwnedSkins", string.Join(",", acc.OwnedSkins)));

                        var character = Database.LoadCharacter(acc);

                        if (!character.IsNull)
                        {
                            var exp = character.Export();
                            exp.Add("NextLevelEXP", Player.GetNextLevelEXP(character.Level));
                            data.Add(exp);
                        }
                    }
            }, () => _listenEvent.Set());
            _listenEvent.WaitOne();

            return accountInUse ? WriteError("Account in use!") : Write(data.ToString());
        }

        private static byte[] Verify(HttpListenerContext context, NameValueCollection query)
        {
            byte[] data = null;

            string username = query["username"];
            string password = query["password"];

            _listenEvent.Reset();
            Program.PushWork(() =>
            {
                AccountModel acc = Database.Verify(username, password, GetIPFromContext(context));
                if (acc == null)
                    data = WriteError("Invalid account.");
                else if (Database.IsAccountInUse(acc))
                    data = WriteError("Account in use!");
                else
                    data = WriteSuccess();
            }, () => _listenEvent.Set());
            _listenEvent.WaitOne();

            return data;
        }

        private static byte[] Register(HttpListenerContext context, NameValueCollection query)
        {
            byte[] data = null;
            string newUsername = query["newUsername"];
            string newPassword = query["newPassword"];

            if (!Database.IsValidUsername(newUsername))
                return WriteError("Invalid username.");

            if (!Database.IsValidPassword(newPassword))
                return WriteError("Invalid password.");

            _listenEvent.Reset();
            Program.PushWork(() =>
            {
                RegisterStatus status = Database.RegisterAccount(newUsername, newPassword, GetIPFromContext(context));
                if (status == RegisterStatus.Success)
                    data = WriteSuccess();
                else data = WriteError(status.ToString());
            }, () => _listenEvent.Set());
            _listenEvent.WaitOne();

            return data;
        }

        private static byte[] AccountPurchaseSkin(HttpListenerContext context, NameValueCollection query)
        {

            byte[] data = null;

            string username = query["username"];
            string password = query["password"];
            int skinType = int.Parse(query["skinType"]);

            _listenEvent.Reset();
            Program.PushWork(() =>
            {
                AccountModel acc = Database.Verify(username, password, GetIPFromContext(context));
                if (acc == null)
                    data = WriteError("Invalid account.");
                else if (Database.IsAccountInUse(acc))
                    data = WriteError("Account in use!");
                else
                    data = Database.BuySkin(acc, skinType) ? WriteSuccess() : WriteError("Could not buy skin");
            }, () => _listenEvent.Set());
            _listenEvent.WaitOne();

            return data;
        }

        private static byte[] AccountChangePassword(HttpListenerContext context, NameValueCollection query)
        {

            byte[] data = null;

            string username = query["username"];
            string password = query["password"];
            string newPassword = query["newPassword"];

            _listenEvent.Reset();
            Program.PushWork(() =>
            {
                AccountModel acc = Database.Verify(username, password, GetIPFromContext(context));
                if (acc == null)
                    data = WriteError("Invalid account.");
                else if (Database.IsAccountInUse(acc))
                    data = WriteError("Account in use!");
                else
                    data = Database.ChangePassword(acc, newPassword) ? WriteSuccess() : WriteError("Could not change password");
            }, () => _listenEvent.Set());
            _listenEvent.WaitOne();

            return data;
        }
    }
}
