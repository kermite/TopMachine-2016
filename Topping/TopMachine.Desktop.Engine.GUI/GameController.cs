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
using TopMachine.Topping.Engine.GUI.RoundManager;
using TopMachine.Engine.Local.GameController.SolutionChooser;
using TopMachine.Topping.Engine.GUI.RackManager;
using TopMachine.Desktop.Overall;
using Topping.Core.Logic.Client;
using TopMachine.Topping.Dto;
using Topping.Core.Proxy.Local.Client;
using TopMachine.Desktop.Controls.Sound;


namespace TopMachine.Topping.Engine.GUI
{
    /// <summary>
    /// Summary description for GameController.
    /// </summary>
    public partial class LocalGameController : IGameController, IKeyHandler, IDisposable
    {

        private static object syncRoot = new object();


        #region Private Things
        private GameCfg gc;
        private GameControllerTimer timer;
        private int _typeOfHistory = 0;
        private string _xml = "";
        #endregion

        #region Public Properties
        public IMessageProxy proxy = null;
        public GameControllers gctls = new GameControllers();
        #endregion

        #region public EVENTS
        public event OnGameControllersCreatedEvent OnGameControllersCreated;
        public event OnInitNewTurnEvent OnInitNewTurn;
        public event OnStartRoundEvent OnStartRound;
        //public event OnActiveTopMachineEvent OnActiveTopMachine;
        public event OnErrorTirageEvent OnErrorTirage;
        public event OnActiveControlEvent OnActiveControl;
        public event OnStopEvent OnStop;
        public event OnInitGameEvent OnInitGame;
        public event OnFinishEvent OnFinish;
        public event OnFinishStatEvent OnFinishStat;
        public event OnNewSelectionChooserEvent OnNewSelectionChooser;
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
        public LocalGameController(IMessageProxy proxy)
        {
            this.proxy = proxy;
            GeneratedGameDto game = proxy.GameState.GeneratedGame;
            gc = new GameCfg();

            gc.Config = proxy.GameState.GeneratedGame.Config;
            gctls.GameController = this;

        }
        #endregion

        #region ObserveGame
        public void ObserveGame()
        {
            gc.observeMode = 1;
            gctls.ClassementControl.Initialize(); 
            NextRound();
        }

        #endregion

        #region INIT

        public void InitGame(GeneratedGameDto generatedGame,bool stat = false)
        {
            return;
        }

        public void InitGame()
        {
            gc.GameState = enuGameState.Initializing;

            gc.OldRack = null;
            gc.intBonus = gc.Config.Bonus;

            gc.GridConfig = new GridConfig();
            gc.GridConfig.Load(gc.Config.Grid, gc.Config.Language.ToString());

            ContinueInitGame();
        }

        Form f = null;

        private void ContinueInitGame()
        {

            gc.Dictionary = new dawgDictionary();

            gc.Dictionary = DicoUtils.Instance.getDico();

            gc.Rounds = new ArrayList();

            gc.GameBoard = gc.GetBoard();
            gc.GameBoard.SetMaxOnBoard(gc.Config.MinLetters, gc.intBonus);
            gc.GameBoard.create(gc.Dictionary, gc.GridConfig.cm);

            gc.Bag = new ShuffleBag(gc.GridConfig.cm);

            gc.Rack = new CRack(gc.GridConfig.cm);
            gc.Rack.init();

            gc.Results = new CResults(500);
            gc.Results.create();

            gc.iTurn = 0;
            gc.iTotalPlayer = gc.iTotalTop = 0;
            gc.TimeGameStart = new TimeSpan(0);

            CreateControllers();

            // Game Controller initialized ready for playing
            gc.GameState = enuGameState.WaitingNextTurn;
            gc.blnGameRun = true;

        }

