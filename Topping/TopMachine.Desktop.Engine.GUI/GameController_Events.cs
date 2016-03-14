
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using TopMachine.Topping.Dawg;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Desktop.Overall;
using Topping.Core.Proxy.Local.Client;

namespace TopMachine.Topping.Engine.GUI
{
    public partial class LocalGameController
    {

        public void CreateEvents()
        {
            gctls.Board.OnWordUpdate += new OnWordUpdateEvent(Board_OnWordUpdate);
            gctls.SolutionChooser.OnRemoveTempWord += new RemoveTempWordEvent(SolutionChooser_OnRemoveTempWord);
            gctls.SolutionChooser.OnSetTempWord += new SetTempWordEvent(SolutionChooser_OnSetTempWord);
            gctls.SolutionChooser.OnSelectWord += new SelectWordEvent(SolutionChooser_OnSelectWord);
            gctls.RoundManager.OnNewTirage += new OnNewTirageEvent(RoundManager_OnNewTirage);
            gctls.HistoryControl.OnSelectedRound += new OnSelectedRoundEvent(HistoryManager_OnSelectedRound);
            gctls.HistoryPlayerControl.OnSelectedRound += new OnSelectedRoundEvent(HistoryManager_OnSelectedRound);
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

        #region SOLUTION CHOOSER EVENTS
        void SolutionChooser_OnSetTempWord(string word, int[] origin, int row, int col, int dir)
        {
            gctls.Board.SetWordTmp(word, origin, row, col, dir);
        }

        void SolutionChooser_OnRemoveTempWord(bool ok)
        {
            gctls.Board.RemoveTmp(true);
        }
        #endregion

        #region BOARD EVENTS
        void Board_OnWordUpdate(string Word, int Horizontal, int Vertical, int Direction, bool Final, int[] TilesStatus, char[] Tirage, bool[] Placed, bool refresh)
        {

            /// Update Rack Display
            if (Tirage != null)
            {
                string stirage = "";
                for (int xx = 0; xx < Tirage.Length; xx++)
                {
                    if (!Placed[xx])
                        stirage += Tirage[xx];
                }

                gctls.Chevalet.SetTirage(stirage);
            }

            if (Word.Length > 1)
            {
                // We create a ROUND TO check WORD Validity
                CRound round = new CRound(gc.GridConfig.cm);
                round.create();
                round.setword(Word);

                int x = 0;
                foreach (int cc in TilesStatus)
                {
                    round.SetTileOrigin(x, cc);
                    x++;
                }

                if (Direction == 0)
                {
                    round.setcolumn(Vertical + 1);
                    round.setrow(Horizontal + 1);
                }
                else
                {
                    round.setcolumn(Horizontal + 1);
                    round.setrow(Vertical + 1);
                }

                round.setdir(Direction);

                if (Final)
                {
                    gc.GameBoard.evalmoveandcheck(round, gc.iTurn);
                }
                else
                {
                    gc.GameBoard.evalmove(round);
                }

                if (Direction == 0)
                {
                    round.setcolumn(Horizontal + 1);
                    round.setrow(Vertical + 1);
                }


                char c = (char)(65 + Vertical);
                string s;
                if (Direction == 1)
                {
                    s = c + "/" + ((int)(Horizontal + 1)).ToString();
                }
                else
                {
                    s = ((int)(Horizontal + 1)).ToString() + " / " + c;
                }


                ///// UPDATE DISPLAY WITH CURRENT WORD OR VALIDATED WORD
                if (Final == false)
                {
                    gctls.RoundControl.WordDisplay(false, Word, s, round.points().ToString());
                }
                else
                {
                    lock (syncRoot)
                    {
                        gctls.Board.RemoveTmp(true);
                        if (round.countfromrack() > 0)
                        {
                            if (gc.Config.Toping == true)
                            {

                                if (gc.Config.TypeOfGame != 1)
                                {   
                                    if (gc.CurrentRound == null || gc.CurrentRound.points() < round.points())
                                    {
                                        gctls.RoundControl.WordDisplay(true, Word, s, round.points().ToString());
                                        gc.CurrentRound = round;
                                        CheckRoundTop();
                                    }
                                }
                                else
                                {
                                    gctls.RoundControl.WordDisplay(true, Word, s, round.points().ToString());
                                    gc.CurrentRound = round;
                                    CheckRoundTop();
                                }

                            }
                            else
                            {
                                gctls.RoundControl.WordDisplay(true, Word, s, round.points().ToString());
                                gc.CurrentRound = round;
                                CheckRoundTop();
                            }
                        }
                    }
                }
            }


           // Console.Write("Word : " + Word + " " + new String(Tirage));
        }
        #endregion

        #region Keyhandlers
        bool _isHandleKey = false;

        public bool HandleKey(System.Windows.Forms.KeyEventArgs e)
        {
            if (gc.GameState == enuGameState.Initializing || gc.GameState == enuGameState.IsStopping)
            {
                return false;
            }

            _isHandleKey = true;
            bool result = false;

            try
            {
                foreach (IKeyHandler handler in gctls.KeyHandlers)
                {
                    try
                    {
                        result = handler.HandleKey(e);
                        if (result) break;
                    }
                    catch (Exception ee)
                    {
                        result = false;
                    }
                }

                if (gc.GameState == enuGameState.Playing)
                {
                    switch (e.KeyData)
                    {
                        case System.Windows.Forms.Keys.F5:
                            if (proxy.GamePermissions.HasFlag(global::Topping.Core.Logic.Client.GamePermissions.CanPauseRound))
                            {
                                PauseRound();
                            }
                            break;
                        case System.Windows.Forms.Keys.F1:
                            if (gc.GameState == enuGameState.Playing && proxy.GamePermissions.HasFlag(global::Topping.Core.Logic.Client.GamePermissions.CanEndRound))
                            {
                                EndRound(false);
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Le coup n'est pas encore initialisé ou aucune partie est en cours");
                            }
                            break;
                    }

                }
            }
            catch (Exception ee)
            {
               // NLog.LogManager.GetLogger("wcf").ErrorException("LocalGameController : HandleKey", ee);
            }
            finally
            {
                _isHandleKey = false;
            }

            e.Handled = result;
            return result;
        }
        #endregion

        public void RemoveEvent()
        {
            /* if (gctls.HistoryManager != null)
             {
                 gctls.HistoryManager.RemoveEvent();
             }*/
        }

        private void PauseRound()
        {
            if (timer != null)
            {
                if (timer.Pause())
                {
                    gctls.Board.ResultControl.Visible = false;
                }
                else
                {
                    gctls.Board.ResultControl.Visible = true;
                }
            }
        }

    }
}
