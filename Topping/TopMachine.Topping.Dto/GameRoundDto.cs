using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TopMachine.Topping.Dto
{
    [Serializable]
    [DataContract]
    public class GameRoundDto
    {
        [DataMember]
        public int Turn { get; set; }
        [DataMember]
        public string Word { get; set; }
        [DataMember]
        public string Direction { get; set; }
        [DataMember]
        public int Points { get; set; }
        [DataMember]
        public string Rack { get; set; }
        [DataMember]
        public bool SolutionSet { get; set; }

        public GameRoundDto()
        {
        }

        public GameRoundDto(int turn, string word, string direction, int points, string rack)
        {
            Turn = turn;
            Word = word;
            Direction = direction;
            Points = points;
            Rack = rack;
            SolutionSet = true; 

        }

    }

    [Serializable]
    [DataContract]
    public class PlayerSummaryDto
    {

        [DataMember]
        public string Player    { get; set; }
        [DataMember]
        public float Time { get; set; }
        [DataMember]
        public int Turn { get; set; }
        [DataMember]
        public int Total { get; set; }
        [DataMember]
        public int TotalTop { get; set; }
        [DataMember]
        public int PointsLost { get; set; }
        [DataMember]
        public float Percentage { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        
        public bool Stored { get; set; } 

        [IgnoreDataMember]
        [XmlIgnore]
        public List<PlayedGameRoundDto> Rounds { get; set; }

        public PlayerSummaryDto()
        {
            Rounds = new List<PlayedGameRoundDto>();
        }

        public PlayerSummaryDto(string player, float time, int turn, int total, int totalTop, int ptsLost, float percentage)
        {
            Player = player;
            Time = time;
            Turn = turn;
            Total = total;
            TotalTop = totalTop;
            PointsLost = ptsLost;
            Percentage = percentage;
            Rounds = new List<PlayedGameRoundDto>();
        }
        public string DisplayTime() 
        { 
            return String.Format("{0:00}:{1:00}", ((int)Time) / 60, Time % 60);
        }
        public void Replace(PlayerSummaryDto sum)
        {
            Player = sum.Player;
            Time = sum.Time;
            Turn = sum.Turn;
            Total = sum.Total;
            TotalTop = sum.TotalTop;
            PointsLost = sum.PointsLost;
            Percentage = sum.Percentage;
        }

        public void Replace(PlayerSummaryDto sum, PlayedGameRoundDto rnd)
        {
            Player = sum.Player;
            Time = sum.Time;
            Turn = sum.Turn;
            Total = sum.Total;
            TotalTop = sum.TotalTop;
            PointsLost = sum.PointsLost;
            Percentage = sum.Percentage;
            Rounds.Add(rnd);
        }

        
    }

    [Serializable]
    [DataContract]
    public class PlayedGameRoundDto
    {

        [DataMember]
        public int Turn { get; set; }
        [DataMember]
        public string Word { get; set; }
        [DataMember]
        public string Direction { get; set; }
        [DataMember]
        public int Points { get; set; }
        [DataMember]
        public string Player { get; set; }
        [DataMember]
        public float Time { get; set; }

        public PlayedGameRoundDto()
        {
        }

        public PlayedGameRoundDto(string player, float time, int turn, string word, string direction, int points)
        {
            Player = player;
            Time = time;
            Turn = turn;
            Word = word;
            Direction = direction;
            Points = points;
        }

        public static string ConvertCoordonnee(int row, int col, int direction)
        {
            string txt = "";
            char c = (char)col;
            char r = (char)row;

            if (direction == 1)
            {
                txt = ((char)(r + 64)).ToString() + ((int)c).ToString();
            }

            if (direction == 0)
            {
                txt = ((int)c).ToString() + ((char)(r + 64)).ToString();
            }
            return txt;
        }

    }
}
