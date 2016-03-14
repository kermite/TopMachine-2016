
using System.Configuration.Provider;
using Db4objects.Db4o.Config.Attributes;
using db4oProviders.Common;
using CMWA.Packager;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using db4oProviders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TopMachine.Desktop.Overall;
using TopMachine.Topping.Dto;
using Topping.Web.Security.db4o;
 

namespace Topping.Core.Data.Db4o
{


    public class ToppingAccessor : IDisposable
    {
        private string _ApplicationName;
        private string _roleName;
        public string dbFile;
        public IObjectContainer entity;

        public ToppingAccessor()
        {
            string connectionString;
            this.dbFile = "";
            this.entity = null;
            this._ApplicationName = "TOPMACHINE";
            this._roleName = "Admin";
            string str2 = "Db4o";
            if (ConfigurationManager.ConnectionStrings[str2] != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings[str2].ConnectionString;
            }
            else
            {
                connectionString = PackageManager.Instance.Project.GetFileName(@"TopMachineData\Databases\Topping4dbo");
            }
            this.entity = new SafeDB(connectionString);
        }

        public ToppingAccessor(string path)
        {
            this.dbFile = "";
            this.entity = null;
            this._ApplicationName = "TOPMACHINE";
            this._roleName = "Admin";
            this.entity = new SafeDB(path);
        }

        public bool AddConfiguration(GameConfig cfg)
        {
            try
            {
                this.entity.Store(cfg);
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
                Game game = new Game {
                    Date = gameDto.DateTime,
                    GameConfigId = gameDto.Config.Id,
                    GameId = gameDto.Id,
                    Status = (long) gameDto.Status,
                    Name = gameDto.Name
                };
                game.GameXml = new ObjectSerializer<GeneratedGameDto>().Serialize(gameDto);
                this.entity.Store(game);
            }
        }

