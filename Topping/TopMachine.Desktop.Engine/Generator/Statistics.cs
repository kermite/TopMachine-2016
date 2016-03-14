using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Dawg;

namespace TopMachine.Topping.Engine.GameController
{
  public  class Statistics
    {
        private int[] Lst_Cpt_Scrabble;
        private int Total_Scrabble;
        private int Cpt_ScrabbleUnic;
        private int Cpt_ScrabbleNonuple;
        private int Cpt_ScrabbleQuadruple;
        private int Cpt_NonScrabbleQuadruple;
        private int Cpt_NonScrabbleNonuple;
        private int ScorePoint;
        private int nbStat;
        //private int ScorePointAverage;

        public Statistics() 
        {
            Lst_Cpt_Scrabble = new int[9]{0,0,0,0,0,0,0,0,0};
            Total_Scrabble = 0;
            Cpt_ScrabbleUnic = 0;
            Cpt_ScrabbleNonuple = 0;
            Cpt_ScrabbleQuadruple = 0;
            Cpt_NonScrabbleNonuple = 0;
            Cpt_NonScrabbleQuadruple = 0;
            ScorePoint = 0;
            nbStat = 0;
        }

        public void AnalyzeGame(CRound lgc,IBoard b,CResults r) 
        {
            ScorePoint = ScorePoint + lgc.points();
            nbStat = 1;
            if (IsScrabble(lgc)) 
            {
                Total_Scrabble++;
                Lst_Cpt_Scrabble[lgc.wordlen() - 7 ]++;
            }
         
            if (IsScrabbleUnic(lgc,r)) 
            {
                Cpt_ScrabbleUnic++;
            }
            int val;
            val = IsScrabbleNonuple(lgc,b); 
            if (val == 1)
            {
                Cpt_ScrabbleNonuple++;
            }
            else if (val == 2) 
            {
                Cpt_NonScrabbleNonuple++;
            }


            val = IsScrabbleQuadruple(lgc, b); 
            if (val == 1)
            {
                Cpt_ScrabbleQuadruple++;
            }
            else if (val == 2) 
            {
                Cpt_NonScrabbleQuadruple++;
            }
        
        }

        public bool IsScrabble(CRound lgc) 
        {
            return lgc.bonus() > 0;
            
        }


        public bool IsScrabbleUnic(CRound lgc,CResults r) 
        {
            if (IsScrabble(lgc))
            {
                if (r.Results.list.Count > 1)
                {
                    return false;
                }
                else
                {
                    return !r.isSousTopScrabble;
                }
            }

            return false; 
        }
        //private int Getcase3xNear(int poscurrent) 
        //{
        //    if (poscurrent == 1) 
        //    {
        //        return 1;
        //    }
        //    else if (poscurrent <= 8)
        //    {
        //        return 8;
        //    }
        //    else 
        //    {
        //        return 15;
        //    }
        //}
        //private bool Get2case3x(int poscurrent,int lngWord) 
        //{ int case3xNear;

        //    case3xNear = Getcase3xNear(poscurrent); 
        //    return (poscurrent + lngWord) >= case3xNear + 7;
            
        //}
     
