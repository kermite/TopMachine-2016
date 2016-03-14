using System;
using System.IO;

using TopMachine;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;


namespace TopMachine.Topping.Engine.GameController.RoundManager
{
	/// <summary>
	/// Summary description for CAutoRoundManager.
	/// </summary>
    public class CManualRoundManager : CRoundManager
    {

        public CManualRoundManager(GameControllers gctls, GameCfg cfg)
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

            return true;


        }
    }
}
