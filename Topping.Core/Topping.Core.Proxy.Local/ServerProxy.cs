namespace Topping.Core.Proxy.Local
{
	using NLog;
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.Windows.Forms;
	using TopMachine.Desktop.Overall;
	using TopMachine.Topping.Dto;
	using Topping.Core.Logic;
	using Topping.Core.Logic.Interface;
	using Topping.Core.Logic.SQL;
	using Topping.Core.Logic.Threads;
	using Topping.Core.Proxy.Local.Client;

	internal class ServerProxy : ClientBase<IProxy>, IProxy, IDisposable
    {
        private int error;
        private VGameState gameServerState;
        public Guid guidPlayer;
        private static volatile ServerProxy instance;
        private Guid MemoryCheckId;
        private static object syncRoot = new object();

        public ServerProxy(string endpointConfigurationName) : base(endpointConfigurationName, TopMachine.Desktop.Configuration.Configuration.Instance.Urls.ToppingService)
        {
            this.guidPlayer = Guid.Empty;
            this.error = 0;
            this.gameServerState = null;
            this.MemoryCheckId = MemoryCheck.Register(this, base.GetType().FullName);
            if (RoomMessageProcessor.Instance != null)
            {
                RoomMessageProcessor.Instance.StartService();
            }
            if (MessageProcessor.Instance != null)
            {
                MessageProcessor.Instance.StartService();
            }
           
        }

        public void ActivateChat()
        {
            this.Invoke("ActivateChat", null);
        }

        private static void AddCustomHeaderUserInformation(OperationContextScope scope, Guid userId)
        {
            MessageHeader header = MessageHeader.CreateHeader("UserID", "CustomHeader", userId);
            if (OperationContext.Current.OutgoingMessageHeaders.Count == 0)
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
            }
        }

        public void AddCustomHeaderUserInformationFromInstance(Guid userId)
        {
            Instance.guidPlayer = userId;
            AddCustomHeaderUserInformation(new OperationContextScope(base.InnerChannel), userId);
        }

        public void AddPlayedRound(PlayerSummaryDto summary, PlayedGameRoundDto pgr)
        {
            this.Invoke("AddPlayedRound", new object[] { summary, pgr });
        }

        public bool ChangedPwdUser(UserDto us, string NewPwd)
        {
            return (bool) this.Invoke("ChangedPwdUser", new object[] { us, NewPwd });
        }

        public bool CheckVersion(string version)
        {
            return (bool) this.Invoke("CheckVersion", new object[] { version });
        }

        public void DeactivateChat()
        {
            this.Invoke("DeactivateChat", null);
        }

        public bool DeleteConfiguration(ConfigGameDto cfg)
        {
            return (bool) this.Invoke("DeleteConfiguration", new object[] { cfg });
        }

        public bool DeleteGameXML(string fn)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(UserDto us)
        {
            return (bool) this.Invoke("DeleteUser", new object[] { us });
        }

        public void Dispose()
        {
            MemoryCheck.Unregister(this.MemoryCheckId);
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
            this.Invoke("FinishGame", null);
        }

        public List<ConfigGameDto> GetConfigurations()
        {
            return (List<ConfigGameDto>) this.Invoke("GetConfigurations", null);
        }

        public FinalRoomDto GetFinalRoom(int RoomId)
        {
            return (FinalRoomDto) this.Invoke("GetFinalRoom", new object[] { RoomId });
        }

        public GeneratedGameDto GetGame()
        {
            return (GeneratedGameDto) this.Invoke("GetGame", null);
        }

        public List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId)
        {
            return (List<GamesDetailDto>) this.Invoke("GetGamesDetail", new object[] { player, year, mont, configId });
        }

        public void GetListPlayedRound(string playerSearch)
        {
            this.Invoke("GetListPlayedRound", new object[] { playerSearch });
        }

        public List<MessageDto> GetMessages()
        {
            this.InvokeGetMessages("GetMessages", null);
            return null;
        }

        public List<RankingDto> GetRankings(string player, int year, int mont, Guid ConfigId)
        {
            throw new NotImplementedException();
        }

        public RoomDto GetRoom(int roomid)
        {
            return (RoomDto) this.Invoke("GetRoom", new object[] { roomid });
        }

        public List<RoomDto> GetRoomsDto()
        {
            return (List<RoomDto>) this.Invoke("GetRoomsDto", null);
        }

        public VGameState GetServerState()
        {
            if (this.gameServerState == null)
            {
                this.gameServerState = GameServerState.Instance;
            }
            return this.gameServerState;
        }

        public List<UserDto> GetUsers()
        {
            return (List<UserDto>) this.Invoke("GetUsers", null);
        }

        public RoomDto InitRoom(VRoom room, bool generate)
        {
            //return (RoomDto) this.Invoke("InitRoom", new object[] { room, generate });
           //var x =  new ObjectSerializer<Room>().Serialize((Room)room);
            return this.Channel.InitRoom(room, generate);
        }

        public object Invoke(string methodName, params object[] parameters)
        {
            object obj3;
            object obj4;
            try
            {
                lock ((obj4 = syncRoot))
                {
                    if (OperationContext.Current == null)
                    {
                        this.AddCustomHeaderUserInformationFromInstance(this.guidPlayer);
                    }
                    MethodInfo method = base.Channel.GetType().GetMethod(methodName);
                    object obj2 = (method == null) ? null : method.Invoke(base.Channel, parameters);
                    this.error = 0;
                    obj3 = obj2;
                }
            }
            catch (Exception exception)
            {
                lock ((obj4 = syncRoot))
                {
                    LogManager.GetLogger("wcf").ErrorException(methodName, exception);
                    this.error++;
                    if (this.error > 1)
                    {
                        Application.ExitThread();
                        Application.Exit();
                        Environment.Exit(0);
                        return null;
                    }
                    if (MessageBox.Show("Une erreur s'est produite, r\x00e9essayez ou annulez", "Erreur de r\x00e9seau : " + methodName, MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    {
                        Application.Exit();
                        Environment.Exit(0);
                        return null;
                    }
                    instance.Dispose();
                    instance = null;
                    instance = new ServerProxy("ToppingProxy");
                    if (OperationContext.Current == null)
                    {
                        this.AddCustomHeaderUserInformationFromInstance(this.guidPlayer);
                    }
                }
                obj3 = instance.Invoke(methodName, parameters);
            }
            return obj3;
        }

        public void InvokeGetMessages(string methodName, params object[] parameters)
        {
            object obj2;
            try
            {
                lock ((obj2 = syncRoot))
                {
                    if (OperationContext.Current == null)
                    {
                        this.AddCustomHeaderUserInformationFromInstance(this.guidPlayer);
                    }
                    List<MessageDto> messages = base.Channel.GetType().GetMethod(methodName).Invoke(base.Channel, parameters) as List<MessageDto>;
                    MessageProxy.Proxy.QueueMessages(messages);
                    this.error = 0;
                }
            }
            catch (Exception exception)
            {
                lock ((obj2 = syncRoot))
                {
                    LogManager.GetLogger("wcf").ErrorException(methodName, exception);
                    this.error++;
                    if (this.error > 1)
                    {
                        Application.ExitThread();
                        Application.Exit();
                        Environment.Exit(0);
                        return;
                    }
                    if (MessageBox.Show("Une erreur s'est produite, r\x00e9essayez ou annulez", "Erreur de r\x00e9seau : " + methodName, MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    {
                        Application.Exit();
                        Environment.Exit(0);
                    }
                    instance.Dispose();
                    instance = null;
                    instance = new ServerProxy("ToppingProxy");
                    if (OperationContext.Current == null)
                    {
                        this.AddCustomHeaderUserInformationFromInstance(this.guidPlayer);
                    }
                }
                instance.InvokeGetMessages(methodName, parameters);
            }
        }

        public RoomDto JoinRoom(int roomId)
        {
            return (RoomDto) this.Invoke("JoinRoom", new object[] { roomId });
        }

        public void LeaveAnyRoom()
        {
            this.Invoke("LeaveAnyRoom", null);
        }

        public RoomDto LeaveRoom()
        {
            return (RoomDto) this.Invoke("LeaveRoom", null);
        }

        public string[] ListPlayers()
        {
            return (string[]) this.Invoke("ListPlayers", null);
        }

        public string[] ListRoomActivePlayers(int roomid)
        {
            return (string[]) this.Invoke("ListRoomActivePlayers", new object[] { roomid });
        }

        public string[] ListRoomPlayers(int roomid)
        {
            return (string[]) this.Invoke("ListRoomPlayers", new object[] { roomid });
           
        }

        public Guid Login(string userName, string passWord, int version)
        {
            Guid userId = (Guid) this.Invoke("Login", new object[] { userName, passWord, version });
            Instance.AddCustomHeaderUserInformationFromInstance(userId);
            return userId;
        }

        public void Logoff()
        {
            this.Invoke("Logoff", null);
        }

        public void ResetRooms()
        {
            this.Invoke("ResetRooms", null);
        }

        public void SendMessage(MessageDto m)
        {
            this.Invoke("SendMessage", new object[] { m });
        }

        public void SendMessageToRoom(int x, MessageDto m)
        {
            this.Invoke("SendMessageToRoom", new object[] { x, m });
        }

        public void SetGame(GeneratedGameDto game)
        {
            this.Invoke("SetGame", new object[] { game });
        }

        public void StartGame()
        {
            this.Invoke("StartGame", null);
        }

        public bool UpdateConfiguration(ConfigGameDto cfg)
        {
            return (bool) this.Invoke("UpdateConfiguration", new object[] { cfg });
        }

        public bool UpdateUser(UserDto us)
        {
            return (bool) this.Invoke("UpdateUser", new object[] { us });
        }

        public static ServerProxy Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if ((instance == null) && (instance == null))
                    {
                        instance = new ServerProxy("ToppingProxy");
                    }
                }
                return instance;
            }
        }

        public string UserName { get; set; }
    }
}

