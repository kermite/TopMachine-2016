using System;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dto;

namespace TopMachine.Topping.Common.Interfaces
{
	/// <summary>
	/// Summary description for IGameController.
	/// </summary>
    
    public delegate void OnGameControllersCreatedEvent(Structures.GameControllers gctls);
    public delegate void OnInitNewTurnEvent();
    public delegate void OnInitGameEvent();
    public delegate void OnStartRoundEvent(string ss);
    public delegate void OnErrorTirageEvent();
    public delegate void OnActiveControlEvent(bool active);
    public delegate void OnStopEvent();
    public delegate void OnFinishEvent();
    public delegate void OnFinishStatEvent(object s);
    public delegate void OnNewSelectionChooserEvent(Structures.GameControllers gctls);

    public interface IGameController : IDisposable 
	{
        TopMachine.Topping.Common.Structures.GameControllers GameControllers { get; set; }

        event OnGameControllersCreatedEvent OnGameControllersCreated;
        event OnInitNewTurnEvent OnInitNewTurn;
        event OnStartRoundEvent OnStartRound;
        event OnErrorTirageEvent OnErrorTirage;
        event OnActiveControlEvent OnActiveControl;
        event OnStopEvent OnStop;
        event OnInitGameEvent OnInitGame;
        event OnFinishEvent OnFinish;
        event OnFinishStatEvent OnFinishStat;
        event OnNewSelectionChooserEvent OnNewSelectionChooser;

        void InitGame();          // Ok VALIDATED
        void InitGame(GeneratedGameDto generatedGame,bool stat = false);
        void SetupNewTurn();
        void StartRound();
        void NextRound();
        void RemoveEvent();
        void SetTimer();
        void Stop(bool toKill);
        bool SelectRoundFromHistory(int round);
    }
}