        private void CreateControllers()
        {
            gctls.SearchResult = new Search.SearchResultsLocal(gc);

            gctls.Board = new Board.Board(GameConfig);
            gctls.Chevalet = new Board.Chevalet();
            gctls.RoundControl = new Board.RoundControl();

            gctls.RoundManager = new CXmlRoundManager(gctls, gc, this);
            gctls.SolutionChooser = new SolutionChooserFromHistory(this, gc);

            gctls.Chevalet.DisplayChevalet();
            gctls.Chevalet.InitChevalet(gc, gctls, this.gc.Config.MaxLetters);

            gctls.HistoryControl = new Board.History(this, proxy.GameState);
            gctls.HistoryPlayerControl = new Board.History(this, proxy.GameState);
            gctls.ClassementControl = new Board.Classement(this);
            gctls.PlayedGameRoundControl = new Board.PlayedGameRoundControl(this);


            gctls.KeyHandlers.Add((IKeyHandler)gctls.Chevalet);
            gctls.KeyHandlers.Add((IKeyHandler)gctls.Board);
            gctls.KeyHandlers.Add((IKeyHandler)gctls.HistoryControl);
            gctls.KeyHandlers.Add((IKeyHandler)gctls.ClassementControl);
            CreateEvents();


            InitTimer(); 

            if (OnGameControllersCreated != null)
            {
                OnGameControllersCreated(gctls);
            }
        }
        #endregion

        #region TOP MANAGER
        public void CheckRoundTop()
        {
            if (gctls.RoundManager.SelectedRound.points() == gc.CurrentRound.points())
            {
                if (gc.Config.Toping)
                {
                    EndRound(true);
                }
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
            if (gc.observeMode == 0)
            {
                if (gc.TimeGameStart.Ticks == 0)
                {
                    gc.TimeGameStart = new TimeSpan(DateTime.Now.Ticks);
                }

                if (gc.GameState != enuGameState.WaitingNextTurn)
                {
                    return;
                }
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


        }
        #endregion

        #region SETUP NEW TURN
        public void SetupNewTurn()
        {
            if (OnInitNewTurn != null) OnInitNewTurn();
            gctls.RackManager = new AutoRackManager(gc, gc.Rack, gc.Bag);
            SetupNewTurnContinue();
        }

        public void SetupNewTurnContinue()
        {
            gc.CurrentGameRound = null;
            gc.CurrentRound = null;

            gc.GameState = enuGameState.RackInput;

            if (gctls.RoundManager.NewTirage(gctls.RackManager) == false)
            {
                FinishGame();
                return;
            }

            if (gc.observeMode > 0)
            {
                EndRound(false);
            }


        }
        #endregion

        #region FinishGame
        private void FinishGame()
        {
            gc.blnGameRun = false;
            gc.GameState = enuGameState.Finished;
            gctls.Chevalet.SetTirage("");
            Sound.play(Sound.SoundType.ENDGAME);
            if (OnFinish != null)
            {
                OnFinish();

            }

            proxy.FinishGame();

            gc.GameState = enuGameState.FinishedAndSaved;

            gctls.SolutionChooser.OnRemoveTempWord += new RemoveTempWordEvent(SolutionChooser_OnRemoveTempWord);
            gctls.SolutionChooser.OnSetTempWord += new SetTempWordEvent(SolutionChooser_OnSetTempWord);
            gctls.SolutionChooser.OnSelectWord += new SelectWordEvent(SolutionChooser_OnSelectWord);

            if (OnNewSelectionChooser != null)
            {
                OnNewSelectionChooser(gctls);
            }

            MessageProxy.Proxy.GamePermissions |= GamePermissions.CanObserve;


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
                        gc.Bag.taketile((byte)c);
                        gc.Rack.remove(0);
                        gc.Rack.add((byte)c);
                        gc.CurrentGameRound.Round.tileorigin[x] = 2;
                    }
                }
            }
        }

        #endregion

        #region EndRound
        private void EndRound(bool valide)
        {

            if (timer != null)
            {
                float time = timer.Stop();

                if (valide)
                {
                    TimeSpan tot;
                    gc.CurrentPlay = time;
                    TimeSpan current = new TimeSpan(DateTime.Now.Ticks);
                    tot = current.Subtract(gc.TimeGameStart);
                    gc.TotalTimePlay = (int)tot.TotalSeconds;
                }
                else
                {
                    gc.CurrentPlay = gc.Config.Minutes * 60 + gc.Config.Seconds;
                    gc.TotalTimePlay += (long)gc.CurrentPlay;


                }
                /*System.GC.ReRegisterForFinalize(timer);
                timer = null;*/

            }

            gc.GameState = enuGameState.GetSolution;
            gctls.SolutionChooser.GetSolutions();

        }
        #endregion

