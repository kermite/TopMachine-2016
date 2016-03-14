using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using TopMachine.Desktop.Overall;
using TopMachine.Topping.Dto;
using TopMachine.Topping.Engine.GameController;
using Topping.Core.Logic;
using Topping.Core.Logic.Client;
using Topping.Core.Proxy.Local.Client;

namespace Topping.Core.Proxy.Local
{
   

    public class GameState : IGameState, IDisposable
    {
        private GameOnlineLoader _go;
        private Guid MemoryCheckId;

        public event CreatedDelegate OnCreated;

        public event FinishStatDelegate OnFinishStatEvent;

        public GameState(Topping.Core.Logic.VRoom r)
        {
            this.Room = r;
            this.Players = new List<string>();
            this.PlayerSummaries = new List<PlayerSummaryDto>();
            this.AddPlayer(Proxy.Instance.UserName);
            this.MemoryCheckId = MemoryCheck.Register(this, base.GetType().FullName);
        }

        public void AddPlayedGameRoundDto(PlayedGameRoundDto pgr)
        {
            string player = pgr.Player;
            if (this.PlayedGameRound == null)
            {
                this.PlayedGameRound = new SerializableDictionary<string, List<PlayedGameRoundDto>>();
            }
            if (!this.PlayedGameRound.ContainsKey(player))
            {
                this.PlayedGameRound.Add(player, new List<PlayedGameRoundDto>());
            }
            this.PlayedGameRound[player].Add(pgr);
        }

        public void AddPlayer(string player)
        {
            if (!this.Players.Contains(player))
            {
                this.Players.Add(player);
            }
        }

        public void AddSummary(PlayerSummaryDto summary)
        {
            PlayerSummaryDto item = this.PlayerSummaries.Find(p => p.Player == summary.Player);
            if (item == null)
            {
                this.PlayerSummaries.Add(summary);
            }
            else
            {
                this.PlayerSummaries.Remove(item);
                this.PlayerSummaries.Add(summary);
            }
        }

        public void Copy(IGameState g)
        {
            this.FinalRoom = g.FinalRoom;
            this.PlayedGameRound = g.PlayedGameRound;
            this.Players = g.Players;
            this.PlayerSummaries = g.PlayerSummaries;
            this.Room = g.Room;
            this.GeneratedGame = g.GeneratedGame;
        }

        public void Dispose()
        {
            this.Room = null;
            if (this.PlayerSummaries != null)
            {
                this.PlayerSummaries.Clear();
            }
            if (this.PlayedGameRound != null)
            {
                this.PlayedGameRound.Clear();
            }
            if (this._go != null)
            {
                this._go.Dispose();
            }
            this._go = null;
            MemoryCheck.Unregister(this.MemoryCheckId);
        }

        public void GenerateGame(bool stat = false)
        {
            if (this._go != null)
            {
                this._go.Dispose();
            }
            GeneratedGameDto g = new GeneratedGameDto {
                Config = this.Room.Configuration
            };
            this._go = new GameOnlineLoader(g);
            if (!stat)
            {
                this._go.OnGameCreated += new GameOnlineLoader.OnGameCreatedEvent(this.go_OnGameCreated);
            }
            else
            {
                this._go.OnGameCreated += new GameOnlineLoader.OnGameCreatedEvent(this.go_OnGameCreated);
                this._go.OnFinishStat += new GameOnlineLoader.OnFinishGameStatEvent(this.go_OnFinishStat);
            }
            this._go.GenerateLocalGame(stat);
        }

        public List<PlayedGameRoundDto> GetPlayerGame(string player)
        {
            return this.Room.GetPlayerGame(player);
        }

        public PlayerSummaryDto GetPlayerSummary()
        {
            string pseudo = MessageProxy.Proxy.Session.Pseudo;
            PlayerSummaryDto item = this.PlayerSummaries.Find(p => p.Player == pseudo);
            if (item == null)
            {
                item = new PlayerSummaryDto {
                    Player = MessageProxy.Proxy.Session.Pseudo
                };
                this.PlayerSummaries.Add(item);
            }
            return item;
        }

        public GameRoundDto GetRound(int round)
        {
            if (round <= this.GeneratedGame.Rounds.Count)
            {
                return this.GeneratedGame.Rounds[round - 1];
            }
            return null;
        }
        public void PlayGame() 
        {
            _go.PlayGame();
        
        }
        private void go_OnFinishStat(object s)
        {
            if (this.OnFinishStatEvent != null)
            {
                this.OnFinishStatEvent(s);
            }
        }

        private void go_OnGameCreated(GeneratedGameDto game)
        {
            this.PlayerSummaries = new List<PlayerSummaryDto>();
            this.GeneratedGame = game;
            MessageProxy.Proxy.SetGame(game);
            this.OnCreated(this.Room);
        }

        public void InitPlayedGameRoundDto(SerializableDictionary<string, List<PlayedGameRoundDto>> lst)
        {
            this.PlayedGameRound = lst;
        }

        public void RemovePlayer(string player)
        {
            if (this.Players.Contains(player))
            {
                this.Players.Remove(player);
            }
        }

        public FinalRoomDto FinalRoom { get; set; }

        public GeneratedGameDto GeneratedGame { get; set; }

        public SerializableDictionary<string, List<PlayedGameRoundDto>> PlayedGameRound { get; set; }

        public List<string> Players { get; set; }

        public List<PlayerSummaryDto> PlayerSummaries { get; set; }

        public Topping.Core.Logic.VRoom Room { get; set; }
    }
}

