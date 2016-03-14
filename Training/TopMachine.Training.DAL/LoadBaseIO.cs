using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;

using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using TopMachine.Topping.Dawg;
using CMWA.Packager.Custom;
using CMWA.Packager;


namespace TopMachine.Training.DAL
{
    /// <summary>
    /// Summary description for Form2.
    /// </summary>
    public class LoadBaseIO : IDisposable
    {
        private DicoUtils currentDico = null; 
        private dawgDictionary dictionary;

        ListAccessor accessor = null; 
        Config config;
        ListConfig listConfig;

        private ArrayList arlJokers;
        private ArrayList arlIncludes = new ArrayList();
        private ArrayList arlExcludes = new ArrayList();
        private SortedList Keys = new SortedList();


        int nbTirages = 0;


        private char[] IncludedLetters, WithLetters, WithoutLetters, Jokers, JokersLower;
        private string strGenre;
        private bool OnlyInvariable;

        private bool isDoDataNext = true;
        private bool blnMonomes = false, blnBinomes = false;
        private int intPossibilites = 1;
        private int intMaxPossibilites = 1;
        private SortedList srtList, srtJoker, srtUpper;
        private int intGameID;
        private string strFileName;
        private bool blnJoker;


        public LoadBaseIO(string key, Config config)
        {
            this.config = config;
            listConfig = new TopMachine.Desktop.Overall.ObjectSerializer<ListConfig>().Deserialize(config.XMLConfig);
            accessor = new ListAccessor("TrainingLists\\Lists\\" + key);
        }

        public LoadBaseIO(Package package)
        {
            accessor = new ListAccessor("TrainingLists\\Lists\\" + package.Key);
            config = accessor.GetConfig();
            listConfig = new TopMachine.Desktop.Overall.ObjectSerializer<ListConfig>().Deserialize(config.XMLConfig);
        }

        public void RecreateList()
        {
            accessor.DeleteWords();
            DoData();
        }
                        

        public int DoData()
        {
            string dico = listConfig.Dico +  listConfig.Base; 
            currentDico =  DicoUtils.GetDicoUtils(dico);
            dictionary = currentDico.getDico(); 

            IncludedLetters = WithLetters = WithoutLetters = null;

            if (listConfig.WithAllLetters != null && listConfig.WithAllLetters.Length > 0)
                IncludedLetters = listConfig.WithAllLetters.ToUpper().ToCharArray();

            if (listConfig.WithAnyLetter != null && listConfig.WithAnyLetter.Length > 0)
                WithLetters  = listConfig.WithAnyLetter.ToUpper().ToCharArray();

            if (listConfig.WithoutLetter != null && listConfig.WithoutLetter.Length > 0)
                WithoutLetters = listConfig.WithoutLetter.ToUpper().ToCharArray();

            if (listConfig.Joker)
            {
                blnJoker = true;
                if (listConfig.LetterJoker !=null && listConfig.LetterJoker.Length > 0)
                {
                    Jokers = listConfig.LetterJoker.ToUpper().ToCharArray();
                    JokersLower = listConfig.LetterJoker.ToLower().ToCharArray();
                }
                else
                {
                    Jokers = new Char[0];
                    JokersLower = new Char[0];
                }

            }

            foreach (ListCriterium crit in listConfig.Criteria)
            {
                if (crit.InclusionRegEx != null && crit.InclusionRegEx.Length > 0)
                {
                    Regex ex = new Regex(crit.InclusionRegEx);
                    arlIncludes.Add(ex);
                }
                else
                {
                    arlIncludes.Add(null);
                }

                if (crit.ExclusionRegEx != null && crit.ExclusionRegEx.Length > 0)
                {
                    Regex exx = new Regex(crit.ExclusionRegEx);
                    arlExcludes.Add(exx);
                }
                else
                {
                    arlExcludes.Add(null);
                }

            }

            intPossibilites = listConfig.MinPossibilities;
            intMaxPossibilites = listConfig.MaxPossibilities;


            srtList = new SortedList();
            srtJoker = new SortedList();
            srtUpper = new SortedList();

            System.Collections.ArrayList arlWords = new System.Collections.ArrayList();
            try
            {
                dictionary.RetrieveWords(arlWords);
            }
            catch (Exception)
            {
                int x = 0;
            }

            int totCount = arlWords.Count;

            foreach (string strWord in arlWords)
            {
                try
                {
                    if (CheckCriterias(strWord.ToUpper()) == true)
                    {
                        if (blnJoker == false)
                        {
                            srtList.Add(GetTirage(strWord) + "." + strWord, strWord);
                        }
                    }
                }
                catch
                { ;}

            }

            if (blnJoker == false)
            {
                RemoveStrings();
            }

            nbTirages = CreateList();

            return nbTirages;
        }

