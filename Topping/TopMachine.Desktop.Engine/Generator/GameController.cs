using System;
using System.Collections;
using System.IO;
//using TopMachine.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using TopMachine;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;
using TopMachine.Topping.Dto;
using TopMachine.Desktop.Controls.Sound;

namespace TopMachine.Topping.Engine.GameController
{
    /// <summary>
    /// Summary description for new.
    /// </summary>
    public partial class ShareGameController : IGameController, IDisposable 
    {

        
        #region Private Things
        private GameCfg gc;
        private int nbMissTop;
        private string path;
        private bool pathIsSet = false;
        private Statistics statistics;
        #endregion

        #region Public Properties
        public GameControllers gctls = new GameControllers();
        #endregion

        #region public EVENTS
        public event OnGameControllersCreatedEvent OnGameControllersCreated;
        public event OnInitNewTurnEvent OnInitNewTurn;
        public event OnInitGameEvent OnInitGame;
        public event OnStartRoundEvent OnStartRound;
        public event OnNewSelectionChooserEvent OnNewSelectionChooser;
        public event OnErrorTirageEvent OnErrorTirage;
        public event OnActiveControlEvent OnActiveControl;
        public event OnFinishEvent OnFinish;
        public event OnFinishStatEvent OnFinishStat;
        public event OnStopEvent OnStop;
        #endregion

        #region Public Properties
        public GameControllers GameControllers
        {
            set { gctls = value; }
            get { return gctls; }
        }

        public GameCfg GameConfig
        {
            set { gc = value; }
            get { return gc; }
        }
        #endregion

        #region Constructor
        public ShareGameController(GameCfg tcfg, string path)
        {
            gc = tcfg;
            this.path = path;

            if (this.path.Length > 0)
            {
                pathIsSet = true;
            }
        }
        #endregion

        #region INIT

        public void InitGame()
        {
            return;
        }

        public void InitGame(GeneratedGameDto generatedGame,bool stat = false)
        {
            gc.GameState = enuGameState.Initializing;


            gc.OldRack = null;
            gc.intBonus = gc.Config.Bonus;

            gc.Dictionary = new dawgDictionary();

            DicoUtils.SetLanguage(generatedGame.Config.Language);
            gc.Dictionary = DicoUtils.Instance.getDico() ;
            


            gc.Rounds = new ArrayList();
            gc.blnGameRun = true;

            gc.GridConfig = new GridConfig();

            gc.GridConfig.Load(gc.Config.Grid, gc.Config.Language.ToString());

            gc.GameBoard = gc.GetBoard();
            gc.GameBoard.create(gc.Dictionary, gc.GridConfig.cm);

            gc.Bag = new ShuffleBag(gc.GridConfig.cm);
            gc.BagDone = new ShuffleBag(gc.GridConfig.cm);
            gc.BagDone.clear();

            gc.Rack = new CRack(gc.GridConfig.cm);
            gc.Rack.init();

            gc.Results = new CResults(500);
            gc.Results.create();

            gc.iTurn = 0;
            gc.iTotalPlayer = gc.iTotalTop = 0;
            gc.TimeGameStart = new TimeSpan(DateTime.Now.Ticks);

            CreateControllers();

            // Game Controller initialized ready for playing
            gc.GameState = enuGameState.WaitingNextTurn;
            if (stat)
            {
                statistics = new Statistics();
            }
            else 
            {
                statistics = null;
            }
        }

        private void CreateControllers()
        {
            if (gc.Config.Explosive)
            {
                gctls.RoundManager = new RoundManager.CExplosiveRoundPlusManager(gctls, gc);
            }
            else
            {
                if (gc.Config.Name.Contains("NEW"))
                {
                    gctls.RoundManager = new RoundManager.CNewRoundManager(gctls, gc);
                }
                else if (gc.Config.Name.Contains("TRAINING")) 
                {
                    gctls.RoundManager = new RoundManager.CTrainingRoundManager(gctls, gc);
                }
                else
                {
                    gctls.RoundManager = new RoundManager.CAutoRoundManager(gctls, gc);
                }
                
            }
            gctls.SearchResult = new Search.SearchResultsLocal(gc);
            gctls.SolutionChooser = new SolutionChooser.SolutionChooserRandom(this, gc);

            CreateEvents();

            if (OnGameControllersCreated != null)
            {
                OnGameControllersCreated(gctls);
            }
        }
        #endregion

        #region Check Rack Validity
        public bool CheckRackValidity()
        {
            CRack rack = new CRack(gc.GridConfig.cm);
            gctls.RackManager.CreateRack(gc.CurrentRackChars, gc.CurrentRackLength, rack);
            return gctls.RackManager.IsCorrectRack(gc.Bag, rack, gc.Config.MaxLetters, gc.iTurn);
        }
        #endregion

        #region NEXT ROUND PREPAIR RACK
        public void NextRound()
        {
            // TopMachine.CDebug.Debug.Write("Start NextRound ");

            try
            {
                System.Windows.Forms.Application.DoEvents();
            }
            catch(Exception ee) {
               // NLog.LogManager.GetLogger("wcf").ErrorException("GameController : NextRound", ee); 
            }

            if (gc.GameState != enuGameState.WaitingNextTurn)
            {
                return;
            }

            if (gc.CurrentGameRound != null)
            {
                CRack newRack = gc.GameBoard.RestFromRound(gc.Rack, gc.CurrentGameRound);
                gc.Rack.Unload();
                gc.Rack = newRack;
            }

            gc.Results = new CResults(10000);
            gc.Results.create();

            SetupNewTurn();

            if (OnActiveControl != null)
            {
                OnActiveControl(false);
            }

            //TopMachine.CDebug.Debug.Write("End NextRound ");

            // EndRound();

        }
        #endregion

