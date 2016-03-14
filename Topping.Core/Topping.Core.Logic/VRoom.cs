using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dto;
using System.Runtime.Serialization;
using CMWA.Packager;
using Topping.Core.Logic.Threads;



namespace Topping.Core.Logic
{
    //[Serializable]
    [DataContract]
    public abstract class  VRoom : RoomDto
    {
        protected static object syncRoot = new object();

        public delegate void OnRoomStatusChangeDelegate(VRoom room);
        public event OnRoomStatusChangeDelegate OnRoomStatusChange;


        [XmlIgnore]
        [IgnoreDataMember]
        protected string generatedFileName;



        [XmlIgnore]
        [IgnoreDataMember]
        public FinalRoomDto finalRoomDto  { get; set; }


        #region GAME

        public abstract void SetGeneratedGame(GeneratedGameDto game, string fn);
        public abstract void SetGeneratedGame(GeneratedGameDto game);
             
        public GeneratedGameDto GetGeneratedGame()
        {
            CheckAccess();
            return finalRoomDto.Game;
        }
             
        public PlayerSummaryDto AddPlayedRound(string player, PlayerSummaryDto summary, PlayedGameRoundDto pgr)
        {
            lock (syncRoot)
            {
                finalRoomDto.PlayerSummaries[player].Replace(summary, pgr);
                return finalRoomDto.PlayerSummaries[player];
            }
        }

        public void StartGame(string player)
        {
            lock (syncRoot)
            {
                if (!finalRoomDto.PlayerSummaries.ContainsKey(player))
                {
                    finalRoomDto.PlayerSummaries.Add(player, new PlayerSummaryDto { Player = player });
                }
            }
        }

        public abstract void FinishGame(string player);

        public abstract void FinishGame();
                

        public void SerializeGame()
        {
            string fn = DateTime.Now.ToString("yyyyMMdd-hhmmss-fff") + ".xml";


            if (System.Configuration.ConfigurationManager.AppSettings["IsLocal"] != null
                || System.Configuration.ConfigurationManager.AppSettings["IsLocal"] == "1")
            {
                fn = FileUtility.GetFile("\\GamesXML\\" + fn, LocationType.PersonalFiles);
            }
            else
            {   
              if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SerializedGamesPath"])) 
                {
                    fn = string.Empty;
                }
                else 
                {

                    fn = System.Configuration.ConfigurationManager.AppSettings["SerializedGamesPath"] + "\\" + fn;
                }
            }
            if (string.IsNullOrEmpty(fn)) return;

            new TopMachine.Desktop.Overall.ObjectSerializer<FinalRoomDto>().SerializeToFile(
                GetRoomFinalDto(), fn);

            if (!(System.Configuration.ConfigurationManager.AppSettings["IsLocal"] != null
                    || System.Configuration.ConfigurationManager.AppSettings["IsLocal"] == "1"))
            {
                SendGames.Instance.AddFile(fn);
            }
           
        }

        public SerializableDictionary<string, List<PlayedGameRoundDto>> GetPlayerGameRound() 
        {
            lock (syncRoot)
            {
                SerializableDictionary<string, List<PlayedGameRoundDto>> lst = new SerializableDictionary<string, List<PlayedGameRoundDto>>(); 
                PlayerSummaryDto psd;
                foreach (var item in finalRoomDto.PlayerSummaries)
                {                          
                    psd = item.Value;
                    lst.Add(item.Key, psd.Rounds.ToList()); 
                  
                }

                return lst;
            }


		        
        }

        public List<PlayedGameRoundDto> GetPlayerGame(string player)
        {
            List<PlayedGameRoundDto> lst = null;
            lock (syncRoot)
            {
            if (finalRoomDto.PlayerSummaries.ContainsKey(player))
            {
                                  
                    lst = finalRoomDto.PlayerSummaries[player].Rounds.ToList();
                }
            }

            return lst;

        }
        #endregion

        public void Clean()
        {
            if (generatedFileName != null)
            {
                if (System.IO.File.Exists(generatedFileName))
                {
                    System.IO.File.Delete(generatedFileName);
                }
            }

            MinimumPlayers = 2;
            MaximumPlayers = 40;

            int wt = 120;
            int.TryParse(System.Configuration.ConfigurationManager.AppSettings["WaitingTime"], out wt);
            WaitingTime = wt;

            finalRoomDto = new FinalRoomDto(); 


            TotalPlayers = 0;
            GameStatus = RoomStatus.Empty;
            LastAccess = DateTime.Now;

            ChangeStatus(GameStatus);
        }

        public void CheckAccess()
        {
            LastAccess = DateTime.Now;
        }

        /// <summary>
        ///  Only used for Local !
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        
        public bool AddPlayer(string player)
        {
            lock (syncRoot)
            {
                bool first = TotalPlayers == 0;
                if (finalRoomDto == null) { finalRoomDto = new FinalRoomDto() ; }
                if (!finalRoomDto.Players.ContainsKey(player))
                {
                    finalRoomDto.Players.Add(player, player);    
                    TotalPlayers = finalRoomDto.Players.Count;
                }
            }
            return true;
        }

        public bool AddPlayer(string player, bool updateCounter)
        {
            lock (syncRoot)
            {

                bool first = TotalPlayers == 0;
                if (finalRoomDto.Players == null) { finalRoomDto.Players = new SerializableDictionary<string, string>(); }
                if (!finalRoomDto.Players.ContainsKey(player))
                {
                    finalRoomDto.Players.Add(player, player);
                    if (updateCounter)
                    {
                        TotalPlayers = finalRoomDto.Players.Count;
                    }
                }
            }
            return true;
        }

