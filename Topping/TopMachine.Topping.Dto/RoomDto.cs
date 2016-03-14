using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace TopMachine.Topping.Dto
{
    [Serializable]
    [DataContract]
    public enum RoomStatus
    {
        [EnumMember]
        Empty = 0,
        [EnumMember]
        Created = 1,
        [EnumMember]
        WaitingForGame = 2,
        [EnumMember]
        WaitingForStart = 3,
        [EnumMember]
        Started = 4,
        [EnumMember]
        Finished = 5
    }
    
    [Serializable]
    [DataContract]
    public class RoomDto
    {
        [DataMember]
        public int RoomId { get; set; }
        [DataMember]
        public Guid GameId { get; set; }
        [DataMember]
        public RoomStatus GameStatus { get; set; }
        [DataMember]
        public string GameFileLocation { get; set; }
        [DataMember]
        public DateTime LastAccess { get; set; }
        [DataMember]
        public int TotalPlayers { get; set; }
        [DataMember]
        public DateTime GameStartTime { get; set; }
        [DataMember]
        public int MinimumPlayers { get; set; }
        [DataMember]
        public int MaximumPlayers { get; set; }
        [DataMember]
        public int WaitingTime { get; set; }
        [DataMember]
        public string Owner { get; set; }
        [DataMember]
        public Guid ConfigId { get; set; }
        [DataMember]
        public ConfigGameDto Configuration { get; set; }
    }
}
