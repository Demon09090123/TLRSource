using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActShuriken : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.Shuriken;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            byte[] nova = GameServer.ShowEffect(ShowEffectIndex.Nova, owner.Id, 0xffeba134, new Position(2.5f, 0));

            foreach (Entity j in owner.Parent.EntityChunks.HitTest(owner.Position, 2.5f))
            {
                if (j is Enemy k &&
                    !k.HasConditionEffect(ConditionEffectIndex.Invincible) &&
                    !k.HasConditionEffect(ConditionEffectIndex.Stasis))
                {
                    k.ApplyConditionEffect(ConditionEffectIndex.Dazed, 1000);
                }
            }

            List<byte[]> stars = new List<byte[]>();
            HashSet<Entity> seeked = new HashSet<Entity>();
            int startId = owner.NextAEProjectileId;
            owner.NextAEProjectileId += eff.Amount;

            float angle = owner.Position.Angle(target);
            float cone = MathF.PI / 8;
            for (int i = 0; i < eff.Amount; i++)
            {
                Entity t = owner.GetNearestEnemy(8, angle, cone, target, seeked) ?? owner.GetNearestEnemy(6, seeked);
                if (t != null) seeked.Add(t);
                int d = owner.GetNextDamage(desc.Projectile.MinDamage, desc.Projectile.MaxDamage, owner.ItemDatas[slot.SlotId]);
                float a = t == null ? MathUtils.NextAngle() : owner.Position.Angle(t.Position);
                List<Projectile> p = new List<Projectile>()
                                            {
                                                 new Projectile(owner, desc.Projectile, startId + i, time, a, owner.Position, d)
                                            };

                stars.Add(GameServer.ServerPlayerShoot(startId + i, owner.Id, desc.Type, owner.Position, a, 0, p));
                owner.AwaitProjectiles(p);
            }

            foreach (Entity j in owner.Parent.PlayerChunks.HitTest(owner.Position, Player.SightRadius))
            {
                if (j is Player k)
                {
                    if (k.Client.Account.Effects || k.Equals(this))
                        k.Client.Send(nova);
                    if (k.Client.Account.AllyShots || k.Equals(this))
                        foreach (byte[] s in stars)
                            k.Client.Send(s);
                }
            }
        }
    }
}
