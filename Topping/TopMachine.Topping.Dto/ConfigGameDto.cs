using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace TopMachine.Topping.Dto
{
    [Serializable]
    [DataContract]
    public class ConfigGameDto
    {
        public ConfigGameDto()
        {
            Bonus = new int[] { 0, 0, 0, 0, 0, 0, 0, 50, 75, 100, 125, 150, 175, 200, 225, 250 };
            ExplosiveRating = 18;
            Id = Guid.Empty;
            MinLetters = 7;
            MaxLetters = 7;
            Language = "FR";
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name {get; set; }
        
        /* Game Settings */
        [DataMember]
        public int Minutes { get; set; }
        [DataMember]
        public int Seconds { get; set; }
        [DataMember]
        public int MinLetters { get; set; }
        [DataMember]
        public int MaxLetters { get; set; }
        [DataMember]
        public bool Joker { get; set; }
        [DataMember]
        public int Grid { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public int TypeOfGame { get; set; }
        [DataMember]
        public bool Explosive { get; set; }
        [DataMember]
        public int ExplosiveRating { get; set; }

        /* Play Settings */
        [DataMember]
        public bool Manual { get; set; }
        [DataMember]
        public bool Toping { get; set; }

        // OverWritten Settings
        [DataMember]
        public int[] Bonus { get; set; }

        // History Settings 
        [DataMember]
        public bool HistoryGame { get; set; }
        [DataMember]
        public bool HistoryDetail { get; set; }
        [DataMember]
        public bool HistoryOverall { get; set; }

        

    }
}
