using System;
using System.Collections;
using System.Text;

namespace TopMachine.Topping.Dawg
{

    #region public class tresults
    public class tresults
    {
        public SortedList list;

        public int nresults()
        {
            return list.Count;
        }

        public int maxPoints;
        public int minPoints;
        public int sousTop;
    }
    #endregion

    #region public class CResults
    public class CResults
    {
        public tresults Results;

        public int maxRow;
        public int maxCount;
        public int maxPoints;
        public int minPoints;
        public int sousTop;
        public bool isSousTopScrabble;
        public bool isScrabble;

        public CResults(int Count)
        {
            maxCount = Count;
            maxPoints = 0;
            sousTop = 0;
            isSousTopScrabble = false;
            isScrabble = false;
            Results = new tresults();
            Results.list = new System.Collections.SortedList();

        }

        public virtual void create()
        {
            init();
        }


        public virtual void init()
        {
            Results.list.Clear();
        }

        public virtual void Unload()
        {
            Results.list.Clear();
            Results = null;
        }


        //public virtual bool addTest(int pts)
        //{
        //    if (maxPoints > pts)
        //    {
        //        if (sousTop < pts)
        //        {
        //            sousTop = pts;
        //       }
        //        return false;
        //    }

        //    return true;
        //}

        public virtual void add(CRound ro)
        {
            string ss, word;
                     

            if (maxPoints > ro.Round.points)
            {
                if (sousTop < ro.Round.points)
                {
                    sousTop = ro.Round.points;
                    isSousTopScrabble = ro.Round.bonus > 0 && isScrabble;
                }
                return;
            }

            int l = ro.Round.wordlen;
            char[] w = new char[l];

            for (int x = 0; x < l; x++)
            {
                if (ro.Round.word[x] != 0)
                {
                    if ((ro.Round.tileorigin[x] & 4) == 4)
                        w[x] = (char)(ro.Round.word[x] + 96);
                    else
                        w[x] = (char)(ro.Round.word[x] + 64);
                }
                else
                    w[x] = '?';

            }

            word = new string(w, 0, l);
            ss = string.Format("{0:00000}.{1:000}.{2:000}.{3}.{4:00}", 10000 - ro.Round.points, ro.Round.row, ro.Round.column, word, ro.Round.dir);

            if (maxPoints < ro.Round.points)
            {
                Results.list = new System.Collections.SortedList();
                System.GC.Collect();
                sousTop = maxPoints;
                maxPoints = ro.Round.points;
                isSousTopScrabble = isScrabble;
                isScrabble = ro.Round.bonus > 0;
               

            }

            try
            {
                Results.list.Add(ss, ro);
            }
            catch (System.Exception ee)
            {
              //  NLog.LogManager.GetLogger("wcf").ErrorException("CResults : Add", ee);
            }

        }


        public virtual void
        addsorted(CRound ro)
        {
            int i;
            add(ro);
        }


        public virtual CRound
        get(int i)
        {
            if (i >= 0 && i < Results.nresults())
                return (CRound)Results.list.GetByIndex(i);
            return null;
        }


        public virtual CRound getlast()
        {
            if (Results.nresults() > 0)
                return (CRound)Results.list.GetByIndex(Results.nresults() - 1);

            return null;
        }

        public virtual void deletelast()
        {
            if (Results.nresults() > 0)
                Results.list.RemoveAt(Results.nresults() - 1);
        }
    }
    #endregion

    #region public override class CResultsAll : CResults
    public class CResultsAll : CResults
    {
        public CResultsAll(int Count)
            : base(Count)
        {
        }

