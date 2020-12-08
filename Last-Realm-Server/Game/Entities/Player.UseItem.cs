using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml.Schema;

namespace Last_Realm_Server.Game.Entities
{
    public partial class Player
    {
        public const float UseCooldownThreshold = 1.1f;
        public const int MaxAbilityDist = 14;

        public Queue<ushort> ShootAEs;
        public int UseDuration;
        public int UseTime;

        public void TryUseItem(int time, SlotData slot, Position target)
        {
            if (!ValidTime(time))
            {
#if DEBUG
                Program.Print(PrintType.Error, "Invalid time useitem");
#endif
                Client.Disconnect();
                return;
            }

            if (slot.SlotId == HealthPotionSlotId)
            {
                if (HealthPotions > 0 && !HasConditionEffect(ConditionEffectIndex.Sick))
                {
                    Heal(100, false);
                    HealthPotions--;
                }
                return;
            }
            else if (slot.SlotId == MagicPotionSlotId)
            {
                if (MagicPotions > 0 && !HasConditionEffect(ConditionEffectIndex.Quiet))
                {
                    Heal(100, true);
                    MagicPotions--;
                }
                return;
            }

            Entity en = Parent.GetEntity(slot.ObjectId);
            if (slot.SlotId != 1)
                (en as IContainer)?.UpdateInventorySlot(slot.SlotId);
            if (en == null || !(en is IContainer))
            {
#if DEBUG
                Program.Print(PrintType.Error, "Undefined entity");
#endif
                return;
            }

            if (en is Player && !en.Equals(this))
            {
#if DEBUG
                Program.Print(PrintType.Error, "Trying to use items from another players inventory");
#endif
                return;
            }

            if (en is Container c)
            {
                if ((en as Container).OwnerId != -1 && (en as Container).OwnerId != Id)
                {
#if DEBUG
                    Program.Print(PrintType.Error, "Trying to use items from another players container/bag");
#endif
                    return;
                }

                if (en.Position.Distance(this) > ContainerMinimumDistance)
                {
#if DEBUG
                    Program.Print(PrintType.Error, "Too far away from container");
#endif
                    return;
                }
            }

            IContainer con = en as IContainer;
            ItemDesc desc = null;
            if (con.Inventory[slot.SlotId] != -1)
                desc = Resources.Type2Item[(ushort)con.Inventory[slot.SlotId]];

            if (desc == null)
            {
#if DEBUG
                Program.Print(PrintType.Error, "Invalid use item");
#endif
                return;
            }

            bool isAbility = slot.SlotId == 1;
            if (isAbility)
            {
                if (slot.ObjectId != Id)
                {
#if DEBUG
                    Program.Print(PrintType.Error, "Trying to use ability from a container?");
#endif
                    return;
                }

                if (UseTime + (UseDuration * (1f / UseCooldownThreshold)) > time)
                {
#if DEBUG
                    Program.Print(PrintType.Error, "Used ability too soon");
#endif
                    return;
                }

                if (MP - desc.MpCost < 0)
                {
#if DEBUG
                    Program.Print(PrintType.Error, "Not enough MP");
#endif
                    return;
                }
            }

            bool inRange = Position.Distance(target) <= MaxAbilityDist && Parent.GetTileF(target.X, target.Y) != null;
            Action callback = null;
            foreach (ActivateEffectDesc eff in desc.ActivateEffects)
            {
                switch (eff.Index)
                { 
                    case ActivateEffectIndex.HealNova:
                        {
                            byte[] nova = GameServer.ShowEffect(ShowEffectIndex.Nova, Id, 0xffffffff, new Position(eff.Range, 0));
                            foreach (Entity j in Parent.PlayerChunks.HitTest(Position, Math.Max(eff.Range, SightRadius)))
                            {
                                if (j is Player k)
                                {
                                    if (Position.Distance(j) <= eff.Range)
                                        k.Heal(eff.Amount, false);
                                    if (k.Client.Account.Effects || k.Equals(this))
                                        k.Client.Send(nova);
                                }
                            }
                        }
                        break;
                    case ActivateEffectIndex.ConditionEffectAura:
                        {
                            uint color = eff.Effect == ConditionEffectIndex.Damaging ? 0xffff0000 : 0xffffffff;
                            byte[] nova = GameServer.ShowEffect(ShowEffectIndex.Nova, Id, color, new Position(eff.Range, 0));
                            foreach (Entity j in Parent.PlayerChunks.HitTest(Position, Math.Max(eff.Range, SightRadius)))
                            {
                                if (j is Player k)
                                {
                                    if (Position.Distance(j) <= eff.Range)
                                        k.ApplyConditionEffect(eff.Effect, eff.DurationMS);
                                    if (k.Client.Account.Effects || k.Equals(this))
                                        k.Client.Send(nova);
                                }
                            }
                        }
                        break;
                    case ActivateEffectIndex.ConditionEffectSelf:
                        {
                            ApplyConditionEffect(eff.Effect, eff.DurationMS);

                            byte[] nova = GameServer.ShowEffect(ShowEffectIndex.Nova, Id, 0xffffffff, new Position(1, 0));
                            foreach (Entity j in Parent.PlayerChunks.HitTest(Position, SightRadius))
                                if (j is Player k && k.Client.Account.Effects)
                                    k.Client.Send(nova);
                        }
                        break;
                    case ActivateEffectIndex.Dye:
                        if (desc.Tex1 != 0)
                            Tex1 = desc.Tex1;
                        if (desc.Tex2 != 0)
                            Tex2 = desc.Tex2;
                        break;
                    case ActivateEffectIndex.Shoot:
                        if (!HasConditionEffect(ConditionEffectIndex.Stunned))
                            ShootAEs.Enqueue(desc.Type);
                        break;
                    case ActivateEffectIndex.Teleport:
                        if (inRange)
                            Teleport(time, target);
                        break;
                    case ActivateEffectIndex.BulletNova:
                        if (inRange)
                        {
                            List<Projectile> projs = new List<Projectile>(20);
                            int novaCount = 20;
                            int startId = NextAEProjectileId;
                            float angleInc = (MathF.PI * 2) / novaCount;
                            NextAEProjectileId += novaCount;
                            for (int i = 0; i < novaCount; i++)
                            {
                                int d = GetNextDamage(desc.Projectile.MinDamage, desc.Projectile.MaxDamage, ItemDatas[slot.SlotId]);
                                Projectile p = new Projectile(this, desc.Projectile, startId + i, time, angleInc * i, target, d);
                                projs.Add(p);
                            }

                            AwaitProjectiles(projs);

                            byte[] line = GameServer.ShowEffect(ShowEffectIndex.Line, Id, 0xFFFF00AA, target);
                            byte[] nova = GameServer.ServerPlayerShoot(startId, Id, desc.Type, target, 0, angleInc, projs);

                            foreach (Entity j in Parent.PlayerChunks.HitTest(Position, SightRadius))
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
                        break;
                    default:
                        Program.Print(PrintType.Error, $"Unhandled AE <{eff.Index.ToString()}>");
                        break;
                }
            }

            if (isAbility)
            {
                MP -= desc.MpCost;
                UseTime = time;
                float cooldownMod = ItemDesc.GetStat(ItemDatas[1], ItemData.Cooldown, ItemDesc.CooldownMultiplier);
                int cooldown = desc.CooldownMS;
                cooldown = cooldown + ((int)(cooldown * -cooldownMod));
                UseDuration = cooldown;
            }


            if (desc.Consumable)
            {
                con.Inventory[slot.SlotId] = -1;
                con.UpdateInventorySlot(slot.SlotId);
            }

            callback?.Invoke();
        }
    }
}
