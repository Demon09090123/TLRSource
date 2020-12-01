using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActTrap : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.Trap;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            var parent = owner.Parent;
            var pos = owner.Position;
            var id = owner.Id;

            if (pos.Distance(target) <= Player.MaxAbilityDist && parent.GetTileF(target.X, target.Y) != null)
            {
                byte[] throwEff = GameServer.ShowEffect(ShowEffectIndex.Throw, id, 0xff9000ff, target);

                foreach (var e in parent.PlayerChunks.HitTest(pos, Player.SightRadius))
                    if (e is Player p && (p.Client.Account.Effects || p.Equals(owner)))
                        p.Client.Send(throwEff);

                Manager.AddTimedAction(1500, () =>
                {
                    if (parent != null)
                        parent.AddEntity(new Trap(owner, eff.Radius, eff.TotalDamage, eff.Effects), target);
                });
            }
        }
    }
}