        public void RemovePlayer(string player)
        {
            CheckAccess();
            lock (syncRoot)
            {
                if (finalRoomDto.Players != null)
                {
                    if (finalRoomDto.Players.ContainsKey(player))
                    {
                        finalRoomDto.Players.Remove(player);

                    }
                    TotalPlayers = finalRoomDto.Players.Count;
                }
            }
        }


        public bool JoinRoom(string player)
        {
            bool first = TotalPlayers == 0;
            CheckAccess();
            LastAccess = DateTime.Now;
            lock (syncRoot)
            {
                if (!finalRoomDto.Players.ContainsKey(player))
                {
                    finalRoomDto.Players.Add(player, player);
                    TotalPlayers = finalRoomDto.Players.Count;
                }
            }

            if (first)
            {
                GameStartTime = DateTime.Now.AddSeconds(WaitingTime);
            }

            return true;
        }

        public void LeaveRoom(string player)
        {
            CheckAccess();

            lock (syncRoot)
            {
                if (finalRoomDto.Players.ContainsKey(player))
                {
                    if (GameStatus >= RoomStatus.Started)
                    {
                        FinishGame(player);
                    }

                    finalRoomDto.Players.Remove(player);
                    TotalPlayers = finalRoomDto.Players.Count;
                }
            }
            if (TotalPlayers == 0)
            {
                Clean();
            }

        }

        public IList<string> GetPlayers()
        {
            CheckAccess();
            return finalRoomDto.Players.Keys.ToList();
        }

        public IList<string> GetActivePlayers()
        {
            CheckAccess();
            return finalRoomDto.PlayerSummaries.Keys.ToList();
        }


        public void ResetRoom()
        {
            CheckAccess();
            finalRoomDto.Players = new SerializableDictionary<string, string>();
        }

        public VRoom()
        {
            Clean();
            GameStatus = RoomStatus.Empty;
        }

        public VRoom(int room)
        {
            GameStatus = RoomStatus.Empty;
            Clean();
            RoomId = room;
        }

        public VRoom(RoomDto r)
        {
            SetDto(r);
        }

        public FinalRoomDto GetRoomFinalDto()
        {
            FinalRoomDto f = new FinalRoomDto();
            f.ConfigId = ConfigId;
            f.GameFileLocation = GameFileLocation;
            f.GameId = GameId;
            f.GameStartTime = DateTime.Now.AddSeconds(WaitingTime);
            f.GameStatus = GameStatus;
            f.LastAccess = LastAccess;
            f.MaximumPlayers = MaximumPlayers;
            f.MinimumPlayers = MinimumPlayers;
            f.Owner = Owner;
            f.RoomId = RoomId;
            f.TotalPlayers = TotalPlayers;
            f.WaitingTime = WaitingTime;
            f.Configuration = Configuration;
            f.Players = finalRoomDto.Players;
            f.Game = GetGeneratedGame();
            f.PlayedRounds = new SerializableDictionary<string, List<PlayedGameRoundDto>>();
            f.PlayerSummaries = finalRoomDto.PlayerSummaries;
            foreach (PlayerSummaryDto s in finalRoomDto.PlayerSummaries.Values.ToList())
            {
                f.PlayedRounds.Add(s.Player, s.Rounds);
            }

            return f;

        }

        public void SetDto(RoomDto r)
        {
            ConfigId = r.ConfigId;
            GameFileLocation = r.GameFileLocation;
            GameId = r.GameId;
            GameStartTime = DateTime.Now.AddSeconds(r.WaitingTime);
            GameStatus = r.GameStatus;
            LastAccess = r.LastAccess;
            MaximumPlayers = r.MaximumPlayers;
            MinimumPlayers = r.MinimumPlayers;
            Owner = r.Owner;
            RoomId = r.RoomId;
            TotalPlayers = r.TotalPlayers;
            WaitingTime = r.WaitingTime;
            Configuration = r.Configuration;
        }

        public RoomDto GetDto()
        {
            DateTime dtm = DateTime.Now;
            return new RoomDto()
            {
                ConfigId = this.ConfigId,
                GameFileLocation = this.GameFileLocation,
                GameId = this.GameId,
                GameStartTime = this.GameStartTime,
                GameStatus = this.GameStatus,
                LastAccess = this.LastAccess,
                MaximumPlayers = this.MaximumPlayers,
                MinimumPlayers = this.MinimumPlayers,
                Owner = this.Owner,
                RoomId = this.RoomId,
                TotalPlayers = this.TotalPlayers,
                WaitingTime = (int)this.GameStartTime.Subtract(dtm).TotalSeconds,
                Configuration = this.Configuration
            };
        }

        public void ChangeStatus(RoomStatus status)
        {
            this.GameStatus = status;
            if (OnRoomStatusChange != null)
            {
                OnRoomStatusChange(this);
            }
        }
        public string GetFinalRound(string UserName) 
        {
            lock (syncRoot)
            {

                string Strmsg = string.Empty;

                if (this.finalRoomDto.PlayerSummaries.ContainsKey(UserName)
                && this.finalRoomDto.PlayerSummaries[UserName].Turn >= this.finalRoomDto.Game.Rounds.Count)
                {
                    PlayerSummaryDto dto = this.finalRoomDto.PlayerSummaries[UserName];
                    if (dto.PointsLost == 0)
                    {
                        Strmsg = " j'ai topé la partie ";
                        Strmsg += " en " + dto.DisplayTime();
                    }
                    else
                    {
                        Strmsg = " j'ai joué la partie ";
                        Strmsg += " en " + dto.DisplayTime();
                        Strmsg += " à " + dto.PointsLost;
                    }
                }
                return Strmsg;
            }  
        }

    }

}
