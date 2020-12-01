using Last_Realm_Server.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Entities.ActivateEffects
{
    public interface IActEffect
    {
        int ActivateID { get; }

        void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time);
    }
}
