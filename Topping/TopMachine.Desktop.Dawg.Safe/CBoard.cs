using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{
    public class CBoard : IBoard
    {

        void Check_double()
        {
            /*  String sn = "";
              SortedList sl = new SortedList();
              int c = 0;
              for (int k = 0; k < 2; k++)
              {
                  if (k == 0)
                  {
                      sn += "---------\r\nStart Horizontal \r\n---------\r\n";
                  }
                  else
                  {
                      sn += "---------\r\nStart Vertical \r\n---------\r\n";
                  }
                  for (int i = 1; i <= dimv; i++)
                  {
                      for (int j = 1; j <= dimh; j++)
                      {
                          if (Board[k].tiles[i * realdimh + j] > 0)
                          {
                              sn += string.Format(" {0} ", cm.GetTileAsciiChar((char)Board[k].tiles[i * realdimh + j]));
                          }
                          else
                          {
                              int x = Board[k].cross[i * realdimh + j];
                              if (x == 0) sn += " - ";
                              else
                              {
                                  if (x != DicoConstants.CROSS_MASK)
                                  {
                                      sn += String.Format("{0:000}", c);
                                      string s = "";
                                      for (int xx = 0; xx < cm.NbLetters; xx++)
                                      {
                                          if ((x & (1 << xx)) > 1)
                                          {
                                              s += cm.GetTileAsciiChar((char)xx);
                                          }
                                      }
                                      sl.Add(c, s);
                                      c++; 
                                  }
                                  else
                                  {
                                      sn += "   ";
                                  }
                              }
                          }
                      }
                      sn += "\r\n";
                  }

                  sn += "\r\n";
                  foreach (int x in sl.Keys)
                  {
                      sn += x.ToString() + " :  " + sl[x] + "\r\n";
                  }
              }

              string fn = "c:\\Test.txt";
              System.IO.StreamWriter sw = null;
              if (System.IO.File.Exists(fn))
              {
                  sw = System.IO.File.AppendText(fn);
              }
              else
              {
                  sw = System.IO.File.CreateText(fn);
              }
              sw.Write(sn);
              sw.Close();
              Console.Write(sn);
            */
        }

        public override void create(dawgDictionary dico, CBoardMaker cmaker)
        {
            this.dico = dico;
            cm = cmaker;
            Board = new _tboard[2];
            Board[0] = new _tboard();
            Board[1] = new _tboard();

            dimh = cm.SizeH;
            dimv = cm.SizeV;
            realdimh = dimh + 2;
            realdimv = dimv + 2;
            init();
            return;
        }

        public override void copy(IBoard SrcBoard)
        {

            Board = new _tboard[2];
            Board[0] = new _tboard();
            Board[1] = new _tboard();
            cm = SrcBoard.cm;

            dimh = SrcBoard.cm.SizeH;
            dimv = SrcBoard.cm.SizeV;
            realdimh = dimh + 2;
            realdimv = dimv + 2;
            int sizedim = (dimh + 2) * (dimv + 2);

            init();
            Array.Copy(Board[0].joker, Board[0].joker, sizedim);
            Array.Copy(Board[0].cross, Board[0].cross, sizedim);
            Array.Copy(Board[0].point, Board[0].point, sizedim);
            Array.Copy(Board[0].tests, Board[0].tests, sizedim);
            Array.Copy(Board[0].tiles, Board[0].tiles, sizedim);
            Array.Copy(Board[1].joker, Board[1].joker, sizedim);
            Array.Copy(Board[1].cross, Board[1].cross, sizedim);
            Array.Copy(Board[1].point, Board[1].point, sizedim);
            Array.Copy(Board[1].tests, Board[1].tests, sizedim);
            Array.Copy(Board[1].tiles, Board[1].tiles, sizedim);
            return;
        }

        public override void init()
        {
            int i, j;

            Board = new _tboard[2];
            Board[0] = new _tboard();
            Board[1] = new _tboard();

            int sizedim = (dimh + 2) * (dimv + 2);
            Board[0].joker = new byte[sizedim];
            Board[0].cross = new int[sizedim];
            Board[0].point = new int[sizedim];
            Board[0].tiles = new byte[sizedim];
            Board[0].tests = new byte[sizedim];
            Board[1].joker = new byte[sizedim];
            Board[1].cross = new int[sizedim];
            Board[1].point = new int[sizedim];
            Board[1].tiles = new byte[sizedim];
            Board[1].tests = new byte[sizedim];

            for (i = 1; i <= dimh; i++)
                for (j = 1; j <= dimv; j++)
                {
                    Board[0].joker[i * realdimh + j] = 0;
                    Board[0].cross[i * realdimh + j] = DicoConstants.CROSS_MASK;
                    Board[0].point[i * realdimh + j] = -1;

                    Board[1].joker[i * realdimh + j] = 0;
                    Board[1].cross[i * realdimh + j] = DicoConstants.CROSS_MASK;
                    Board[1].point[i * realdimh + j] = -1;
                }
        }

        public override void Unload()
        {
        }

        public override byte tile(int row, int column)
        {
            return Board[0].tiles[row * realdimh + column];
        }


        public override int joker(int row, int column)
        {
            return Board[0].joker[row * realdimh + column];
        }


        public override int vacant(int row, int column)
        {
            if (row < 1 || row > dimv ||
                column < 1 || column > dimh)
                return 0;
            return (Board[0].tiles[row * realdimh + column] == 0) ? 1 : 0;
        }


        public override void addround(CRound r)
        {
            byte t;
            int row, column, i;

            row = r.row();
            column = r.column();
            if (r.dir() == DicoConstants.HORIZONTAL)
            {
                for (i = 0; i < r.wordlen(); i++)
                {
                    if (Board[0].tiles[row * realdimh + column + i] == 0)
                    {
                        t = r.gettile(i);
                        Board[0].tiles[row * realdimh + column + i] = t;
                        Board[0].joker[row * realdimh + column + i] = r.joker(i);
                        Board[1].tiles[(column + i) * realdimh + row] = t;
                        Board[1].joker[(column + i) * realdimh + row] = r.joker(i);
                    }
                }
            }
            else
            {
                for (i = 0; i < r.wordlen(); i++)
                {
                    if (Board[1].tiles[column * realdimh + row + i] == 0)
                    {
                        t = r.gettile(i);
                        Board[0].tiles[(row + i) * realdimh + column] = t;
                        Board[0].joker[(row + i) * realdimh + column] = r.joker(i);
                        Board[1].tiles[column * realdimh + row + i] = t;
                        Board[1].joker[column * realdimh + row + i] = r.joker(i);
                    }
                }
            }
            buildcross();
            Check_double();
        }


        public override void removeround(CRound r)
        {
            int row, column, i;

            row = r.row();
            column = r.column();
            if (r.dir() == DicoConstants.HORIZONTAL)
            {
                for (i = 0; i < r.wordlen(); i++)
                    if (r.playedfromrack(i) == 1)
                    {
                        Board[0].tiles[row * realdimh + column + i] = 0;
                        Board[0].joker[row * realdimh + column + i] = 0;
                        Board[1].tiles[(column + i) * realdimh + row] = 0;
                        Board[1].joker[(column + i) * realdimh + row] = 0;
                    }
            }
            else
            {
                for (i = 0; i < r.wordlen(); i++)
                    if (r.playedfromrack(i) == 1)
                    {
                        Board[0].tiles[(row + i) * realdimh + column] = 0;
                        Board[0].joker[(row + i) * realdimh + column] = 0;
                        Board[1].tiles[column * realdimh + row + i] = 0;
                        Board[1].joker[column * realdimh + row + i] = 0;
                    }
            }
            buildcross();
        }


        public override void testround(CRound r)
        {
            byte t;
            int row, column, i;

            row = r.row();
            column = r.column();
            if (r.dir() == DicoConstants.HORIZONTAL)
            {
                for (i = 0; i < r.wordlen(); i++)
                    if (Board[0].tiles[row * realdimh + column + i] == 0)
                    {
                        t = r.gettile(i);
                        Board[0].tiles[row * realdimh + column + i] = t;
                        Board[0].joker[row * realdimh + column + i] = r.joker(i);
                        Board[0].tests[row * realdimh + column + i] = 1;

                        Board[1].tiles[(column + i) * realdimh + row] = t;
                        Board[1].joker[(column + i) * realdimh + row] = r.joker(i);
                        Board[1].tests[row * realdimh + column + i] = 1;
                    }
            }
            else
            {
                for (i = 0; i < r.wordlen(); i++)
                    if (Board[0].tiles[(row + i) * realdimh + column] == 0)
                    {
                        t = r.gettile(i);
                        Board[0].tiles[(row + i) * realdimh + column] = t;
                        Board[0].joker[(row + i) * realdimh + column] = r.joker(i);
                        Board[0].tests[(row + i) * realdimh + column] = 1;

                        Board[1].tiles[column * realdimh + row + i] = t;
                        Board[1].joker[column * realdimh + row + i] = r.joker(i);
                        Board[1].tests[(row + i) * realdimh + column] = 1;
                    }
            }
        }


        public override void removetestround()
        {
            int i, j;
            for (i = 1; i <= dimv; i++)
                for (j = 1; j <= dimh; j++)
                {
                    if (Board[0].tests[i * realdimh + j] == 1)
                    {
                        Board[0].tiles[i * realdimh + j] = 0;
                        Board[0].tests[i * realdimh + j] = 0;
                        Board[0].joker[i * realdimh + j] = 0;

                        Board[1].tiles[j * realdimh + i] = 0;
                        Board[1].joker[j * realdimh + i] = 0;
                        Board[1].tests[i * realdimh + j] = 0;
                    }
                }
        }


        public override char gettestchar(int r, int c)
        {
            return (char)Board[0].tests[r * realdimh + c];
        }

        // The Search path
        public override int evalRaccords(CRound word)
        {
            buildcross();
            if (word.dir() == DicoConstants.VERTICAL)
            {
                return evalRaccordsV(Board[1],
                          null, word);
            }
            else
            {
                return evalRaccords(Board[0],
                          null, word);
            }
        }

        public override int evalRaccordsV(
                    _tboard board,
                    CResults results, CRound word)
        {

            int countWord = 0;
            int len = word.wordlen();
            int row = word.row();
            int col = word.column();

            for (int i = 0; i < len; i++)
            {
                int start = col;
                int end = col;

                if (board.tiles[(row + i) * realdimh + start] != 0)
                {
                    continue;
                }

                while (start > 0 && board.tiles[(row + i) * realdimh + start - 1] > 0)
                {
                    start--;
                }

                while (board.tiles[(row + i) * realdimh + end + 1] > 0)
                {
                    end++;
                }

                if (start != end)
                {
                    int countLength = 0;

                    for (int x = start; x <= end; x++)
                    {
                        if (board.tiles[(row + i) * realdimh + x] != 0)
                        {
                            countLength++;
                        }
                    }

                    if (countLength == 1)
                    {
                        countWord++;
                    }

                    if (countLength > 1)
                    {
                        countWord += 2;
                    }
                }
            }

            return countWord;
        }

        #region evalRaccords Mots
        public override int evalRaccordsMots(CRound word)
        {
            buildcross();
            if (word.dir() == DicoConstants.VERTICAL)
            {
                return evalRaccordsMots(Board[1],
                          null, word, word.column(), word.row());
            }
            else
            {
                return evalRaccordsMots(Board[0],
                          null, word, word.row(), word.column());
            }
        }


        public override int evalRaccordsMots(
                    _tboard board,
                    CResults results, CRound word, int row, int col)
        {
            int countWord = 0;
            int len = word.wordlen();

            for (int i = 0; i < len; i++)
            {
                int start = row;
                int end = row;

                if (board.tiles[start * realdimh + col + i] != 0)
                {
                    continue;
                }

                while (start > 0 && board.tiles[(start - 1) * realdimh + col + i] > 0)
                {
                    start--;
                }

                while (board.tiles[(end + 1) * realdimh + col + i] > 0)
                {
                    end++;
                }

                if (start != end)
                {
                    int countLength = 0;

                    for (int x = start; x <= end; x++)
                    {
                        if (board.tiles[x * realdimh + col + i] != 0)
                        {
                            countLength++;
                        }
                    }

                    if (countLength > 0)
                    {
                        countWord++;
                    }
                }
            }

            return countWord;
        }
        #endregion

        public override int evalRaccords(
                    _tboard board,
                    CResults results, CRound word)
        {
            int countWord = 0;
            int len = word.wordlen();
            int row = word.row();
            int col = word.column();

            for (int i = 0; i < len; i++)
            {
                int start = row;
                int end = row;

                if (board.tiles[start * realdimh + col + i] != 0)
                {
                    continue;
                }

                while (start > 0 && board.tiles[(start - 1) * realdimh + col + i] > 0)
                {
                    start--;
                }

                while (board.tiles[(end + 1) * realdimh + col + i] > 0)
                {
                    end++;
                }

                if (start != end)
                {
                    int countLength = 0;

                    for (int x = start; x <= end; x++)
                    {
                        if (board.tiles[x * realdimh + col + i] != 0)
                        {
                            countLength++;
                        }
                    }

                    if (countLength == 1)
                    {
                        countWord++;
                    }

                    if (countLength > 1)
                    {
                        countWord += 2;
                    }
                }
            }

            return countWord;
        }

        // The Search path
        public override void evalmoveandcheck(CRound word, int iTurn)
        {
            buildcross();
            if (word.dir() == DicoConstants.VERTICAL)
            {
                if (evalmove(Board[1],
                          null, word) == 0 && iTurn > 1)
                {
                    word.setpoints(0);
                    word.setbonus(0, intBonus);
                }
                else
                {
                    evalwords(Board[1], word);
                }

            }
            else
            {
                if (
                        evalmove(Board[0],
                          null, word) == 0 && iTurn > 1)
                {
                    word.setpoints(0);
                    word.setbonus(0, intBonus);
                }
                else
                {
                    evalwords(Board[0], word);
                }
            }

            if (iTurn == 1)
            {
                int len = word.wordlen();
                int col = word.column();
                int row = word.row();

                if (row != (dimv + 1) / 2)
                {
                    word.setpoints(0);
                    word.setbonus(0, intBonus);
                }
                else
                {
                    if (col > (dimh + 1) / 2 || col + len <= (dimh + 1) / 2)
                    {
                        word.setpoints(0);
                        word.setbonus(0, intBonus);
                    }
                }
            }
        }

        public override void evalwords(
            _tboard board,
                    CRound word)
        {

			System.String s = word.getword();
            uint c = dico.SearchWord(s);

            if (c == 0)
            {
                word.ErrorList.Add(s);
                word.setpoints(0);
                word.setbonus(0, intBonus);
            }

            int len = word.wordlen();
            int row = word.row();
            int col = word.column();

            for (int i = 0; i < len; i++)
            {
                int start = row;
                int end = row;

                while (start > 0 && board.tiles[(start - 1) * realdimh + col + i] > 0)
                {
                    start--;
                }

                while (board.tiles[(end + 1) * realdimh + col + i] > 0)
                {
                    end++;
                }

                if (start != end)
                {
                    char[] newWord = new char[31];

                    int countWord = 0;

                    for (int x = start; x <= end; x++)
                    {
                        if (board.tiles[x * realdimh + col + i] == 0)
                        {
                            newWord[countWord++] = (char)(word.gettile(i) + 64);
                        }
                        else
                        {
                            newWord[countWord++] = (char)(board.tiles[x * realdimh + col + i] + 64);
                        }
                    }

                    System.String sss = new System.String(newWord, 0, countWord);

                    c = dico.SearchWord(sss);
                    if (c == 0)
                    {
                        word.ErrorList.Add(sss);
                        word.setpoints(0);
                        word.setbonus(0, intBonus);
                    }
                }
            }
        }



        public override int evalmove(CRound word)
        {

			buildcross();
            if (word.dir() == DicoConstants.VERTICAL)
                return evalmove(Board[1],
                          null, word);
            else
                return evalmove(Board[0],
                          null, word);
        }

        // The Search path
        public override int evalmove(
                    _tboard board,
                    CResults results, CRound word)
        {
            int isOk = 0;
            int i, pts, ptscross;
            int l, t, fromrack;
            int len, row, col, wordmul;

            fromrack = 0;
            pts = 0;
            ptscross = 0;
            wordmul = 1;

            len = word.wordlen();
            row = word.row();
            col = word.column();

            for (i = 0; i < len; i++)
            {
                if (board.tiles[row * realdimh + col + i] > 0)
                {
                    isOk = 1;
                    if (board.joker[row * realdimh + col + i] == 0)
                        pts += cm.GetTilePoints(word.gettile(i));
                }
                else
                {
                    if (word.joker(i) == 0)
                        l = cm.GetTilePoints(word.gettile(i)) * cm.GetTileMultiplier(col + i, row);
                    else
                        l = 0;

                    pts += l;
                    wordmul *= cm.GetWordMultiplier(col + i, row);


					t = board.point[row * realdimh + col + i];
                    if (t >= 0)
                    {
                        ptscross += (t + l) * cm.GetWordMultiplier(col + i, row);
                        isOk = 1;
                    }

                    fromrack++;
                }
            }


            if (fromrack <= maxOnBoard)
            {
                pts = ptscross + pts * wordmul + intBonus[fromrack];
                word.setbonus(fromrack, intBonus); // 50 * (fromrack == 7) + 75 * (fromrack == 8));
                word.setpoints(pts);

                if (word.dir() == DicoConstants.VERTICAL)
                {
                    word.setrow(col);
                    word.setcolumn(row);
                }

                CRound c = new CRound(cm);
                c.create();
                c.copy(word.Round);

                if (results != null)
                    results.addsorted(c);

                if (word.dir() == DicoConstants.VERTICAL)
                {
                    word.setrow(row);
                    word.setcolumn(col);
                }
            }

            return isOk;
        }
        public override bool IsWordMultiplier2(int col, int row)
        {
            int x = cm.GetWordMultiplier(col, row);
            return x == 2;
        }

        public override bool IsWordMultiplier3(int col,int row) 
        {
           int x  = cm.GetWordMultiplier(col,row);
           return x == 3;
        }

        public override void ExtendRight(_tboard board,
                     CRack rack, CRound partialword,
                      uint n, int row, int column, int anchor)
        {
            char l;
            uint succ;

            if (row > dimh + 1 || column > dimh + 1) return;

            if (board.tiles[row * realdimh + column] == 0)
            {

                if (dico.Word(n) > 0 && column > anchor)
                {
                    evalmove(board, globalResults, partialword);
                }

                for (succ = dico.Succ(n); succ > 0; succ = dico.Next(succ))
                {
                    l = dico.Chr(succ);

                    /*if (board.cross[column * realdimh + row] > 0 && board.cross[row * realdimh + column] != DicoConstants.CROSS_MASK)
                    {
                        Console.Write("Temporary Code"); 
                    }*/

                    if ((board.cross[row * realdimh + column] & (1 << l)) > 0)
                    {
                        if (rack.isin(l) > 0)
                        {
                            rack.remove(l);
                            partialword.addrightfromrack(l, 0);
                            ExtendRight(board,
                                        rack, partialword,
                                                    succ, row, column + 1, anchor);
                            partialword.removerighttorack((char)1, 0);
                            rack.add(l);
                        }
                        if (rack.isin((byte)DicoConstants.JOKER_TILE) > 0)
                        {
                            rack.remove((byte)DicoConstants.JOKER_TILE);
                            partialword.addrightfromrack(l, 1);
                            ExtendRight(board, rack, partialword, succ, row, column + 1, anchor);
                            partialword.removerighttorack(l, 1);
                            rack.add(DicoConstants.JOKER_TILE);
                        }
                    }
                }
            }
            else
            {
                l = (char)board.tiles[row * realdimh + column];
                for (succ = dico.Succ(n); succ > 0; succ = dico.Next(succ))
                {
                    if (dico.Chr(succ) == l)
                    {
                        partialword.addrightfromboard(l);
                        ExtendRight(board,
                                    rack, partialword, succ, row, column + 1, anchor);
                        partialword.removerighttoboard(l);
                    }
                }
            }
        }


        public override void LeftPart(_tboard board,
                 CRack rack, CRound partialword,
                 uint n, int row, int anchor, int limit)
        {
            char l;
            uint succ;

            ExtendRight(board, rack,
                        partialword, n, row, anchor, anchor);

            if (limit > 0)
            {
                for (succ = dico.Succ(n); succ > 0; succ = dico.Next(succ))
                {
                    l = dico.Chr(succ);
                    if (l > 0)
                    {
                        if (rack.isin(l) > 0)
                        {
                            rack.remove(l);
                            partialword.addrightfromrack(l, 0);
                            partialword.setcolumn(partialword.column() - 1);
                            LeftPart(board,
                                     rack, partialword,
                                     succ, row, anchor, limit - 1);
                            partialword.setcolumn(partialword.column() + 1);
                            partialword.removerighttorack(l, 0);
                            rack.add(l);
                        }
                        if (rack.isin(DicoConstants.JOKER_TILE) > 0)
                        {
                            rack.remove(DicoConstants.JOKER_TILE);
                            partialword.addrightfromrack(l, 1);
                            partialword.setcolumn(partialword.column() - 1);
                            LeftPart(board, rack, partialword,
                                     succ, row, anchor, limit - 1);
                            partialword.setcolumn(partialword.column() + 1);
                            partialword.removerighttorack(l, 1);
                            rack.add(DicoConstants.JOKER_TILE);
                        }
                    }
                }
            }
        }


        public override void search_aux(_tboard board, CRack rack, int dir)
        {
            int row, column, lastanchor;
            CRound partialword = new CRound(cm);

            partialword.create();
            for (row = 1; row <= dimv; row++)
            {
                partialword.init();
                partialword.setdir(dir);
                partialword.setrow(row);
                lastanchor = 0;
                for (column = 1; column <= dimh; column++)
                {
                    if (board.tiles[row * realdimh + column] == 0 &&
                        (board.tiles[row * realdimh + column - 1] > 0
                         || board.tiles[row * realdimh + column + 1] > 0 ||
                         board.tiles[(row - 1) * realdimh + column] > 0 ||
                         board.tiles[(row + 1) * realdimh + column] > 0))
                    {
                        if (board.tiles[row * realdimh + column - 1] > 0)
                        {
                            partialword.setcolumn(lastanchor + 1);
                            ExtendRight(board, rack, partialword,
                                        dico.Root(), row, lastanchor + 1, column);
                        }
                        else
                        {
                            partialword.setcolumn(column);
                            LeftPart(board, rack, partialword,
                                     dico.Root(), row, column, column -
                                     lastanchor - 1);
                        }
                        lastanchor = column;
                    }
                }
            }
            //	delete partialword;
        }

        public override void search(dawgDictionary dic, CResults results, CRack rack, int OnBoard, System.Int32[] Bonus)
        {
            dico = dic;
            globalResults = results;

            maxOnBoard = OnBoard;
            intBonus = Bonus;

            search_aux(Board[0],
                     rack, DicoConstants.HORIZONTAL);

            search_aux(Board[1],
                     rack, DicoConstants.VERTICAL);
        }

        public override void search_first(dawgDictionary dic, CResults results, CRack rack, int OnBoard, System.Int32[] Bonus)
        {
            dico = dic;
            globalResults = results;

            maxOnBoard = OnBoard;
            intBonus = Bonus;

            CRound partialword = new CRound(cm);
            int row = (dimv + 1) / 2, column = (dimh + 1) / 2;

            partialword.create();
            partialword.setrow(row);
            partialword.setcolumn(column);
            partialword.setdir(DicoConstants.HORIZONTAL);

            LeftPart(Board[0],
                     rack, partialword, dic.Root(), row, column,
                     rack.ntiles() - 1);

        }

        public override uint lookup(uint t, byte[] s, int pos)
        {

            uint p;
            //begin:
            bool restart = true;
            while (restart)
            {
                restart = false;
                if (s[pos] == 0)
                    return t;
                if (dico.Succ(t) == 0)
                    return uint.MaxValue;

                p = dico.Succ(t);
                do
                {
                    if (dico.Chr(p) == s[pos])
                    {
                        t = p;
                        pos++;
                        restart = true;
                        break;
                        //goto begin;
                    }
                    else if (dico.Last(p) > 0)
                    {
                        return uint.MaxValue;
                    }
                    p = dico.Next(p);
                } while (true);
            }
            return uint.MaxValue;
        }

        public override uint checkout_tile(byte[] tiles, int tndx, byte[] joker, int jndx, int[] points, int pndx)
        {
            uint node, succ;
            uint mask;

            int t = tndx;
            int j = jndx;

            mask = 0;
            points[pndx] = 0;

            while (tiles[t - 1] > 0)
            {
                j--;
                t--;
                if ((joker[j]) == 0)
                    (points[pndx]) += cm.GetTilePoints(tiles[t]);
            }

			node = lookup(dico.Root(), tiles, t);
			if (node == uint.MaxValue)
				return 0;

			//node = 0;

            for (succ = dico.Succ(node); succ > 0; succ = dico.Next(succ))
            {
                uint lkup = lookup(succ, tiles, tndx + 1);
                if (lkup != uint.MaxValue && dico.Word(lkup) > 0)
                {
                    mask = mask | (uint)(1 << dico.Chr(succ));
                }
                if (dico.Last(succ) > 0)
                    break;
            }

            t = tndx;
            j = jndx;
            while (tiles[t + 1] > 0)
            {
                j++;
                t++;
                if (joker[j] == 0)
                {
                    (points[pndx]) += cm.GetTilePoints(tiles[t]);
                }
            }

            return mask;
        }


        public override void
        check(byte[] tiles,
                    byte[] joker,
                    int[] cross,
                    int[] point)
        {
            int i, j;

            for (i = 1; i <= dimv; i++)
            {
                for (j = 1; j <= dimh; j++)
                {
                    point[j * realdimh + i] = -1;
                    if (tiles[i * realdimh + j] > 0)
                    {
                        cross[j * realdimh + i] = 0;
                    }
                    else if (tiles[i * realdimh + j - 1] > 0 ||
                             tiles[i * realdimh + j + 1] > 0)
                    {
                        cross[j * realdimh + i] =
                           (int)checkout_tile(tiles, i * realdimh + j,

                           joker, i * realdimh + j,
                                         point, j * realdimh + i);
                    }
                    else
                    {
                        cross[j * realdimh + i] = DicoConstants.CROSS_MASK;
                    }
                }
            }
        }


        public override void buildcross()
        {
            check(Board[0].tiles, Board[0].joker, Board[1].cross, Board[1].point);
            check(Board[1].tiles, Board[1].joker, Board[0].cross, Board[0].point);

        }

        public override CRack
        RestFromRound(CRack source, CRound round)
        {
            byte i;
            CRack r;

            r = new CRack(cm);
            r.init();
            r.copy(source);

            for (i = 0; i < round.wordlen(); i++)
            {
                if (round.playedfromrack(i) > 0)
                {
                    if (round.joker(i) > 0)
                        r.remove(DicoConstants.JOKER_TILE);
                    else
                        r.remove(round.gettile(i));

                }
            }

            return r;

        }

    }
}