        public int CreateList()
        {

            string oldKey = "";
            int intTirageID = 1;
            int intC = 0;

            if (srtList != null)
            {

                SortedList srtTmp = new SortedList();
                foreach (string str in srtList.Keys)
                {
                    string[] strTmp = str.Split('.');


                    if (strTmp[0] != oldKey)
                    {
                        if (intC > 0)
                        {
                            AddRow(srtTmp, intTirageID);
                            intTirageID++;


                            if (intTirageID % 10000 == 0)
                            {
                                accessor.Save();
                                accessor.ReinitializeEntity();
                            }

                            srtTmp.Clear();
                        }
                        oldKey = strTmp[0];
                        intC = 0;
                    }

                    if (blnJoker)
                    {
                        string t = strTmp[2].Substring(0, strTmp[2].IndexOf((char)0));
                        srtTmp.Add(str, t);
                    }
                    else
                    {
                        srtTmp.Add(str, strTmp[1]);
                    }
                    intC++;

                }

                if (intC > 0)
                {

                    AddRow(srtTmp, intTirageID);

                    if (intTirageID % 10000 == 0)
                    {
                        accessor.Save();
                        accessor.ReinitializeEntity();
                    }

                    intTirageID++;
                }

                intTirageID++;
            }

            // Time To Save 
            accessor.Save();
            accessor.Dispose();

            return intTirageID;
        }
        

        public void AddRow( SortedList srtTirages, int ID)
        {
            string strTirage = srtTirages.GetKey(0).ToString().Split('.')[0];

            Word cfg = new Word
            {
                Rack = strTirage
            };

            WordList wl = new WordList(); 

                int intC = srtTirages.Keys.Count;
                if (intC > 0)
                {
                    if ((blnMonomes && intC == 1)
                        || (blnBinomes && intC == 2)
                        || (intC <= intMaxPossibilites
                                && intC >= intPossibilites))
                    {

                        foreach (string key in srtTirages.Keys)
                        {
                            wl.Words.Add(srtTirages[key].ToString().ToUpper());
                        }
                    }
                }

                cfg.WordsXML = new TopMachine.Desktop.Overall.ObjectSerializer<WordList>().Serialize(wl);

            accessor.AddWord(cfg); 

        }

