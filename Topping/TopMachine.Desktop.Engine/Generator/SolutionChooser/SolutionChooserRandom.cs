
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
	public class SolutionChooserProvider : ISolutionChooser
    {

        private System.ComponentModel.Container components = null;
        CResults results;
        GameCfg gc;
        bool LoadView = false;
        IGameController gctl;

        CRound SelectedRound { get; set; }


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

        public SolutionChooserProvider()
		{
		}

        public SolutionChooserProvider(IGameController gctl, GameCfg gc)
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
            return SelectedRound;
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
