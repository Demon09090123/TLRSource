using Last_Realm_Server.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Logic.Conditionals
{
    public class IfNoConditionEffect : Conditional
    {
        public readonly ConditionEffectIndex Effect;

        public IfNoConditionEffect(ConditionEffectIndex effect, params Behavior[] behaviors) : base(behaviors)
        {
            Effect = effect;
        }

        public override bool ConditionMet(Entity host)
        {
            return !host.HasConditionEffect(Effect);
        }
    }
}
