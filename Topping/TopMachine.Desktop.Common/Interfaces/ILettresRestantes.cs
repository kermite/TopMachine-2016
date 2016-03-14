using System;

namespace TopMachine.Topping.Common.Interfaces
{
    public interface ILettresRestantes
    {
        System.Windows.Forms.Control ResultControl { get; }
        void ResetLetters();
        void ResetLettersTotal();
        void SetConfig(Structures.GameCfg cfg); 
    }
}
