using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActHealNova : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.HealNova;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            byte[] nova = GameServer.ShowEffect(ShowEffectIndex.Nova, owner.Id, 0xffffffff, new Position(eff.Range, 0));
            foreach (Entity j in owner.Parent.PlayerChunks.HitTest(owner.Position, Math.Max(eff.Range, Player.SightRadius)))
            {
                if (j is Player k)
                {
                    if (owner.Position.Distance(j) <= eff.Range)
                        k.Heal(eff.Amount, false);
                    if (k.Client.Account.Effects || k.Equals(this))
                        k.Client.Send(nova);
                }
            }


        }
    }
}
