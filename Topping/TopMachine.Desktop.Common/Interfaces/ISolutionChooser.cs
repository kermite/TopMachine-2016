using System;
using System.Collections; 
using System.Windows.Forms;

using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg; 
namespace TopMachine.Topping.Common.Interfaces
{
	/// <summary>
	/// Summary description for ISolutionChooser.
	/// </summary>
    /// 
    public delegate void RemoveTempWordEvent(bool ok);
    public delegate void SetTempWordEvent(string word, int[] origin, int row, int col, int dir);
    public delegate void SelectWordEvent(CRound round);

	public interface ISolutionChooser : IDisposable 
	{

        event RemoveTempWordEvent OnRemoveTempWord;
        event SetTempWordEvent OnSetTempWord;
        event SelectWordEvent OnSelectWord;

        Structures.GameCfg GameConfig { set; }

        void GetSolutions();
        void SetResults(CResults results);
        void SetResults(CResults results, bool visible);
        void SetResults(CResults results, ArrayList list, bool visible);
        /*Control ResultControl { get; }*/
        /*void SetVisible(bool ok);*/
	}
}
