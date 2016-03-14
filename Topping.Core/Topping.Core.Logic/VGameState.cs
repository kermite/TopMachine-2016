using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Topping.Core.Logic.Threads;

using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dto;
using System.Web;


namespace Topping.Core.Logic
{

    public  abstract class  VGameState
    {

        protected static object syncRoot = new object();
        
        public int MaxAmountRooms { get; set; }

        
        public List<VRoom> Rooms { get; set; }
        public List<ConfigGameDto> Configurations { get; set; }
        public Dictionary<string, Player> Players { get; set; }
        public Dictionary<Guid, string> PlayersByGuid { get; set; }
        public List<string> Pseudos { get; set; }
     

        public string ServerPath { get; set; }

        public RoomDto getRoom(int roomid)
        {
            return Rooms.First(x => x.RoomId == roomid).GetDto();
        }

        public abstract void InitRooms();
       

        protected void r_OnRoomStatusChange(VRoom room)
        {
            MessageDto mm = new MessageDto();
            mm.From = "System";
            mm.MessageType = MessageType.RoomStatusChange;
            mm.ToRoom = room.RoomId;
            mm.Room = room.GetDto();
            MessageProcessor.Instance.AddMessage(mm);
        }

        public Player AccessPlayer(Guid player)
        {
            if (PlayersByGuid.ContainsKey(player))
            {
                Player p = Players[PlayersByGuid[player]];
                p.LastAccess = DateTime.Now;
                return p;
            }

            return null;
        }

        public Player AccessPlayer(string player)
        {
            if (Players.ContainsKey(player))
            {
                Player p = Players[player];
                p.LastAccess = DateTime.Now;
                return p;
            }

            return null;
        }

        public RoomDto RemovePlayer(Player p)
        {
            lock (syncRoot)
            {
                PlayersByGuid.Remove(p.PlayerGuid);
                Players.Remove(p.Pseudo);
                Pseudos.Remove(p.Pseudo);
            }

            if (p.CurrentRoom > -1)
            {
                VRoom r = Rooms[p.CurrentRoom];
                p.Dispose();
                if (r != null)
                {
                    return r.GetDto();
                }
            }
            return null;

        }

        public Guid AddPlayer(string player)
        {
            lock (syncRoot)
            {
                if (Players.ContainsKey(player))
                {
                    Player pp = Players[player.ToLower()];
                    pp.LastAccess = DateTime.Now;
                    return pp.PlayerGuid;
                }
                else
                {
                    Player p = new Player(player);
                    Guid g = Guid.NewGuid();
                    p.PlayerGuid = g;
                    Players.Add(player.ToLower(), p);
                    PlayersByGuid.Add(g, player.ToLower());
                    Pseudos.Add(player);
                    return g;
                }
            }

            return Guid.Empty;
        }

        public void ResetRooms()
        {
            lock (syncRoot)
            {
                foreach (VRoom r in Rooms.ToArray())
                {
                    r.Clean();
                }
            }
        }


        public RoomDto CreateRoom(VRoom room, bool generate, string player)
        {
            lock (syncRoot)
            {
                foreach (VRoom r in Rooms.ToArray())
                {
                    if (r.GameStatus == RoomStatus.Empty)
                    {
                        r.SetGeneratedGame(null);
                        r.Configuration = room.Configuration;
                        r.ConfigId = room.ConfigId;
                        r.GameStatus = RoomStatus.Created;
                        r.MaximumPlayers = room.MaximumPlayers;
                        r.MinimumPlayers = room.MinimumPlayers;
                        r.Owner = player;
                        r.JoinRoom(player);
                        Players[player].CurrentRoom = r.RoomId;
                        if (generate)
                        {
                            GenerateGame(r);
                        }
                        return r.GetDto();
                    }
                }
            }

            return null;
        }

        private void GenerateGame(VRoom r)
        {
            if (r.GameStatus == RoomStatus.Finished || r.finalRoomDto.Game == null) 
            {
                GameGenerator generator = new GameGenerator(this, r.Configuration.Id, ServerPath, r);
                Thread thread = new System.Threading.Thread(generator.Start);
                thread.Start();
            }
        }

    }
}
