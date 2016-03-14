


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;




namespace TopMachine.Topping.Dawg
{
    public class WordNdx
    {
        public string Word { get; set; }
        public uint Index { get; set; }
    }

    public struct Dawg_edge
    {
        public UInt32 allptr;

        public uint ptr
        {
            get { return allptr & 0xFFFFFF; }
        }

        public uint term
        {
            get { return allptr >> 24 & 1; }
        }

        public uint last
        {
            get { return allptr >> 25 & 1; }
        }

        public uint fill
        {
            get { return allptr >> 26 & 1; }
        }

        public uint chr
        {
            get { return allptr >> 27; }
        }

    }

    public class Dict_header
    {
        // Size = 36
        public byte[] ident = new byte[22];        // 0 - 21
        public char unused_1;       // 22     
        public char unused_2;       // 23
        public uint root;            // 24 - 25
        public uint nwords;          // 26 - DicoConstants.LETTERS
        public uint edgesused;      // 28
        public uint nodesused;      // 30
        public uint nodessaved;     // 32
        public uint edgessaved;     // 34
    }

    public class Dictionary
    {
        public Dawg_edge[] dawg;      // 0 - 31
        public uint root;           // 32
        public uint nwords;          // 34
        public uint nnodes;          // 36
        public uint nedges;          // 38
    };

    public class params_7plus1_t
    {
        public int search_len;
        public int search_wordlistlen;
        public int search_wordlistlenmax;
        public bool additional = false;
        public bool stoponfirst = false;
        public char[] search_add;
        public char[] search_addtst;
        public char[] search_wordtst;
        public char[] search_letters;
        public ArrayList search_wordlist;

        public params_7plus1_t()
        {
            search_wordtst = new char[DicoConstants.DIC_WORD_MAX + 3];
            search_letters = new char[DicoConstants.DIC_WORD_MAX + 3];
            search_add = new char[DicoConstants.DIC_WORD_MAX + 3];
            search_addtst = new char[DicoConstants.DIC_WORD_MAX + 3];

        }
    };

    public class params_cross_t
    {
        public int wordlen;
        public int wordlistlen;
        public int wordlistlenmax;
        public char[] mask;

        public params_cross_t()
        {
            mask = new char[DicoConstants.DIC_WORD_MAX + 3];
        }
    };

    public class dawgDictionary : IDisposable
    {
        // params_cross_t  ParamsCrossSearch; 
        // params_7plus1_t Params7plus1Search;

        public Dictionary dictionary;

        public dawgDictionary()
        {
        }

        #region READ Functions
        public void LoadHeader(Byte[] b, Dict_header h)
        {
            ReadByte(b, ref h.ident, 0, 22);
            h.root = ReadUInt(b, 23, 4);
            h.nwords = ReadUInt(b, 27, 4);
            h.edgesused = ReadUInt(b, 31, 4);
            h.nodesused = ReadUInt(b, 35, 4);
            h.nodessaved = ReadUInt(b, 39, 4);
            h.edgessaved = ReadUInt(b, 43, 4);
        }

        public void FillHeader(Byte[] b)
        {
            int c = 48;
            for (int x = 0; x <= dictionary.nedges; x++)
            {
                dictionary.dawg[x].allptr = ReadUIntNo(b, c, 4);
                c += 4;
            }
        }


        public uint ReadUIntNo(Byte[] b, int start, int len)
        {
            uint i = 0;
            int shift = 0;
            for (int x = start; x < start + len; x++)
            {
                i = i | (uint)b[x] << (shift);
                shift += 8;
            }

            return i;
        }

        public  uint ReadUInt(Byte[] b, int start, int len)
        {
            uint i = 0;
            int shift = 0;
            for (int x = start + 1; x < start + len; x++)
            {
                i = i | (uint)b[x] << shift;
                shift += 8;
            }

            return i;
        }


        public  void ReadByte(Byte[] b, ref byte[] s, int start, int len)
        {
                for (int x = start; x < start + len; x++)
                {
                    s[x - start] = b[x];
                }
        }

