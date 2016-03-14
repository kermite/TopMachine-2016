using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{
    public delegate void SearchProgressDelegate(int words, int h, int v);

    public struct _tboard
    {
        public byte[] tiles;
        public int[] cross;
        public int[] point;
        public byte[] joker;
        public byte[] tests;
    }

    public class IBoard
    {
        const int CROSS_MASK = 0x7FFFFFE;

        public CBoardMaker cm;
        public int maxOnBoard;
        public int[] intBonus;
        public int dimh, dimv, realdimh, realdimv;
        public _tboard[] Board;

        public CResults globalResults = new CResults(100);
        public dawgDictionary dico;

        public event SearchProgressDelegate SearchProgress;

        public virtual void SetMaxOnBoard(int x, int[] Bonus)
        {
            maxOnBoard = x;
            intBonus = Bonus;
        }

        public virtual void create(dawgDictionary dico, CBoardMaker cmaker)
        { }

        public virtual void init()
        { }
        public virtual void Unload()

        { }
        public virtual byte tile(int row, int column)

        { return 0; }

        public virtual void copy(IBoard SrcBoard)
        { return; }

        public virtual int joker(int row, int column)
        { return 0; }

        public virtual int vacant(int row, int column)
        { return 0; }

        public virtual void addround(CRound r)
        { }

        public virtual void removeround(CRound r)
        { }

        public virtual void testround(CRound r)
        { }

        public virtual void removetestround()
        { }

        public virtual char gettestchar(int r, int c)
        { return (char) 0; }

        public virtual bool IsWordMultiplier2(int r, int c)
        {
            return false;
        }

        public virtual bool IsWordMultiplier3(int r, int c) 
        {
            return false;
        }

        public virtual int evalRaccordsMots(CRound word)
        { return 0; }

        public virtual int evalRaccordsMots(
            _tboard board,
                    CResults results, CRound word, int row, int col)

        { return 0; }

        // The Search path
        public virtual int evalRaccords(CRound word)
        { return 0; }

        public virtual int evalRaccords(
            _tboard board,
                    CResults results, CRound word)

        { return 0; }

        public virtual int evalRaccordsV(
                _tboard board, CResults results, CRound word)


        { return 0; }

        public virtual void evalmoveandcheck(CRound word, int iturn)
        { }

        public virtual int evalmove(CRound word)
        { return 0; }

        public virtual int evalmove(
_tboard board,
                    CResults results, CRound word)
        { return 0; }

        public virtual void evalwords(
        _tboard board,
        CRound word)
        { }

        public virtual void ExtendRight(
            _tboard board, CRack rack, CRound partialword, uint n, int row, int column, int anchor)
        { }

        public virtual void LeftPart(
            _tboard board, CRack rack, CRound partialword, uint n, int row,
         int anchor, int limit)
        { }

        public virtual void
            search_aux(
                _tboard board, 
                         CRack rack,  int dir)
        { }

        public virtual void search(dawgDictionary dico, CResults Results, CRack rack,
             int OnBoard, int[] Bonus)
        { }

        public virtual void search_first(dawgDictionary dico, CResults Results, CRack rack, int OnBoard, int[] Bonus)
        { }

        public virtual uint lookup(uint t, byte[] s, int pos)
        { return 0; }

        public virtual uint checkout_tile(byte[] tiles, int tndx, byte[] joker, int jndx, int[] points, int pndx)
        { return 0; }

        public virtual  void
        check(byte []tiles,
                    byte []joker,
                    int  []cross,
                    int []point)
        {
        }

        public virtual void buildcross()
        {
        }

        public virtual CRack
        RestFromRound(CRack source, CRound round)
        {
            return null;
        }

    }
}