        /// <summary>
        ///  un scrabble positionne a une place bien definie et qu'aucune lettre n'est sur les cases rouges
        /// </summary>
        /// <param name="lgc"></param>
        /// <returns></returns>
        public int IsScrabbleNonuple(CRound lgc,IBoard b)
        {
            int position, row, column;

            position = lgc.dir();
            row = lgc.row();
            column = lgc.column(); 
 
          
               

                //return isPlaceNonuple(lgc,1, 1, DicoConstants.HORIZONTAL,b)
                //   || isPlaceNonuple(lgc,8, 1, DicoConstants.HORIZONTAL,b)  
                //   || isPlaceNonuple(lgc,1, 1, DicoConstants.VERTICAL,b)
                //   || isPlaceNonuple(lgc,1, 8, DicoConstants.VERTICAL,b) 
                //   || isPlaceNonuple(lgc,1, 15, DicoConstants.HORIZONTAL,b) 
                //   || isPlaceNonuple(lgc,8, 15, DicoConstants.HORIZONTAL,b) 
                //   || isPlaceNonuple(lgc,15, 1, DicoConstants.VERTICAL,b) 
                //   || isPlaceNonuple(lgc,15, 8, DicoConstants.VERTICAL,b);

                int cptCase3x = 0;
                if (position == DicoConstants.HORIZONTAL)
                {
                    for (int i = 0; i < lgc.wordlen(); i++)
                    {
                        if (b.vacant(row, i + column) == 1 && b.IsWordMultiplier3(row, i + column))
                        {
                            cptCase3x++;
                        }
                    }
                }
                if (position == DicoConstants.VERTICAL)
                {
                    for (int i = 0; i < lgc.wordlen(); i++)
                     {                       
                        if (b.vacant(row + i, column) == 1 && b.IsWordMultiplier3(row + i, column))
                        {
                            cptCase3x++;
                        }
                    }
                }
                
                if (cptCase3x >= 2)
                {
                    if (IsScrabble(lgc))
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                
            return 0;
        }

        public int IsScrabbleQuadruple(CRound lgc, IBoard b)
        {
            int position, row, column;

            position = lgc.dir();
            row = lgc.row();
            column = lgc.column();

          


                //return isPlaceNonuple(lgc,1, 1, DicoConstants.HORIZONTAL,b)
                //   || isPlaceNonuple(lgc,8, 1, DicoConstants.HORIZONTAL,b)  
                //   || isPlaceNonuple(lgc,1, 1, DicoConstants.VERTICAL,b)
                //   || isPlaceNonuple(lgc,1, 8, DicoConstants.VERTICAL,b) 
                //   || isPlaceNonuple(lgc,1, 15, DicoConstants.HORIZONTAL,b) 
                //   || isPlaceNonuple(lgc,8, 15, DicoConstants.HORIZONTAL,b) 
                //   || isPlaceNonuple(lgc,15, 1, DicoConstants.VERTICAL,b) 
                //   || isPlaceNonuple(lgc,15, 8, DicoConstants.VERTICAL,b);

                int cptCase2x = 0;
                if (position == DicoConstants.HORIZONTAL)
                {
                    for (int i = 0; i < lgc.wordlen(); i++)
                    {
                        if (b.vacant(row, i + column) == 1 && b.IsWordMultiplier2(row, i + column))
                        {
                            cptCase2x++;
                        }
                    }
                }
                if (position == DicoConstants.VERTICAL)
                {
                    for (int i = 0; i < lgc.wordlen(); i++)
                    {                      
                        if (b.vacant(row + i, column) == 1 && b.IsWordMultiplier2(row + i, column))
                        {
                            cptCase2x++;
                        }
                    }
                }

              if(cptCase2x >= 2) 
              {
                if (IsScrabble(lgc))
                {
                    return 1;
                }
                else 
                {
                    return 2;
                }
              }
             
            return 0;
                
            
           
        }
        public static Statistics operator + (Statistics s1,Statistics s2) 
        {
            Statistics result = new Statistics();

            result.Cpt_ScrabbleNonuple = s1.Cpt_ScrabbleNonuple + s2.Cpt_ScrabbleNonuple;
            result.Cpt_ScrabbleQuadruple = s1.Cpt_ScrabbleQuadruple + s2.Cpt_ScrabbleQuadruple;
            result.Cpt_NonScrabbleNonuple = s1.Cpt_NonScrabbleNonuple + s2.Cpt_NonScrabbleNonuple;
            result.Cpt_NonScrabbleQuadruple = s1.Cpt_NonScrabbleQuadruple + s2.Cpt_NonScrabbleQuadruple;
            result.Cpt_ScrabbleUnic = s1.Cpt_ScrabbleUnic + s2.Cpt_ScrabbleUnic;
            result.Total_Scrabble = s1.Total_Scrabble + s2.Total_Scrabble;
            result.Lst_Cpt_Scrabble = new int[s1.Lst_Cpt_Scrabble.Length];
            result.ScorePoint = (s1.ScorePoint + s2.ScorePoint);
            result.nbStat = s1.nbStat + s2.nbStat;  
           
            int max = s1.Lst_Cpt_Scrabble.Length;
            for (int i = 0; i < max; i++)
            {
                result.Lst_Cpt_Scrabble[i] = s1.Lst_Cpt_Scrabble[i] + s2.Lst_Cpt_Scrabble[i];    
            }
            
            return result;
        }

        public override string ToString()
        {
            string s;

            s = "total des Scrabbles : " + Total_Scrabble + "\r\n";
            s += "total des Scrables nonuples : " + Cpt_ScrabbleNonuple + "\r\n";
            s += "total des scrabble quadruples : " + Cpt_ScrabbleQuadruple + "\r\n";
            s += "total des Scrabbles uniques : " + Cpt_ScrabbleUnic + "\r\n";
            s += "total des Non Scrabbles nonuples : " + Cpt_NonScrabbleNonuple + "\r\n";
            s += "total des Non scrabbles quadruples : " + Cpt_NonScrabbleQuadruple + "\r\n";
            int max = Lst_Cpt_Scrabble.Length;
  
            for (int i = 0; i < max; i++)
            {
                if (Lst_Cpt_Scrabble[i] > 0)
                {
                    s += "total Scrabble de longueur " + (i + 7) + " : " + Lst_Cpt_Scrabble[i] + "\r\n";
                }
            }
            s += "Top : " + ScorePoint + "\r\n"; 
            s += "Moyenne : " + (int)ScorePoint / nbStat ; 
            return s;
        }
    }
}
