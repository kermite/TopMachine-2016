using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Topping.Core.Logic;
using Topping.Core.Logic.Threads;
using Topping.Core.Data.SQL ;
using TopMachine.Topping.Dto;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using TopMachine.Desktop.Overall;
using System.Security;
using System.Web.Security;
using Topping.Core.Logic.SQL;
using Topping.Core.Logic.Interface;
using System.ServiceModel.Description;

//using System.ServiceModel.Persistence.SqlPersistenceProviderFactory;


namespace TopMachine.Services.WCF.SQL
{

    public class Topping : IProxy
    {

        public Topping()
        {
        }


        public bool DeleteGameXML(string fn)
        {
            try
            {
                if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SerializedGamesPath"]))
                {

                    string nfn = System.Configuration.ConfigurationManager.AppSettings["SerializedGamesPath"] + "\\" + fn;
                    System.IO.File.Delete(nfn);
                }
            }
            catch (Exception)
            {
                ;
            }

            return true;

        }

        #region Properties

        #endregion

        #region ServerState
        VGameState gameServerState = null;
       // [DurableOperation(CanCreateInstance=true)]
        public VGameState GetServerState()
        {
            return GameServerState.Instance;
        }

        #endregion

        public Player CheckAccess()
        {
            Guid g = Guid.Empty;
            if (OperationContext.Current.IncomingMessageHeaders.FindHeader("UserID", "CustomHeader") > 0)
            {
                g = OperationContext.Current.IncomingMessageHeaders.GetHeader<Guid>("UserID", "CustomHeader");
            }

            Player p = this.GetServerState().AccessPlayer(g);

            if (p == null) { throw( new SecurityException("Invalid Access")); }
            return p;
        }

        public Player CheckAccessAdmin()
        {
            Guid g = Guid.Empty;
            if (OperationContext.Current.IncomingMessageHeaders.FindHeader("UserID", "CustomHeader") > 0)
            {
                g = OperationContext.Current.IncomingMessageHeaders.GetHeader<Guid>("UserID", "CustomHeader");
            }

            Player p = this.GetServerState().AccessPlayer(g);

            if (p == null) { throw (new SecurityException("Invalid Access")); }

            ToppingAccessor ta = new ToppingAccessor();
            Users us_server = ta.GetUserSql(p.Pseudo);
            if (!ta.IsAdminUser(us_server))
           {
               throw (new SecurityException("Invalid Access")); 
           }
            return p;
        }


        public bool CheckVersion(string version)
        {
            return true;
        }



        #region Chatting

        public void ActivateChat()
        {
            try
            {
                Player p = CheckAccess();
                this.GetServerState().Players[p.Pseudo.ToLower()].ChatActivated = true;
            }
            catch (Exception ee)
            {
                
                //NLog.LogManager.GetLogger("wcf").ErrorException("ActivateChat", ee);
                throw;
            }
        }


        public void DeactivateChat()
        {
            try
            {
                Player p = CheckAccess();
                this.GetServerState().Players[p.Pseudo.ToLower()].ChatActivated = false;
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("DectivateChat", ee);
                throw;
            }
        }


        public void SendMessage(MessageDto m)
        {
            MessageProcessor.Instance.AddMessage(m);
        }


        public System.Collections.Generic.List<MessageDto> GetMessages()
        {
            try
            {
                Player pp = CheckAccess();
                if (pp != null)
                {
                    Player p = this.GetServerState().Players[pp.Pseudo.ToLower()];
                    MessagesDto m = new MessagesDto();
                    return p.GetMessages();
                }

                return new List<MessageDto>();
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("GetMessages", ee);
                throw;
            }
        }
        #endregion

        #region Login
        private UserDto ConvertUserServerClient(Users us_server) 
        {
            UserDto us_result = new UserDto();
          
            us_result.UserName = us_server.UserName;
            us_result.FirstName = us_server.FirstName;
            us_result.LastName = us_server.LastName;
            us_result.Email = us_server.Email;
            us_result.LastLoginDate = us_server.LastActivityDate;

			us_result.IsAdmin = us_server.isAdmin;
            
            return us_result;

        }
		private Users ConvertUserClientServer(UserDto us)
		{
			Users us_server;
			us_server = new ToppingAccessor().GetUserSql(us.UserName);

			us_server.UserName = us.UserName;
			us_server.FirstName = us.FirstName;
			us_server.LastName = us.LastName;
			us_server.Email = us.Email;



			return us_server;
		}

