using System;
using Topping.Core.Logic;
using TopMachine.Topping.Dto;
using System.Collections.Generic;

namespace Topping.Core.Logic.Client
{
    public delegate void CreatedDelegate(VRoom r);
    public delegate void FinishStatDelegate(object s);

    public interface IGameState : IDisposable
    {
        event CreatedDelegate OnCreated;
        event FinishStatDelegate OnFinishStatEvent;
        void Copy(IGameState g);
        void AddPlayer(string player);
        void GenerateGame(bool stat=false);
        void RemovePlayer(string player);
        void AddPlayedGameRoundDto(PlayedGameRoundDto pgr);
        void InitPlayedGameRoundDto(SerializableDictionary<string, List<PlayedGameRoundDto>> lst);
        void AddSummary(PlayerSummaryDto summary);
        void PlayGame();
        PlayerSummaryDto GetPlayerSummary(); 

        GameRoundDto GetRound(int round);
        List<PlayedGameRoundDto> GetPlayerGame(string player);

        GeneratedGameDto GeneratedGame { get; set; }
        System.Collections.Generic.List<string> Players { get; set; }
        Topping.Core.Logic.VRoom Room { get; set; }
        List<PlayerSummaryDto> PlayerSummaries { get; set; }
        FinalRoomDto FinalRoom { get; set; }
        SerializableDictionary<string, List<PlayedGameRoundDto>> PlayedGameRound { get; set; }
       

    }

}
