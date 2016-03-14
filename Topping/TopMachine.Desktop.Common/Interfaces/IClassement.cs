using System;
using TopMachine.Topping.Dto;
using System.Collections.Generic;

namespace TopMachine.Topping.Common.Interfaces
{
    public delegate bool OnSelectedPlayerEvent(string player, List<PlayedGameRoundDto> lstPlayerRound);

    public interface IClassement
    {
        event OnSelectedPlayerEvent OnSelectedPlayer;

        Structures.GameCfg GameConfig { set; }
        System.Windows.Forms.Control ResultControl { get; }

        void Initialize();
        void DisplayTurn(int turn, int temps, int total);
        void UpdateTurn(int sec);
        
    }
}
