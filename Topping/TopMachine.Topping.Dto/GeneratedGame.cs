using System;

using System.Collections.Generic;
using System.Text;
using TopMachine.Topping.Dto;
using System.Runtime.Serialization;

namespace TopMachine.Topping.Dto
{
    [DataContract]
    public enum GameStatus
    {
        [EnumMember]
        Empty = 0,
        [EnumMember]
        Partial = 1,
        [EnumMember]
        Complete = 2
    }

    [Serializable]
    [DataContract]
    public class GeneratedGameDto
    {
        public GeneratedGameDto()
        {
            Rounds = new List<GameRoundDto>();
            Config = new ConfigGameDto();
            Id = TopMachine.Desktop.Overall.Guids.SequentialGuid();
            DateTime = DateTime.Now;
            Status = GameStatus.Complete;
            Name = ""; 
        }

        [DataMember]
        public GameStatus Status { get; set; }
        [DataMember]
        public ConfigGameDto Config { get; set; }
        [DataMember]
        public List<GameRoundDto> Rounds { get; set; }
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public string Name { get; set; }
    }


}
