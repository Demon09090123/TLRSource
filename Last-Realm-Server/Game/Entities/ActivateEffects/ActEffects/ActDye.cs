using Last_Realm_Server.Common;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActDye : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.Dye;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            if (desc.Tex1 != 0)
                owner.Tex1 = desc.Tex1;
            if (desc.Tex2 != 0)
                owner.Tex2 = desc.Tex2;
        }
    }
}
