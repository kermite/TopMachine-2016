namespace Topping.Core.Proxy.Local.Client
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;
	using TopMachine.Desktop.Controls.Sound;
	using TopMachine.Desktop.Overall;
	using TopMachine.Topping.Dto;
	using Topping.Core.Logic;
	using Topping.Core.Logic.Client;
	using Topping.Core.Proxy.Local;

	public class Session : ISession, IDisposable
    {
        public VRoom _currentRoom;
        private bool authenticated = false;
        private Dictionary<string, IMessageHandler> messageHandlers = new Dictionary<string, IMessageHandler>();
        private string password = "";
        private string pseudo = "";
        private Dictionary<string, IMessageHandler> tmpMessageHandlers = new Dictionary<string, IMessageHandler>();

        public void AddMessageHandler(string key, IMessageHandler h, bool update)
        {
            lock (this.tmpMessageHandlers)
            {
                if (this.tmpMessageHandlers.ContainsKey(key))
                {
                    this.tmpMessageHandlers.Remove(key);
                    this.tmpMessageHandlers.Add(key, h);
                }
                else
                {
                    this.tmpMessageHandlers.Add(key, h);
                }
            }
            if (update)
            {
                this.UpdateHandlers();
            }
        }

        public void CheckCurrentRoom(VRoom r)
        {
            if ((this.CurrentRoom != null) && (this.CurrentRoom.RoomId == r.RoomId))
            {
                this.CurrentRoom = r;
            }
        }

        public void Dispose()
        {
            foreach (string str in this.messageHandlers.Keys)
            {
                MemoryCheck.AddError(this.messageHandlers[str].GetType().FullName + " Message Handler is not disposed");
            }
            this.tmpMessageHandlers.Clear();
            this.messageHandlers.Clear();
        }

        public void HandleMessage(MessageDto message)
        {
            this.HandleMessageLocally(message);
            lock (this.messageHandlers)
            {
                foreach (IMessageHandler handler in this.messageHandlers.Values)
                {
                    if (handler != null)
                    {
                        handler.HandleMessage(message);
                    }
                }
            }
            this.UpdateHandlers();
        }

        public void HandleMessageLocally(MessageDto message)
        {
            Predicate<VRoom> match = null;
            Predicate<VRoom> predicate2 = null;
            Predicate<VRoom> predicate3 = null;
            Predicate<VRoom> predicate4 = null;
            Predicate<VRoom> predicate5 = null;
            RoomDto r = null;
            VRoom room = null;
            switch (message.MessageType)
            {
                case MessageType.RoomCreate:
                    r = message.Room;
                    if (match == null)
                    {
                        match = p => p.RoomId == r.RoomId;
                    }
                    this.Rooms.Find(match).SetDto(r);
                    break;

                case MessageType.RoomEnter:
                    r = message.Room;
                    if (predicate2 == null)
                    {
                        predicate2 = p => p.RoomId == r.RoomId;
                    }
                    room = this.Rooms.Find(predicate2);
                    room.SetDto(r);
                    room.AddPlayer(message.From, false);
                    break;

                case MessageType.RoomLeave:
                    r = message.Room;
                    if (predicate4 == null)
                    {
                        predicate4 = p => p.RoomId == message.ToRoom;
                    }
                    room = this.Rooms.Find(predicate4);
                    room.GameStartTime = DateTime.Now.AddSeconds((double) r.WaitingTime);
                    if (room != null)
                    {
                        room.RemovePlayer(message.From);
                    }
                    break;

                case MessageType.RoomGameStart:
                {
                    r = message.Room;
                    if (predicate3 == null)
                    {
                        predicate3 = p => p.RoomId == message.ToRoom;
                    }
                    room = this.Rooms.Find(predicate3);
                    room.GameStartTime = DateTime.Now.AddSeconds((double) r.WaitingTime);
                    if (room != null)
                    {
                        room.GameStatus = RoomStatus.Started;
                    }
                    GeneratedGameDto game = MessageProxy.Proxy.GetGame();
                    MessageProxy.Proxy.GameState = MessageProxy.Proxy.SetGameState(r.Configuration);
                    MessageProxy.Proxy.GameState.GeneratedGame = game;
                    break;
                }
                case MessageType.RoomStatusChange:
                    r = message.Room;
                    if (predicate5 == null)
                    {
                        predicate5 = p => p.RoomId == r.RoomId;
                    }
                    room = this.Rooms.Find(predicate5);
                    if (room != null)
                    {
                        room.GameStatus = r.GameStatus;
                        room.TotalPlayers = r.TotalPlayers;
                        room.GameStartTime = DateTime.Now.AddSeconds((double) r.WaitingTime);
                    }
                    break;

                case MessageType.Private:
                    if (message.MessageModule == MessageModule.Game)
                    {
                        SerializableDictionary<string, List<PlayedGameRoundDto>> lst = new ObjectSerializer<SerializableDictionary<string, List<PlayedGameRoundDto>>>().Deserialize(message.XmlObject);
                        MessageProxy.Proxy.GameState.InitPlayedGameRoundDto(lst);
                    }
                    break;

                case MessageType.GameSummaryUpdated:
                {
                    PlayerSummaryDto summary = new ObjectSerializer<PlayerSummaryDto>().Deserialize(message.XmlObject);
                    MessageProxy.Proxy.GameState.AddSummary(summary);
                    break;
                }
                case MessageType.Logoff:
					Sound.play(Sound.SoundType.LOGOUT);
					this.Pseudos.Remove(message.From);
                    break;

                case MessageType.Login:
                    if (!this.Pseudos.Contains(message.From))
                    {
						Sound.play(Sound.SoundType.CHAT);

						this.Pseudos.Add(message.From);
                    }
                    break;

                case MessageType.GameSummaryRoundUpdated:
                {
                    PlayedGameRoundDto pgr = new ObjectSerializer<PlayedGameRoundDto>().Deserialize(message.XmlObject);
                    MessageProxy.Proxy.GameState.AddPlayedGameRoundDto(pgr);
                    break;
                }
            }
        }

        public void RemoveMessageHandler(string key)
        {
            lock (this.messageHandlers)
            {
                this.messageHandlers.Remove(key);
            }
        }

        public void SetCompleteGame(int roomid)
        {
            FinalRoomDto dto = MessageProxy.Proxy.getFinalRoom(roomid);
            MessageProxy.Proxy.DeleteCopy();
            if (MessageProxy.Proxy.GameState != null)
            {
                MessageProxy.Proxy.GameState.Dispose();
                MessageProxy.Proxy.GameState = null;
            }
            MessageProxy.Proxy.GameState = new GameState(this.CurrentRoom);
            MessageProxy.Proxy.GameState.GeneratedGame = dto.Game;
            MessageProxy.Proxy.GameState.FinalRoom = dto;
        }

        private void UpdateHandlers()
        {
            lock (this.tmpMessageHandlers)
            {
                if (this.tmpMessageHandlers.Count > 0)
                {
                    foreach (string str in this.tmpMessageHandlers.Keys)
                    {
                        if (this.messageHandlers.ContainsKey(str))
                        {
                            this.messageHandlers[str] = null;
                            this.messageHandlers[str] = this.tmpMessageHandlers[str];
                        }
                        else
                        {
                            this.messageHandlers.Add(str, this.tmpMessageHandlers[str]);
                        }
                    }
                }
                this.tmpMessageHandlers.Clear();
            }
        }

        public bool Authenticated
        {
            get
            {
                return this.authenticated;
            }
            set
            {
                this.authenticated = value;
            }
        }

        public List<ConfigGameDto> Configurations { get; set; }

        public VRoom CurrentRoom
        {
            get
            {
                return this._currentRoom;
            }
            set
            {
                this._currentRoom = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public string Pseudo
        {
            get
            {
                return this.pseudo;
            }
            set
            {
                this.pseudo = value;
            }
        }

        public List<string> Pseudos { get; set; }

        public List<VRoom> Rooms { get; set; }
    }
}