        public List<UserDto> GetUsers()
        {
            List<UserDto> lst_result = new List<TopMachine.Topping.Dto.UserDto>(); 
            CheckAccessAdmin();
            List <Users> lst = new ToppingAccessor().GetUsers();

            foreach (var item in lst)
            {
               lst_result.Add(ConvertUserServerClient(item)); 
            }

            return lst_result;
        }
        public bool ChangedPwdUser(UserDto us,string NewPwd)
        {
            CheckAccessAdmin();
            
            return new ToppingAccessor().ChangedPwdUsers(us,NewPwd);
        }

      
        public bool UpdateUser(UserDto us)
        {
            CheckAccessAdmin();

            try
            {
                
                
                return new ToppingAccessor().UpdateUsers(us);
            }
            catch (Exception)
            {

                return false;
            }
          
            
           
        }

        public bool DeleteUser(UserDto us)
        {
			CheckAccessAdmin();
			return new ToppingAccessor().DeleteUsers(us);
		}

        public Guid Login(string userName, string passWord, int version)
        {
            if (version < 6000)
            {
                //NLog.LogManager.GetLogger("wcf").Error("Login - incorrect version");
                throw new Exception("Vous devez installez la dernière version de TopMachine");
            }
                


            Guid p_id;
            try
            {  
               
             
               // SQLiteMembershipProvider mbp = new SQLiteMembershipProvider();

                if (Membership.ValidateUser(userName, passWord))
                {

                    p_id = this.GetServerState().AddPlayer(userName.ToLower());
                    MessageDto m = new MessageDto();
                    m.From = userName;
                    m.MessageType = MessageType.Login;
                    SendMessage(m);

                    return p_id;
                }

                return Guid.Empty;
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("Login", ee);
                throw;
            }
        }

        public void Logoff()
        {
            try
            {
                Player p = CheckAccess();
                if (p != null)
                {
                    LeaveRoom();
                    string UserName = p.Pseudo.ToLower();
                    this.GetServerState().RemovePlayer(p);

                    MessageDto m = new MessageDto();
                    m.From = UserName;
                    m.MessageType = MessageType.Logoff;
                    SendMessage(m);
                }
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("Logoff", ee);
                throw;
            }
        }
        #endregion

        #region ROOMS

        public void SendMessageToRoom(int x, MessageDto m)
        {
            try
            {
                CheckAccess();
                RoomMessageProcessor.Instance.AddMessage(m);
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("SendMessageToRoom", ee);
                throw;
            }
        }
        public RoomDto GetRoom(int roomid)
        {
            try
            {
                CheckAccess();
                RoomDto r = this.GetServerState().getRoom(roomid);
                r.WaitingTime = (int)r.GameStartTime.Subtract(DateTime.Now).TotalSeconds;
                return r;
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("GetRoom", ee);
                throw;
            }

        }

        public List<VRoom> GetRooms()
        {
            try
            {
                CheckAccess();
                return this.GetServerState().Rooms;
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("GetRooms", ee);
                throw;
            }

        }
        public List<RoomDto> GetRoomsDto()
        {
            try
            {
                CheckAccess();
                DateTime dtm = DateTime.Now;
                List<RoomDto> rdto = (from c in this.GetServerState().Rooms
                                      select new RoomDto()
                                      {
                                          ConfigId = c.ConfigId,
                                          GameFileLocation = c.GameFileLocation,
                                          GameId = c.GameId,
                                          GameStartTime = c.GameStartTime,
                                          GameStatus = c.GameStatus,
                                          LastAccess = c.LastAccess,
                                          MaximumPlayers = c.MaximumPlayers,
                                          MinimumPlayers = c.MinimumPlayers,
                                          Owner = c.Owner,
                                          RoomId = c.RoomId,
                                          TotalPlayers = c.TotalPlayers,
                                          WaitingTime = (int)c.GameStartTime.Subtract(dtm).TotalSeconds,
                                          Configuration = c.Configuration
                                      }
                                      ).ToList();
                return rdto;
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("GetRoomsDto", ee);
                throw;
            }
        }
        #endregion

