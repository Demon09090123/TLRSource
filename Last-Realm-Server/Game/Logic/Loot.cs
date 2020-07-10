using Last_Realm_Server.Game.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Logic
{
    public abstract class Loot : IBehavior
    {
        public virtual int TryObtainItem(Entity host, Player player, int position, float threshold)
        {
            return -1;
        }
    }
}
