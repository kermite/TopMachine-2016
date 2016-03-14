using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TopMachine.Topping.Dto
{

    [DataContract]
    public enum MessageType
    {
        [EnumMember]
        NoType = 0,
        [EnumMember]
        Error = 1,
        [EnumMember]
        Result = 2,

        [EnumMember]
        Public = 1000,
        [EnumMember]
        Private = 1001,

        [EnumMember]
        RoomCreate = 2000,
        [EnumMember]
        RoomEnter = 2001,
        [EnumMember]
        RoomLeave = 2002,
        [EnumMember]
        RoomGameStart = 2003,
        [EnumMember]
        RoomStatusChange = 2004,
        [EnumMember]
        GameStart = 3000,
        [EnumMember]
        GameStatus = 3004,
        [EnumMember]
        GameSummaryUpdated = 3005,
        [EnumMember]
        GamePlayerGame = 3006,
        [EnumMember]
        Logoff = 3007,
        [EnumMember]
        Login = 3008,
        [EnumMember]
        FinalRoom = 3009,
        [EnumMember]
        RelaunchGame = 3010,
        [EnumMember]
        GameSummaryRoundUpdated = 3011,
    
        
    }

    [DataContract]
    public enum MessageModule
    {
        [EnumMember]
        System = 0,
        [EnumMember]
        Chat = 1,
        [EnumMember]
        Room = 2,
        [EnumMember]
        Game = 3,
        [EnumMember]
        Config = 4,
        [EnumMember]
        Admin = 5
    }

    [DataContract]
    public enum MessageTransportType
    {
        [EnumMember]
        Object = 0,
        [EnumMember]
        XML = 1,
        [EnumMember]
        JSON = 2
    }


    [Serializable]
    [DataContract]
    public class MessagesDto
    {
        [DataMember]
        public MessageDto[] Messages { get; set; }
    }


    [Serializable]
    [DataContract]
    public class MessageDto

    {
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string From { get; set; }
        [DataMember]
        public string To { get; set; }
        [DataMember]
        public int ToRoom { get; set; }
        [DataMember]
        public MessageModule MessageModule { get; set; }
        [DataMember]
        public MessageType MessageType { get; set; }
        [DataMember]
        public string XmlObjectType { get; set; }
        [DataMember]
        public string XmlObject { get; set; }
        [DataMember]
        public RoomDto Room { get; set; }
    }
}
