using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{
    public class WordContainer
    {
        bool invariable = false; 
        string genre = "";
        string word;
        string prefix;
        string suffix;
        string baseWord;
        string before;
        string after;
        string additional;
        string sort;
        int type;
        int x, y;

        public bool Invariable { get { return invariable; } set { invariable = value; } }
        public int Type { get { return type; } set { type = value; } }
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return x; } set { y = value; } }
        public string Genre { get { return genre; } set { sort = genre; } }
        public string Sort { get { return sort; } set { sort = value; } }
        public string Additional { get { return additional; } set { additional = value; } }
        public string Word { get { return word; } set { word = value; } }
        public string BaseWord { get { return baseWord; } set { baseWord = value; } }
        public string Prefix { get { return prefix; } set { prefix = value; } }
        public string Suffix { get { return suffix; } set { suffix  = value; } }
        public string Before { get { return before; } set { before = value; } }
        public string After { get { return after; } set { after= value; } }
    }
}
