using System;
namespace TopMachine.Topping.Common.Interfaces
{
    public interface IDemoPlayer
    {
        int Infos { get; set; }
        Structures.GameCfg GameConfig { set; }
        System.Windows.Forms.Control ResultControl { get; }

        bool NextInfo();
        bool NextPostInfo();
        void PreparePostRound();
        void PrepareRound();
    }
}
