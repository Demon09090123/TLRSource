using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActBulletNova : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.BulletNova;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            var parent = owner.Parent;
            var pos = owner.Position;
            var id = owner.Id;

            if (pos.Distance(target) <= Player.MaxAbilityDist && parent.GetTileF(target.X, target.Y) != null)
            {
                var projs = new List<Projectile>(20);
                var novaCount = 20;
                var startId = owner.NextAEProjectileId;
                float angleInc = (MathF.PI * 2) / novaCount;
                owner.NextAEProjectileId += novaCount;
                for (int i = 0; i < novaCount; i++)
                {
                    int d = owner.GetNextDamage(desc.Projectile.MinDamage, desc.Projectile.MaxDamage, owner.ItemDatas[slot.SlotId]);
                    Projectile p = new Projectile(owner, desc.Projectile, startId + i, time, angleInc * i, target, d);
                    projs.Add(p);
                }

                owner.AwaitProjectiles(projs);

                byte[] line = GameServer.ShowEffect(ShowEffectIndex.Line, id, 0xFFFF00AA, target);
                byte[] nova = GameServer.ServerPlayerShoot(startId, id, desc.Type, target, 0, angleInc, projs);

                foreach (Entity j in owner.Parent.PlayerChunks.HitTest(owner.Position, Player.SightRadius))
                {
                    if (j is Player k)
                    {
                        if (k.Client.Account.Effects)
                            k.Client.Send(line);
                        if (k.Client.Account.AllyShots || k.Equals(this))
                            k.Client.Send(nova);
                    }
                }
            }
        }
    }
}
