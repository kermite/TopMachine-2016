
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using TopMachine;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;

namespace TopMachine.Topping.Engine.GameController
{
    public partial class ShareGameController
    {

        public void CreateEvents()
        {
            gctls.SolutionChooser.OnSelectWord += new SelectWordEvent(SolutionChooser_OnSelectWord);
            gctls.RoundManager.OnNewTirage += new OnNewTirageEvent(RoundManager_OnNewTirage);
        }


        bool HistoryManager_OnSelectedRound(int round)
        {
            return SelectRoundFromHistory(round);
        }

        void RoundManager_OnNewTirage()
        {
            gc.blnRoundEnd = false;
            StartRound();
        }

        void SolutionChooser_OnSelectWord(CRound round)
        {
            FinalizeRound(round);
            NextRound();
        }


        public void RemoveEvent()
        {

        }
        
    }
}