        public bool ChangedPwdUsers(db4oProviders.User us, string newPWD)
        {
            try
            {
                us.Password = db4oMembershipProvider.GetEncryption(newPWD);
                this.entity.Store(us);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteConfiguration(ConfigGameDto cfg)
        {
            try
            {
                GameConfig config = null;
                config = this.entity.Cast<GameConfig>().Where<GameConfig>(p => (p.Id == cfg.Id)).FirstOrDefault<GameConfig>();
                if (config != null)
                {
                    this.entity.Delete(config);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteUsers(db4oProviders.User us)
        {
            try
            {
                if (us != null)
                {
                    this.entity.Delete(us);
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
            if (this.entity != null)
            {               
                this.entity.Dispose();
                this.entity = null;
            }
        }

        public void FinishGame(string player, GeneratedGameDto game, PlayerSummaryDto sum, bool doDetail, bool doHistory, bool doStats)
        {
            db4oProviders.User vid;
            if (sum.Turn != 0)
            {
                vid = this.entity.Cast<db4oProviders.User>().Where<db4oProviders.User>(p => (p.UserName == player)).FirstOrDefault<db4oProviders.User>();
                if (vid != null)
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
                    detail.Negative = new long?((long) sum.PointsLost);
                    detail.Percentage = new double?(((double) sum.Total) / ((double) num));
                    detail.Time = new long?((long) sum.Time);
                    detail.Top = new long?((long) sum.Total);
                    detail.TopLost = new long?((long) (game.Rounds.Count - sum.TotalTop));
                    detail.UserId = vid.PKID;
                    detail.GameConfigId = game.Config.Id;
                    sum.Percentage = (float) detail.Percentage.Value;
                    if (doHistory)
                    {
                        this.entity.Store(detail);
                    }
                    if (doStats)
                    {
                        GameRanking ranking = this.entity.Cast<GameRanking>().Where<GameRanking>(p => ((((p.Year == n.Year) && (p.Month == n.Month)) && (p.ConfigGameId == game.Config.Id)) && (p.Playerid == vid.PKID))).FirstOrDefault<GameRanking>();
                        if (ranking == null)
                        {
                            ranking = new GameRanking {
                                Year = n.Year,
                                Month = n.Month,
                                ConfigGameId = game.Config.Id,
                                Playerid = vid.PKID
                            };
                        }
                        ranking.NbParties++;
                        ranking.LostTops += (double) detail.TopLost.GetValueOrDefault(0L);
                        ranking.Time += (int) detail.Time.GetValueOrDefault(0L);
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
                        this.entity.Store(ranking);
                    }
                }
            }
        }

        public List<ConfigGameDto> GetConfigurations()
        {
            List<ConfigGameDto> list = new List<ConfigGameDto>();
            IDb4oLinqQuery<GameConfig> query = this.entity.Cast<GameConfig>().Select<GameConfig, GameConfig>(p => p);
            foreach (GameConfig config in query)
            {
                ConfigGameDto item = new ObjectSerializer<ConfigGameDto>().Deserialize(config.XmlConfig);
                list.Add(item);
            }
            return list;
        }

        public EnrolledUser getEnrolledUser(db4oProviders.User us)
        {
            IDb4oLinqQuery<EnrolledUser> self = this.entity.Cast<EnrolledUser>().Where<EnrolledUser>(eu => (eu.UserName == us.UserName) && (eu.ApplicationName == this._ApplicationName));
            if (self.Count<EnrolledUser>() > 0)
            {
                return self.First<EnrolledUser>();
            }
            return null;
        }

        public List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId)
        {
            //Guid pid = this.entity.Cast<db4oProviders.User>().Where<db4oProviders.User>(u => (u.UserName == player)).Select<db4oProviders.User, Guid>(u => u.PKID).FirstOrDefault<Guid>();
            List<GamesDetail> list = null;
            DateTime start = new DateTime(year, mont, 1);
            DateTime end = start.AddMonths(1);
            list = this.entity.Cast<GamesDetail>().Where<GamesDetail>(p => ((((p.Date > start) && (p.Date < end))) && (p.GameConfigId == configId))).ToList<GamesDetail>();
            List<GamesDetailDto> list2 = new List<GamesDetailDto>();
		
			//Console.Write(list.Count);
            foreach (GamesDetail detail in list)
            {
                long? nullable3;
                GamesDetailDto item = new GamesDetailDto {
                    Date = detail.Date,
                    Percentage = (float) detail.Percentage.GetValueOrDefault(0.0),
                    UserName = player,
                    Negative = (int) detail.Negative.GetValueOrDefault(0L),
                    Total = (int) detail.Total.GetValueOrDefault(0L),
                    Top = (int) detail.Top.GetValueOrDefault(0L),
                    GameId = detail.GameId,
                    TopLost = (int) detail.TopLost.GetValueOrDefault(0L),
                    ConfigId = detail.GameConfigId
                };
                long? time = detail.Time;
                time = detail.Time;
                item.Time = (time.HasValue ? new long?(time.GetValueOrDefault() / 60L) : (nullable3 = null)) + ":" + (time.HasValue ? new long?(time.GetValueOrDefault() % 60L) : (nullable3 = null));
                list2.Add(item);
            }
            return list2;
        }

        public IObjectContainer GetObjectContainer()
        {
            if (this.entity == null)
            {
                this.entity = Db4oEmbedded.OpenFile(this.dbFile);
            }
            return this.entity;
        }

        public List<RankingDto> GetRankings(string player, int year, int mont, Guid configId)
        {
           // Guid pid = this.entity.Cast<db4oProviders.User>().Where<db4oProviders.User>(u => (u.UserName == player)).Select<db4oProviders.User, Guid>(u => u.PKID).FirstOrDefault<Guid>();
            List<GameRanking> list = null;
            if ((player.Length > 0) && (configId == Guid.Empty))
            {
                list = this.entity.Cast<GameRanking>().Where<GameRanking>(p => (((p.Year == year)) && (p.Month == mont))).ToList<GameRanking>();
            }
            if ((player.Length > 0) && (configId != Guid.Empty))
            {
                list = this.entity.Cast<GameRanking>().Where<GameRanking>(p => ((p.ConfigGameId == configId))).ToList<GameRanking>();
            }
            List<RankingDto> list2 = new List<RankingDto>();
            Console.Write(list.Count);
            foreach (GameRanking ranking in list)
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

        public db4oProviders.User GetUser(Guid pkid)
        {
            return this.entity.Cast<db4oProviders.User>().Where<db4oProviders.User>(u => (u.PKID == pkid)).First<db4oProviders.User>();
        }

        public db4oProviders.User GetUser(string UserName)
        {
            IDb4oLinqQuery<db4oProviders.User> self = this.entity.Cast<db4oProviders.User>().Where<db4oProviders.User>(u => u.UserName == UserName);
            if (self.Count<db4oProviders.User>() > 0)
            {
                return self.First<db4oProviders.User>();
            }
            return null;
        }

        public List<db4oProviders.User> GetUsers()
        {
            return this.entity.Cast<db4oProviders.User>().Select<db4oProviders.User, db4oProviders.User>(u => u).ToList<db4oProviders.User>();
        }

        public bool IsAdminUser(db4oProviders.User us)
        {
            EnrolledUser user = this.getEnrolledUser(us);
            return ((user != null) && user.Roles.Contains(this._roleName));
        }

        public bool Login(string userName, string passWord)
        {
            if (this.entity.Cast<db4oProviders.User>().Where<db4oProviders.User>(p => (p.UserName == userName)).FirstOrDefault<db4oProviders.User>() == null)
            {
                db4oProviders.User user2 = new db4oProviders.User {
                    PKID = Guids.SequentialGuid(),
                    UserName = userName
                };
                this.entity.Store(user2);
            }
            return true;
        }

        public bool UpdateConfiguration(ConfigGameDto cfg)
        {
            try
            {
                GameConfig config = null;
                if (cfg.Id == Guid.Empty)
                {
                    config = new GameConfig {
                        Id = Guid.NewGuid()
                    };
                    cfg.Id = config.Id;
                }
                else
                {
                    config = this.entity.Cast<GameConfig>().Where<GameConfig>(p => (p.Id == cfg.Id)).FirstOrDefault<GameConfig>();
                }
                config.XmlConfig = new ObjectSerializer<ConfigGameDto>().Serialize(cfg);
                this.entity.Store(config);
                
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool UpdateUsers(db4oProviders.User us, List<string> roles, EnrolledUser en)
        {
            try
            {
                if (us.PKID == Guid.Empty)
                {
                    us.PKID = Guid.NewGuid();
                    us.ApplicationName = this._ApplicationName;
                    us.IsApproved = true;
                    en = new EnrolledUser(us.UserName, us.ApplicationName);
                }
                this.entity.Store(us);
                if (en == null)
                {
                    en = new EnrolledUser(us.UserName, us.ApplicationName);
                }
                if (roles != null)
                {
                    en.Roles = roles;
                    this.entity.Store(en);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

       
    }
}

