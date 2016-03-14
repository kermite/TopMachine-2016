
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using TopMachine.Desktop.Overall;
using TopMachine.Topping.Dto;
using Topping.Core.Logic;
using Topping.Core.Logic.Client;

using Topping.Core.Proxy.Local;
using Topping.Core.Logic.SQL;
using Topping.Core.Logic.Interface;
using NLog;

namespace Topping.Core.Proxy.Local.Client
{   

    public class MessageProxy : IMessageProxy, IDisposable
    {
        private static MessageProxy _messageProxy;
        public bool isLoading = false;
        public bool isSuspended = false;
        private Guid MemoryCheckId;
        public int ModeGlobal = 0;
        public int ModeProxy = 0;
        private Timer tmr = null;

        public MessageProxy()
        {
            this.MemoryCheckId = MemoryCheck.Register(this, base.GetType().FullName);
        }

        public void ActivateChat()
        {
            if (this.GamePermissions.HasFlag(Topping.Core.Logic.Client.GamePermissions.CanChat))
            {
                this.client.ActivateChat();
            }
            else
            {
                this.client.DeactivateChat();
            }
        }

        public void AddPlayedRound(PlayerSummaryDto summary, PlayedGameRoundDto pgr)
        {
            summary.Player = this.Session.Pseudo;
            pgr.Player = this.Session.Pseudo;
            this.GameState.GetPlayerSummary().Replace(summary, pgr);
            this.client.AddPlayedRound(summary, pgr);
        }

        public bool ChangedPwdUser(UserDto us, string NewPwd)
        {
            return this.client.ChangedPwdUser(us, NewPwd);
        }

        public bool CheckVersion(string version)
        {
            return this.client.CheckVersion(version);
        }

        public IGameState CreateGameState(ConfigGameDto dto)
        {
            this.LeaveAnyRoom();
            this.ResetRooms();
            VRoom room = null;
            if (this.ModeProxy == 0)
            {
                room = new Topping.Core.Logic.DB4o.Room { Configuration = dto };
            }
            else 
            {
                // a cause de la serialisation datacontract wcf 
                room = new Topping.Core.Logic.SQL.Room { Configuration = dto };
            }
            RoomDto roomdt = Proxy.InitRoom(room, true);
            VRoom r = this.Session.Rooms.Find(p => p.RoomId == roomdt.RoomId);
            r.SetDto(r);
            this.Session.CurrentRoom = r;
            r.AddPlayer(this.Session.Pseudo);
            if (this._GameState != null)
            {
                this._GameState.Dispose();
                this._GameState = null;
            }
            GC.Collect();
            this._GameState = new Topping.Core.Proxy.Local.GameState(r);
            return this.GameState;
        }

        public void DeactivateChat()
        {
            this.client.DeactivateChat();
        }

        public bool DeleteConfiguration(ConfigGameDto cfg)
        {
            return this.client.DeleteConfiguration(cfg);
        }

        public void DeleteCopy()
        {
            if (this._CurrentGameState != null)
            {
                this._CurrentGameState.Dispose();
                this._CurrentGameState = null;
            }
        }

        public bool DeleteUser(UserDto us)
        {
            return this.client.DeleteUser(us);
        }

        public void Dispose()
        {
            if (this.Session != null)
            {
                ((IDisposable) this.Session).Dispose();
                this.Session = null;
            }
            this.DeleteCopy();
            if (this._GameState != null)
            {
                this._GameState.Dispose();
                this._GameState = null;
            }
            if (this.tmr != null)
            {
                this.tmr.Enabled = false;
                this.tmr.Dispose();
                this.tmr = null;
            }
            ((IDisposable) this.client).Dispose();
            if (_messageProxy != null)
            {
                MemoryCheck.Unregister(this.MemoryCheckId);
                _messageProxy = null;
            }
        }

        public void FinishGame()
        {
            this.client.FinishGame();
        }

        public List<ConfigGameDto> GetConfigurations()
        {
            if (this.Session == null) 
            {              
                MessageProxy.Proxy.InitializeLocal(); 
            }
            this.Session.Configurations = this.client.GetConfigurations();
            return this.Session.Configurations;
        }

