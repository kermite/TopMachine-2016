using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Desktop.Overall;

namespace TopMachine.Topping.Engine.GUI.Board
{
    public partial class RoundControl : UserControl, IRoundControl
    {
        Guid MemoryCheckId;

        public RoundControl()
        {
            InitializeComponent();
            MemoryCheckId = MemoryCheck.Register(this, "RoundControl CREATE");
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();

            }

            MemoryCheck.Unregister(MemoryCheckId);
            base.Dispose(disposing);
        }

        #region IRoundControl Members

        public void InitTurn()
        {
            throw new NotImplementedException();
        }

        public Control ResultControl
        {
            get { return this; }
        }

        public void WordDisplay(bool Final, string word, string s, string pt)
        {
            if (this.IsDisposed) return; 

            Label l = lblWordCurrent;
            if (Final)
            {
                l = lblWordFinal;
                
                if (int.Parse(pt) == 0)
                {
                    l.BackColor = Color.Red;
                }

                else
                {
                    l.BackColor = Color.YellowGreen;
                }
            }
            l.Text = word + " " + s + " " + pt;
          
            //throw new NotImplementedException();
        }

        public void SetChrono(long Seconds, long SecondsAll)
        {
            if (this.IsDisposed) return; 

            System.Globalization.NumberFormatInfo ni = new System.Globalization.NumberFormatInfo();
            ni.NumberDecimalDigits = 2;
            ni.NumberDecimalSeparator = ".";

            if (Seconds < 0) Seconds = 0; 

            lblChronoLeft.Text = ((long)(Seconds / 60)).ToString()
                            + ":"
                               + ((long)(Seconds % 60)).ToString("00");

            lblChronoAll.Text =((long)(SecondsAll / 60)).ToString()
                            + ":"
                               + ((long)(SecondsAll % 60)).ToString("00");


        }

        public void SetScore(int player, int top, int coup, double percentage)
        {
            if (this.IsDisposed) return; 

            lblRound.Text = "N° " + (coup+1).ToString();
            lblTotal.Text = (player-top).ToString() + " / " + top.ToString();

            System.Globalization.NumberFormatInfo ni = new System.Globalization.NumberFormatInfo();
            ni.NumberDecimalDigits = 2;
            ni.NumberDecimalSeparator = ".";

            lblPercentage.Text = percentage.ToString("00.00") + "%";
            lblPercentage.Visible = true;

            int neg = player - top;

            
        }

        #endregion

        #region IRoundControl Members


        public void RoundDisplay(TopMachine.Topping.Dawg.CRound round)
        {
            if (this.IsDisposed) return; 

            char c = (char)(64+ round.row());
            string s;
            if (round.dir() == 1)
            {
                s = c + "/" + ((int)(round.column())).ToString();
            }
            else
            {
                s = ((int)(round.column())).ToString() + " / " + c;
            }
            lblWordCurrent.Text = lblWordFinal.Text ;
            Label l = lblWordFinal;
            
            l.Text = "TOP : " + round.getwordwithjoker() + " " 
                        + s + " " + round.points().ToString(); 
        }

        #endregion
    }
}
