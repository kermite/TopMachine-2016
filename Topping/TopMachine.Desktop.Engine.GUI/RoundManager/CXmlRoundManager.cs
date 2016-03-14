using System;
using System.Collections;
using System.IO;
//using TopMachine.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

using TopMachine.Topping.Dawg;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Engine.GUI.Board;
using TopMachine.Topping.Dto;


namespace TopMachine.Topping.Engine.GUI.RoundManager
{
	/// <summary>
	/// Summary description for CAutoRoundManager.
	/// </summary>
	public class CXmlRoundManager : CRoundManager
	{ // gestion de la db


        public string wordOriginalPc, wordPc, positionPc,tiragePc;
        public string wordPlayer, positionPlayer, PointPlayer,timePlayer;

        private LocalGameController controller;

        public CXmlRoundManager(GameControllers gctls, GameCfg cfg, string xml, Guid joueur)
            : base(gctls, cfg)
        {
        }


        public CXmlRoundManager(GameControllers gctls, GameCfg cfg, LocalGameController controller)
            : base(gctls, cfg)
        {
            this.controller = controller; 
		}

        private bool GetTiragePc()
        {
            tiragePc = "";
           

            GameRoundDto round = controller.proxy.GameState.GetRound(GameConfig.iTurn);

            if (round == null)
            {
                return true; 
            }

            this.tiragePc = round.Rack;
            if (tiragePc.Length == 0) return false;

            if (round.SolutionSet == true)
            {
                GetSolutionPc();
            }

            return true;
                    
        }

        private bool GetSolutionPc()
        {

            GameRoundDto round = controller.proxy.GameState.GetRound(GameConfig.iTurn);

            if (round == null || round.SolutionSet == false)
            {
                return true;
            }

            this.tiragePc = round.Rack;
            if (tiragePc.Length == 0) return false;

            this.wordPc = round.Word;
            this.wordOriginalPc = round.Word;

            this.positionPc = round.Direction;
            int pts = round.Points;


            _selectedRound = new CRound(GameConfig.GridConfig.cm);
            _selectedRound.create();
            _selectedRound.setword(this.wordPc.ToUpper());
            _selectedRound.setpoints(pts);


            char[] pos = this.positionPc.ToCharArray();

            if (pos[0] >= 'A' && pos[0] <= 'Z')
            {
                _selectedRound.setdir(1);
                _selectedRound.setrow((int)(pos[0]) - 64);
                int col = int.Parse(positionPc.Substring(1));
                _selectedRound.setcolumn(col);
            }
            else
            {
                _selectedRound.setdir(0);
                _selectedRound.setrow((int)pos[pos.Length - 1] - 64);
                int col = int.Parse(positionPc.Substring(0, positionPc.Length - 1));
                _selectedRound.setcolumn(col);
            }

            for (int x = 0; x < wordPc.Length; x++)
            {
                if (_selectedRound.dir() == 1)
                {
                    if (gc.GameBoard.vacant(_selectedRound.row(), _selectedRound.column() + x) == 1)
                    {
                        if (wordPc[x] >= 'A' && wordPc[x] <= 'Z')
                        {
                            _selectedRound.SetTileOrigin(x, 2);
                        }
                        else
                        {
                            _selectedRound.SetTileOrigin(x, 2 | 4);
                        }
                    }
                    else
                    {
                        if (wordPc[x] >= 'A' && wordPc[x] <= 'Z')
                        {
                            _selectedRound.SetTileOrigin(x, 1);
                        }
                        else
                        {
                            _selectedRound.SetTileOrigin(x, 1 | 4);
                        }
                    }
                }
                else
                {
                    if (gc.GameBoard.vacant(_selectedRound.row() + x, _selectedRound.column()) == 1)
                    {
                        if (wordPc[x] >= 'A' && wordPc[x] <= 'Z')
                        {
                            _selectedRound.SetTileOrigin(x, 2);
                        }
                        else
                        {
                            _selectedRound.SetTileOrigin(x, 2 | 4);
                        }
                    }
                    else
                    {
                        if (wordPc[x] >= 'A' && wordPc[x] <= 'Z')
                        {
                            _selectedRound.SetTileOrigin(x, 1);
                        }
                        else
                        {
                            _selectedRound.SetTileOrigin(x, 1 | 4);
                        }
                    }
                }
            }

            for (int x = 0; x < wordPc.Length; x++)
            {
                if (wordPc[x] != wordOriginalPc[x])
                {
                    if (wordOriginalPc[x] >= 'a' && wordOriginalPc[x] <= 'z')
                    {
                        _selectedRound.RemoveAfterRound = _selectedRound.cm.GetTileCode(wordPc[x]);
                    }
                }
            }

            return true;

        }
        public override bool NewTirage(IRackManager RackMan)
        {
            GameConfig.iTurn++;
           
            // chercher les infos dans la db verifier si on est en mode visualisation
            this.GetTiragePc();
            if (tiragePc.Length == 0)
            {
                return false; // fin du jeu
            }

            RackMan.addFormatTirage(this.tiragePc, true);
            RackMan.copyTo(GameConfig.Rack, GameConfig.Bag);

            NewTirage();

            return true;
        }
       

	}
}
