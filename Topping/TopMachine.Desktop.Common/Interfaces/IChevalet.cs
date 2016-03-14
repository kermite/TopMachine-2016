using System;
using TopMachine.Topping.Common.Structures;
namespace TopMachine.Topping.Common.Interfaces
{
    public interface IChevalet
    {

        void ChangeColor(System.Drawing.Color c);
        //string Current { get; }
        void DisplayChevalet();
        void InitChevalet(TopMachine.Topping.Common.Structures.GameCfg g, GameControllers ctls, int nbMax);
        void InitRack();
        void replaceAllLetters();
        void ResetLetters();
        System.Windows.Forms.Control ResultControl { get; }
        //void SetBack();
        void SetLetter(char Letter);
        void SetTirage(string Tirage);
        void ShuffleWord();
        String ToString();
    }
}
