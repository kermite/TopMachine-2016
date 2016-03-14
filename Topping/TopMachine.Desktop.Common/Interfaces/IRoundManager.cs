using System;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg; 

namespace TopMachine.Topping.Common.Interfaces
{
	/// <summary>
	/// Summary description for IRoundManager.
	/// </summary>
    /// 
    public delegate void OnNewTirageEvent();

	public interface IRoundManager
	{
        event OnNewTirageEvent OnNewTirage;

        CRound SelectedRound { get; }
        GameCfg GameConfig { set;get;}
        bool NewTirage(IRackManager RackMan);
        string GetTirage();
        string SetRemainRack(IRackManager RackMan);
        System.Collections.ArrayList LoadComments();
  	}
}

