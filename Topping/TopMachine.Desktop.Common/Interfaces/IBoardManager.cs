using System;
using TopMachine.Topping.Dawg; 
namespace TopMachine.Topping.Common.Interfaces
{
    public delegate void OnWordUpdateEvent(string Word, int Horizontal, int Vertical, int Direction, bool Final, int[]TilesStatus, char[]Tirage, bool[]Placed,bool refresh);

    public interface IBoardManager
    {
        Structures.GameCfg GameConfig { set; }

        
        bool LoadArrow(bool doRenew, bool doRenewPos, int dir);
        event OnWordUpdateEvent OnWordUpdate;
        void RemoveAll(bool redraw);
        bool RemoveTmp(bool redraw);
        void Reset();
        void SetTirage(string Tirage);
        void SetWord(CRound round);
        void SetWord(string strWord, int[] orig, int posv, int posh, int direction);

        void SetWordTmp(CRound round);
        void SetWordTmp(string strWord, int[] orig, int posv, int posh, int direction);
        void UpdateWord(bool Validate, bool refresh);
        void ResetLetters(); 
        System.Windows.Forms.Control ResultControl { get; }
       


    }
}