        public FinalRoomDto getFinalRoom(int roomid)
        {
            return this.client.GetFinalRoom(roomid);
        }

        public GeneratedGameDto GetGame()
        {
            return this.client.GetGame();
        }

        public List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId)
        {
            return this.client.GetGamesDetail(player, year, mont, configId);
        }

        public IGameState GetGameState()
        {
            return this.GameState;
        }

        public void GetListPlayedRound(string playerSearch)
        {
            this.client.GetListPlayedRound(playerSearch);
        }

        public List<MessageDto> GetMessages()
        {
            return this.client.GetMessages();
        }

        private void GetPlayers()
        {
            string[] strArray = this.ListPlayers();
            this.Session.Pseudos = new List<string>();
            foreach (string str in strArray)
            {
                this.Session.Pseudos.Add(str);
            }
        }

        public List<RankingDto> GetRankings(string player, int year, int mont, Guid ConfigId)
        {
            return this.client.GetRankings(player, year, mont, ConfigId);
        }

        public RoomDto GetRoom(int roomid)
        {
            return this.client.GetRoom(roomid);
        }

        private void GetRoomsDto()
        {
            List<RoomDto> roomsDto = this.client.GetRoomsDto();
            this.Session.Rooms = new List<VRoom>();
            foreach (RoomDto dto in roomsDto)
            {
                Topping.Core.Logic.DB4o.Room r = new Topping.Core.Logic.DB4o.Room(dto);
                this.Session.Rooms.Add(r);
            }
        }

        public List<UserDto> GetUsers()
        {
            return this.client.GetUsers();
        }

        public void InitializeLocal()
        {
            this.ModeProxy = 0;
            this.GamePermissions = Topping.Core.Logic.Client.GamePermissions.CanValidate | Topping.Core.Logic.Client.GamePermissions.CanPauseRound | Topping.Core.Logic.Client.GamePermissions.CanEndRound;
            if (this.Session != null)
            {
                ((IDisposable) this.Session).Dispose();
            }
            this.Session = new Topping.Core.Proxy.Local.Client.Session();
        }

        public void InitializeOnline()
        {
            this.ModeProxy = 1;
            this.GamePermissions = Topping.Core.Logic.Client.GamePermissions.CanValidate | Topping.Core.Logic.Client.GamePermissions.CanChat;
            if (this.Session != null)
            {
                ((IDisposable) this.Session).Dispose();
            }
            this.Session = new Topping.Core.Proxy.Local.Client.Session();
        }

        public RoomDto InitRoom(VRoom room, bool generate)
        {
            RoomDto r = this.client.InitRoom(room, generate);
            this.Session.CurrentRoom = this.Session.Rooms.Find(p => p.RoomId == r.RoomId);
            this.Session.CurrentRoom.SetDto(r);
            return r;
        }

        public RoomDto JoinRoom(int roomId)
        {
            RoomDto r = this.client.JoinRoom(roomId);
            this.Session.CurrentRoom = this.Session.Rooms.Find(p => p.RoomId == roomId);
            this.Session.CurrentRoom.SetDto(r);
            string[] strArray = this.client.ListRoomPlayers(roomId);
            foreach (string str in strArray)
            {
                this.Session.CurrentRoom.AddPlayer(str);
            }
            return this.Session.CurrentRoom.GetDto();
        }

        public void LeaveAnyRoom()
        {
            this.Session.CurrentRoom = null;
            this.client.LeaveAnyRoom();
        }

        public void LeaveRoom()
        {
            this.client.LeaveRoom();
            this.Session.CurrentRoom = null;
        }

        public string[] ListPlayers()
        {
            return this.client.ListPlayers();
        }

        public string[] ListRoomActivePlayers(int roomId)
        {
            return this.client.ListRoomActivePlayers(roomId);
        }

        public string[] ListRoomPlayers(int roomId)
        {
            return this.client.ListRoomPlayers(roomId);
        }

        public IGameState LoadGameState()
        {
            this.DeleteCopy();
            if (this._CurrentGameState != null)
            {
                this._CurrentGameState.Dispose();
            }
            this._CurrentGameState = new Topping.Core.Proxy.Local.GameState(this.GameState.Room);
            if (this._GameState != null)
            {
                this._CurrentGameState.Copy(this._GameState);
            }
            this._GameState = new Topping.Core.Proxy.Local.GameState(this.Session.CurrentRoom);
            return this.GameState;
        }

