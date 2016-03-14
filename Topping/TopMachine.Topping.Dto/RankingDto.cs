using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TopMachine.Topping.Dto
{
    [DataContract]
    public class RankingDto
    {
        [DataMember]
        public string Period { get; set; }
        [DataMember]
        public string Player { get; set; }
        [DataMember]
        public Guid Config { get; set; }
        [DataMember]
        public string ConfigName { get; set; }
        [DataMember]
        public double Percentage { get; set; }
        [DataMember]
        public int NbGames { get; set; }
        [DataMember]
        public double NbLostTops { get; set; }
        [DataMember]
        public int NbTops { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public string BestTime { get; set; }
        [DataMember]
        public double BestScore { get; set; }


    }
}