        public void RemoveStrings()
        {

            SortedList srtNewList = new SortedList();
            SortedList srtTmp = new SortedList();

            string oldKey = "";
            int intC = 0;

            foreach (string str in srtList.Keys)
            {

                string[] strTmp = str.Split('.');

                if (strTmp[0] != oldKey)
                {
                    if (intC > 0)
                    {
                        if ((blnMonomes && intC == 1)
                            || (blnBinomes && intC == 2)
                            )
                        {
                            foreach (string strKey in srtTmp.Keys)
                            {
                                string s = (string)srtTmp[strKey];
                                try
                                {
                                    srtNewList.Add(strKey, s);
                                }
                                catch
                                {
                                    ;
                                }
                            }

                        }
                        else
                        {
                            if (intPossibilites > 0 && intC >= intPossibilites
                                && intC <= intMaxPossibilites)
                            {
                                foreach (string strKey in srtTmp.Keys)
                                {
                                    string s = (string)srtTmp[strKey];
                                    try
                                    {
                                        srtNewList.Add(strKey, s);
                                    }
                                    catch
                                    {
                                        ;
                                    }
                                }

                            }
                        }

                    }
                    oldKey = strTmp[0];
                    srtTmp.Clear();
                    intC = 0;
                }

                srtTmp.Add(str, strTmp[1]);
                intC++;
            }

            if (intC > 0)
            {
                if ((blnMonomes && intC == 1)
                    || (blnBinomes && intC == 2)
                    || (intPossibilites > 0 && intC >= intPossibilites))
                {
                    foreach (string strKey in srtTmp.Keys)
                    {
                        string s = (string)srtTmp[strKey];
                        srtNewList.Add(strKey, s);
                    }

                }
            }

            srtList = srtNewList;

        }

        public bool CheckCriterias(string strWord)
        {
            // Check Length
            if (strWord.Length < listConfig.MinLetters
                || strWord.Length > listConfig.MaxLetters)
                return false;

            if (IncludedLetters != null)
            {
                bool found = true;
                foreach (char c in IncludedLetters)
                {
                    if (strWord.IndexOf(c) == -1)
                        found = false;
                }

                if (!found) return false;
            }

            if (WithLetters != null)
            {
                bool found = false;
                foreach (char c in WithLetters)
                {
                    if (strWord.IndexOf(c) != -1)
                        found = true;
                }

                if (!found) return false;
            }


            if (WithoutLetters != null)
            {
                foreach (char c in WithoutLetters)
                {
                    if (strWord.IndexOf(c) != -1)
                        return false;
                }
            }

            if (blnJoker == true)
            {
                if (Jokers.Length != 0)
                {
                    if (strWord.IndexOfAny(Jokers) == -1)
                        return false;
                }

                arlJokers = CheckJokers(strWord);

                if (arlJokers.Count == 0)
                    return false;
            }

            // Check Criteria
            bool foundreg = true;
            if (arlIncludes.Count > 0)
            {
                int x = arlIncludes.Count;

                for (int xx = 0; xx < x; xx++)
                {
                    Regex exI = (Regex)arlIncludes[xx];
                    Regex exE = (Regex)arlExcludes[xx];

                    bool inc = false;
                    bool exc = true;

                    if (exI != null)
                    {
                        int c = exI.Matches(strWord).Count;
                        int cc = listConfig.Criteria[xx].InclusionRegExCount;

                        if (cc == 0)
                        {
                            if (c == 0)
                            {
                                inc = true;
                            }
                        }
                        else
                        {
                            if (cc < 0 && c == -cc)
                            {
                                inc = true; 
                            }

                            if (cc > 0 && c >= cc)
                            {
                                inc = true;
                            }

                        }
                    }
                    else
                    {
                        inc = true; 
                    }


                    if (exE != null)
                    {
                        int c = exE.Matches(strWord).Count;
                        int cc = listConfig.Criteria[xx].ExclusionRegExCountCount;

                        if (cc == 0)
                        {
                            if (c == 0)
                            {
                                exc = false;
                            }
                        }
                        else
                        {
                            if (cc < 0 && c == -cc)
                            {
                                exc = false;
                            }

                            if (cc > 0 && c >= cc)
                            {
                                exc = false;
                            }

                        }
                    }
                    else
                    {
                        exc = true;
                    }

                    if (!listConfig.MatchAllCriteria)
                    {
                        if (inc && exc)
                        {
                            foundreg = true;
                            break;
                        }
                        else
                        {
                            foundreg = false; 
                        }
                    }
                    else
                    {
                        if (!inc || !exc)
                        {
                            foundreg = false;
                            break;
                        }
                       
                    }
                }
            }

            return foundreg;
        }

