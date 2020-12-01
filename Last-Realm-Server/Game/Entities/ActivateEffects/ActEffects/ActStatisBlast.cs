using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActStatisBlast : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.StasisBlast;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            var parent = owner.Parent;
            var pos = owner.Position;
            var id = owner.Id;

            if (pos.Distance(target) <= Player.MaxAbilityDist && parent.GetTileF(target.X, target.Y) != null)
            {
                byte[] blast = GameServer.ShowEffect(ShowEffectIndex.Collapse, id, 0xffffffff, target,
                    new Position(target.X + 3, target.Y));

                var notif = new List<byte[]>();

                foreach (var e in parent.EntityChunks.HitTest(target, 3))
                {
                    if (e is Enemy en)
                    {
                        if (en.HasConditionEffect(ConditionEffectIndex.StasisImmune))
                        {
                            notif.Add(GameServer.Notification(en.Id, "Immune", 0xff00ff00));
                            continue;
                        }

                        if (en.HasConditionEffect(ConditionEffectIndex.Stasis))
                            continue;

                        notif.Add(GameServer.Notification(en.Id, "Statis", 0xffff0000));
                        en.ApplyConditionEffect(ConditionEffectIndex.Stasis, eff.DurationMS);
                        en.ApplyConditionEffect(ConditionEffectIndex.StasisImmune, eff.DurationMS + 3000);
                    }
                }

            }
        }
    }
}
