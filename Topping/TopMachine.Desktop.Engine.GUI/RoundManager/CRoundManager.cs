using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

using TopMachine.Topping.Dawg;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Engine.GUI.Board;


namespace TopMachine.Topping.Engine.GUI.RoundManager
{
    public class CRoundManager : IRoundManager
    {
        protected GameCfg gc;
        protected GameControllers gctls;
        protected IRackManager rackManager;

        public event OnNewTirageEvent OnNewTirage;


        public CRound _selectedRound;
        public CRound SelectedRound
        {
            get { return _selectedRound; }
        }

        public GameCfg GameConfig
        {
            set { gc = value; }
            get { return gc; }
        }

        public virtual bool NewTirage(IRackManager RackMan)
        {
            if (OnNewTirage != null)
                OnNewTirage();

            return true;
        }

        public CRoundManager(GameControllers gctls, GameCfg cfg)
        {
            this.gctls = gctls;
            gc = cfg;

        }

        public virtual System.Collections.ArrayList LoadComments()
        {
            return null;
        }

        public string GetTirage()
        {
            // formate le tirage 
            string ss = "";
            gc.Rack.AlignTiles();

            for (int x = 0; x < gc.Rack.ntiles(); x++)
            {
                char c = System.Convert.ToChar(gc.Rack.GetRack(x));
                if (c > 0) ss += new string(c, 1);
            }
            return ss;

        }

        public string SetRemainRack(IRackManager RackMan)
        {
            rackManager = RackMan;

            string ss = "";
            //if (RackMan.IsIncorrectRack(GameConfig.Config.intMaxLettres, GameConfig.iTurn)) return ss;
            gc.CurrentRackChars = new char[gc.Config.MaxLetters];
            for (int x = 0; x < gc.Rack.ntiles(); x++)
            {

                char c = char.ToUpper(System.Convert.ToChar(gc.Rack.GetRack(x)));
                if (c > 0) gc.CurrentRackChars[x] = c;
                ss += c; 
            }

            gc.CurrentRackLength = ss.Length;
            return ss;
        }

        protected void NewTirage()
        {
            if (OnNewTirage != null)
            {
                OnNewTirage();
            }
        }

        public string GetTirage(CRack rack)
        {
            // formate le tirage 
            string ss = "";
            rack.AlignTiles();

            for (int x = 0; x < gc.Rack.ntiles(); x++)
            {

                char c = System.Convert.ToChar(rack.GetRack(x));
                if (c > 0) ss += new string(c, 1);

            }
            return ss;

        }

    }
}
