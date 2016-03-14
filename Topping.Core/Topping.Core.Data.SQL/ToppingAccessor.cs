
using CMWA.Packager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TopMachine.Desktop.Overall;
using TopMachine.Topping.Dto;
using Topping.Core.Data.SQL;
using System.Web.Security;

 

namespace Topping.Core.Data.SQL
{


    public class ToppingAccessor : IDisposable
    {
      
        
        public TopMachineDBEntities _context;

        public ToppingAccessor()
        {
           // string connectionString = "";
            this._context = null;
            //string key = "TopMachineDBEntities";
            //if (ConfigurationManager.ConnectionStrings[key] != null)
            //{
            //    connectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            //}
            //else 
            //{
            //    throw new Exception("ConnectionString Not Exist");
            //}
          
            this._context = new TopMachineDBEntities();
        }

               
        public bool AddConfiguration(Topping.Core.Data.SQL.GameConfigs cfg)
        {
            try
            {
                this._context.GameConfigs.Add(cfg);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddGame(GeneratedGameDto gameDto)
        {
            if (gameDto.Config.HistoryGame)
            {
                Games game = new Games {
                    Date = gameDto.DateTime,
                    GameConfigId = gameDto.Config.Id,
                    GameId = gameDto.Id,
                    Status = (int) gameDto.Status,
                    Name = gameDto.Name
                };
                game.GameXml = new ObjectSerializer<GeneratedGameDto>().Serialize(gameDto);
                this._context.Games.Add(game);
                _context.SaveChanges();
            }
        }

        public bool ChangedPwdUsers(UserDto us, string newPWD)
        {
            try
            {				
				var CurrentUs = GetUserSql(us.UserName);
				CurrentUs.Pwd = Password.GetHash(newPWD);
				_context.SaveChanges();
				return true;

            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public bool DeleteConfiguration(ConfigGameDto cfg)
        {
            try
            {
                GameConfigs config = null;
                config = this._context.GameConfigs.Where<GameConfigs>(p => (p.Id == cfg.Id)).FirstOrDefault<GameConfigs>();
                if (config != null)
                {
                    this._context.GameConfigs.Remove(config);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteUsers(UserDto us)
        {
            try
            {
                if (us != null)
                {
					var CurrentUs = GetUserSql(us.UserName);
					if (CurrentUs != null)
					{
						_context.Users.Remove(CurrentUs);
						_context.SaveChanges();
					}

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            if (this._context != null)
            {
                this._context.Dispose();
                this._context = null;
            }
        }

        public void FinishGame(string player, GeneratedGameDto game, PlayerSummaryDto sum, bool doDetail, bool doHistory, bool doStats)
        {
            
            if (sum.Turn != 0)
            {
                var CurrentUs = GetUserSql(player);
                if (CurrentUs != null)
                {
                    DateTime n = DateTime.Now;
                    GamesDetail detail = new GamesDetail();
                    if (doDetail)
                    {
                        detail.DetailXml = new ObjectSerializer<List<PlayedGameRoundDto>>().Serialize(sum.Rounds);
                    }
                    int num = game.Rounds.Sum<GameRoundDto>((Func<GameRoundDto, int>) (p => p.Points));
                    detail.Date = n;
                    detail.GameId = game.Id;
                    detail.Negative = new int?((int) sum.PointsLost);
                    detail.Percentage = new double?(((double) sum.Total) / ((double) num));
                    detail.Time = new int?((int) sum.Time);
                    detail.Top = new int?((int) sum.Total);
                    detail.TopLost = new int?((int) (game.Rounds.Count - sum.TotalTop));
                    detail.UserId = CurrentUs.UserId;
                    detail.GameConfigId = game.Config.Id;
                    sum.Percentage = (float) detail.Percentage.Value;
                    if (doHistory)
                    {
                        this._context.GamesDetail.Add(detail);
                    }
                    if (doStats)
                    {
                        GameRankings ranking = this._context.GameRankings.Where<GameRankings>(p => ((((p.Year == n.Year) && (p.Month == n.Month)) && (p.ConfigGameId == game.Config.Id)) && (p.Playerid == (CurrentUs.UserId)))).FirstOrDefault();
                        if (ranking == null)
                        {
                            ranking = new GameRankings {
                                Year = n.Year,
                                Month = n.Month,
                                ConfigGameId = game.Config.Id,
                                Playerid = CurrentUs.UserId
                            };
                        }
                        ranking.NbParties++;
                        ranking.LostTops += (int) detail.TopLost.GetValueOrDefault(0);
                        ranking.Time += (int) detail.Time.GetValueOrDefault(0);
                        if (detail.Percentage == 1.0)
                        {
                            ranking.TotalTops++;
                        }
                        ranking.PlayerTop += (int) detail.Top.Value;
                        ranking.GameTop += num;
                        ranking.Percentage = ((float) ranking.PlayerTop) / ((float) ranking.GameTop);
                        if (ranking.BestScore <= detail.Percentage)
                        {
                            ranking.BestScore = detail.Percentage.GetValueOrDefault();
                            if ((ranking.BestTime == 0) || (ranking.BestTime > detail.Time.GetValueOrDefault()))
                            {
                                ranking.BestTime = (int) detail.Time.GetValueOrDefault();
                            }
                        }
                        this._context.GameRankings.Add(ranking);
                       
                    }
                }
                _context.SaveChanges();
            }
        }

        public List<ConfigGameDto> GetConfigurations()
        {
            List<ConfigGameDto> list = new List<ConfigGameDto>();
            var query = this._context.GameConfigs.Select<GameConfigs, GameConfigs>(p => p);
            foreach (GameConfigs config in query)
            {
                ConfigGameDto item = new ObjectSerializer<ConfigGameDto>().Deserialize(config.XmlConfig);
                list.Add(item);
            }
            return list;
        }

     
        public List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId)
        {
			var CurrentUs = GetUserSql(player);
			List<GamesDetail> list = null;
            DateTime start = new DateTime(year, mont, 1);
            DateTime end = start.AddMonths(1);
            list = this._context.GamesDetail.Where<GamesDetail>(p => ((((p.Date > start) && (p.Date < end)) && (p.UserId == CurrentUs.UserId)) && (p.GameConfigId == configId))).ToList<GamesDetail>();
            List<GamesDetailDto> list2 = new List<GamesDetailDto>();
            Console.Write(list.Count);
            foreach (GamesDetail detail in list)
            {
                long? nullable3;
                GamesDetailDto item = new GamesDetailDto {
                    Date = detail.Date,
                    Percentage = (float) detail.Percentage.GetValueOrDefault(0.0),
                    UserName = player,
                    Negative = (int) detail.Negative.GetValueOrDefault(0),
                    Total = (int) detail.Total.GetValueOrDefault(0),
                    Top = (int) detail.Top.GetValueOrDefault(0),
                    GameId = detail.GameId,
                    TopLost = (int) detail.TopLost.GetValueOrDefault(0),
                    ConfigId = (Guid)detail.GameConfigId
                };
                long? time = detail.Time;
                time = detail.Time;
                item.Time = (time.HasValue ? new long?(time.GetValueOrDefault() / 60L) : (nullable3 = null)) + ":" + (time.HasValue ? new long?(time.GetValueOrDefault() % 60L) : (nullable3 = null));
                list2.Add(item);
            }
            return list2;
        }

        //public IObjectContainer GetObjectContainer()
        //{
        //    if (this._context == null)
        //    {
        //        this._context = Db4oEmbedded.OpenFile(this.dbFile);
        //    }
        //    return this._context;
        //}

        public List<RankingDto> GetRankings(string player, int year, int mont, Guid configId)
        {
            var CurrentUs = GetUserSql(player);
            List<GameRankings> list = null;
            if ((player.Length > 0) && (configId == Guid.Empty))
            {
                list = this._context.GameRankings.Where<GameRankings>(p => (((p.Playerid == CurrentUs.UserId) && (p.Year == year)) && (p.Month == mont))).ToList<GameRankings>();
            }
            if ((player.Length > 0) && (configId != Guid.Empty))
            {
                list = this._context.GameRankings.Where<GameRankings>(p => ((p.Playerid == CurrentUs.UserId) && (p.ConfigGameId == configId))).ToList<GameRankings>();
            }
            List<RankingDto> list2 = new List<RankingDto>();
            Console.Write(list.Count);
            foreach (GameRankings ranking in list)
            {
                RankingDto item = new RankingDto {
                    BestScore = ranking.BestScore * 100.0,
                    BestTime = (ranking.BestTime / 60) + ":" + (ranking.BestTime % 60),
                    Config = ranking.ConfigGameId,
                    NbGames = ranking.NbParties,
                    NbLostTops = ((float) ranking.LostTops) / ((float) ranking.NbParties),
                    NbTops = ranking.TotalTops,
                    Percentage = (((float) ranking.PlayerTop) / ((float) ranking.GameTop)) * 100f,
                    Period = ranking.Month + "/" + ranking.Year,
                    Player = player
                };
                int num = ranking.Time / ranking.NbParties;
                item.Time = (num / 60) + ":" + (num % 60);
                list2.Add(item);
            }
            return list2;
        }
		      
        public Users GetUserSql(string UserName)
        {
           var us = _context.Users.Where(x=>x.LoweredUserName == UserName.ToLower());

           if (us != null) 
           {
               return us.FirstOrDefault();
           }

           return null;
        }

        public List<Users> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool IsAdminUser(Users us)
        {
			return us.isAdmin;
        }

		private bool CreateUser(UserDto us, string passWord)
		{
			try
			{
				Users us_server = new Users();
				us_server.UserId = Guid.NewGuid();
				us_server.UserName = us.UserName;
				us_server.Pwd = Password.GetHash(passWord);
				us_server.isAdmin = us.IsAdmin;
				us_server.LastActivityDate = DateTime.Now;
				us_server.FirstName = us.FirstName;
				us_server.LastName = us.LastName;
				us_server.LoweredUserName = us.UserName.ToLower();
				_context.Users.Add(us_server);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{

				return false;
			}

		}

		private bool CreateUser(string userName, string passWord)
		{
			try
			{
				Users us = new Users();
				us.UserId = Guid.NewGuid();
				us.UserName = userName;
				us.LoweredUserName = userName.ToLower();
				us.Pwd = Password.GetHash(passWord);
				us.isAdmin = true;
				us.LastActivityDate = DateTime.Now;
				_context.Users.Add(us);
				_context.SaveChanges();
				return true;
			}
			catch (Exception)
			{

				return false;
			}
			
		}

        public bool Login(string userName, string passWord)
        {
			if (GetUsers().Count == 0)
			{
				CreateUser(userName, passWord);

			}
			var currentUs = GetUserSql(userName);
			if (currentUs != null)
			{
				return Password.VerifyHash(passWord, currentUs.Pwd);
			}
			return false;
					

        }

        public bool UpdateConfiguration(ConfigGameDto cfg)
        {
            try
            {
                GameConfigs config = null;
                if (cfg.Id == Guid.Empty)
                {
                    config = new GameConfigs {
                        Id = Guid.NewGuid()
                    };
                    cfg.Id = config.Id;
                   this._context.GameConfigs.Add(config);
                }
                else
                {
                    config = this._context.GameConfigs.Where<GameConfigs>(p => (p.Id == cfg.Id)).FirstOrDefault<GameConfigs>();
                }
                config.XmlConfig = new ObjectSerializer<ConfigGameDto>().Serialize(cfg);
              
                _context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private Users ConvertUserClientServer(UserDto us)
        {
            Users us_server;
            us_server = GetUserSql(us.UserName);
			if (us_server != null)
			{
				us_server.UserName = us.UserName;
				us_server.FirstName = us.FirstName;
				us_server.LastName = us.LastName;
				us_server.Email = us.Email;
				us_server.LoweredUserName = us.UserName.ToLower();
				us_server.isAdmin = us.IsAdmin;
			}
			else
			{
				CreateUser(us, "TopMachine");
			}
            return us_server;
        }

        public bool UpdateUsers(UserDto us)
        {
          Users user =  ConvertUserClientServer(us);
           
            _context.SaveChanges();
                        
                        
         
            return true;
        }



      
    }
}

