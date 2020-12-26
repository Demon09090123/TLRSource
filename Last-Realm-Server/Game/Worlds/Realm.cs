using Last_Realm_Server.Common;

namespace Last_Realm_Server.Game.Worlds
{
    public class Realm : World
    {
        public Realm(WorldDesc desc) : base(desc)
        {
            var setPiece = Manager.SetPieces["Nexus"];
            setPiece.ApplySetPiece(200, 200, this);
        }
    }
}
