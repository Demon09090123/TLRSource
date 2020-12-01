using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActLightning : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.Lightning;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            float angle = owner.Position.Angle(target);
            float cone = MathF.PI / 4;
            Entity start = owner.GetNearestEnemy(Player.MaxAbilityDist, angle, cone, target);

            if (start == null)
            {
                float[] angles = new float[3] { angle, angle - cone, angle + cone };
                byte[][] lines = new byte[3][];
                for (int i = 0; i < 3; i++)
                {
                    float x = (int)(Player.MaxAbilityDist * MathF.Cos(angles[i])) + owner.Position.X;
                    float y = (int)(Player.MaxAbilityDist * MathF.Sin(angles[i])) + owner.Position.Y;
                    lines[i] = GameServer.ShowEffect(ShowEffectIndex.Line, owner.Id, 0xffff0088, new Position(x, y), new Position(350, 0));
                }

                foreach (Entity j in owner.Parent.PlayerChunks.HitTest(owner.Position, Player.SightRadius))
                {
                    if (j is Player k && k.Client.Account.Effects)
                    {
                        k.Client.Send(lines[0]);
                        k.Client.Send(lines[1]);
                        k.Client.Send(lines[2]);
                    }
                }
            }
            else
            {
                Entity prev = owner;
                Entity current = start;
                var targets = new HashSet<Entity>();
                var pkts = new List<byte[]>();
                targets.Add(current);
                (current as Enemy).Damage(owner, eff.TotalDamage, eff.Effects, false, true);
                for (int i = 1; i < eff.MaxTargets + 1; i++)
                {
                    pkts.Add(GameServer.ShowEffect(ShowEffectIndex.Lightning, prev.Id, 0xffff0088,
                        new Position(current.Position.X, current.Position.Y),
                        new Position(350, 0)));

                    if (i == eff.MaxTargets)
                        break;

                    Entity next = current.GetNearestEnemy(10, targets);
                    if (next == null)
                        break;

                    targets.Add(next);
                    (next as Enemy).Damage(owner, eff.TotalDamage, eff.Effects, false, true);
                    prev = current;
                    current = next;
                }

                foreach (Entity j in owner.Parent.PlayerChunks.HitTest(owner.Position, Player.SightRadius))
                    if (j is Player k && k.Client.Account.Effects)
                        foreach (byte[] p in pkts)
                            k.Client.Send(p);

            }
        }
    }
}
