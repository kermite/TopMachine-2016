using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TopMachine.Topping.Dto
{
    /// <summary>
    /// Use for Serialization of the complete game
    /// For later use
    /// </summary>
    [Serializable]
    [DataContract]
    public class FinalRoomDto : RoomDto
    {
        [DataMember]
        public SerializableDictionary<string, string> Players = new SerializableDictionary<string, string>();

        [DataMember]
        public GeneratedGameDto Game = null;

        [DataMember]
        public SerializableDictionary<string, List<PlayedGameRoundDto>> PlayedRounds { get; set; }

        [DataMember]
        public SerializableDictionary<string, PlayerSummaryDto> PlayerSummaries { get; set; }

        public FinalRoomDto() 
        {
            Players = new SerializableDictionary<string, string>();
            PlayedRounds = new SerializableDictionary<string, List<PlayedGameRoundDto>>();
            PlayerSummaries = new SerializableDictionary<string, PlayerSummaryDto>();
            Game = new GeneratedGameDto(); 
        }

        public void ReinitGame() 
        {
            PlayedRounds.Clear();
            PlayerSummaries.Clear();  
        }

    }
}
