using System;
using System.Collections;
using System.IO;
//using TopMachine.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

using TopMachine.Topping.Dawg;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Engine.GUI.Board;



namespace TopMachine.Engine.Local.GameController.SolutionChooser
{  
	/// <summary>
	/// Summary description for Results.
	/// </summary>
	public class SolutionChooserFromHistory : ISolutionChooser
    {

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

        #endregion  



        public SolutionChooserFromHistory(IGameController gctl, GameCfg gc)
		{

            this.gctl = gctl;
            this.gc = gc;
		}


        #region KeyHandler
        public bool HandleKey(System.Windows.Forms.KeyEventArgs e)
        {

            return e.Handled;
        }
        #endregion

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
            IRoundManager rm = gctl.GameControllers.RoundManager;
            CRound historound = rm.SelectedRound;

            int indice;
            CRound round = null;

            round = historound;

            if (historound.RemoveAfterRound > 0)
            {
                gc.Bag.taketile(historound.RemoveAfterRound);
                historound.RemoveAfterRound = 0; 
            }

            //((TopMachine.Engine.Local.GameController.RoundManager.CXmlRoundManager)(gctl.GameControllers.RoundManager)).CheckOrigin(round); 

            if (round == null)
            {
                MessageBox.Show("Partie non valide"); 
                System.Windows.Forms.Application.Exit();
            }

            int Direction = round.dir(); 
            int Horizontal = round.column(); 
            int Vertical = round.row();
            char cc = (char)(64 + Vertical);
            string s;
            if (Direction == 1)
            {
                s = cc + "/" + ((int)(Horizontal)).ToString();
            }
            else
            {
                s = ((int)(Horizontal)).ToString() + " / " + cc;
            }
            return round;
        }
        #endregion

        #region LAYOUT TOGGLING

        private void SelectRound()
		{
            try
            {
                if (OnSelectWord != null)
                {
                    OnSelectWord(GetSelectedRound());
                }
            }
            catch (Exception ee)
            {
                throw (ee);
            }
		}


        #endregion 

        #region public void GetSolutions()
        private delegate void SetVisibleDelegate(bool ok);
        public void GetSolutions()
        {
            SelectRound();
        }
        #endregion 


	
        public void Dispose()
        {
            ;
        }
    }

}
