using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TopMachine.Topping.DAL
{
    public class MotDto
    {
        public string Mot { get; set; }
        public int Type { get; set; }
        public int DicoId { get; set; }
    }

    public class DicoDto
    {
        public MotDto[] Mots { get; set; }

        public int DicoId
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string Mot
        {
            get;
            set;
        }

        public string Definition
        {
            get;
            set;
        }


        public string Genre
        {
            get;
            set;
        }

        public bool Invariable
        {
            get;
            set;
        }

        public bool Ods6
        {
            get;
            set;
        }

        public bool Ods6Modif
        {
            get;
            set;
        }

        public bool Visible
        {
            get;
            set;
        }
    }
}
