using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TopMachine.Topping.Dto
{
    [Serializable]
    [DataContract]
    public class GamesDetailDto
    {
        [DataMember]
        public Guid GameId { get; set; }
        [DataMember]
        public Guid ConfigId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string ConfigName { get; set; }
        [DataMember]
        public int Total { get; set; }
        [DataMember]
        public int Solos { get; set; }
        [DataMember]
        public int Warnings { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public float Percentage { get; set; }
        [DataMember]
        public int TopLost { get; set; }
        [DataMember]
        public int Top { get; set; }
        [DataMember]
        public int Negative { get; set; }
        [DataMember]
        public int Selection { get; set; }
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        public DateTime Date { get; set; }



    }
}