        public System.Collections.ArrayList CheckJokers(string strWord)
        {

            System.Collections.ArrayList arlList = new System.Collections.ArrayList();
            System.Collections.ArrayList arlOK = new System.Collections.ArrayList();

            char[] newWord;
            string strReturn = "";


            if (Jokers.Length == 0)
            {
                for (int x = 0; x < strWord.Length; x++)
                {
                    newWord = strWord.ToCharArray();
                    newWord[x] = '?';
                    strReturn = new String(newWord);

                    string key = GetTirageJoker(new string(newWord));

                    if (!srtJoker.Contains(key))
                    {
                        srtJoker.Add(key, key);
                        arlList.Clear();
                        dictionary.Search7(strReturn, arlList, true);

                        int c = (from xx in arlList.Cast<string>() select xx.ToUpper()).Distinct().Count();
 
                        if (CheckCount(c))
                        {
                            SortedList sl = new SortedList();
                            foreach (string s in arlList)
                            {
                                string supper = s.ToUpper();
                                if (!sl.ContainsKey(supper))
                                {
                                    sl.Add(supper, supper);
                                    srtList.Add(key + "." + s, s);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (char c in Jokers)
                {
                    int pos = strWord.IndexOf(c);
                    if (pos >= 0)
                    {
                        newWord = strWord.ToCharArray();
                        newWord[pos] = '?';
                        strReturn = new String(newWord);

                        string key = GetTirageJoker(new string(newWord));

                        if (!srtJoker.Contains(key))
                        {
                            srtJoker.Add(key, key);
                            arlList.Clear();
                            dictionary.Search7(strReturn, arlList, true);

                            int c2 = (from xx in arlList.Cast<string>() select xx.ToUpper()).Distinct().Count();

                            if (CheckCount(c2))
                            {
                                bool ok = true;
                                foreach (string s in arlList)
                                {
                                    if (s.IndexOfAny(JokersLower) == -1)
                                    {
                                        ok = false;
                                    }
                                }

                                if (ok)
                                {
                                    SortedList sl = new SortedList();
                                    foreach (string s in arlList)
                                    {
                                        string supper = s.ToUpper();
                                        if (!sl.ContainsKey(supper))
                                        {
                                            sl.Add(supper, supper);
                                            srtList.Add(key + "." + s, s);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }

            return arlOK;
        }

        public bool CheckCount(int cnt)
        {
            if ((blnMonomes && cnt == 1)
                || (blnBinomes && cnt == 2)
                || (cnt <= intMaxPossibilites
                                && cnt >= intPossibilites))
            {
                return true;
            }

            return false;
        }

        public string GetTirage(string strWord)
        {
            char[] c = strWord.ToUpper().ToCharArray();
            for (int x = 0; x < c.Length - 1; x++)
            {
                for (int y = x + 1; y < c.Length; y++)
                {
                    if (c[x] > c[y] && c[x] >= 'A')
                    {
                        char cc = c[x]; c[x] = c[y]; c[y] = cc;
                    }
                }
            }

            return new String(c);
        }

        public string GetTirageJoker(string strWord)
        {
            char[] c = strWord.ToCharArray();

            for (int x = 0; x < c.Length - 1; x++)
            {
                for (int y = x + 1; y < c.Length; y++)
                {
                    if (c[x] > c[y])
                    {
                        char cc = c[x]; c[x] = c[y]; c[y] = cc;
                    }
                }
            }

            return new String(c);
        }



        #region IDisposable Members

        public void Dispose()
        {
           if (arlJokers != null) arlJokers.Clear();
           if (arlIncludes != null) arlIncludes.Clear();
           if (arlExcludes != null) arlExcludes.Clear();
           if (Keys != null) Keys.Clear();
           if (srtJoker != null) srtJoker.Clear();
           if (srtList != null) srtList.Clear();
           if (srtUpper != null) srtList.Clear();

           if (accessor != null)
           {
               accessor.Dispose();
               accessor = null;
           }

        }

        #endregion
    }


}

