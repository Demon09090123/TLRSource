using Last_Realm_Server.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Logic.Transitions
{
    public class TimedTransition : Transition
    {
        public readonly int Time;

        public TimedTransition(string targetState, int time = 1000) : base(targetState)
        {
            Time = time;
        }

        public override void Enter(Entity host)
        {
            host.StateCooldown.Add(Id, Time);
        }

        public override bool Tick(Entity host)
        {
            host.StateCooldown[Id] -= Settings.MillisecondsPerTick;
            if (host.StateCooldown[Id] <= 0)
            {
                host.StateCooldown[Id] = Time;
                return true;
            }
            return false;
        }

        public override void Exit(Entity host)
        { 
            host.StateCooldown.Remove(Id);
        }
    }
}
