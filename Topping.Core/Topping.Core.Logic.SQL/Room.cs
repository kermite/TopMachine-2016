using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using TopMachine.Topping.Dto;
using Topping.Core.Data.SQL;
using System.Runtime.Serialization;

using Topping.Core.Logic.Threads;
using Topping.Core.Data;


namespace Topping.Core.Logic.SQL
{
    //[Serializable]
    [DataContract]
    public class Room : VRoom
    {
        public Room()
            : base()
        {

        }

        public Room(int room)
            : base(room)
        {

        }

        public Room(RoomDto r)
            : base(r)
        {

        }

  

        #region GAME

        public override void SetGeneratedGame(GeneratedGameDto game, string fn)
        {
            CheckAccess();
            this.finalRoomDto.Game = game;
            generatedFileName = fn;
            using (ToppingAccessor ta = new ToppingAccessor())
            {
                ta.AddGame(game);
            }
        }

        public override void SetGeneratedGame(GeneratedGameDto game)
        {
            CheckAccess();
            this.finalRoomDto.Game = game;

            if (game != null)
            {
                game.DateTime = DateTime.Now; 
                GameStartTime = DateTime.Now.AddSeconds(WaitingTime);
                Configuration = game.Config;
                ConfigId = game.Config.Id;
                using (ToppingAccessor ta = new ToppingAccessor())
                {
                    ta.AddGame(game);
                }
            }
        }

                    
        public override void FinishGame(string player)
        {

            if (finalRoomDto.PlayerSummaries.ContainsKey(player))
            {
                using (ToppingAccessor ta = new ToppingAccessor())
                {
                    PlayerSummaryDto dto = finalRoomDto.PlayerSummaries[player];
                    if (dto != null && dto.Stored == false)
                    {
                        if (dto.Rounds.Count > 0)
                        {
                            ta.FinishGame(player, finalRoomDto.Game, finalRoomDto.PlayerSummaries[player],
                            finalRoomDto.Game.Config.HistoryDetail, finalRoomDto.Game.Config.HistoryGame, finalRoomDto.Game.Config.HistoryOverall);
                        }
                        dto.Stored = true;
                    }
                }
            }
            if (GameStatus != RoomStatus.Finished)
            {

                if (GameStatus == RoomStatus.Started && finalRoomDto.PlayerSummaries.Values.Where(p => p.Stored == false).Count() == 0)
                {
                    if (finalRoomDto.PlayerSummaries.Count > 0)
                    {
                        SerializeGame();
                    }

                    GameStartTime = DateTime.Now.AddSeconds(WaitingTime);
                    ChangeStatus(RoomStatus.Finished);

                }
                else
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["IsLocal"] != null
                            || System.Configuration.ConfigurationManager.AppSettings["IsLocal"] == "1")
                    {
                        if (GameStatus != RoomStatus.Finished)
                        {
                            SerializeGame();
                            ChangeStatus(RoomStatus.Finished);
                        }
                    }
                }
            }
        }

        public override void FinishGame()
        {
            if (GameStatus != RoomStatus.Finished)
            {
                foreach (string player in finalRoomDto.Players.Keys.ToList())
                {
                    if (finalRoomDto.PlayerSummaries.ContainsKey(player))
                    {
                        using (ToppingAccessor ta = new ToppingAccessor())
                        {

                            PlayerSummaryDto dto = finalRoomDto.PlayerSummaries[player];
                            if (dto != null && dto.Stored == false)
                            {
                                ta.FinishGame(player, finalRoomDto.Game, finalRoomDto.PlayerSummaries[player],
                                finalRoomDto.Game.Config.HistoryDetail, finalRoomDto.Game.Config.HistoryGame, finalRoomDto.Game.Config.HistoryOverall);
                                dto.Stored = true;
                            }
                        }
                    }
                }

                if (GameStatus == RoomStatus.Started && finalRoomDto.PlayerSummaries.Values.Where(p => p.Stored == false).Count() == 0)
                {
                    if (finalRoomDto.PlayerSummaries.Count > 0)
                    {
                        SerializeGame();
                    }
                    finalRoomDto.Game = null;
                    GameStartTime = DateTime.Now.AddSeconds(WaitingTime);
                    ChangeStatus(RoomStatus.Finished);
                }
            }
        }

        #endregion

    }

}
