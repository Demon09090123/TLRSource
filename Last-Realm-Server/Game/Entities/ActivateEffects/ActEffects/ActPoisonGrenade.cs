using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActPoisonGrenade : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.PoisonGrenade;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            var parent = owner.Parent;
            var pos = owner.Position;
            var id = owner.Id;

            if (pos.Distance(target) <= Player.MaxAbilityDist && parent.GetTileF(target.X, target.Y) != null)
            {
                var placeholder = new Placeholder();
                parent.AddEntity(placeholder, target);

                byte[] throwEff = GameServer.ShowEffect(ShowEffectIndex.Throw, id, 0xffddff00, target);
                byte[] nova = GameServer.ShowEffect(ShowEffectIndex.Nova, placeholder.Id, 0xffddff00, new Position(eff.Radius, 0));

                foreach (Entity j in parent.PlayerChunks.HitTest(pos, Player.SightRadius))
                    if (j is Player k && (k.Client.Account.Effects || k.Equals(this)))
                        k.Client.Send(throwEff);

                Manager.AddTimedAction(1500, () =>
                {
                    if (placeholder.Parent != null)
                    {
                        if (owner.Parent != null)
                        {
                            foreach (Entity j in parent.PlayerChunks.HitTest(owner.Position, Player.SightRadius))
                                if (j is Player k && (k.Client.Account.Effects || k.Equals(this)))
                                    k.Client.Send(nova);
                            foreach (Entity j in parent.EntityChunks.HitTest(placeholder.Position, eff.Radius))
                                if (j is Enemy e)
                                    e.ApplyPoison(owner, new ConditionEffectDesc[0], (int)(eff.TotalDamage / (eff.DurationMS / 1000f)), eff.TotalDamage);
                        }
                        placeholder.Parent.RemoveEntity(placeholder);
                    }
                });
            }


        }
    }
}