        public uint
        Next(uint e)
        {
            if (Last(e) == 0)
                return e + 1;
            return 0;
        }



        public uint
        Succ(uint e)
        {
            return (dictionary.dawg[e]).ptr;
        }


        public uint
        Root()
        {
            return dictionary.root;
        }

        public char
        Chr(uint e)
        {
            return (char)dictionary.dawg[e].chr;
        }

        public uint
        Last(uint e)
        {
            return (dictionary.dawg[e]).last;
        }


        public uint
        Word(uint e)
        {
            return (dictionary.dawg[e]).term;
        }


        public  Dawg_edge
        SeekEdgeptr(string s, int start, Dawg_edge eptr)
        {
            if (start < s.Length)
            {
                uint x = eptr.ptr;

                Dawg_edge p;
                do
                {
                    p = dictionary.dawg[x];
                    if ((char)p.chr + 64 == s[start])
                    {
                        return SeekEdgeptr(s, start + 1, p);
                    }
                    x++;
                } while (p.last == 0);

                return dictionary.dawg[0];
            }
            else
                return eptr;
        }
        #endregion

        #region Load
        internal int Load(byte[] b)
        {
            Dict_header header = new Dict_header();

            dictionary = new Dictionary();
            LoadHeader(b, header);
            dictionary.dawg = new Dawg_edge[header.edgesused + 1];
            dictionary.root = header.root;
            dictionary.nwords = header.nwords;
            dictionary.nnodes = header.nodesused;
            dictionary.nedges = header.edgesused;

            FillHeader(b);
            return 0;
        }

        #region  allWords without index
        ArrayList allWords;
        public void RetrieveWords(ArrayList all)
        {
            string s = "";
            allWords = all;
            _RetrieveWords(dictionary.dawg[dictionary.root], s);
        }

        string _RetrieveWords(Dawg_edge i, string s)
        {

            try
            {

                if (i.term > 0)  /* edge points at a complete word */
                {
                    allWords.Add(s);
                }

                if (i.ptr > 0)
                {           /* Compute index: is it non-zero ? */
                    uint x = i.ptr;

                    Dawg_edge p;
                    do
                    {                         /* for each edge out of this node */
                        p = dictionary.dawg[x];
                        s += (char)(p.chr + 'a' - 1);
                        s = _RetrieveWords(p, s);
                        x++;
                    } while (p.last == 0);
                }

                s = s.Substring(0, s.Length - 1);
            }
            catch (Exception ee)
            {
              //  NLog.LogManager.GetLogger("wcf").ErrorException("Dawg : RetrieveWords", ee);
            }

            return s;

        }
        #endregion

        
        public void RetrieveWordsNdx(ArrayList all)
        {
            string s = "";
            allWords = all;
            _RetrieveWordsNdx(dictionary.dawg[dictionary.root], s, dictionary.root);
        }

        string _RetrieveWordsNdx(Dawg_edge i, string s, uint indx)
        {

            try
            {

                if (i.term > 0)  /* edge points at a complete word */
                {
                    allWords.Add(new WordNdx { Index = indx, Word = s });
                }

                if (i.ptr > 0)
                {           /* Compute index: is it non-zero ? */
                    uint x = i.ptr;

                    Dawg_edge p;
                    do
                    {                         /* for each edge out of this node */
                        p = dictionary.dawg[x];
                        s += (char)(p.chr + 'a' - 1);
                        s = _RetrieveWordsNdx(p, s,x);
                        x++;
                    } while (p.last == 0);
                }

                s = s.Substring(0, s.Length - 1);
            }
            catch (Exception ee)
            {
              //  NLog.LogManager.GetLogger("wcf").ErrorException("Dawg : RetrieveWordsNdx", ee);
            }

            return s;

        }
        #endregion

