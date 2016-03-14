using System;

namespace TopMachine.Topping.Common.Interfaces
{
	/// <summary>
	/// Summary description for SearchResults.
	/// </summary>
	public interface ISearchResult
	{
		void SearchResults();
        int CheckResults();
        bool CheckTop();
        int GetTotal();
        int GetTotalGame();
	}
}
