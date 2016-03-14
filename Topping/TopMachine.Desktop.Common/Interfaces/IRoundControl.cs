using System;
using TopMachine.Topping.Dawg;
namespace TopMachine.Topping.Common.Interfaces
{

    public interface IRoundControl
    {
        void InitTurn();
        System.Windows.Forms.Control ResultControl { get; }
        void WordDisplay(bool Final, string word, string s, string pt);
        void RoundDisplay(CRound round);
        void SetChrono(long Seconds, long SecondsAll);
        void SetScore(int player, int top, int coup, double percentage);
    }
}
