using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopMachine.Training.DAL
{
    public class WordStatistic
    {
        public int Status { get; set; }
        public long Counter { get; set; }
        public long Lost { get; set; }
        public long Succeeded { get; set; }
        public long Total { get; set; }
        public List<string> Words { get; set; }
    }

    public class WordList
    {
        public List<string> Words { get; set; }

        public WordList()
        {
            Words = new List<string>(); 
        }
    }

    public class GameConfig
    {
        public int TotalRounds { get; set; }
        public int TypeOfPlay { get; set; }
        public int Suppress { get; set; }
        public int DisplayPlusOne { get; set; }
        public bool Pause { get; set; }
        public bool DisplayPossibilities { get; set; }
        public int Chrono { get; set; }
        public int PlayMode { get; set; }
    }

    public class ListCriterium
    {
        public string InclusionRegEx { get; set; }
        public int InclusionRegExCount { get; set; }
        public string ExclusionRegEx { get; set; }
        public int ExclusionRegExCountCount { get; set; }
    }

    public class ListConfig
    {
        public string Name { get; set; }

        public bool Binomes { get; set; }
        public bool Joker { get; set; }
        public bool OnlyOne { get; set; }
        public bool AddFake { get; set; }

        public string Dico { get; set; }
        public string Base { get; set; }

        public int MaxLetters { get; set; }
        public int MinLetters { get; set; }

        public int PercentageFake { get; set; }
        public int MinPossibilities { get; set; }
        public int MaxPossibilities { get; set; }

        public string WithAllLetters { get; set; }
        public string WithAnyLetter { get; set; }
        public string WithoutLetter { get; set; }

        public string LetterJoker { get; set; }

        public bool MatchAllCriteria { get; set; }
        public List<ListCriterium> Criteria { get; set; }

        public ListConfig()
        {
            Criteria = new List<ListCriterium>();
            Dico = "FR";
        }
    }
}
