using System;
using System.IO;

using TopMachine;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;


namespace TopMachine.Topping.Engine.GameController.Search
{
	/// <summary>
	/// Summary description for LocalResults.
	/// </summary>
	public class SearchResultsLocal : ISearchResult
	{
		GameCfg gc;
        //StreamWriter sw;
        public SearchResultsLocal(GameCfg cfg)
		{
			gc = cfg;
        }
        public bool CheckTop()
        {
            return false;
        }

        public int GetTotal()
        {
            CRound rTop = (CRound)gc.Results.Results.list.GetByIndex(0);
            return rTop.points();
        }
        public int GetTotalGame()
        {
            CRound rTop = (CRound)gc.Results.Results.list.GetByIndex(0);
            return rTop.points();
        }

        public int CheckResults()
        {
            return gc.Results.Results.nresults();
        }

		public void SearchResults()
		{   
			gc.Results = new CResults(500);

            CRack rack = new CRack(gc.GridConfig.cm);
			rack.copy(gc.Rack);
               
			if (gc.iTurn > 1)
                gc.GameBoard.search(gc.Dictionary, gc.Results, rack, gc.Config.MinLetters, gc.intBonus);
			else
                gc.GameBoard.search_first(gc.Dictionary, gc.Results, rack, gc.Config.MinLetters, gc.intBonus);
           
           
           }
	}
}
