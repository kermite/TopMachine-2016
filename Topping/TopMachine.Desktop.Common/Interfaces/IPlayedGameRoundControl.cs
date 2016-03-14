using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Dto;

namespace TopMachine.Topping.Common.Interfaces
{   
    public interface IPlayedGameRoundControl
    {
        event OnSelectedPlayerEvent OnSelectedPlayer;

        Structures.GameCfg GameConfig { set; }
        System.Windows.Forms.Control ResultControl { get; }

        bool AutoRefresh { get; set; }
        void RefreshBoardRound();
    }
}
