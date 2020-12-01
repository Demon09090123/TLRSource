using Last_Realm_Server.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Last_Realm_Server.Game.Entities.ActivateEffects
{
    public class ActEffectManager
    {
        static readonly Dictionary<int, IActEffect> _actEffects;

        static ActEffectManager()
        {
            _actEffects = new Dictionary<int, IActEffect>();

            //preload all the activateEffect instances

            var instances = Assembly.GetAssembly(typeof(IActEffect)).GetTypes().Where(_ => _.IsClass);

            foreach(var inst in instances)
            {
                var actEffect = Activator.CreateInstance(inst) as IActEffect;

                _actEffects.Add(actEffect.ActivateID, actEffect);

                Program.Print(PrintType.Info, "ActivateEffect Instances Loaded...");
            }
        }

        public static void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time) => _actEffects[(int)eff.Index].Activate(owner, desc, target, eff, slot, time);
    }
}
