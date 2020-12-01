using Last_Realm_Server.Common;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActShoot : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.Shoot;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            if (!owner.HasConditionEffect(ConditionEffectIndex.Stunned))
                owner.ShootAEs.Enqueue(desc.Type);
        }
    }
}