        #region Search DICO Internal parameters prefilled
        bool _SearchCross(string mask, ArrayList arlList, bool doFirstOnly)
        {
            int i;
            params_cross_t prms = new params_cross_t();

            for (i = 0; i < DicoConstants.DIC_WORD_MAX && i < mask.Length; i++)
            {
                prms.mask[i] = (char)(mask[i]);
            }

            prms.mask[i] = '\0';

            //prms.dic            = dic;
            prms.wordlen = 0;
            prms.wordlistlen = 0;
            prms.wordlistlenmax = DicoConstants.RES_CROS_MAX;
            return _SearchCrossRec(prms, arlList, dictionary.dawg[dictionary.root].ptr, doFirstOnly);
        }

        bool _SearchCrossRec(params_cross_t prms, ArrayList arlList, uint ndx, bool doFirstOnly)
        {
            Dawg_edge current = dictionary.dawg[ndx];

            if (prms.mask[prms.wordlen] == '?')
            {
                do
                {
                    current = dictionary.dawg[ndx];
					if (current.chr != 0)
					{



						prms.mask[prms.wordlen] = (char)(current.chr + 64);

						if (prms.mask[prms.wordlen + 1] == 0 && current.term > 0)
						{
							if (doFirstOnly) return true;
							arlList.Add(new String(prms.mask));
						}

						prms.wordlen++;
						bool res = _SearchCrossRec(prms, arlList, current.ptr, doFirstOnly);
						if (doFirstOnly && res) return true;
						prms.wordlen--;
						prms.mask[prms.wordlen] = '?';

						ndx++;
					}
				}
				while (current.last == 0);
            }
            else
            {
                do
                {
                    current = dictionary.dawg[ndx];
                    if (prms.mask[prms.wordlen] == (char)(current.chr + 64))
                    {
                        if (prms.mask[prms.wordlen + 1] == 0 && current.term > 0)
                        {
                            if (doFirstOnly) return true;
                            arlList.Add(new String(prms.mask));
                        }

                        prms.wordlen++;
                        bool res = _SearchCrossRec(prms, arlList, current.ptr, doFirstOnly);
                        if (doFirstOnly && res) return true;
                        prms.wordlen--;
                        break;
                    }
                    ndx++;
                }
                while (current.last == 0);
            }

            return false;
        }

