using Last_Realm_Server.Common;
using Last_Realm_Server.Utils;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActDecoy : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.Decoy;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            owner.Parent.AddEntity(new Decoy(owner, owner.Position.Angle(target), eff.DurationMS), owner.Position);
        }
    }
}
