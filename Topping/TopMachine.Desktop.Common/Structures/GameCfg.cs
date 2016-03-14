using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TopMachine.Topping.Dawg;
using TopMachine.Topping.Dto;

namespace TopMachine.Topping.Common.Structures
{ // besoin du dawg,dsStats,DBManager.dsStats
	public enum enuGameState 
    { 
        Initializing = 0, 
        RackInput = 1, 
        Playing = 2, 
        GetSolution = 3, 
        WaitingNextTurn =4, 
        Finished=5, 
        FinishedAndSaved=6,
        FinishedError = 7,
        IsStopping = 8

    }; 

    public enum GameType
    {
         LocalConfig = 0, 
         LocalDb = 1,
         LocalXml = 2, 
         Share = 3, 
         Comment = 4,
         ShareLocal = 5
    }

	public class GameCfg : IDisposable 
	{
        public GeneratedGameDto generatedGame; 

        public GameType gameType = GameType.LocalConfig; 

        #region DAWG Objects
        public TopMachine.Topping.Dawg.IBoard GameBoard = (TopMachine.Topping.Dawg.IBoard)new CBoard();

		public dawgDictionary Dictionary = new dawgDictionary();
        public ShuffleBag Bag;
        public ShuffleBag BagDone;
        public CRack Rack; 
        public CRack OldRack;

		public CResults Results = new CResults(500);
        #endregion

        public GridConfig GridConfig;

        public ConfigGameDto Config;

        public int[] intBonus;

        public bool demo = false;
        public int observeMode = 0;

        public Guid GameID = Guid.Empty;



        #region Game Play
        public int iTurn = 0; 
		public int iTotalTop = 0; 
		public int iTotalPlayer = 0;
        public int iTotalPlayerTop = 0;

		public bool blnGameRun = false;
		public bool blnRoundEnd = true;
		
        public System.TimeSpan TimePlayStart;

		public long TimePlay = 0;
		public float CurrentPlay = 0; 
 
		public System.TimeSpan  TimeGameStart;
		public long TimeGame = 0;
        public long TotalTimePlay = 0; 
		public float CurrentGame = 0; 
        #endregion		

        #region Rounds Management
		public ArrayList Rounds;
		public CRound CurrentRound, CurrentGameRound;
        #endregion

        #region Rack management
        public char[] CurrentRackChars; 
		public int    CurrentRackLength;
        #endregion 

        #region Game State
		private  enuGameState _gameState = enuGameState.Initializing;

        public enuGameState GameState
        {
            get { return _gameState; }
            set { 
                _gameState = value;

                if (_gameState == enuGameState.GetSolution)
                {
                    
                    Console.Write("");
                }
            }
        }
        #endregion 

        public TopMachine.Topping.Dawg.IBoard GetBoard()
        {
            return (TopMachine.Topping.Dawg.IBoard)new CBoard();
        }

        public GameCfg()
		{
           
		}

        public void Dispose()
        {
            generatedGame = null;
            Dictionary = null;
            if (Bag != null) Bag.Dispose();
            if (BagDone != null) BagDone.Dispose();
            if (Rack != null) Rack.Dispose();
            if (OldRack != null) OldRack.Dispose();
            if (Results != null) Results.Unload();
            Results = null;

            if (Rounds != null)
            {
                foreach (PlayedRound item in Rounds)
                {
                    item.Dispose();
                }
            } 
        }
    }
}
