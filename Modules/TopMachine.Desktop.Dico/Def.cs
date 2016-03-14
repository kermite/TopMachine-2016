using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TopMachine.Desktop.Dico
{
    public partial class Def : UserControl
    {
        TopMachine.Topping.DAL.Dico currentWord = new TopMachine.Topping.DAL.Dico();

        public Def()
        {
            InitializeComponent();
        }

        public Def(TopMachine.Topping.DAL.Dico ct)
        {
           // this.Dock = DockStyle.Top;
            InitializeComponent();
            currentWord = ct;
            this.Width = 280;
            SetDef(); 
        }

        public void SetDef(TopMachine.Topping.DAL.Dico currentWord)
        {
            lblDef.Text = currentWord.Definition;
            lblWord.Text = currentWord.Autres == null || currentWord.Autres.Length == 0 ? currentWord.Mot : currentWord.Autres;
            lblGenre.Text = currentWord.Genre;
            if (currentWord.isInvariable) lblGenre.Text += "Inv.";
        }

        
        public void SetDef()
        {

            lblDef.Text = currentWord.Definition;
            lblWord.Text = currentWord.Autres == null || currentWord.Autres.Length == 0 ? currentWord.Mot : currentWord.Autres;
            lblGenre.Text = currentWord.Genre;
            if (currentWord.isInvariable) lblGenre.Text += "Inv.";

            if (!currentWord.Hidden.GetValueOrDefault(false))
            {
                this.BackColor = Color.Green;
            }
            else
            {
                this.BackColor = Color.Red;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            ResizeDef();
            base.OnResize(e);
        }

        public void ResizeDef()
        {
            Graphics g = this.CreateGraphics();

            SizeF size = g.MeasureString(lblDef.Text, lblDef.Font, this.Width);
            this.Height = lblDef.Top + (int) size.Height + 15;
        }

        private void Def_Click(object sender, EventArgs e)
        {
            currentWord.Hidden = !currentWord.Hidden;
            if (! (bool) currentWord.Hidden.GetValueOrDefault(false))
            {
                this.BackColor = Color.Green;
            }
            else
            {
                this.BackColor = Color.Red;
            }
        }

    }
}