        #region Room

        private void SendRoomMessage(RoomDto r, MessageDto m)
        {
            try
            {
                CheckAccess();
                RoomMessageProcessor.Instance.AddMessage(m);
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("SendRoomMessage", ee);
                throw;
            }
        }

        public void ResetRooms()
        {
            return;
            /*             CheckAccess();
                        this.GetServerState().ResetRooms();*/
        }

        public RoomDto InitRoom(VRoom room, bool generate)
        {
            try
            {
                Player p = CheckAccess();
                string UserName = p.Pseudo.ToLower();
                RoomDto r = this.GetServerState().CreateRoom(room, generate, UserName);

                MessageDto m = new MessageDto();
                m.From = "System";
                m.MessageType = MessageType.RoomCreate;
                m.Room = r;
                m.Text = UserName;
                SendMessage(m);
                return r;
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("InitRoom", ee);
                throw;
            }
        }


        public RoomDto JoinRoom(int roomId)
        {
            try
            {
                Player p = CheckAccess();
                string UserName = p.Pseudo.ToLower();

                CheckAccess();

                if (roomId != p.CurrentRoom)
                {
                    LeaveAnyRoom();
                }

                VRoom r = this.GetServerState().Rooms[roomId];
                bool ok = r.JoinRoom(UserName);
                //  this.GetServerState().Players[UserName].CurrentRoom = r.RoomId ;
                p.CurrentRoom = r.RoomId;

                MessageDto m = null;
                if (r.GameStatus == RoomStatus.Started)
                {
                    GetListPlayedGameRound(); 

                    m = new MessageDto();                                    
                    string Strmsg = " je suis entrain d'observer ";

                    m.From = UserName ;
                    m.MessageType = MessageType.Public;
                    m.Text = Strmsg;
                    m.Room = r.GetDto();
                    m.ToRoom = r.RoomId;
                    SendRoomMessage(r, m);

                    

                }

                m = new MessageDto();
                if (ok)
                {
                    m.From = UserName;
                    m.MessageType = MessageType.RoomEnter;
                    m.Text = UserName;
                    m.Room = r.GetDto();
                    m.ToRoom = r.RoomId;

                }
                else
                {
                    m.From = UserName;
                    m.MessageType = MessageType.Error;
                    m.Text = UserName;
                }
                SendMessage(m);

                return r.GetDto();
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("JoinRoom", ee);
                throw;
            }
        }

        public void LeaveAnyRoom()
        {
            LeaveRoom();
        }

        public RoomDto LeaveRoom()
        {
            try
            {
                Player p = CheckAccess();
                if (p == null)
                {
                    return null;
                }
                if (p.CurrentRoom != -1)
                {
                    VRoom r = this.GetServerState().Rooms[p.CurrentRoom];

                    MessageDto m = new MessageDto();
                    m.From = p.Pseudo.ToLower();
                    m.MessageType = MessageType.Public;
                    m.Text =  " j'ai quitté le salon ";
                    m.Room = r.GetDto();
                    m.ToRoom = r.RoomId;
                    SendRoomMessage(r, m);


                    r.LeaveRoom(p.Pseudo.ToLower());

                    p.CurrentRoom = -1;
                    m = new MessageDto();
                    m.From = p.Pseudo.ToLower();
                    m.MessageType = MessageType.RoomLeave;
                    m.Text = p.Pseudo.ToLower();
                    m.Room = r.GetDto();
                    m.ToRoom = r.RoomId;
                    SendMessage(m);



                    return r.GetDto();
                }

                return null;

            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("LeaveRoom", ee);
                throw;
            }
        }

        public string[] ListPlayers()
        {
            try
            {
                List<string> lstPlayers = this.GetServerState().Pseudos;
                if (lstPlayers != null) return lstPlayers.ToArray();
                return null;
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("ListPlayers", ee);
                throw;
            }
        }

