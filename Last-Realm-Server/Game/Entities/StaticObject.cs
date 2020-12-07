using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Entities
{
    public class StaticObject : Entity
    {
        public StaticObject(ushort type) : base(type)
        {

        }

        public override bool HitByProjectile(Projectile projectile)
        {
#if DEBUG
            if (projectile.Owner == null || !(projectile.Owner is Player))
                throw new Exception("Projectile owner is undefined");
#endif

            if (Desc.Enemy)
            {
                int damageWithDefense = this.GetDefenseDamage(projectile.Damage, Desc.PhysicalDefense, projectile.Desc.ArmorPiercing);
                HP -= damageWithDefense;

                Player owner = projectile.Owner as Player; 
                
                byte[] packet = GameServer.Damage(Id, new ConditionEffectIndex[0], damageWithDefense);
                foreach (Entity en in Parent.PlayerChunks.HitTest(Position, Player.SightRadius))
                    if (en is Player player && player.Client.Account.AllyDamage && !player.Equals(owner))
                        player.Client.Send(packet);

                if (HP <= 0)
                {
                    Dead = true;
                    Parent.RemoveStatic((int)Position.X, (int)Position.Y);
                    return true;
                }
            }
            return false;
        }
    }
}