        public void LoadTimer()
        {
            if (this.tmr != null)
            {
                this.tmr.Enabled = false;
                this.tmr.Dispose();
                this.tmr = null;
            }
            this.tmr = new Timer();
            this.tmr.Interval = 0x1388;
            this.tmr.Tick += new EventHandler(this.tmr_Tick);
            this.tmr.Start();
        }

        public bool Login(string userName, string passWord)
        {
            bool flag = this.client.Login(userName, passWord, 0x1770) != Guid.Empty;
            if (flag)
            {
                this.Session.Pseudo = userName;
            }
            return flag;
        }

        public void Logoff()
        {
            this.client.Logoff();
        }

        public void QueueMessages(List<MessageDto> messages)
        {
            this.isLoading = true;
            foreach (MessageDto dto in messages)
            {
                try
                {
                    this.Session.HandleMessage(dto);
                }
                catch (Exception exception)
                {
                    LogManager.GetLogger("wcf").Error("QueueMessages : Message type is " + dto.MessageType.ToString());
                    if (exception.InnerException != null)
                    {
                        LogManager.GetLogger("wcf").ErrorException("QueueMessages", exception.InnerException);
                    }
                    LogManager.GetLogger("wcf").ErrorException("QueueMessages", exception);
                }
            }
            this.isLoading = false;
        }

        public void ResetRooms()
        {
            this.client.ResetRooms();
        }

        public void Resume()
        {
            this.isSuspended = false;
            this.tmr.Enabled = true;
        }

        public void SendMessage(MessageDto m)
        {
            if (this.ModeGlobal == 0)
            {
                this.client.SendMessage(m);
            }
        }

        public void SendMessageToRoom(int x, MessageDto m)
        {
            this.client.SendMessageToRoom(x, m);
        }

        public void SetGame(GeneratedGameDto game)
        {
            this.client.SetGame(game);
        }

        public IGameState SetGameState(ConfigGameDto dto)
        {
            this.DeleteCopy();
            if (this._GameState != null)
            {
                this._GameState.Dispose();
                this._GameState = null;
            }
            this._GameState = new Topping.Core.Proxy.Local.GameState(this.Session.CurrentRoom);
            return this.GameState;
        }

        public void Start()
        {
            this.GetRoomsDto();
            this.GetPlayers();
            this.LoadTimer();
        }

        public void StartGame()
        {
            this.client.StartGame();
        }

        public void Stop()
        {
        }

        public void StopTimer()
        {
            this.tmr.Enabled = false;
            this.tmr.Dispose();
            this.tmr = null;
        }

        public void Suspend()
        {
            this.isSuspended = true;
            this.tmr.Enabled = false;
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            this.client.GetMessages();
        }

        List<RankingDto> IMessageProxy.GetRankings(string player, int year, int mont, Guid ConfigId)
        {
            return this.client.GetRankings(player, year, mont, ConfigId);
        }

        public bool UpdateConfiguration(ConfigGameDto cfg)
        {
            return this.client.UpdateConfiguration(cfg);
        }

        public bool UpdateUser(UserDto us)
        {
            return this.client.UpdateUser(us);
        }

        private IGameState _CurrentGameState { get; set; }

        private IGameState _GameState { get; set; }

        private IProxy client
        {
            get
            {
                switch (this.ModeProxy)
                {
                    case 0:
                        return Topping.Core.Proxy.Local.Proxy.Instance;

                    case 1:
                        return ServerProxy.Instance;
                }
                return null;
            }
        }

        public Topping.Core.Logic.Client.GamePermissions GamePermissions { get; set; }

        public IGameState GameState
        {
            get
            {
                if (this._CurrentGameState != null)
                {
                    return this._CurrentGameState;
                }
                return this._GameState;
            }
            set
            {
                this._GameState = value;
            }
        }

        public static MessageProxy Proxy
        {
            get
            {
                if (_messageProxy == null)
                {
                    _messageProxy = new MessageProxy();
                }
                return _messageProxy;
            }
        }

        public ISession Session { get; set; }
    }
}

