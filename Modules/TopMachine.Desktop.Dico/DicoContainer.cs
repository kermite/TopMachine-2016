using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Desktop.Dico
{
    public class DicoReaderConfig
    {
        public string Start { get; set; }
        public bool OnlyNew { get; set; }
        public string End { get; set; }
        public int LenMin { get; set; }
        public int LenMax { get; set; }
        public int Max { get; set; }
        public bool Visible { get; set; }
        public string CurrentLast { get; set; }

        public int CurrentPage { get; set; }

        public List<TopMachine.Topping.DAL.Dico> MotRows;
    }

    public class Dico
    {
        public List<TopMachine.Topping.DAL.Dico> DicoList { get; set; }

        public Dico()
        {

            DicoList = new List<TopMachine.Topping.DAL.Dico>(); 
        }
    }

}
