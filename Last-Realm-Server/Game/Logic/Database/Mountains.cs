using Last_Realm_Server.Common;
using Last_Realm_Server.Game.Logic.Behaviors;
using Last_Realm_Server.Game.Logic.Conditionals;
using Last_Realm_Server.Game.Logic.Loots;
using Last_Realm_Server.Game.Logic.Transitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Logic.Database
{
    public class Mountains : IBehaviorDatabase
    {
        public void Init(BehaviorDb db)
        {
            db.Init("Medusa", 
                new IfConditionEffect(ConditionEffectIndex.Slowed, 
                    new Shoot(32, 16)),
                new Shoot(7, 1, cooldown: 5000), 
                new Wander(.4f), 
                new Grenade(radius: 2, damage: 20, cooldown: 1500, color: 0xffFFFF00, effect: ConditionEffectIndex.Paralyzed, effectDuration: 300),
                new ItemLoot("Demon Blade", 0.2f, 0));

            db.Init("Beholder", 
                new Wander(1f),
                new Shoot(16, 3, cooldown: 1000));

            db.Init("Beer God",
                new Wander(2));
        }
    }
}
