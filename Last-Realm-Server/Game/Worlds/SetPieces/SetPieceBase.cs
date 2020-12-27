using Last_Realm_Server.Common;
using Last_Realm_Server.Game.Entities;
using System.Collections.Generic;

namespace Last_Realm_Server.Game.Worlds.SetPieces
{
    public class SetPieceBase
    {
        public string ID { get; private set; }
        private MapBase _mapBase;

        public SetPieceBase(SetPieceDesc desc)
        {
            ID = desc.ID;
            _mapBase = desc.Map;
        }

        public void ApplySetPiece(int worldX, int worldY, World world)
        {
            var width = _mapBase.Width;
            var height = _mapBase.Height;
            var radiusX = _mapBase.Width / 2;
            var radiusY = _mapBase.Height / 2;

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    var pos = new IntPoint(worldX + x - radiusX, worldY + y - radiusY);

                    var toTile = _mapBase.Tiles[x, y];

                    world.UpdateTile(pos.X, pos.Y, toTile.GroundType);
                    world.AddRegion(pos, toTile.Region);

                    if (toTile.ObjectType != 0xff && toTile.ObjectType != 0)
                    {
                        var en = Entity.Resolve(toTile.ObjectType);

                        if (en == null)
                            continue;

                        if (en.Desc.Static)
                        {
                            var tile = world.GetTile(pos.X, pos.Y);
                            if (en.Desc.BlocksSight)
                                tile.BlocksSight = true;
                            tile.StaticObject = (StaticObject)en;
                        }

                        world.AddEntity(en, new Position(pos.X + .5f, pos.Y + .5f));
                    }
                }

            Program.Print(PrintType.Info, $"SetPiece {ID} Applied in {world.Name}!");
        }
    }
}
