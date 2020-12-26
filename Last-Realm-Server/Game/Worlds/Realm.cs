using Last_Realm_Server.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Last_Realm_Server.Game.Worlds
{
    public class Realm : World
    {
        public Realm(WorldDesc desc) : base(desc)
        {
            //sample Spawn
            Map.Regions.Add(Region.Spawn, new List<IntPoint>()
            {
                new IntPoint(500, 500)
            });
        }
    }
}
