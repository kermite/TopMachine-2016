
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using TopMachine.Topping.Dto;

namespace Topping.Core.Logic.Interface
{
   
   
    [ServiceContract(Namespace="", SessionMode=SessionMode.Allowed)]
   [ServiceKnownType(typeof(Topping.Core.Logic.SQL.Room))]
    public interface IProxy
    {
        [OperationContract]
        void ActivateChat();
        [OperationContract]
        void AddPlayedRound(PlayerSummaryDto summary, PlayedGameRoundDto pgr);
        [OperationContract]
        bool ChangedPwdUser(UserDto us, string NewPwd);
        [OperationContract]
        bool CheckVersion(string version);
        [OperationContract]
        void DeactivateChat();
        [OperationContract]
        bool DeleteConfiguration(ConfigGameDto cfg);
        [OperationContract]
        bool DeleteGameXML(string fn);
        [OperationContract]
        bool DeleteUser(UserDto us);
        [OperationContract]
        void FinishGame();
        [OperationContract]
        List<ConfigGameDto> GetConfigurations();
        [OperationContract]
        FinalRoomDto GetFinalRoom(int RoomId);
        [OperationContract]
        GeneratedGameDto GetGame();
        [OperationContract]
        List<GamesDetailDto> GetGamesDetail(string player, int year, int mont, Guid configId);
        [OperationContract]
        void GetListPlayedRound(string playerSearch);
        [OperationContract]
        List<MessageDto> GetMessages();
        [OperationContract]
        List<RankingDto> GetRankings(string player, int year, int mont, Guid ConfigId);
        [OperationContract]
        RoomDto GetRoom(int roomid);
        [OperationContract]
        List<RoomDto> GetRoomsDto();
        [OperationContract]
        List<UserDto> GetUsers();
        [OperationContract]
        RoomDto InitRoom(VRoom room, bool generate);
        [OperationContract]
        RoomDto JoinRoom(int roomId);
        [OperationContract]
        void LeaveAnyRoom();
        [OperationContract]
        RoomDto LeaveRoom();
        [OperationContract]
        string[] ListPlayers();
        [OperationContract]
        string[] ListRoomActivePlayers(int roomid);
        [OperationContract]
        string[] ListRoomPlayers(int roomid);
        [OperationContract]
        Guid Login(string userName, string passWord, int version);
        [OperationContract]
        void Logoff();
        [OperationContract]
        void ResetRooms();
        [OperationContract]
        void SendMessage(MessageDto m);
        [OperationContract]
        void SendMessageToRoom(int x, MessageDto m);
        [OperationContract]
        void SetGame(GeneratedGameDto game);
        [OperationContract]
        void StartGame();
        [OperationContract]
        bool UpdateConfiguration(ConfigGameDto cfg);
        [OperationContract]
        bool UpdateUser(UserDto us);
    }
}

