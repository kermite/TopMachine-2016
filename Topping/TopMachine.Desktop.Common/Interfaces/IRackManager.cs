using System;
using TopMachine.Topping.Dawg; 

namespace TopMachine.Topping.Common.Interfaces 
{
    public interface IRackManager
    {
        void addFormatTirage(string formatTirage, bool restart);
        void AddLetter(byte l);
        void AddLetter(char c);
        byte ChooseRandomBag();
        void ChooseTirage(bool joker, int intMinLettres);
        void copy(CRack Track, ShuffleBag Tbag);
        void copyTo(CRack Track, ShuffleBag Tbag);
        void CreateRack(char[] CurrentRackChars, int CurrentRackLength, CRack rack);
        string GetTirage(int intMaxLettres);
        bool IsBagEmpty();
        bool IsBagEmpty(ShuffleBag bag);
        bool IsCorrectJoker();
        bool IsCorrectRack(ShuffleBag bag, CRack rack, int minRack, int turn);
        bool IsCorrectRack(int intMaxLettres, int turn);
        int nbTilesBag();
        int nbTilesRack();
        bool RejetTirage();
        void RemoveLetter(char c);
        bool IsEndGame();
       // string GetBag();
        ShuffleBag Bag { get; }
        CRack Rack { get; }
    }
}
