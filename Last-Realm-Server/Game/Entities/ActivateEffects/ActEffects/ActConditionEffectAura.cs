using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActConditionEffectAura : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.ConditionEffectAura;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            uint color = eff.Effect == ConditionEffectIndex.Damaging ? 0xffff0000 : 0xffffffff;
            byte[] nova = GameServer.ShowEffect(ShowEffectIndex.Nova, owner.Id, color, new Position(eff.Range, 0));
            foreach (Entity j in owner.Parent.PlayerChunks.HitTest(owner.Position, Math.Max(eff.Range, Player.SightRadius)))
            {
                if (j is Player k)
                {
                    if (owner.Position.Distance(j) <= eff.Range)
                        k.ApplyConditionEffect(eff.Effect, eff.DurationMS);
                    if (k.Client.Account.Effects || k.Equals(this))
                        k.Client.Send(nova);
                }
            }



        }
    }
}