        #region FinalizeRound
        public void FinalizeRound(CRound round)
        {

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

            if (gc.Config.Manual) // gc.Config.Joker == true &&  _typeOfHistory != 2)
            {
                ReplaceJoker();
            }

            word = gc.CurrentGameRound.getword();

            gc.iTotalPlayer += gc.CurrentRound.points();
            gc.iTotalTop += gctls.RoundManager.SelectedRound.points();

            if (gc.CurrentRound.points() == gctls.RoundManager.SelectedRound.points())
            {
                gc.iTotalPlayerTop++;
            }

            int[] origin = new int[word.Length];
            for (int x = 0; x < word.Length; x++)
            {
                origin[x] = gc.CurrentGameRound.Round.tileorigin[x];
            }

            gc.GameBoard.addround(gc.CurrentGameRound);


            PlayedRound pr = null;
            pr = new PlayedRound(gc, gc.Rack, gc.CurrentRound.points(), gctls.RoundManager.SelectedRound.points());

            gc.Rounds.Add(pr);

            PlayerSummaryDto dto = new PlayerSummaryDto(
                "",
                gc.TotalTimePlay,
                gc.iTurn,
                gc.iTotalPlayer,
                gc.iTotalPlayerTop,
                gc.iTotalPlayer - gc.iTotalTop,
                gc.iTotalPlayer / gc.iTotalTop);

            PlayedGameRoundDto dtoRound = new PlayedGameRoundDto(
                "", gc.CurrentPlay,
                gc.iTurn, gc.CurrentRound.getword(),
                PlayedGameRoundDto.ConvertCoordonnee(gc.CurrentRound.row(), gc.CurrentRound.column(), gc.CurrentRound.dir()),
                gc.CurrentRound.points());

            if (gc.observeMode == 0)
            {
                proxy.AddPlayedRound(dto, dtoRound);
            }

            gctls.HistoryControl.SetRound(dto.Turn);

            gctls.Board.RemoveTmp(true);
            gctls.Board.SetWord(gc.CurrentGameRound);

            gctls.RoundControl.SetScore(gc.iTotalPlayer, gc.iTotalTop, gc.iTurn, (double)(gc.iTotalPlayer * 100) / gc.iTotalTop);
            gctls.RoundControl.RoundDisplay(gc.CurrentGameRound);
            gc.GameState = enuGameState.WaitingNextTurn;
            //Sound.play(Sound.SoundType.ENDROUND);


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

                gctls.Chevalet.SetTirage(ss);
                gctls.Board.SetTirage(ss);
                // code

                gc.CurrentGameRound = null;


                if (OnStartRound != null)
                {
                    OnStartRound(ss);
                }

                if (gc.iTurn == 0)
                {
                    gc.TimePlayStart = new TimeSpan(DateTime.Now.Ticks);
                }
                if (gc.GameState != enuGameState.Finished)
                {
                    SetTimer();
                    gc.GameState = enuGameState.Playing;
                }
            }
            else
            {
                if (OnErrorTirage != null) OnErrorTirage();
                return;
            }



        }
        #endregion

        #region STOP
        public void Stop(bool toKill)
        {

            if (timer != null)
            {
                timer.Stop();
            }

         
                if (gc != null)
                {
                    if (toKill)
                    {
                        if (gc.GameState < enuGameState.Finished)
                        {
                            if (proxy != null)
                            {
                            proxy.FinishGame();
                            }
                        }
             
                    gc.GameState = enuGameState.Finished;
                    gc.GameState = enuGameState.IsStopping;
                    }
                 if (OnStop != null)
                {
                    OnStop();
                }

                if (timer != null) timer.Dispose(); 

            }
        }
        #endregion

        #region SearchResults
        public void SearchResults()
        {
            gctls.SolutionChooser.SetResults(gc.Results);
        }
        #endregion

        #region Timer

        public void InitTimer()
        {
            if (timer == null)
            {
                timer = new GameControllerTimer(gc.Config.Minutes * 60 + gc.Config.Seconds);
                timer.OnChronoEnd += new GameControllerTimer.OnChronoEndEvent(timer_OnChronoEnd);
                timer.OnChronoUpdate += new GameControllerTimer.OnChronoUpdateEvent(timer_OnChronoUpdate);
            }
        }

        public bool EndSound = false;
        public void SetTimer()
        {

            gc.TimePlayStart = new TimeSpan(DateTime.Now.Ticks);
            if (gc.Config.Seconds > 0 || gc.Config.Minutes > 0)
            {
                gc.CurrentPlay = 0;
                gc.TimePlay = gc.Config.Minutes * 60 + gc.Config.Seconds;

                EndSound = false;

                timer.StartTimer();
            }


        }

        void timer_OnChronoUpdate(double timeSpent, double timeLeft)
        {
            if (!EndSound && timeLeft <= 15 && !gc.demo)
            {
                // Control*** Set round Manager
                // Play Sound
                // WordLibrary.WavPlayer.Play(Tools.FileUtility.GetFile("\\Sounds\\gong.wav"));
                Sound.play(Sound.SoundType.BEFOREENDROUND);
                EndSound = true;
            }

            gctls.RoundControl.SetChrono((long)timeLeft, 0);
            gctls.RoundControl.SetChrono((long)timeLeft, (long)new TimeSpan(DateTime.Now.Ticks).Subtract(gc.TimeGameStart).TotalSeconds);
        }

        void timer_OnChronoEnd()
        {
            lock (syncRoot)
            {
                EndRound(false);
            }
        }

        #endregion

        #region HISTORY Navigation

        public bool SelectRoundFromHistory(int round)
        {
            if (MessageProxy.Proxy.GamePermissions.HasFlag(GamePermissions.CanObserve))
            {

                PlayedRound cround = null;
                gctls.Board.RemoveAll(true);
                gctls.Board.RemoveTmp(true);

                gc.GameBoard = gc.GetBoard();
                gc.GameBoard.create(gc.Dictionary, gc.GridConfig.cm);
                gc.GameBoard.SetMaxOnBoard(gc.Config.MinLetters, gc.intBonus);
                gc.GameBoard.init();

                for (int x = 0; x < round; x++)
                {
                    cround = (PlayedRound)gc.Rounds[x];
                    gctls.Board.SetWord(cround.PlacedRound);
                    gc.GameBoard.addround(cround.PlacedRound);
                }

                if (round < gc.Rounds.Count)
                {
                    cround = (PlayedRound)gc.Rounds[round];
                    gc.iTurn = round + 1;

                    gc.Rack = new CRack(gc.GridConfig.cm);
                    gc.Rack.copy(cround.CRack);
                    gc.Rack.UnalignTiles();

                    gc.Bag = cround.Bag;

                    gctls.SearchResult.SearchResults();
                    gctls.SolutionChooser.SetResults(gc.Results, true);

                    gctls.Board.SetTirage(gctls.RoundManager.GetTirage());
                    gctls.Chevalet.SetTirage(gctls.RoundManager.GetTirage());

                }
            }
            return true;
        }

        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            if (gctls != null)
            {

                gctls.Board.OnWordUpdate -= new OnWordUpdateEvent(Board_OnWordUpdate);
                gctls.SolutionChooser.OnRemoveTempWord -= new RemoveTempWordEvent(SolutionChooser_OnRemoveTempWord);
                gctls.SolutionChooser.OnSetTempWord -= new SetTempWordEvent(SolutionChooser_OnSetTempWord);
                gctls.SolutionChooser.OnSelectWord -= new SelectWordEvent(SolutionChooser_OnSelectWord);
                gctls.RoundManager.OnNewTirage -= new OnNewTirageEvent(RoundManager_OnNewTirage);
                gctls.HistoryControl.OnSelectedRound -= new OnSelectedRoundEvent(HistoryManager_OnSelectedRound);
                gctls.HistoryPlayerControl.OnSelectedRound -= new OnSelectedRoundEvent(HistoryManager_OnSelectedRound);

                gctls.KeyHandlers.Remove((IKeyHandler)gctls.Chevalet);
                gctls.KeyHandlers.Remove((IKeyHandler)gctls.Board);
                gctls.KeyHandlers.Remove((IKeyHandler)gctls.HistoryControl);
                gctls.KeyHandlers.Remove((IKeyHandler)gctls.ClassementControl);

                gctls.Dispose(); 
            
            }

            gctls = null;

            this.Stop(true);

            if (gc != null) gc.Dispose();
            gc = null;

            if (timer != null)
            {

                timer.OnChronoEnd -= new GameControllerTimer.OnChronoEndEvent(timer_OnChronoEnd);
                timer.OnChronoUpdate -= new GameControllerTimer.OnChronoUpdateEvent(timer_OnChronoUpdate);
            }
        
                 
           
        }

        #endregion
    }
}
