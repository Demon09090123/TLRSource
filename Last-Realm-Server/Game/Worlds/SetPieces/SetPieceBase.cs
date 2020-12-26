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
            {
                for (var x = 0; x < width; x++)
                {
                    var curX = worldX + x - radiusX;
                    var curY = worldY + y - radiusY;

                    var toTile = _mapBase.Tiles[x, y];
                    var fromTile = world.GetTile(x, y);

                    fromTile.Type = toTile.GroundType;
                    world.Regions[fromTile.Region].Remove(new IntPoint(x, y));
                    fromTile.Region = toTile.Region;

                    if (!world.Regions.ContainsKey(fromTile.Region))
                        world.Regions[fromTile.Region] = new List<IntPoint>();

                    world.Regions[fromTile.Region].Add(new IntPoint(x, y));
                    world.Regions[fromTile.Region].Add(new IntPoint(x, y));

                    fromTile.UpdateCount++;

                    if (toTile.ObjectType != 0xff && toTile.ObjectType != 0)
                    {
                        var en = Entity.Resolve(toTile.ObjectType);

                        if (en.Desc.Static)
                        {
                            if (en.Desc.BlocksSight)
                                fromTile.BlocksSight = true;

                            fromTile.StaticObject = (StaticObject)en;
                        }

                        world.AddEntity(en, new Position(curX + 0.5f, curY + 0.5f));
                    }
                }

                world.UpdateCount = int.MaxValue / 2;
            }

            Program.Print(PrintType.Info, $"SetPiece {ID} Applied in {world.Name}!");
        }
    }
}
