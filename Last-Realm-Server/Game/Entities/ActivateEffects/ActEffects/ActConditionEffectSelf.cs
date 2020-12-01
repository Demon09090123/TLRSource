using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActConditionEffectSelf : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.ConditionEffectSelf;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            owner.ApplyConditionEffect(eff.Effect, eff.DurationMS);

            byte[] nova = GameServer.ShowEffect(ShowEffectIndex.Nova, owner.Id, 0xffffffff, new Position(1, 0));
            foreach (Entity j in owner.Parent.PlayerChunks.HitTest(owner.Position, Player.SightRadius))
                if (j is Player k && k.Client.Account.Effects)
                    k.Client.Send(nova);
        }
    }
}
