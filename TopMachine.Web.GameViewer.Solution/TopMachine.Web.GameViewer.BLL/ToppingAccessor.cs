using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Data.Objects;

using System.Data.Common;
using System.Data.SqlClient;
using TopMachine.Topping.Dto;

namespace TopMachine.Web.GameViewer.BLL
{
    public class ToppingAccessor : IDisposable
    {
        ToppingGamesEntities entity;


        public ToppingAccessor()
        {

            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["ToppingGamesEntities"].ConnectionString;

            entity = new ToppingGamesEntities(conn);

        }


        public List<GameType> GetGameTypes()
        {
            return entity.GameTypes.OrderBy("Id").ToList(); 
        }

        public List<GameCollector> GetGameCollectors(int TypeId, string filter, string order, int page, int pageSize)
        {
            IQueryable<GameCollector> list = null; 

            if (filter.Length > 0)
            {
                list = entity.GameCollectors.Where(filter).Where(p=>p.Type == TypeId); 
            }
            else
            {
                list = entity.GameCollectors.Where(p=>p.Type == TypeId);
            }

            if (pageSize != 0)
            {
                list = list.Skip(page * pageSize).Take(pageSize); 
            }

            return list.ToList(); 

        }

        public List<GameSet> GetGameSets(int TypeId, string filter, string order, int page, int pageSize)
        {
            IQueryable<GameSet> list = null;

            if (filter.Length > 0)
            {
                list = entity.GameSets.Where(filter).Where(p => p.GameCollectorId == TypeId);
            }
            else
            {
                list = entity.GameSets.Where(p => p.GameCollectorId == TypeId);
            }

            if (pageSize != 0)
            {
                list = list.Skip(page * pageSize).Take(pageSize);
            }

            return list.ToList();

        }

        public List<GameShort> GetGames(int TypeId, string filter, string order, int page, int pageSize)
        {
            IQueryable<GameShort> list = null;

            if (filter.Length > 0)
            {
                list = entity.GameShorts.Where(filter).Where(p => p.SetId == TypeId);
            }
            else
            {
                list = entity.GameShorts.Where(p => p.SetId == TypeId);
            }

            if (pageSize != 0)
            {
                list = list.Skip(page * pageSize).Take(pageSize);
            }

            return list.ToList();

        }


        public void InsertFinalRoomDto(FinalRoomDto dto, int collector)
        {
            int y = dto.Game.DateTime.Year;
            int m = dto.Game.DateTime.Month;
            int d = dto.Game.DateTime.Day;

            GameSet gs = entity.GameSets.Where(p =>
                    p.Nom == dto.Configuration.Name
                    && p.Annee == y
                    && p.Mois == m
                    && p.Jour == d
                    && p.GameCollectorId == collector ).FirstOrDefault();



            if (gs == null)
            {
                gs = new GameSet
                {
                    Annee = y,
                    Mois = m,
                    Jour = d,
                    Nom = dto.Configuration.Name,
                    GameCollectorId = collector
                };

                entity.AddToGameSets(gs);
            }

            entity.SaveChanges();

            Game g = new Game();
            g.Date = dto.Game.DateTime;
            g.Name = dto.Configuration.Name;
            g.SetId = gs.Id;
            g.GameXml = new TopMachine.Desktop.Overall.ObjectSerializer<GeneratedGameDto>().Serialize(dto.Game);

            entity.AddToGames(g);
            entity.SaveChanges();


            foreach (PlayerSummaryDto pd in dto.PlayerSummaries.Values)
            {
                GameDetail gd = new GameDetail();
                gd.GameId = g.Id;
                gd.Negative = pd.PointsLost;
                gd.Percentage = pd.Percentage;
                gd.Pseudo = pd.Player;
                gd.Time = (int)pd.Time;
                gd.Top = pd.PointsLost == 0 ? true : false;
                gd.Total = pd.Total;
                gd.Warnings = 0;
                gd.Solos = 0;
                gd.TopLost = dto.Game.Rounds.Count - pd.TotalTop;

                if (dto.PlayedRounds.ContainsKey(gd.Pseudo))
                {
                    List<PlayedGameRoundDto> rounds = dto.PlayedRounds[gd.Pseudo];
                    gd.RoundsXml = new TopMachine.Desktop.Overall.ObjectSerializer<List<PlayedGameRoundDto>>().Serialize(rounds);
                }

                entity.AddToGameDetails(gd);
            }

            entity.SaveChanges();

            GameConfig gc = entity.GameConfigs.Where(p => p.OriginalId == dto.Configuration.Id).FirstOrDefault();

            if (gc == null)
            {
                gc = new GameConfig();
                gc.OriginalId = dto.Configuration.Id;
                gc.Xml = new TopMachine.Desktop.Overall.ObjectSerializer<ConfigGameDto>().Serialize(dto.Configuration);
                entity.AddToGameConfigs(gc);
                entity.SaveChanges();
            }

            foreach (PlayerSummaryDto pd in dto.PlayerSummaries.Values)
            {
                GameRanking gr = entity.GameRankings.Where(p =>
                p.ConfigGameId == gc.Id
                && p.Year == y
                && p.Month == m
                && p.Pseudo == pd.Player).FirstOrDefault();

                if (gr == null)
                {

                    gr = new GameRanking
                    {
                        Year = y,
                        Month = m,
                        Pseudo = pd.Player,
                        ConfigGameId = (int) gc.Id,
                        GameTop = 0,
                        LostTops = 0,
                        NbParties = 0,
                        Percentage = 0,
                        PlayerTop = 0,
                        Position = 0,
                        Selection= 0,
                        Time = 0,
                        TotalTops = 0,
                        BestScore = 0,
                        BestTime = 0
                    };

                    entity.AddToGameRankings(gr);

                }

                int top = dto.Game.Rounds.Sum(p => p.Points);

                gr.NbParties++;
                gr.LostTops += (dto.Game.Rounds.Count - pd.TotalTop);
                gr.Time += (int)pd.Time;
                if (pd.Percentage == 1) gr.TotalTops++;
                gr.PlayerTop += (int)pd.Total;
                gr.GameTop += top;
                gr.Percentage = (float)gr.PlayerTop / top;

                if (gr.BestScore <= pd.Percentage)
                {
                    gr.BestScore = pd .Percentage;
                    if (gr.BestTime == 0 || gr.BestTime > pd.Time)
                    {
                        gr.BestTime = (int)pd.Time;
                    }
                }
            }

            entity.SaveChanges();
        }


        public void Save()
        {
            entity.SaveChanges();
            entity.AcceptAllChanges();
        }


        #region IDisposable Members

        public void Dispose()
        {
            entity.Dispose();
        }

        #endregion
    }
}
