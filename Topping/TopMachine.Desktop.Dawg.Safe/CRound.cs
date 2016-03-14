using System;
using System.Collections.Generic;
using System.Text;

namespace TopMachine.Topping.Dawg
{
    public class tround : IDisposable 
    {
        public tround()
        {
            word = new char[DicoConstants.ROUND_INTERNAL_MAX];
            tileorigin = new byte[DicoConstants.ROUND_INTERNAL_MAX];
        }

        public void init()
        {
            for (int x = 0; x < DicoConstants.ROUND_INTERNAL_MAX; x++)
            {
                word[x] = (char)0;
                tileorigin[x] = 0;
            }

            wordlen = 0;
            row = 1;
            column = 1;
            dir = DicoConstants.HORIZONTAL;
            points = 0;
            bonus = 0;
            heuristic = 0;
        }

        public char[] word;       //[ROUND_INTERNAL_MAX];
        public byte[] tileorigin; //[ROUND_INTERNAL_MAX];

        public int wordlen;
        public int row, column;
        public int dir;
        public int points;
        public int bonus;
        public int heuristic;

        public void Dispose()
        {
            word = null;
            tileorigin = null;
        }
    };

    public class CRound : IDisposable 
    {
        public System.Collections.ArrayList ErrorList;
        public tround Round;
        public CBoardMaker cm;
        public CRound CopyJoker;
        public int RemoveAfterRound; 
        
        public void copyJoker()
        {
            CopyJoker = new CRound(cm);
            CopyJoker.create();
            CopyJoker.copy(Round);
        }

        public CRound(CBoardMaker cm)
        {
            this.cm = cm;
        }

        public tround create()
        {
            Round = new tround(); // (struct tround *)malloc(sizeof(struct tround));
            init();
            return Round;
        }

        public void init()
        {
            ErrorList = new System.Collections.ArrayList();
            Round.init();
        }


        public void copy(tround source)
        {
            Round.bonus = source.bonus;
            Round.column = source.column;
            Round.dir = source.dir;
            Round.heuristic = source.heuristic;
            Round.points = source.points;
            Round.row = source.row;
            for (int x = 0; x < DicoConstants.ROUND_INTERNAL_MAX; x++)
            {
                Round.word[x] = source.word[x];
                Round.tileorigin[x] = source.tileorigin[x];
            }
            Round.wordlen = source.wordlen;
        }

        public void setword(char[] c)
        {
            for (int x = 0; x < DicoConstants.ROUND_INTERNAL_MAX; x++)
            {
                Round.word[x] = c[x];
            }
        }


        public void setword(System.String s)
        {
            char[] path = s.ToCharArray();
        	
            for (int x = 0; x < s.Length; x++) 
            {
                path[x] = (char) cm.GetTileCode(path[x]); 
            }

            Array.Copy(path, Round.word, s.Length);
            Round.wordlen = s.Length; 
        }

        public System.String getwordwithjoker()
        {
            char[] word = new char[DicoConstants.ROUND_INTERNAL_MAX];

            int l = Round.wordlen;
            for (int x = 0; x < l; x++)
            {
                if (Round.word[x] != 0)
                {
                    if ((Round.tileorigin[x] & 4) == 4)
                        word[x] = (char)(Round.word[x] + (char)96);
                    else
                        word[x] = (char)(Round.word[x] + (char)64);
                }
                else
                    word[x] = '?';
            }

            return new string(word, 0, l);
        }

        public System.String getword()
        {
            char[] word = new char[DicoConstants.ROUND_INTERNAL_MAX];

            for (int x = 0; x < Round.wordlen; x++)
            {
                word[x] = (char)(Round.word[x] + (char)64);
            }

            return new string(word, 0, Round.wordlen);
        }

        public void setrow(int row)
        {
            Round.row = row;
        }


        public void setcolumn(int c)
        {
            Round.column = c;
        }


        public void setpoints(int p)
        {
            Round.points = p;
        }


        public void setdir(int d)
        {
            Round.dir = d;
        }


        public void setbonus(int b, System.Int32[] intBonus)
        {
            Round.bonus = intBonus[b];
        }


        public byte gettile(int n)
        {
            return (byte) Round.word[n];
        }


        public byte joker(int c)
        {
            return (byte) (Round.tileorigin[c] & (byte) DicoConstants.JOKER);
        }


        public int playedfromrack(int c)
        {
            return Round.tileorigin[c] & DicoConstants.FROMRACK;
        }

        public int countfromrack()
        {
            int c = 0;
            for (int x = 0; x < Round.wordlen; x++)
            {
                c += ((Round.tileorigin[x] & DicoConstants.FROMRACK) > 1 ? 1 : 0);
            }

            return c;
        }


        public int wordlen()
        {
            return Round.wordlen;
        }


        public int row()
        {
            return Round.row;
        }


        public int column()
        {

            return Round.column;
        }


        public int points()
        {
            return Round.points;
        }


        public int dir()
        {
            return Round.dir;
        }


        public int bonus()
        {
            return Round.bonus;
        }


        public void addrightfromboard(char c)
        {
            Round.word[Round.wordlen] = c;
            Round.tileorigin[Round.wordlen++] = DicoConstants.FROMBOARD;
        }

        public void SetTileOrigin(int letter, int x)
        {
            Round.tileorigin[letter] = (byte)x;
        }


        public int GetTileOrigin(int letter)
        {
            return (int)Round.tileorigin[letter];
        }

        public void removerighttoboard(char c)
        {
            c++; /* unused */
            Round.wordlen--;
            Round.word[Round.wordlen] = (char)0;
            Round.tileorigin[Round.wordlen] = (byte)0;
        }

        public void addrightfromrack(char c, int j)
        {
            Round.word[Round.wordlen] = c;
            Round.tileorigin[Round.wordlen] = DicoConstants.FROMRACK;
            if (j == 1)
            {
                Round.tileorigin[Round.wordlen] |= DicoConstants.JOKER;
            }
            Round.wordlen++;
        }


        public void removerighttorack(char c, int j)
        {
            Round.wordlen--;
            Round.word[Round.wordlen] = (char)0;
            Round.tileorigin[Round.wordlen] = 0;
            if (j == 1)
            {
                c = (char)DicoConstants.JOKER_TILE;
            }
        }

        public bool Compare(CRound r)
        {
            if (r.Round.column == Round.column &&
                    r.Round.row == Round.row &&
                     r.Round.dir == Round.dir)
            {
                if (r.Round.wordlen == Round.wordlen)
                {
                    string s = getword();
                    string t = r.getword();
                    if (s.CompareTo(t) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Dispose()
        {
            if (ErrorList != null)  ErrorList.Clear();
            ErrorList = null;
            if (Round != null) Round.Dispose();
            cm = null;
            if (CopyJoker != null) CopyJoker.Dispose();  
        }
    }
}
