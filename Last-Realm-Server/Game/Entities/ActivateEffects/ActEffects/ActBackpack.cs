using Last_Realm_Server.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Entities.ActivateEffects.ActEffects
{
    public class ActBackpack : IActEffect
    {
        public int ActivateID => (int)ActivateEffectIndex.Backpack;

        public void Activate(Player owner, ItemDesc desc, Position target, ActivateEffectDesc eff, SlotData slot, int time)
        {
            //if (owner.HasBackpack)
            //    callback = () =>
            //    {
            //        con.Inventory[slot.SlotId] = desc.Type;
            //        con.UpdateInventorySlot(slot.SlotId);
            //        SendError("You already have a backpack.");
            //    };
            //else
            //{
            //    owner.HasBackpack = true;
            //    owner.SendInfo("8 more spaces. Woohoo!");
            //}
        }
    }
}
