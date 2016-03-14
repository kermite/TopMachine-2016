
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

using TopMachine;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;


namespace TopMachine.Topping.Engine.GameController.SolutionChooser
{  
	/// <summary>
	/// Summary description for Results.
	/// </summary>
	public class SolutionChooserRandom : ISolutionChooser
    {

        private System.ComponentModel.Container components = null;
        CResults results;
        GameCfg gc;
        bool LoadView = false;
        IGameController gctl;

        #region Public Properties and Events
        public event RemoveTempWordEvent OnRemoveTempWord;
        public event SetTempWordEvent OnSetTempWord;
        public event SelectWordEvent OnSelectWord; 

        public GameCfg GameConfig
        {
            set { gc = value; }
        }

        public System.Windows.Forms.Control ResultControl
        {
            get { return null; }
        }
        #endregion  

        public SolutionChooserRandom()
		{
		}

        public SolutionChooserRandom(IGameController gctl, GameCfg gc)
		{
            this.gctl = gctl;
            this.gc = gc;

    	}

        #region LIST INIT 
        public void SetResults(CResults results)
        {
            SetResults(results, false);
        }
        public void SetResults(CResults results, ArrayList list, bool visible)
        {
        }

        public void SetResults(CResults results, bool visible)
		{           
            this.results = results;           
        }
        #endregion 

        #region private CRound GetSelectedRound()
        private CRound GetSelectedRound()
        { 
            int indice;
            CRound round;

            RemoveJokers(results);
            RemoveNotQ(results);
            RemoveNotW(results);

            if (gc.iTurn == 1)
            {
                RemoveRight(results);
            }

            int c = new System.Random().Next(results.Results.list.Count);
            round = (CRound) results.Results.list.GetByIndex(c);

            int Direction = round.dir(); 
            int Horizontal = round.column(); 
            int Vertical = round.row();
            char cc = (char)(65 + Vertical);
            string s;
            if (Direction == 1)
            {
                s = cc + "/" + ((int)(Horizontal + 1)).ToString();
            }
            else
            {
                s = ((int)(Horizontal + 1)).ToString() + " / " + cc;
            }

            return round;
        }
        #endregion

