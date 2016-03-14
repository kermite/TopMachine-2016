using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Dawg;

namespace TopMachine.Topping.Engine.GUI.Search
{
    public class SearchResultsLocal: ISearchResult
    {
       GameCfg gc;

        public SearchResultsLocal(GameCfg cfg)
		{
			gc = cfg;
        }


        public int CheckResults()
        {
            return gc.Results.Results.nresults(); 
        }

        int totPts = 0;
        int totGame = 0;


        public int GetTotal()
        {
            return gc.CurrentRound.points();
        }

        public int GetTotalGame()
        {
            if (gc.Results.Results.nresults() > 0)
            {
                CRound rTop = (CRound)gc.Results.Results.list.GetByIndex(0);
                return rTop.points();
            }
            else
            {
                return 0; 
            }
        }

        public bool CheckTop()
        {
            CRound rTop = (CRound)gc.Results.Results.list.GetByIndex(0);
            int top = rTop.points();
            int totpts = 0;
            int nbTops = 0;
            
            if (gc.CurrentRound == null) return false; 

            if (gc.CurrentRound.points() == top)
            {
                foreach (CRound r in gc.Results.Results.list.GetValueList())
                {
                    if (r.points() == top)
                    {
                        nbTops++;
                        if (gc.CurrentRound.Compare(r))
                        {
                            totpts = r.points();
                        }
                    }
                }
            }


            totPts = gc.CurrentRound.points();

            if (totpts == top)
            {
                return true;
            }

            return false;
        }

		public void SearchResults()
		{
            totPts = 0;
            gc.Results = new CResultsAll(500);

            CRack rack = new CRack(gc.GridConfig.cm);
			rack.copy(gc.Rack);
               
			if (gc.iTurn > 1)
                gc.GameBoard.search(gc.Dictionary, gc.Results, rack, gc.Config.MinLetters, gc.intBonus);
			else
                gc.GameBoard.search_first(gc.Dictionary, gc.Results, rack, gc.Config.MinLetters, gc.intBonus);

           }
	
    }
}