        public override void add(CRound ro)
        {
            string ss, word;
            int l = ro.Round.wordlen;
            char[] w = new char[l];
            for (int x = 0; x < l; x++)
            {
                if (ro.Round.word[x] != 0)
                {
                    if ((ro.Round.tileorigin[x] & 4) == 4)
                        w[x] = (char)(ro.Round.word[x] + 96);
                    else
                        w[x] = (char)(ro.Round.word[x] + 64);
                }
                else
                    w[x] = '?';

            }

            word = new string(w, 0, l);
            ss = string.Format("{0:00000}.{1:000}.{2:000}.{3}.{4:00}", 10000 - ro.Round.points, ro.Round.row, ro.Round.column, word, ro.Round.dir);

            CRound round = null;

            if (maxPoints < ro.Round.points)
            {
                maxPoints = ro.Round.points;
                isSousTopScrabble = isScrabble;
                isScrabble = ro.Round.bonus > 0;
                
            }

            if (Results.nresults() == maxCount)
            {
                if (ro.Round.points < minPoints)
                {
                    return;
                }

                deletelast();

                round = getlast();
                minPoints = round.Round.points;
            }
            else
            {
                if (ro.Round.points < minPoints)
                {
                    minPoints = ro.Round.points;
                }
            }

            Results.list.Add(ss, ro);
        }
    }
    #endregion

    #region public override class CResultsByRowCol : CResults
    public class CResultsByRowCol : CResults
    {
        public ArrayList list;

        public CResultsByRowCol(int Count, int row)
            : base(Count)
        {
            maxRow = row;
            maxCount = Count;
            maxPoints = 0;

            list = new System.Collections.ArrayList(row * 2);

            for (int x = 0; x < row * 2; x++)
            {
                tresults Results = new tresults();
                Results.list = new System.Collections.SortedList();
                list.Add(Results);
            }
        }

        public override void add(CRound ro)
        {
            char[] ss = new char[200];
            char[] word = new char[200];

            int tgt = 0;
            if (ro.Round.dir == 1)
                tgt = ro.Round.row - 1;
            else
                tgt = maxRow + ro.Round.column - 1;

            tresults Results = (tresults)list[tgt];

            if (Results.maxPoints > ro.Round.points)
            {
                if (Results.sousTop < ro.Round.points)
                {
                    Results.sousTop = ro.Round.points;
                    isSousTopScrabble = ro.Round.bonus > 0 && isScrabble;
                }
                return;
            }

            int l = ro.Round.wordlen;
            for (int x = 0; x < l; x++)
            {
                if (ro.Round.word[x] != 0)
                {
                    if ((ro.Round.tileorigin[x] & 4) == 4)
                        word[x] = (char)(ro.Round.word[x] + 96);
                    else
                        word[x] = (char)(ro.Round.word[x] + 64);
                }
                else
                    word[x] = '?';
            }

            string key = string.Format("{0:00000}.{1:000}.{2:000}.{3}.{4:00}",
                10000 - ro.Round.points,
                ro.Round.row,
                ro.Round.column,
                new string(word, 0, l),
                ro.Round.dir);

            if (Results.maxPoints < ro.Round.points)
            {
                Results.list.Clear();
                Results.sousTop = Results.maxPoints;
                isSousTopScrabble = isScrabble;
                isScrabble = ro.Round.bonus > 0;
                Results.maxPoints = ro.Round.points;
            }

            Results.list.Add(key, ro);
        }

        public override void addsorted(CRound ro)
        {
            int i;
            add(ro);
        }


        public int isIn(int tgt, int i)
        {
            tresults Results = (tresults)list[tgt];
            return Results.nresults();
        }


        public CRound get(int x, int i)
        {
            tresults Results = (tresults)list[x];
            if (i >= 0 && i < Results.nresults())
                return (CRound)Results.list.GetByIndex(i);

            return null;
        }


        public CRound getlast(int x)
        {
            tresults Results = (tresults)list[x];
            if (Results.nresults() > 0)
                return (CRound)Results.list.GetByIndex(Results.nresults() - 1);

            return null;
        }

        public void deletelast(int x)
        {
            tresults Results = (tresults)list[x];
            if (Results.nresults() > 0)
                Results.list.RemoveAt(Results.nresults() - 1);
        }
    }

    #endregion


}