        #region private void RemoveRight(CResults results)
        private void RemoveRight(CResults results)
        {
            int right = -1; int notright = -1;

            ArrayList obj = new ArrayList();

            try
            {
                // Suppression des coupos JOKERS si possible
                foreach (CRound round in results.Results.list.Values)
                {
                    if ((round.wordlen() <= 4 && round.column() > 7) || (round.wordlen() > 4 && round.column() > 4))
                    {
                        right = 1;
                    }
                    else
                    {
                        notright = 1;

                    }

                }
            }
            catch (Exception ee)
            {
                throw new Exception("Error : RemoveRight Test", ee);
            }

            try
            {
                if (right == 1 && notright == 1)
                {
                    foreach (string s in results.Results.list.Keys)
                    {
                        CRound round = (CRound)results.Results.list[s];
                        if ((round.wordlen() <= 4 && round.column() > 7) || (round.wordlen() > 4 && round.column() > 4))
                        {
                            obj.Add(s);
                        }
                    }
                }

                foreach (string s in obj)
                {
                    results.Results.list.Remove(s);
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Error : Remove RemoveRight Suppress", ee);
            }
        }
        #endregion

        #region private void RemoveJokers(CResults results)
        private void RemoveJokers(CResults results)
        {

            int joker = -1; int notjoker = -1;
            ArrayList obj = new ArrayList();

            try
            {
                // Suppression des coupos JOKERS si possible
                foreach (CRound round in results.Results.list.Values)
                {
                    bool j = false;
                    for (int x = 0; x < round.wordlen(); x++)
                    {
                        if (round.playedfromrack(x) == 2 && round.joker(x) == 4)
                        {
                            j = true;
                            break;
                        }
                    }

                    if (j)
                    {
                        joker = 1;
                    }
                    else
                    {
                        notjoker = 1;
                    }
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Error : RemoveJokers Test", ee);
            }

            try
            {
                if (joker == 1 && notjoker == 1)
                {
                    foreach (string s in results.Results.list.Keys)
                    {
                        CRound round = (CRound)results.Results.list[s];
                        bool j = false;
                        for (int x = 0; x < round.wordlen(); x++)
                        {
                            if (round.playedfromrack(x) == 2 && round.joker(x) == 4)
                            {
                                j = true;
                                break;
                            }
                        }

                        if (j)
                        {
                            obj.Add(s);
                        }
                    }
                }

                foreach (string s in obj)
                {
                    results.Results.list.Remove(s);
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Error : Remove Joker Suppress", ee);
            }
        }
        #endregion

        #region private void RemoveNotQ(CResults results)
        private void RemoveNotQ(CResults results)
        {
            int q = -1; int notq = -1;
            ArrayList obj = new ArrayList();

            try
            {
                // Suppression des coupos JOKERS si possible
                foreach (CRound round in results.Results.list.Values)
                {
                    bool qq = false;
                    string word = round.getwordwithjoker();
                    for (int x = 0; x < round.wordlen(); x++)
                    {
                        if (round.playedfromrack(x) == 2 && word[x] == 'Q')
                        {
                            qq = true;
                            break;
                        }
                    }

                    if (qq)
                    {
                        q = 1;
                    }
                    else
                    {
                        notq = 1;
                    }
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Error : RemoveQ Test", ee);
            }

            try
            {
                if (q == 1 && notq == 1)
                {
                    foreach (string s in results.Results.list.Keys)
                    {
                        CRound round = (CRound)results.Results.list[s];
                        string word = round.getwordwithjoker();

                        bool qq = false;
                        for (int x = 0; x < round.wordlen(); x++)
                        {
                            if (round.playedfromrack(x) == 2 && word[x] == 'Q')
                            {
                                qq = true;
                            }
                        }

                        if (!qq)
                        {
                            obj.Add(s);
                        }
                    }
                }

                foreach (string s in obj)
                {
                    results.Results.list.Remove(s);
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Error : RemoveQ Suppress", ee);
            }

        }
        #endregion 

        #region private void RemoveNotW(CResults results)
        private void RemoveNotW(CResults results)
        {
            int q = -1; int notq = -1;
            ArrayList obj = new ArrayList();

            try
            {
                // Suppression des coupos JOKERS si possible
                foreach (CRound round in results.Results.list.Values)
                {
                    bool qq = false;
                    string word = round.getwordwithjoker();
                    for (int x = 0; x < round.wordlen(); x++)
                    {
                        if (round.playedfromrack(x) == 2 && word[x] == 'W')
                        {
                            qq = true;
                            break;
                        }
                    }

                    if (qq)
                    {
                        q = 1;
                    }
                    else
                    {
                        notq = 1;
                    }
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Error : RemoveQ Test", ee);
            }

            try
            {
                if (q == 1 && notq == 1)
                {
                    foreach (string s in results.Results.list.Keys)
                    {
                        CRound round = (CRound)results.Results.list[s];
                        string word = round.getwordwithjoker();

                        bool qq = false;
                        for (int x = 0; x < round.wordlen(); x++)
                        {
                            if (round.playedfromrack(x) == 2 && word[x] == 'W')
                            {
                                qq = true;
                            }
                        }

                        if (!qq)
                        {
                            obj.Add(s);
                        }
                    }
                }

                foreach (string s in obj)
                {
                    results.Results.list.Remove(s);
                }
            }
            catch (Exception ee)
            {
                throw new Exception("Error : RemoveQ Suppress", ee);
            }

        }
        #endregion 

        #region LAYOUT TOGGLING

        private void SelectRound(bool ok)
		{
            if (OnSelectWord != null)
            {
                OnSelectWord(GetSelectedRound()); 
            }

		}


        #endregion 

        #region public void GetSolutions()
        private delegate void SetVisibleDelegate(bool ok);
        public void GetSolutions()
        {
            SelectRound(true);
        }
        #endregion 

        public void SetVisible(bool ok)
        {
        }


        public void Dispose()
        {
            ;
        }
    }

}
