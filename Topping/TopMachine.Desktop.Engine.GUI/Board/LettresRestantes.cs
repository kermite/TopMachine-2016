using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;



namespace TopMachine.Topping.Engine.GUI.Board
{
    public partial class LettresRestantes : UserControl,ILettresRestantes
    {
        private GameCfg gc;
        private int _maxTop;


        public int MaxTop
        {
            get { return _maxTop; }
        }

        public GameCfg GameConfig
        {
            set { gc = value; }
        } 

        private Label[] objPlacers;
        private Label[] objLabels;


        public Control ResultControl
        {
            get { return this; }
        }

        public LettresRestantes(GameCfg gc)
        {
            this.gc = gc; 
            InitializeComponent();
        }

        public void SetConfig(GameCfg gc)
        {
            this.gc = gc;
        }

        public LettresRestantes()
        {
            InitializeComponent();
        }

        #region Letter BAG Management On Form


        public void ResetLettersTotal()
        {
            ShuffleBag bag = gc.Bag;
                if (bag != null)
                {
                    for (int x = 0; x < gc.GridConfig.NbLetters; x++)
                    {
                        int tot = bag.isIn(x);
                        objLabels[x].Text = tot.ToString();

                        if (tot == 0)
                            objPlacers[x].Enabled = false;
                        else
                            objPlacers[x].Enabled = true;
                    }

                    lblLettres.Text = "Lettres Restantes : " + bag.ntiles().ToString();
                }
        }

        public void ResetLetters()
        {
            ShuffleBag bag = gc.Bag;

            pnlLetters.Controls.Clear();

            objPlacers = new Label[gc.GridConfig.NbLetters];
            objLabels = new Label[gc.GridConfig.NbLetters];

            int nbLength = (this.Width-20) / 35;


            _maxTop = 0;

            for (int x = 0; x < gc.GridConfig.NbLetters; x++)
            {
                char c = gc.GridConfig.GetChar(x); 

                objLabels[x] = new Label();

                int left, top, height, width;
                string pts;

                left = 10+35 * (x % nbLength);
                top = 55 * (x / nbLength);
                pts = gc.GridConfig.Values[x].ToString();

                Label letterp = new Label();
                letterp.Text = new string(c,1); 
                letterp.Left= left;
                letterp.Top =  top;
                letterp.Width =30;
                letterp.Height = 30;
                letterp.ForeColor = Color.White;

                objLabels[x].Width = 30;
                objLabels[x].Height = 18;
                objLabels[x].Left = 10+(35 * (x % nbLength));
                objLabels[x].Top = 30 + 55 * (x / nbLength);
                objLabels[x].BackColor = Color.SteelBlue;

                _maxTop = System.Math.Max(_maxTop, objLabels[x].Top + 23);
                pnlLetters.Controls.Add(letterp);
                pnlLetters.Controls.Add(objLabels[x]);
                objPlacers[x] = letterp;
            }

            try
            {
                this.Height = _maxTop + 25;
            }
            catch
            {
                ;
            }
            ResetLettersTotal();
        }

        #endregion
    }
}
