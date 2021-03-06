﻿using Last_Realm_Server.Common;
using Last_Realm_Server.Networking;
using Last_Realm_Server.Utils;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Last_Realm_Server.Game.Entities
{
    public partial class Player
    {
        private const float MoveSpeedThreshold = 1.1f;

        public float MoveMultiplier = 1f;
        public int MoveTime;
        public int AwaitingMoves;
        public Queue<int> AwaitingGoto;
        public int TickId;
        public float PushX;
        public float PushY;

        public int ValidThreshold = 0;//To account for desync between client and server

        public bool ValidMove(int time, Position pos)
        {
            int diff = time - MoveTime;
            float speed = GetMovementSpeed();
            float dist = (speed * diff) * MoveSpeedThreshold;
            Position pushedServer = new Position(Position.X - (diff * PushX), Position.Y - (diff * PushY));
            if (pos.Distance(pushedServer) > dist && pos.Distance(Position) > dist)
            {
                if (ValidThreshold > 3)
                {
                    Program.Print(PrintType.Error, "INVALID MOVEMENT DIST/SPD = " + pos.Distance(pushedServer) + " : " + speed);
                    return false;
                }
                ValidThreshold++;
            }
            return true;
        }

        public void TryMove(int time, Position pos)
        {
            if (!ValidTime(time))
            {
                Client.Disconnect();
                return;
            }

            if (AwaitingGoto.Count > 0)
            {
                foreach (int gt in AwaitingGoto)
                {
                    if (gt + TimeUntilAckTimeout < time)
                    {
                        Program.Print(PrintType.Error, "Goto ack timed out");
                        Client.Disconnect();
                        return;
                    }
                }
                return;
            }

            if (!ValidMove(time, pos))
            {
                Client.Disconnect();
                return;
            }

            if (TileFullOccupied(pos.X, pos.Y))
            {
                Client.Disconnect();
                return;
            }

            AwaitingMoves--;
            if (AwaitingMoves < 0)
            {
                Client.Disconnect();
                return;
            }

            Tile tile = Parent.Tiles[(int)pos.X, (int)pos.Y];
            TileDesc desc = Resources.Type2Tile[tile.Type];
            if (desc.Damage > 0 && !HasConditionEffect(ConditionEffectIndex.Invincible))
            {
                if (!(tile.StaticObject?.Desc.ProtectFromGroundDamage ?? false) && Damage(desc.Id, ProjType.None, desc.Damage, new ConditionEffectDesc[0], true, false))
                    return;
            }

            Parent.MoveEntity(this, pos);
            if (CheckProjectiles(time))
                return;

            if (desc.Push)
            {
                PushX = desc.DX;
                PushY = desc.DY;
            }
            else
            {
                PushX = 0;
                PushY = 0;
            }

            MoveMultiplier = GetMoveMultiplier();
            MoveTime = time;
        }

        public void TryGotoAck(int time)
        {
            if (!ValidTime(time))
            {
#if DEBUG
                Program.Print(PrintType.Error, "GotoAck invalid time");
#endif
                Client.Disconnect();
                return;
            }

            if (!AwaitingGoto.TryDequeue(out int t))
            {
#if DEBUG
                Program.Print(PrintType.Error, "No GotoAck to ack");
#endif
                Client.Disconnect();
                return;
            }
        }

        public bool Teleport(int time, Position pos)
        {
            if (!RegionUnblocked(pos.X, pos.Y))
                return false;

            Tile tile = Parent.GetTileF((int)pos.X, (int)pos.Y);
            if (tile == null || TileUpdates[(int)pos.X, (int)pos.Y] != tile.UpdateCount)
                return false;

            Parent.MoveEntity(this, pos);
            AwaitingGoto.Enqueue(time);

            byte[] eff = GameServer.ShowEffect(ShowEffectIndex.Teleport, Id, 0xFFFFFFFF, pos);
            byte[] go = GameServer.Goto(Id, pos);

            foreach (Player player in Parent.Players.Values)
            {
                if (player.Client.Account.Effects)
                    player.Client.Send(eff);
                player.Client.Send(go);
            }
            return true;
        }
    }
}
