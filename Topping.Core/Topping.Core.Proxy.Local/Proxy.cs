namespace Topping.Core.Proxy.Local
{
	using Data.Db4o;
	using Logic.DB4o;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.CompilerServices;
	using TopMachine.Desktop.Overall;
	using TopMachine.Topping.Dto;

	using Topping.Core.Logic;

	using Topping.Core.Logic.Interface;
	using Topping.Core.Logic.Threads;
	using Topping.Core.Proxy.Local.Client;


	internal class Proxy : IProxy, IDisposable
    {
        private VGameState gameServerState = null;
        private static volatile Proxy instance;
        private static object syncRoot = new object();

        private Proxy()
        {
        }

        public void ActivateChat()
        {
            this.GetServerState().Players[this.UserName].ChatActivated = true;
        }

        public void AddPlayedRound(PlayerSummaryDto summary, PlayedGameRoundDto pgr)
        {
            VRoom r = this.GetServerState().Rooms[this.GetServerState().Players[this.UserName].CurrentRoom];
            r.AddPlayedRound(this.UserName, summary, pgr);
            MessageDto m = new MessageDto {
                From = "System",
                MessageType = MessageType.GameSummaryUpdated,
                Text = this.UserName,
                XmlObjectType = summary.GetType().FullName,
                XmlObject = new ObjectSerializer<PlayerSummaryDto>().Serialize(summary),
                Room = r.GetDto()
            };
            m.Room.Configuration = null;
            this.SendRoomMessage(r, m);
            m = new MessageDto {
                From = "System",
                MessageType = MessageType.GameSummaryRoundUpdated,
                Text = this.UserName,
                XmlObjectType = pgr.GetType().FullName,
                XmlObject = new ObjectSerializer<PlayedGameRoundDto>().Serialize(pgr),
                Room = r.GetDto(),
                ToRoom = r.RoomId
            };
            this.SendRoomMessage(r, m);
        }

        public bool ChangedPwdUser(UserDto us, string NewPwd)
        {
            throw new NotImplementedException();
        }

        public bool CheckVersion(string version)
        {
            return true;
        }

        public void DeactivateChat()
        {
            this.GetServerState().Players[this.UserName].ChatActivated = false;
        }

        public bool DeleteConfiguration(ConfigGameDto cfg)
        {
            using (ToppingAccessor accessor = new ToppingAccessor())
            {
                return accessor.DeleteConfiguration(cfg);
            }
        }

        public bool DeleteGameXML(string fn)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(UserDto us)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (RoomMessageProcessor.Instance != null)
            {
                RoomMessageProcessor.Instance.StopService();
            }
            if (MessageProcessor.Instance != null)
            {
                MessageProcessor.Instance.StopService();
            }
        }

        public void FinishGame()
        {
            this.GetServerState().Rooms[0].FinishGame(this.UserName);
        }

        public List<ConfigGameDto> GetConfigurations()
        {
            using (ToppingAccessor accessor = new ToppingAccessor())
            {
                return accessor.GetConfigurations();
            }
        }

        public FinalRoomDto GetFinalRoom(int RoomId)
        {
            VRoom room = this.GetServerState().Rooms[this.GetServerState().Players[this.UserName].CurrentRoom];
            return room.finalRoomDto;
        }

        public GeneratedGameDto GetGame()
        {
            return this.GetServerState().Rooms[this.GetServerState().Players[this.UserName].CurrentRoom].GetGeneratedGame();
        }

        public List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId)
        {
            using (ToppingAccessor accessor = new ToppingAccessor())
            {
                return accessor.GetGamesDetail(player, year, mont, configId);
            }
        }

        public void GetListPlayedRound(string playerSearch)
        {
        }

        public List<MessageDto> GetMessages()
        {
            if (this.UserName != null)
            {
                Player player = this.GetServerState().Players[this.UserName];
                MessageProxy.Proxy.QueueMessages(player.GetMessages());
            }
            return null;
        }

        public List<RankingDto> GetRankings(string player, int year, int mont, Guid ConfigId)
        {
            using (ToppingAccessor accessor = new ToppingAccessor())
            {
                return accessor.GetRankings(player, year, mont, ConfigId);
            }
        }

        public RoomDto GetRoom(int roomId)
        {
            return this.GetServerState().getRoom(roomId);
        }

        public List<VRoom> GetRooms()
        {
            return this.GetServerState().Rooms;
        }

        public List<RoomDto> GetRoomsDto()
        {
            return (from c in this.GetServerState().Rooms select c).ToList<RoomDto>();
        }

        public VGameState GetServerState()
        {
            if (this.gameServerState == null)
            {
                this.gameServerState = GameLocalState.Instance;
            }
            return this.gameServerState;
        }

        public List<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

        public RoomDto InitRoom(VRoom room, bool generate)
        {
            RoomDto dto = this.GetServerState().CreateRoom(room, generate, this.UserName);
            MessageDto m = new MessageDto {
                From = "System",
                MessageType = MessageType.RoomCreate,
                Room = dto,
                Text = this.UserName
            };
            this.SendRoomMessage(room, m);
            return dto;
        }

        public RoomDto JoinRoom(int roomId)
        {
            VRoom r = this.GetServerState().Rooms[roomId];
            MessageDto m = new MessageDto {
                From = "System",
                MessageType = MessageType.RoomEnter,
                Text = this.UserName
            };
            this.SendRoomMessage(r, m);
            return r.GetDto();
        }

        public void LeaveAnyRoom()
        {
            Player player = this.GetServerState().Players[this.UserName];
            if (player.CurrentRoom >= 0)
            {
                VRoom r = this.GetServerState().Rooms[player.CurrentRoom];
                r.LeaveRoom(player.Pseudo);
                player.CurrentRoom = -1;
                MessageDto m = new MessageDto {
                    From = this.UserName,
                    MessageType = MessageType.RoomLeave,
                    Room = r.GetDto(),
                    Text = this.UserName
                };
                this.SendRoomMessage(r, m);
            }
        }

        public RoomDto LeaveRoom()
        {
            VRoom r = this.GetServerState().Rooms[this.GetServerState().Players[this.UserName].CurrentRoom];
            r.FinishGame(this.UserName);
            r.LeaveRoom(this.UserName);
            this.GetServerState().Players[this.UserName].CurrentRoom = -1;
            MessageDto m = new MessageDto {
                From = this.UserName,
                MessageType = MessageType.RoomLeave,
                Text = this.UserName,
                Room = r.GetDto()
            };
            this.SendRoomMessage(r, m);
            return r.GetDto();
        }

        public string[] ListPlayers()
        {
            List<string> pseudos = this.GetServerState().Pseudos;
            if (pseudos != null)
            {
                return pseudos.ToArray();
            }
            return null;
        }

        public string[] ListRoomActivePlayers(int roomid)
        {
            VRoom room = this.GetServerState().Rooms[roomid];
            return room.GetActivePlayers().ToArray<string>();
        }

        public string[] ListRoomPlayers(int roomid)
        {
            VRoom room = this.GetServerState().Rooms[roomid];
            return room.GetPlayers().ToArray<string>();
        }

        public Guid Login(string userName, string passWord, int version)
        {
            Guid empty = Guid.Empty;
            this.UserName = userName.ToLower();
            ToppingAccessor accessor = new ToppingAccessor();
            if (accessor.Login(this.UserName, passWord))
            {
                accessor.Dispose();
                empty = this.GetServerState().AddPlayer(this.UserName);
                MessageDto m = new MessageDto {
                    From = this.UserName,
                    MessageType = MessageType.Login
                };
                this.SendMessage(m);
            }
            return empty;
        }

        public void Logoff()
        {
            MessageDto m = new MessageDto {
                From = this.UserName,
                MessageType = MessageType.Logoff
            };
            this.SendMessage(m);
        }

        public void ResetRooms()
        {
            this.GetServerState().ResetRooms();
        }

        public void SendMessage(MessageDto m)
        {
            MessageProcessor.Instance.AddMessage(m);
        }

        public void SendMessageToRoom(int x, MessageDto m)
        {
            RoomMessageProcessor.Instance.AddMessage(m);
        }

        private void SendRoomMessage(VRoom r, MessageDto m)
        {
            RoomMessageProcessor.Instance.AddMessage(m);
        }

        public void SetGame(GeneratedGameDto game)
        {
            VRoom room = this.GetServerState().Rooms[this.GetServerState().Players[this.UserName].CurrentRoom];
            if (room.GetGeneratedGame() == null)
            {
                room.SetGeneratedGame(game);
                room.GameStatus = RoomStatus.Created;
                MessageDto m = new MessageDto {
                    From = "System",
                    MessageType = MessageType.RoomStatusChange,
                    Text = "La partie est g\x00e9n\x00e9r\x00e9e",
                    ToRoom = room.RoomId,
                    Room = room.GetDto()
                };
                this.SendMessage(m);
            }
        }

        public void StartGame()
        {
            this.GetServerState().Rooms[this.GetServerState().Players[this.UserName].CurrentRoom].StartGame(this.UserName);
        }

        public bool UpdateConfiguration(ConfigGameDto cfg)
        {
            using (ToppingAccessor accessor = new ToppingAccessor())
            {
                return accessor.UpdateConfiguration(cfg);
            }
        }

        public bool UpdateUser(UserDto us)
        {
            throw new NotImplementedException();
        }

        public static Proxy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Proxy();
                        }
                    }
                }
                return instance;
            }
        }

        public string UserName { get; set; }
    }
}

