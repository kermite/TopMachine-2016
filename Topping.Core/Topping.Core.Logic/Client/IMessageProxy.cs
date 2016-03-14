using System;
using TopMachine.Topping.Dto;
using System.Collections.Generic;
namespace Topping.Core.Logic.Client
{

    public enum GamePermissions
    {
        CanEndRound = 1,
        CanPauseRound = 2,
        CanChat = 4,
        CanObserve = 8,
        CanValidate = 16
    }
            
    public interface IMessageProxy
    {
        GamePermissions GamePermissions { get; set; }
        IGameState CreateGameState(ConfigGameDto dto);
        IGameState GameState { get; set; }
        ISession Session { get; set; }

        // Login 
        bool Login(string userName, string passWord);
        bool CheckVersion(string version);


        // Messages 
        void ActivateChat();
        void DeactivateChat();
        List<MessageDto> GetMessages();
        void SendMessage(TopMachine.Topping.Dto.MessageDto m);
        void SendMessageToRoom(int x, TopMachine.Topping.Dto.MessageDto m);

        // Rooms 
        RoomDto GetRoom(int roomid);
        //System.Collections.Generic.List<Room> GetRooms();
        //System.Collections.Generic.List<RoomDto> GetRoomsDto();
        RoomDto InitRoom(Topping.Core.Logic.VRoom room, bool generate);
        RoomDto JoinRoom(int roomId);
        void Logoff();
        void GetListPlayedRound(string playerSearch);

        void ResetRooms();
        void LeaveAnyRoom();
        void LeaveRoom();
        string[] ListRoomPlayers(int roomId);
        string[] ListRoomActivePlayers(int roomId);

        string[] ListPlayers();

        // Rooms and Game
        GeneratedGameDto GetGame();
        void SetGame(GeneratedGameDto game);
        void AddPlayedRound(PlayerSummaryDto summary, PlayedGameRoundDto pgr);
        void FinishGame();
        FinalRoomDto  getFinalRoom(int roomid);
        void StartGame();
        List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId);


        //Configurationn
        bool UpdateConfiguration(TopMachine.Topping.Dto.ConfigGameDto cfg);
        bool DeleteConfiguration(TopMachine.Topping.Dto.ConfigGameDto cfg);
        System.Collections.Generic.List<TopMachine.Topping.Dto.ConfigGameDto> GetConfigurations();
        System.Collections.Generic.List<TopMachine.Topping.Dto.RankingDto> GetRankings(string player, int year, int mont, Guid ConfigId);

    }
}
