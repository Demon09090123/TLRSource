using Last_Realm_Server.Common;
using Last_Realm_Server.Utils;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActTeleport : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.Teleport;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            var parent = owner.Parent;
            var pos = owner.Position;

            if (pos.Distance(target) <= Player.MaxAbilityDist && parent.GetTileF(target.X, target.Y) != null)
                owner.Teleport(time, target);
        }
    }
}
