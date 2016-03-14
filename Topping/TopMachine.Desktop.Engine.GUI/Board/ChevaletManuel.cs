using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


using TopMachine.Topping.Common.Interfaces;
using TopMachine.Desktop.Overall;
using TopMachine.Topping.Common.Structures;

namespace TopMachine.Topping.Engine.GUI.Board
{
    public partial class ChevaletManuel : UserControl, IChevaletManuel, IKeyHandler
    {
        GameCfg gc;
        GameControllers gctls; 

        public event OnRackSubmitEvent OnRackSubmit;
        public event OnRackUpdatedEvent OnRackUpdated;

        public Control ResultControl
        {
            get { return this; }
        }


        public ChevaletManuel(GameControllers gctls, GameCfg gc)
        {
            this.gctls = gctls; 
            this.gc = gc;
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);

            InitChevalet();
            this.Visible = false;
        }

        private void InitChevalet()
        {
            chevalet1.InitChevalet(gc, gctls, gc.Config.MaxLetters);
            chevalet1.InitRack();
            lettresRestantes1.SetConfig(gc);
            lettresRestantes1.ResetLetters();
            this.Visible = false;
        }


        public void SetTirage(string Tirage)
        {
            this.Visible = true;
            chevalet1.SetTirage(Tirage);
            this.Height = lettresRestantes1.Top + lettresRestantes1.MaxTop + 30;
            lettresRestantes1.SetConfig(gc);
            lettresRestantes1.ResetLetters();
        }


        public bool HandleKey(System.Windows.Forms.KeyEventArgs e)
        {
            bool result = false;

            if (e.KeyData == Keys.F4)
            {
                Validate();
                return true;
            }

            result = SendKeyInputRack(e.KeyCode);

            if (result)
            {
                lettresRestantes1.ResetLettersTotal();
            }
            return result;
        }

        private new void Validate()
        {
            if (OnRackSubmit != null)
            {
                bool result = OnRackSubmit();
                if (!result)
                {
                    lblError.Text = "Le tirage est incorrect";
                }
                else
                {
                    lblError.Text = "";
                    this.Visible = false;
                }
            }
        }

        private bool SendKeyInputRack(Keys k)
        {
            char KeyCode = (char)k; 

            string newRack = "";

            if (KeyCode == 8)
            {
                if (gc.CurrentRackLength > 0)
                {
                    char c = gc.CurrentRackChars[gc.CurrentRackLength - 1];
                    int tile = gc.GridConfig.GetCharFromAscii(c);
                    gc.Rack.remove(tile);
                    gc.Bag.replacetile((byte)tile);

                    gc.CurrentRackLength--;
                    newRack = new string(gc.CurrentRackChars, 0, gc.CurrentRackLength);
                    chevalet1.SetTirage(newRack);
                }

                if (OnRackUpdated != null)
                {
                    OnRackUpdated();
                }

                return true;
            }

            if (KeyCode == 188) KeyCode = '?';

            if (gc.CurrentRackLength < gc.Config.MaxLetters)
            {
                int ttile = gc.GridConfig.GetCharFromAscii(Char.ToUpper(KeyCode));
                if (ttile != -1)
                {
                    if (gc.Bag.isIn((byte)(ttile)) > 0)
                    {
                        gc.Bag.taketile((byte)(ttile));
                        gc.Rack.add((byte)(ttile));

                        gc.CurrentRackChars[gc.CurrentRackLength] = KeyCode;
                        gc.CurrentRackLength++;
                    }

                    newRack = new string(gc.CurrentRackChars, 0, gc.CurrentRackLength);
                    chevalet1.SetTirage(newRack);

                    if (OnRackUpdated != null)
                    {
                        OnRackUpdated();
                    }
                    return true;
                }
            }

            return false;


        }


        private void btnManual_Click(object sender, EventArgs e)
        {
            Validate();
        }
    }
}
