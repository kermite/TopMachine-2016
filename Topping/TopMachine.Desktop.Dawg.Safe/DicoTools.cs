using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{
    public class DicoTools
    {
        public static int[] GetDicoLetters(string language)
        {
            int[] letters = null;
            if (language == null || language == "") language = "FR";
            switch (language)
            {
                case "FR":
                    letters = new int[] {2, 9, 2, 2, 3, 15, 2, 2, 2, 8, 1, 1, 5, 3, 6, 6, 2, 1, 6, 6, 6, 6, 2, 1, 1, 1, 1, 0 };
                    break;
                case "UK":
                case "US":
                    letters = new int[] { 2, 9,2 ,2 , 4, 12, 2, 3, 2, 9, 1, 1, 4, 2, 6, 8, 2, 1, 6, 4, 6, 4, 2, 2, 1, 2, 1, 0 };
                    break;
                case "NL":
                    letters = new int[] { 2, 6, 2, 2, 5, 18, 2, 3, 2, 4, 2, 3, 3, 3, 10, 6 , 2, 1,5,5,5,3,2,2,1,1,2,0 };
                    break;

            }

            return letters; 
        }


        public static int[] GetDicoPoints(string language)
        {
            int[] letters = null;

            if (language == null || language == "") language = "FR";
            switch (language)
            {
                case "FR":
                    letters = new int[] { 0, 1, 3, 3, 2, 1, 4, 2, 4, 1, 8, 10, 1, 2, 1, 1, 3, 8, 1, 1, 1, 1, 4, 10, 10, 10, 10, 0 };
                    break;
                case "UK":
                case "US":
                    letters = new int[] { 0, 1, 3, 3, 2, 1, 4, 2, 4, 1, 8, 5, 1, 3, 1, 1, 3, 10, 1, 1, 1, 1, 4, 4, 8, 4, 10, 0 };
                    break;
                case "NL":
                    letters = new int[] { 0, 1, 3, 5, 2, 1, 4, 3, 4, 1, 4, 3, 3, 3, 1, 1, 3, 10, 1, 2, 2, 5, 4, 5, 8, 8, 4, 0 };
                    break;

            }

            return letters;
        }


        public static string GetDicoName(string language, int dico)
        {
            return "BASE" + language; 
        }

    }
}
