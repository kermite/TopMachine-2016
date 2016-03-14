using System;
using System.IO;

using TopMachine;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;
using System.Diagnostics;


namespace TopMachine.Topping.Engine.GameController.RoundManager
{
	/// <summary>
	/// Summary description for CAutoRoundManager.
	/// </summary>
	public class CAutoRoundManager : CRoundManager
	{


		public CAutoRoundManager(GameControllers gctls, GameCfg cfg)
			: base(gctls, cfg)
		{

		}
		public string GetTirage(CRack rack)
		{
			// formate le tirage 
			string ss = "";
			rack.AlignTiles();

			for (int x = 0; x < rack.ntiles(); x++)
			{

				char c = System.Convert.ToChar(rack.GetRack(x));
				if (c > 0) ss += new string(c, 1);

			}
			return ss;

		}
		public override bool NewTirage(IRackManager RackMan)
		{   // choisi un tirage de maniere aleatoire

			RackMan.copy(gc.Rack, gc.Bag);

			if (RackMan.IsEndGame())
			{
				return false;
			}

			bool ok = false;

			GameConfig.OldRack = new CRack(gc.GridConfig.cm);
			GameConfig.OldRack.copy(GameConfig.Rack);
			GameConfig.OldRack.AlignTiles();

			GameConfig.iTurn++;

			int rejet = 0;
			do
			{
                Debug.WriteLine("");
                Debug.Write("new tirage : ");
				RackMan.ChooseTirage(GameConfig.Config.Joker, GameConfig.Config.MaxLetters);
				// tester integralite des tirages
				//    string ss = GetTirage(RackMan.Rack);  
          
				if (RackMan.IsCorrectRack(GameConfig.Config.MaxLetters, GameConfig.iTurn))
				{
					ok = true;
				}
				else
				{
					// si le rack n'est pas correct mais peut etre ameliorable
                    Debug.WriteLine("");
                    Debug.Write("Rejet -");
                    rejet++;
					RackMan.RejetTirage();
					GameConfig.OldRack.empty();
				}


			} while (!ok);
            Debug.WriteLine("");
            // si correct on recopy le bag et le rack
			RackMan.copyTo(GameConfig.Rack, GameConfig.Bag);
			NewTirage();

			return true;


		}
	}
}