        public string[] ListRoomPlayers(int roomid)
        {
            try
            {
                Player p = CheckAccess();

                if (roomid > -1)
                {
                    VRoom r = this.GetServerState().Rooms[roomid];
                    return r.GetPlayers().ToArray();
                }
                return null;
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("ListRoomPlayers", ee);
                throw;
            }
        }


        public string[] ListRoomActivePlayers(int roomid)
        {
            try
            {
                Player p = CheckAccess();

                if (roomid > -1)
                {
                    VRoom r = this.GetServerState().Rooms[roomid];
                    return r.GetActivePlayers().ToArray();
                }
                return null;
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("ListRoomPlayers", ee);
                throw;
            }
        }

        #endregion

        #region Game

        private void GetListPlayedGameRound() 
        {
            int Roomid;
            Player p = CheckAccess();
            SerializableDictionary<string, List<PlayedGameRoundDto>> lst; 
            string UserName = p.Pseudo.ToLower();
            Roomid = this.GetServerState().Players[UserName].CurrentRoom;
            if (Roomid > -1)
            {
                VRoom r = this.GetServerState().Rooms[Roomid];
                lst = r.GetPlayerGameRound();

                MessageDto m = new MessageDto();
                m.MessageType = MessageType.Private;
                m.MessageModule = MessageModule.Game;
                m.To = UserName;
                m.From = UserName;
                m.XmlObjectType = lst.GetType().FullName;
                m.XmlObject = new ObjectSerializer<SerializableDictionary<string, List<PlayedGameRoundDto>>>().Serialize(lst);
                SendMessage(m);

            
            
            }
        }

        public void GetListPlayedRound(string playerSearch)
        {
            try
            {
                int Roomid;
                List<PlayedGameRoundDto> lstPlayedRoundDto;
                Player p = CheckAccess();
                string UserName = p.Pseudo.ToLower();
                Roomid = this.GetServerState().Players[UserName].CurrentRoom;
                if (Roomid > -1)
                {
                    VRoom r = this.GetServerState().Rooms[Roomid];
                    lstPlayedRoundDto = r.GetPlayerGame(playerSearch);
                    MessageDto m = new MessageDto();
                    m.MessageType = MessageType.Private;
                    m.MessageModule = MessageModule.Game;
                    m.To = UserName;
                    m.From = playerSearch;
                    m.XmlObjectType = lstPlayedRoundDto.GetType().FullName;
                    m.XmlObject = new ObjectSerializer<List<PlayedGameRoundDto>>().Serialize(lstPlayedRoundDto);
                    SendMessage(m);

                }
            }
            catch (Exception)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("GetListPlayedRound", ee);
                throw;
            }

        }

        public GeneratedGameDto GetGame()
        {
            try
            {
                Player p = CheckAccess();
                string UserName = p.Pseudo.ToLower();
                int Roomid;
                Roomid = this.GetServerState().Players[UserName].CurrentRoom;
                if (Roomid > -1)
                {
                    VRoom r = this.GetServerState().Rooms[Roomid];

                    /* DELETED ON 20/07 -> EMPTY OBJECT ???? ANY NEED OF THIS ?*/
                    /*
                     * MessageDto m = new MessageDto();
                        m.From = "System";
                        m.MessageType = MessageType.GameSummaryUpdated ;
                        m.Text = UserName;
                        m.Room = r.GetDto();
                        m.ToRoom = Roomid;
                        SendRoomMessage(r, m);*/

                    return this.GetServerState().Rooms[Roomid].GetGeneratedGame();
                }
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("GetGame", ee);
                throw;
            }
            return null;
        }

        public void SetGame(GeneratedGameDto game)
        {
            try
            {
                Player p = CheckAccess();
                string UserName = p.Pseudo.ToLower();
                int Roomid;
                Roomid = this.GetServerState().Players[UserName].CurrentRoom;
                if (Roomid > -1)
                {
                    VRoom r = this.GetServerState().Rooms[Roomid];
                    if (r.GameStatus == RoomStatus.Finished ||  r.GetGeneratedGame() == null)
                    {
                        r.SetGeneratedGame(game);
                        r.ChangeStatus(RoomStatus.WaitingForStart);
                    }
                }
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("SetGame", ee);
                throw;
            }
        }


