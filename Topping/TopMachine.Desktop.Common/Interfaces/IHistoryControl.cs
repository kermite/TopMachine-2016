using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Dto;

namespace TopMachine.Topping.Common.Interfaces
{
    public delegate bool OnSelectedRoundEvent(int round);
   
        public interface IHistoryControl
        {
            event OnSelectedRoundEvent OnSelectedRound;
            System.Windows.Forms.Control ResultControl { get; }
            void SetHistory(List<PlayedGameRoundDto> lstRoundPlayer);
            void SetRound(int round);
            void SetRound(GameRoundDto rndDis);
            
        }
}
