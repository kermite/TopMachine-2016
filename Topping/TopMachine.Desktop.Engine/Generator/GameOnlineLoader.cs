using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

using TopMachine;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;
using TopMachine.Topping.Dto;

namespace TopMachine.Topping.Engine.GameController
{
    public class GameOnlineLoader : IDisposable 
    {
        public delegate void OnGameCreatedEvent(GeneratedGameDto game);
        public delegate void OnFinishGameStatEvent(object s);
        public event OnGameCreatedEvent OnGameCreated;
        public event OnInitNewTurnEvent OnGameNewTurn;
        public event OnFinishGameStatEvent OnFinishStat;
        GameCfg _cfg = null;

        long configId = 0;
        ShareGameController _gctl = null;
        GeneratedGameDto originalConfig;

        public GameOnlineLoader(GeneratedGameDto g)
        {
            originalConfig = g;
            _cfg = new GameCfg();

            _cfg.Config = g.Config;
        }
        public void GenerateLocalGame(bool stat)
        {
            try
            {
                if (!stat)
                {
                    GenerateLocalGame();
                    return;
                }
                _gctl = new ShareGameController(_cfg, "");
                _gctl.InitGame(originalConfig, stat);
                _gctl.OnFinishStat += new Common.Interfaces.OnFinishStatEvent(gctl_OnFinishStat); 
                _gctl.OnInitNewTurn += new OnInitNewTurnEvent(_gctl_OnInitNewTurn);
                _gctl.NextRound();
            }
            catch (Exception ee)
            {
                //NLog.LogManager.GetLogger("wcf").ErrorException("GameOnlineLoader : GenerateLocalGame(stat)", ee);
            }
        }

        void _gctl_OnFinishStat(object s)
        {
            if (OnFinishStat != null) 
            {
                OnFinishStat(s); 
            }
        }


        public void GenerateLocalGame()
        {
            try
            {
                _gctl = new ShareGameController(_cfg, "");
                _gctl.InitGame(originalConfig);
                _gctl.OnFinish += new OnFinishEvent(gctl_OnFinish);
                _gctl.OnInitNewTurn += new OnInitNewTurnEvent(_gctl_OnInitNewTurn);
                _gctl.NextRound();
            }
            catch (Exception ee)
            {
               // NLog.LogManager.GetLogger("wcf").ErrorException("GameOnlineLoader : GenerateLocalGame", ee);
            }
        }

        void _gctl_OnInitNewTurn()
        {
            if (OnGameNewTurn != null)
            {
                OnGameNewTurn();
            }
        }
        void gctl_OnFinishStat(object stat)
        {
            Statistics s = (Statistics)stat;
            OnFinishStat(stat);
           

        }
        public void PlayGame() 
        {
            gctl_OnFinish();
        }
        
        void gctl_OnFinish()
        {

            if (OnGameCreated != null)
            {   
                foreach (PlayedRound cr in _gctl.GameConfig.Rounds)
                {
                    GameRoundDto round = new GameRoundDto();


                    CRound rnd = cr.PlacedRound;
                    char c = (char)rnd.column();
                    char r = (char)rnd.row();
                    string txt = "";
                    if (rnd.dir() == 1)
                    {
                        txt = ((char)(r + 64)).ToString() + ((int)c).ToString();
                    }

                    if (rnd.dir() == 0)
                    {
                        txt = ((int)c).ToString() + ((char)(r + 64)).ToString();
                    }

                    round.Direction = txt;
                    round.Points = rnd.points();
                    round.Rack = cr.Rack;
                    round.Turn = cr.RoundNumber;
                    round.Word = rnd.getwordwithjoker();
                    round.SolutionSet = true;
                    originalConfig.Rounds.Add(round);
                }

                OnGameCreated(originalConfig);
            }

            _gctl.OnFinish -= new OnFinishEvent(gctl_OnFinish);
            _gctl.OnInitNewTurn -= new OnInitNewTurnEvent(_gctl_OnInitNewTurn);
        }

        public void Dispose()
        {
           if(_gctl != null) _gctl.Dispose();
           if (_cfg != null)  _cfg.Dispose();  
          
        }
    }
}