        public void AddPlayedRound(PlayerSummaryDto summary, PlayedGameRoundDto pgr)
        {
            try
            {
                Player p = CheckAccess();
                string UserName = p.Pseudo.ToLower();

                int Roomid;
                Roomid = this.GetServerState().Players[UserName].CurrentRoom;
                if (Roomid > -1)
                {
                    VRoom r = this.GetServerState().Rooms[Roomid];
                    summary = r.AddPlayedRound(UserName, summary, pgr);
                    MessageDto m = new MessageDto();
                    m.From = "System";
                    m.MessageType = MessageType.GameSummaryUpdated;
                    m.Text = UserName;
                    m.XmlObjectType = summary.GetType().FullName;
                    m.XmlObject = new ObjectSerializer<PlayerSummaryDto>().Serialize(summary);
                    m.Room = r.GetDto();
                    m.ToRoom = Roomid;
                    SendRoomMessage(r, m);


                    m = new MessageDto();
                    m.From = "System";
                    m.MessageType = MessageType.GameSummaryRoundUpdated;
                    m.Text = UserName;
                    m.XmlObjectType = pgr.GetType().FullName;
                    m.XmlObject = new ObjectSerializer<PlayedGameRoundDto>().Serialize(pgr);
                    m.Room = r.GetDto();
                    m.ToRoom = Roomid;
                    SendRoomMessage(r, m);

                }
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("AddPlayedRound", ee);
                throw;
            }
        }

        public void FinishGame()
        {
            try
            {
                Player p = CheckAccess();
                if (p == null)
                {
                    return;
                }
                string UserName = p.Pseudo.ToLower();

                int Roomid;
                
                Roomid = this.GetServerState().Players[UserName].CurrentRoom;
                if (Roomid > -1)
                {
                    string Strmsg;

                    VRoom r = this.GetServerState().Rooms[Roomid];
                    r.FinishGame(UserName);
                    Strmsg = r.GetFinalRound(UserName); 

                    //  connaitre si le joueur a fini la partie completement 
                    if (Strmsg != string.Empty  )
                    {
                        MessageDto m = new MessageDto();
                        m.From = UserName ;
                        m.MessageType = MessageType.Public;
                        m.Text = Strmsg;
                        m.Room = r.GetDto();
                        m.ToRoom = Roomid;
                        SendRoomMessage(r, m);
                    }
                }
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("FinishGame", ee);
                throw;
            }
        }
        public FinalRoomDto GetFinalRoom(int RoomId)
        {
            try
            {               
                VRoom r = this.GetServerState().Rooms[RoomId];
                return r.finalRoomDto;

            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("FinalRoom", ee);
                throw;
            }

        }

        public void StartGame()
        {
            try
            {
                Player p = CheckAccess();
                string UserName = p.Pseudo.ToLower();
                int Roomid;
                Roomid = this.GetServerState().Players[UserName].CurrentRoom;
                if (Roomid > -1)
                {
                    VRoom r = this.GetServerState().Rooms[Roomid];
                    r.StartGame(UserName);
                }
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("StartGame", ee);
                throw;
            }
        }
        #endregion

        #region Configuration
        public List<ConfigGameDto> GetConfigurations()
        {
            CheckAccess();
            return new ToppingAccessor().GetConfigurations();
        }

        public bool UpdateConfiguration(ConfigGameDto cfg)
        {
            CheckAccessAdmin();
            return new ToppingAccessor().UpdateConfiguration(cfg);
        }

        public bool DeleteConfiguration(ConfigGameDto cfg)
        {
            CheckAccessAdmin();
            return new ToppingAccessor().DeleteConfiguration(cfg);
        }

        #endregion

        #region IDisposable Members

       // [DurableOperation(CompletesInstance=true)]
        public void Dispose()
        {
            RoomMessageProcessor.Instance.StopService();
            MessageProcessor.Instance.StopService();
        }

        #endregion

        #region IProxy Members


        public List<RankingDto> GetRankings(string player, int year, int mont, Guid ConfigId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IProxy Members


        public List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
