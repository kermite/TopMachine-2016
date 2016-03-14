
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using TopMachine.Topping.Dawg;
using CMWA.Packager.Tools.Bytes;
using CMWA.Packager;
using System.Drawing;
namespace TopMachine.Topping.Common.Structures
{
    public class GridConfig
    {
        public CBoardMaker cm = new CBoardMaker();

        public int NbLetters = 0;
        public int[] Vowels, Consonants, Values, Amount, Ascii;
        public int dimX, dimY, dimRX, dimRY;
        public int[] LetterMultipliers, WordMultipliers;
        public System.Drawing.Color[] LetterColor, WordColor;

        public GridConfig()
        {
        }

        public char GetChar(int x)
        {
            return (char)Ascii[x];
        }

        public int GetCharFromAscii(int x)
        {
            for (int xx = 0; xx < NbLetters; xx++)
            {
                if (Ascii[xx] == x) return (char)xx;
            }

            return -1;
        }

        public System.Drawing.Color GetColor(int row, int col)
        {
            int x = GetLetterMultiplier(row, col);
            if (x > 1)
            {
                return LetterColor[x - 1];
            }

            x = GetWordMultiplier(row, col);
            if (x > 1)
            {
                return WordColor[x - 1];
            }

            return WordColor[0];
        }

        public int GetLetterMultiplier(int row, int col)
        {
            return LetterMultipliers[row * dimRX + col];
        }


        public int GetWordMultiplier(int row, int col)
        {
            return WordMultipliers[row * dimRX + col];
        }

        public void Load(int Type, string Language)
        {
            String s;
            String[] v;
            string path = "";

            if (Language.Length == 0) Language = "FR";
            switch (Type)
            {
                case 0:
                    path = "GRIDN" + Language;
                    break;
                case 1:
                    path = "GRIDS" + Language;
                    break;
                case 2:
                    path = "GRIDN" + Language;
                    break;
                case 3:
                    path = "GRIDN" + Language;
                    break;

            }

            byte[] b = PackageManager.Instance.Project.GetBytes("TopMachineData\\Data\\" + path);

            using (MemoryStream ms = new MemoryStream(b))
            {
                using (StreamReader sr = new StreamReader(ms))
                {
                    ms.Position = 0;

                    sr.ReadLine();

                    // Letter Colors 
                    LetterColor = new System.Drawing.Color[4];
                    s = sr.ReadLine();
                    v = s.Split(new char[] { ',' });
                    for (int x = 0; x < 4; x++)
                    {
                        LetterColor[x] = GetColor(v[x]);
                    }
                    sr.ReadLine();

                    // Word Colors 
                    WordColor = new System.Drawing.Color[4];
                    s = sr.ReadLine();
                    v = s.Split(new char[] { ',' });
                    for (int x = 0; x < 4; x++)
                    {
                        WordColor[x] = GetColor(v[x]);
                    }
                    sr.ReadLine();

                    // Word Colors 
                    s = sr.ReadLine();
                    NbLetters = int.Parse(s);
                    sr.ReadLine();

                    // Vowels 
                    Vowels = new int[NbLetters];
                    s = sr.ReadLine();
                    v = s.Split(new char[] { ',' });
                    for (int x = 0; x < NbLetters; x++)
                    {
                        Vowels[x] = int.Parse(v[x]);
                    }
                    sr.ReadLine();

                    // Consonants 
                    Consonants = new int[NbLetters];
                    s = sr.ReadLine();
                    v = s.Split(new char[] { ',' });
                    for (int x = 0; x < NbLetters; x++)
                    {
                        Consonants[x] = int.Parse(v[x]);
                    }
                    sr.ReadLine();


                    // Letter Total
                    Amount = new int[NbLetters];
                    s = sr.ReadLine();
                    v = s.Split(new char[] { ',' });
                    for (int x = 0; x < NbLetters; x++)
                    {
                        Amount[x] = int.Parse(v[x]);
                    }
                    sr.ReadLine();


                    // Letter Values 
                    Values = new int[NbLetters];
                    s = sr.ReadLine();
                    v = s.Split(new char[] { ',' });
                    for (int x = 0; x < NbLetters; x++)
                    {
                        Values[x] = int.Parse(v[x]);
                    }
                    s = sr.ReadLine();

                    // ASCII Values 
                    Ascii = new int[NbLetters];
                    s = sr.ReadLine();
                    v = s.Split(new char[] { ',' });
                    for (int x = 0; x < NbLetters; x++)
                    {
                        Ascii[x] = (int)v[x][0];
                    }
                    sr.ReadLine();

                    // Grid Size
                    s = sr.ReadLine();
                    v = s.Split(new char[] { ',' });
                    dimX = int.Parse(v[0]);
                    dimY = int.Parse(v[0]);
                    sr.ReadLine();

                    // Letter Total
                    dimRX = dimX + 2;
                    dimRY = dimY + 2;
                    LetterMultipliers = new int[dimRX * dimRY];
                    WordMultipliers = new int[dimRX * dimRY];


                    for (int x = 0; x < dimRX * dimRY; x++)
                    {
                        LetterMultipliers[x] = 1;
                        WordMultipliers[x] = 1;
                    }

                    s = sr.ReadLine();
                    v = s.Split(new char[] { ',' });
                    int y = 0;
                    while (v[0] == "oo")
                    {
                        for (int x = 0; x < dimRX; x++)
                        {
                            int tgt = y * dimRX + x;
                            switch (v[x])
                            {
                                case "oo":
                                    LetterMultipliers[tgt] = 0;
                                    WordMultipliers[tgt] = 0;
                                    break;
                                case "T2":
                                    LetterMultipliers[tgt] = 2;
                                    break;
                                case "T3":
                                    LetterMultipliers[tgt] = 3;
                                    break;
                                case "T4":
                                    LetterMultipliers[tgt] = 4;
                                    break;
                                case "W2":
                                    WordMultipliers[tgt] = 2;
                                    break;
                                case "W3":
                                    WordMultipliers[tgt] = 3;
                                    break;
                                case "W4":
                                    WordMultipliers[tgt] = 4;
                                    break;

                            }
                        }
                        s = sr.ReadLine();
                        if (s == null) break;
                        v = s.Split(new char[] { ',' });
                        y++;
                    }

                }
            }

            b = null;

            cm.SetBoard(dimX, dimY, LetterMultipliers, WordMultipliers);
            cm.SetTiles(NbLetters, Vowels, Consonants, Amount, Values, Ascii);
        }

        public System.Drawing.Color GetColor(string col)
        {
            Color coll = Color.FromName(col);

            if (coll != Color.Empty) return coll; 


            
            int[] cols = new int[3];

            cols[0] = int.Parse(col.Substring(0, 3));
            cols[1] = int.Parse(col.Substring(3, 3));
            cols[2] = int.Parse(col.Substring(6, 3));

            return System.Drawing.Color.FromArgb(cols[0], cols[1], cols[2]);
        }


    }
}