        public void _SearchWordByLen(params_7plus1_t prms, int i, uint ndx, char c)
        {

            Dawg_edge edgeptr;
            do
            {
                edgeptr = dictionary.dawg[ndx];
                if (edgeptr.chr > 0)
                {

                    /* is the letter available in search_letters */
                    if (prms.search_letters[edgeptr.chr] != 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 64);
                        prms.search_letters[edgeptr.chr]--;
                        if (i == prms.search_len - 1)
                        {
                            if (edgeptr.term > 0)
                            {
                                prms.search_wordlist.Add(c + "." + new String(prms.search_wordtst));
                            }
                        }
                        else
                        {
                            _SearchWordByLen(prms, i + 1, edgeptr.ptr, c);
                        }
                        prms.search_letters[edgeptr.chr]++;
                        prms.search_wordtst[i] = '\0';
                    }

                    /* the letter is of course available if we have a joker available */
                    if (prms.search_letters[0] > 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 96);
                        prms.search_letters[0]--;
                        if (i == prms.search_len - 1)
                        {
                            if (edgeptr.term != 0)
                            {
                                prms.search_wordlist.Add(c + "." + new String(prms.search_wordtst));
                            }
                        }
                        else
                        {
                            _SearchWordByLen(prms, i + 1, edgeptr.ptr, c);
                        }
                        prms.search_letters[0]++;
                        prms.search_wordtst[i] = '\0';
                    }
                }
                ndx++;
            } while (edgeptr.last == 0);
        }

        public void _SearchWordByLen(params_7plus1_t prms, int i, uint ndx)
        {

            Dawg_edge edgeptr;
            do
            {
                edgeptr = dictionary.dawg[ndx];
                if (edgeptr.chr > 0)
                {

                    /* is the letter available in search_letters */
                    if (prms.search_letters[edgeptr.chr] != 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 64);
                        prms.search_letters[edgeptr.chr]--;
                        if (i == prms.search_len - 1)
                        {
                            if (edgeptr.term > 0)
                            {
                                prms.search_wordlist.Add(new String(prms.search_wordtst));
                            }
                        }
                        else
                        {
                            _SearchWordByLen(prms, i + 1, edgeptr.ptr);
                        }
                        prms.search_letters[edgeptr.chr]++;
                        prms.search_wordtst[i] = '\0';
                    }

                    /* the letter is of course available if we have a joker available */
                    if (prms.search_letters[0] > 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 96);
                        prms.search_letters[0]--;
                        if (i == prms.search_len - 1)
                        {
                            if (edgeptr.term != 0)
                            {
                                prms.search_wordlist.Add(new String(prms.search_wordtst));
                            }
                        }
                        else
                        {
                            _SearchWordByLen(prms, i + 1, edgeptr.ptr);
                        }
                        prms.search_letters[0]++;
                        prms.search_wordtst[i] = '\0';
                    }
                }
                ndx++;
            } while (edgeptr.last == 0);
        }

        public bool _SearchWordByLenTest(params_7plus1_t prms, int i, uint ndx)
        {
            Dawg_edge edgeptr;
            do
            {
                edgeptr = dictionary.dawg[ndx];
                if (edgeptr.chr > 0)
                {

                    /* is the letter available in search_letters */
                    if (prms.search_letters[edgeptr.chr] != 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 64);
                        prms.search_letters[edgeptr.chr]--;
                        if (i == prms.search_len - 1)
                        {
                            if (edgeptr.term > 0)
                            {
                                return true; 
                            }
                        }
                        else
                        {
                           if (_SearchWordByLenTest(prms, i + 1, edgeptr.ptr)) return true;
                        }
                        prms.search_letters[edgeptr.chr]++;
                        prms.search_wordtst[i] = '\0';
                    }

                    /* the letter is of course available if we have a joker available */
                    if (prms.search_letters[0] > 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 96);
                        prms.search_letters[0]--;
                        if (i == prms.search_len - 1)
                        {
                            if (edgeptr.term != 0)
                            {
                                return true; 
                            }
                        }
                        else
                        {
                            if (_SearchWordByLenTest(prms, i + 1, edgeptr.ptr)) return true;
                        }
                        prms.search_letters[0]++;
                        prms.search_wordtst[i] = '\0';
                    }
                }
                ndx++;
            } while (edgeptr.last == 0);

            return false; 
        }


        public void _SearchWordByLenLonger(params_7plus1_t prms, int i, uint ndx)
        {

            Dawg_edge edgeptr;
            do
            {
                edgeptr = dictionary.dawg[ndx];
                if (edgeptr.chr > 0)
                {

                    /* is the letter available in search_letters */
                    if (prms.search_letters[edgeptr.chr] != 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 64);
                        prms.search_letters[edgeptr.chr]--;
                        if (i == prms.search_len - 1)
                        {
                            if (edgeptr.term > 0)
                            {
                                prms.search_wordlist.Add(new String(prms.search_wordtst));
                            }
                        }
                        else
                        {
                            _SearchWordByLenLonger(prms, i + 1, edgeptr.ptr);
                        }
                        prms.search_letters[edgeptr.chr]++;
                        prms.search_wordtst[i] = '\0';
                    }

                    /* the letter is of course available if we have a joker available */
                    if (prms.search_letters[0] > 0)
                    {
                        bool ok = true;
                        if (prms.additional)
                        {
                            if (prms.search_addtst[edgeptr.chr] > 0)
                            {
                                prms.search_addtst[edgeptr.chr]--;
                            }
                            else
                            {
                                ok = false;
                            }
                        }

                        if (ok)
                        {
                            prms.search_wordtst[i] = (char)(edgeptr.chr + 96);
                            prms.search_letters[0]--;
                            if (i == prms.search_len - 1)
                            {
                                if (edgeptr.term != 0)
                                {
                                    prms.search_wordlist.Add(new String(prms.search_wordtst));
                                }
                            }
                            else
                            {
                                _SearchWordByLenLonger(prms, i + 1, edgeptr.ptr);
                            }
                            prms.search_letters[0]++;
                            prms.search_wordtst[i] = '\0';

                            if (prms.additional)
                            {
                                prms.search_addtst[edgeptr.chr]++;
                            }
                        }
                    }
                }
                ndx++;
            } while (edgeptr.last == 0);
        }
        void _SearchAnyWord(params_7plus1_t prms, int i, uint ndx)
        {

            Dawg_edge edgeptr;
            do
            {
                edgeptr = dictionary.dawg[ndx];
                if (edgeptr.chr > 0)
                {

                    /* is the letter available in search_letters */
                    if (prms.search_letters[edgeptr.chr] != 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 64);
                        prms.search_letters[edgeptr.chr]--;
                        //if (i == prms.search_len - 1)
                        //{
                        if (edgeptr.term > 0)
                        {
                            prms.search_wordlist.Add(new String(prms.search_wordtst));
                        }
                        //}
                        // else
                        //{
                        _SearchAnyWord(prms, i + 1, edgeptr.ptr);
                        // }
                        prms.search_letters[edgeptr.chr]++;
                        prms.search_wordtst[i] = '\0';
                    }

                    /* the letter is of course available if we have a joker available */
                    if (prms.search_letters[0] > 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 96);
                        prms.search_letters[0]--;
                        //if (i == prms.search_len - 1)
                        //{
                        if (edgeptr.term != 0)
                        {
                            prms.search_wordlist.Add(new String(prms.search_wordtst));
                        }
                        // }
                        // else
                        //{
                        _SearchAnyWord(prms, i + 1, edgeptr.ptr);
                        //}
                        prms.search_letters[0]++;
                        prms.search_wordtst[i] = '\0';
                    }
                }
                ndx++;
            } while (edgeptr.last == 0);


        }


        void _SearchAnyWord(params_7plus1_t prms, int i, uint ndx, int minLength, int maxLength)
        {

            Dawg_edge edgeptr;
            do
            {
                edgeptr = dictionary.dawg[ndx];
                if (edgeptr.chr > 0)
                {

                    /* is the letter available in search_letters */
                    if (prms.search_letters[edgeptr.chr] != 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 64);
                        prms.search_letters[edgeptr.chr]--;
                        //if (i == prms.search_len - 1)
                        //{
                        if (edgeptr.term > 0 && i + 1 >= minLength && i + 1 <= maxLength)
                        {
                            prms.search_wordlist.Add(new String(prms.search_wordtst, 0, i + 1));
                        }

                        if (i <= maxLength)
                        {
                            _SearchAnyWord(prms, i + 1, edgeptr.ptr, minLength, maxLength);
                        }
                        // }
                        prms.search_letters[edgeptr.chr]++;
                        prms.search_wordtst[i] = '\0';
                    }

                    /* the letter is of course available if we have a joker available */
                    if (prms.search_letters[0] > 0)
                    {
                        prms.search_wordtst[i] = (char)(edgeptr.chr + 96);
                        prms.search_letters[0]--;
                        //if (i == prms.search_len - 1)
                        //{
                        if (edgeptr.term != 0 && i + 1 >= minLength && i + 1 <= maxLength)
                        {
                            prms.search_wordlist.Add(new String(prms.search_wordtst, 0, i + 1));
                        }
                        // }
                        // else
                        //{
                        if (i <= maxLength)
                        {
                            _SearchAnyWord(prms, i + 1, edgeptr.ptr, minLength, maxLength);
                        }
                        //}
                        prms.search_letters[0]++;
                        prms.search_wordtst[i] = '\0';
                    }
                }
                ndx++;
            } while (edgeptr.last == 0);


        }

        #endregion

        public void SearchMask(string srack, ArrayList arlList)
        {
            _SearchCross(srack, arlList, false);
        }

        public uint SearchWord(string word)
        {
            Dawg_edge e;
            e = SeekEdgeptr(word, 0, dictionary.dawg[dictionary.root]);
            return e.term;
        }

        public void SearchAnyWord(String srack, ArrayList arlList, bool joker)
        {
            int i, j, wordlen;

            char[] rr = new char[DicoConstants.DIC_WORD_MAX]; // = new char[DIC_WORD_MAX];
            rr = srack.ToUpper().ToCharArray();

            params_7plus1_t prms = new params_7plus1_t();
            prms.search_wordlist = arlList;
            prms.search_letters = new char[DicoConstants.LETTERS];
            prms.search_wordtst = new char[DicoConstants.DIC_WORD_MAX];

            for (wordlen = 0; wordlen < srack.Length; wordlen++)
            {
                if (char.IsLetter(rr[wordlen]))
                {
                    prms.search_letters[rr[wordlen] - 64]++;
                }
                else if (rr[wordlen] == '?')
                {
                    if (joker)
                    {
                        prms.search_letters[0]++;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (wordlen < 1)
                return;

            prms.search_wordlistlenmax = DicoConstants.RES_7PL1_MAX;
            prms.search_len = srack.Length;
            prms.search_wordtst[srack.Length] = '\0';
            _SearchAnyWord(prms, 0, dictionary.dawg[dictionary.root].ptr);
        }

        public void SearchAnyWordSmaller(String srack, ArrayList arlList, bool joker)
        {
            int i, j, wordlen;

            char[] rr = new char[DicoConstants.DIC_WORD_MAX]; // = new char[DIC_WORD_MAX];
            rr = srack.ToUpper().ToCharArray();

            params_7plus1_t prms = new params_7plus1_t();
            prms.search_wordlist = arlList;
            prms.search_letters = new char[DicoConstants.LETTERS];
            prms.search_wordtst = new char[DicoConstants.DIC_WORD_MAX];

            for (wordlen = 0; wordlen < srack.Length; wordlen++)
            {
                if (char.IsLetter(rr[wordlen]))
                {
                    prms.search_letters[rr[wordlen] - 64]++;
                }
                else if (rr[wordlen] == '?')
                {
                    if (joker)
                    {
                        prms.search_letters[0]++;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (wordlen < 1)
                return;

            prms.search_wordlistlenmax = DicoConstants.RES_7PL1_MAX;
            prms.search_len = srack.Length;
            prms.search_wordtst[srack.Length] = '\0';

            for (int x = srack.Length - 1; x > 0; x--)
            {
                _SearchAnyWord(prms, 0, dictionary.dawg[dictionary.root].ptr, x, x);
                if (arlList.Count > 0) return; 
            }
        }


        public void SearchAnyWord(String srack, ArrayList arlList, bool joker, int minLength, int maxLength)
        {
            int i, j, wordlen;

            char[] rr = new char[DicoConstants.DIC_WORD_MAX]; // = new char[DIC_WORD_MAX];
            rr = srack.ToUpper().ToCharArray();

            params_7plus1_t prms = new params_7plus1_t();
            prms.search_wordlist = arlList;
            prms.search_letters = new char[DicoConstants.LETTERS];
            prms.search_wordtst = new char[DicoConstants.DIC_WORD_MAX];

            for (wordlen = 0; wordlen < srack.Length; wordlen++)
            {
                if (char.IsLetter(rr[wordlen]))
                {
                    prms.search_letters[rr[wordlen] - 64]++;
                }
                else if (rr[wordlen] == '?')
                {
                    if (joker)
                    {
                        prms.search_letters[0]++;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (wordlen < 1)
                return;

            prms.search_wordlistlenmax = DicoConstants.RES_7PL1_MAX;
            prms.search_len = srack.Length;
            prms.search_wordtst[srack.Length] = '\0';
            _SearchAnyWord(prms, 0, dictionary.dawg[dictionary.root].ptr, minLength, maxLength);
        }

        public void
        Search7pl1(string srack,
                ArrayList arlList,
                bool joker)
        {
            int i, j, wordlen;

            char[] rr = new char[DicoConstants.DIC_WORD_MAX]; // = new char[DIC_WORD_MAX];
            rr = srack.ToUpper().ToCharArray();

            params_7plus1_t prms = new params_7plus1_t();
            prms.search_wordlist = arlList;
            prms.search_letters = new char[DicoConstants.LETTERS];
            prms.search_wordtst = new char[DicoConstants.DIC_WORD_MAX];

            for (wordlen = 0; wordlen < srack.Length; wordlen++)
            {
                if (char.IsLetter(rr[wordlen]))
                {
                    prms.search_letters[rr[wordlen] - 64]++;
                }
                else if (rr[wordlen] == '?')
                {
                    if (joker)
                    {
                        prms.search_letters[0]++;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (wordlen < 1)
                return;

            prms.search_wordlistlenmax = DicoConstants.RES_7PL1_MAX;
            prms.search_len = srack.Length;
            prms.search_wordtst[srack.Length] = '\0';
            _SearchWordByLen(prms, 0, dictionary.dawg[dictionary.root].ptr, ' ');

            /* search for all the words that can be done with the letters +1 */
            prms.search_len = srack.Length + 1;
            prms.search_wordtst[srack.Length + 1] = '\0';
            for (i = 'A'; i <= 'Z'; i++)
            {
                prms.search_letters[i - 64]++;
                _SearchWordByLen(prms, 0, dictionary.dawg[dictionary.root].ptr, (char)i);
                prms.search_letters[i - 64]--;
            }
        }


        public void Search7(string srack,
                ArrayList arlList,
                bool joker)
        {
            int i, j, wordlen;

            char[] rr = new char[DicoConstants.DIC_WORD_MAX]; // = new char[DIC_WORD_MAX];
            rr = srack.ToUpper().ToCharArray();

            params_7plus1_t prms = new params_7plus1_t();
            prms.search_wordlist = arlList;
            prms.search_letters = new char[DicoConstants.LETTERS];
            prms.search_wordtst = new char[DicoConstants.DIC_WORD_MAX];

            for (wordlen = 0; wordlen < srack.Length; wordlen++)
            {
                if (char.IsLetter(rr[wordlen]))
                {
                    prms.search_letters[rr[wordlen] - 64]++;
                }
                else if (rr[wordlen] == '?')
                {
                    if (joker)
                    {
                        prms.search_letters[0]++;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (wordlen < 1)
                return;

            prms.search_wordlistlenmax = DicoConstants.RES_7PL1_MAX;
            prms.search_len = srack.Length;
            prms.search_wordtst[srack.Length] = '\0';
            _SearchWordByLen(prms, 0, dictionary.dawg[dictionary.root].ptr, ' ');

            /* search for all the words that can be done with the letters +1 */
            /*prms.search_len = srack.Length + 1;
            prms.search_wordtst[srack.Length + 1] = '\0';
            for (i = 'A'; i <= 'Z'; i++)
            {
                prms.search_letters[i - 64]++;
                _SearchWordByLen(prms, 0, dictionary.dawg[dictionary.root].ptr, (char)i);
                prms.search_letters[i - 64]--;
            }*/
        }

        public void
        SearchAnyWordLonger(string srack,
                ArrayList arlList)
        {
            int i, j, wordlen;

            char[] rr = new char[DicoConstants.DIC_WORD_MAX]; // = new char[DIC_WORD_MAX];
            rr = srack.ToUpper().ToCharArray();

            params_7plus1_t prms = new params_7plus1_t();
            prms.search_wordlist = arlList;
            prms.search_letters = new char[DicoConstants.LETTERS];
            prms.search_wordtst = new char[DicoConstants.DIC_WORD_MAX];

            for (wordlen = 0; wordlen < srack.Length; wordlen++)
            {
                if (char.IsLetter(rr[wordlen]))
                {
                    prms.search_letters[rr[wordlen] - 64]++;
                }
            }

            if (wordlen < 1)
                return;

            int len = srack.Length;

            prms.search_len = srack.Length;
            prms.search_wordtst[prms.search_len] = '\0';

            for (int x = len; x <= 15; x++)
            {
                prms.search_letters[0]++;
                prms.search_len++;
                prms.search_wordtst[prms.search_len] = '\0';
                _SearchWordByLenLonger(prms, 0, dictionary.dawg[dictionary.root].ptr);
            }
        }

        public void
        SearchAnyWordLonger(string srack, string additional,
                ArrayList arlList, int maxLettres)
        {
            int i, j, wordlen;

            char[] rr = new char[DicoConstants.DIC_WORD_MAX]; // = new char[DIC_WORD_MAX];
            rr = srack.ToUpper().ToCharArray();

            params_7plus1_t prms = new params_7plus1_t();
            prms.search_wordlist = arlList;
            prms.search_letters = new char[DicoConstants.LETTERS];
            prms.search_wordtst = new char[DicoConstants.DIC_WORD_MAX];

            for (wordlen = 0; wordlen < srack.Length; wordlen++)
            {
                if (char.IsLetter(rr[wordlen]))
                {
                    prms.search_letters[rr[wordlen] - 64]++;
                }
            }

            for (wordlen = 0; wordlen < additional.Length; wordlen++)
            {
                prms.search_add[additional[wordlen] - 64]++;
                prms.search_addtst[additional[wordlen] - 64]++;
            }

            wordlen = srack.Length;

            prms.additional = true;
            prms.stoponfirst = true;
            if (wordlen < 1)
                return;

            int len = srack.Length;

            prms.search_len = srack.Length;
            prms.search_wordtst[prms.search_len] = '\0';

            for (int x = 0; x <= additional.Length && x < maxLettres - wordlen; x++)
            {
                prms.search_letters[0]++;
                prms.search_len++;
                prms.search_wordtst[prms.search_len] = '\0';
                _SearchWordByLenLonger(prms, 0, dictionary.dawg[dictionary.root].ptr);
            }
        }

        public bool Search7Test(string srack)
        {
            return Search7Test(srack.ToCharArray(), srack.Length);
        }

        public bool Search7Test(char[] rr, int rrlen)
        {
            int i, j, wordlen;

            params_7plus1_t prms = new params_7plus1_t();
            prms.search_wordlist = null;
            prms.search_letters = new char[DicoConstants.LETTERS];
            prms.search_wordtst = new char[DicoConstants.DIC_WORD_MAX];

            for (wordlen = 0; wordlen < rrlen; wordlen++)
            {
                    prms.search_letters[rr[wordlen] - 64]++;
            }

            if (wordlen < 1)
                return false; ;

            prms.search_wordlistlenmax = DicoConstants.RES_7PL1_MAX;
            prms.search_len = rrlen;
            prms.search_wordtst[rrlen] = '\0';
            return _SearchWordByLenTest(prms, 0, dictionary.dawg[dictionary.root].ptr);
        }
        ///****************************************/
        ///****************************************/

        public void SearchRacc(string srack, ArrayList arlList)
        {
            /* search_racc will try to add a letter in front and at the end of a word */

            int i, wordlistlen;
            Dawg_edge edge;

            char[] wordtst = new char[DicoConstants.DIC_WORD_MAX];
            char[] word = new char[DicoConstants.DIC_WORD_MAX];
            string strword;

            for (int x = 0; x < srack.Length; x++) word[x] = srack[x];

            if (dictionary == null)
                return;

            /* let's try for the front */
            Array.Copy(word, 0, wordtst, 1, srack.Length);
            for (i = 'a'; i <= 'z'; i++)
            {
                wordtst[0] = (char)i;
                strword = new string(wordtst, 0, srack.Length + 1);
                if (SearchWord(strword) > 0)
                    arlList.Add(strword);
            }

            /* add a letter at the end */
            strword = new String(word, 0, srack.Length);
            edge = SeekEdgeptr(strword, 0, dictionary.dawg[dictionary.root]);

            /* points to what the next letter can be */
            uint index = edge.ptr;
            edge = dictionary.dawg[edge.ptr];

            if (index != dictionary.root)
            {
                do
                {
                    if (edge.term > 0)
                    {
                        char c = (char)(edge.chr + 96);
                        arlList.Add(strword + c);
                    }
                    index++;
                    edge = dictionary.dawg[index];
                } while (edge.last != 0);
            }
        }





        #region IDisposable Members

        public void Dispose()
        {
            if (dictionary != null)
            {
                dictionary.dawg = null;
            }
        }

        #endregion
    }
}
