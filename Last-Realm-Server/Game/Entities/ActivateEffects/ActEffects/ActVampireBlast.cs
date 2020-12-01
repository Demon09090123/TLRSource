using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActVampireBlast : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.VampireBlast;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            var parent = owner.Parent;
            var pos = owner.Position;
            var id = owner.Id;

            if (pos.Distance(target) <= Player.MaxAbilityDist && parent.GetTileF(target.X, target.Y) != null)
            {
                byte[] line = GameServer.ShowEffect(ShowEffectIndex.Line, id, 0xFFFF0000, target);
                byte[] burst = GameServer.ShowEffect(ShowEffectIndex.Burst, id, 0xFFFF0000, target, new Position(target.X + eff.Radius, target.Y));
                var lifeSucked = 0;

                List<Entity> enemies = new List<Entity>();
                List<Entity> players = new List<Entity>();
                List<byte[]> flows = new List<byte[]>();

                foreach(var e in parent.EntityChunks.HitTest(target, eff.Radius))
                {
                    if (e is Enemy en && !en.HasConditionEffect(ConditionEffectIndex.Invincible)
                        && !en.HasConditionEffect(ConditionEffectIndex.Stasis))
                    {
                        en.Damage(owner, eff.TotalDamage, eff.Effects, true, true);
                        lifeSucked += eff.TotalDamage;
                        enemies.Add(en);
                    }
                }

                foreach (var e in parent.PlayerChunks.HitTest(pos, eff.Radius))
                {
                    if (e is Player p)
                    {
                        players.Add(p);
                        p.Heal(lifeSucked, false);
                    }
                }

                if (enemies.Count > 0)
                {
                    for (var i = 0; i < 5; i++)
                    {
                        var a = enemies[MathUtils.Next(enemies.Count)];
                        var b = players[MathUtils.Next(players.Count)];

                        flows.Add(GameServer.ShowEffect(ShowEffectIndex.Flow, b.Id, 0xfffffff, a.Position));
                    }
                }

                foreach(var e in parent.PlayerChunks.HitTest(pos, Player.SightRadius))
                {
                    if (e is Player p)
                    {
                        if (p.Client.Account.Effects)
                        {
                            p.Client.Send(line);
                            foreach (byte[] b in flows)
                                p.Client.Send(b);
                        }

                        if (p.Client.Account.Effects && p.Equals(owner))
                            p.Client.Send(burst);
                    }
                }
            }
        }
    }
}