        #region SETUP NEW TURN
        public void SetupNewTurn()
        {

            if (gc.GameState == enuGameState.Finished && gc.GameState == enuGameState.FinishedAndSaved)
            {
                return;
            }

            if (OnInitNewTurn != null) OnInitNewTurn();
            gctls.RackManager = new RackManager.RackManager(gc, gc.Rack, gc.Bag);
            SetupNewTurnContinue();
        }

        public void SetupNewTurnContinue()
        {
            if (gc.GameState == enuGameState.Finished && gc.GameState == enuGameState.FinishedAndSaved)
            {
                return;
            }

            gc.CurrentGameRound = null;
            gc.CurrentRound = null;

            gc.GameState = enuGameState.RackInput;
            if (!gctls.RoundManager.NewTirage(gctls.RackManager))
            {

                FinishGame();
                return;
            }

        }
        #endregion

        #region FinishGame
        private void FinishGame()
        {
            if (gc.GameState != enuGameState.Finished && gc.GameState != enuGameState.FinishedAndSaved)
            {
                gc.blnGameRun = false;
                gc.GameState = enuGameState.Finished;

                if (statistics == null)
                {
                    if (OnFinish != null)
                    {
                        OnFinish();
                    }
                }
                else 
                {

                    if (OnFinishStat  != null)
                    {
                        OnFinishStat(statistics);
                    }
                }
                
            }
        }
        #endregion

        #region ReplaceJoker
        private void ReplaceJoker()
        {
            for (int x = 0; x < gc.CurrentGameRound.wordlen(); x++)
            {
                if (gc.CurrentGameRound.joker(x) == 4 && gc.CurrentGameRound.playedfromrack(x) == 2)
                {
                    int c = gc.CurrentGameRound.gettile(x);
                    if (gc.Bag.isIn(c) > 0)
                    {
                        gc.Bag.replacetile(0);
                        gc.Rack.remove(0);
                        // We Remove the Joker

                        // We replace with the letter
                        gc.Bag.taketile((byte)c);
                        gc.Rack.add((byte)c);
                        gc.CurrentGameRound.Round.tileorigin[x] = 2;
                    }
                }
            }
        }

        #endregion

        #region EndRound
        private void EndRound()
        {
            gc.GameState = enuGameState.GetSolution;
            gctls.SolutionChooser.GetSolutions();

        }
        #endregion

        #region FinalizeRound
        public void FinalizeRound(CRound round)
        {
            // si c'est pas db
            string s, word;
            string[] sss;
            word = "";

            gc.CurrentGameRound = round;

            if (gc.CurrentRound == null)
            {
                gc.CurrentRound = new CRound(gc.GridConfig.cm);
                gc.CurrentRound.create();
            }

            CRack rack = new CRack(gc.GridConfig.cm);
            rack.copy(gc.Rack);

            if (gc.Config.Joker == true)
            {
                gc.CurrentGameRound.copyJoker();
                ReplaceJoker();
            }

            word = gc.CurrentGameRound.getword();

            gc.iTotalTop += gctls.SearchResult.GetTotalGame();
            gc.iTotalPlayer += gctls.SearchResult.GetTotal();


            // postion des lettres via le board
            int[] origin = new int[word.Length];
            for (int x = 0; x < word.Length; x++)
            {
                origin[x] = gc.CurrentGameRound.Round.tileorigin[x];

                if (gc.CurrentGameRound.playedfromrack(x) == 2)
                {
                    if (gc.CurrentGameRound.joker(x) == 4)
                    {
                        gc.BagDone.replacetile(27);
                    }
                    else
                    {
                        gc.BagDone.replacetile(gc.CurrentGameRound.Round.word[x]);
                    }
                }
            }
            if (statistics != null)
            {
                statistics.AnalyzeGame(round, gc.GameBoard,gc.Results);
            }
            gc.GameBoard.addround(gc.CurrentGameRound);
            PlayedRound pr = new PlayedRound(gc, rack, gctls.SearchResult.GetTotal(), gctls.SearchResult.GetTotalGame());
            gc.Rounds.Add(pr);

            gc.GameState = enuGameState.WaitingNextTurn;
           

        }
        #endregion

        #region StartRound
        public void StartRound()
        {
            string ss;


            ss = gctls.RoundManager.GetTirage();
            if (ss.Length > 0)
            {
                SearchResults();
                if (gc.Results.Results.list.Count == 0)
                {
                    gctls.RackManager.RejetTirage();
                    gc.iTurn--;
                    SetupNewTurnContinue();
                }

                gc.CurrentGameRound = null;
                gc.GameState = enuGameState.Playing;

                if (OnStartRound != null)
                {
                    OnStartRound(ss);
                }

                EndRound();

            }
            else
            {
                if (OnErrorTirage != null) OnErrorTirage();
                return;
            }



        }
        #endregion

        #region SearchResults
        public void SearchResults()
        {
            gctls.SearchResult.SearchResults();
            gctls.SolutionChooser.SetResults(gc.Results);
        }
        #endregion

        #region Not Implemented Interfaces
        public void SetTimer() { }
        public void Stop(bool toKill ) {
            
        }
        public bool SelectRoundFromHistory(int round) { return false; }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            statistics = null;

            if (gctls.SolutionChooser != null)
            {
                gctls.SolutionChooser.OnSelectWord -= new SelectWordEvent(SolutionChooser_OnSelectWord);
            }
            if (gctls.RoundManager != null)
            {
                gctls.RoundManager.OnNewTirage -= new OnNewTirageEvent(RoundManager_OnNewTirage);
            }
            if (gc != null)
            {
                gc.Dispose(); 

            }
          gctls.Dispose();
           
            this.Stop(true); 
        }

        #endregion
    }
}
