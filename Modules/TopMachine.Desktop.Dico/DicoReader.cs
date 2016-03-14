using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Desktop.Controls;
using TopMachine.Topping.DAL;
using CMWA.Packager.Tools;

namespace TopMachine.Desktop.Dico
{
    public partial class DicoReader : TitledUserControl
    {

        private DicoReaderConfig cfg = new DicoReaderConfig();
        DicoLoader dr = new DicoLoader();

        
        public DicoReader()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cfg = new DicoReaderConfig
            {
                CurrentLast = txtEnd.Text.ToUpper(),
                End = txtEnd.Text.ToUpper(),
                LenMax = (int)txtLenMax.Value,
                LenMin = (int)txtLenMin.Value,
                Max = (int)numericUpDown1.Value,
                Start = txtStart.Text,
                Visible = chkVisible.Checked,
                OnlyNew = chkNew.Checked
            };

            if (cfg.End.Length == 0) cfg.End = cfg.Start;
            pnlControl.Visible = true;
            pnlContent.Visible = true;
            pnlConfig.Visible = false;

            prg.Show(this);

            Application.DoEvents();
            dr.InitWordList(cfg);

            LoadWords();
        }

        private void LoadWords()
        {

            prg.Show(this);
            Application.DoEvents();

            cfg.MotRows = new List<TopMachine.Topping.DAL.Dico>();


            try
            {
                foreach (TopMachine.Topping.DAL.Dico dc in dr.LoadWordList(cfg))
                {
                    cfg.MotRows.Add(dc);
                    cfg.Start = dc.Key;
                }
            }
            catch (Exception)
            {

                ;
            }


            prg.Hide();

            if (cfg.MotRows.Count == 0)
            {
                MessageBox.Show("Plus de résultats", "Fin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }


            flowLayoutPanel1.Controls.Clear();
            this.flowLayoutPanel1.SuspendLayout();
            for (int x = 0; x < cfg.MotRows.Count; x++)
            {
                DicoDefinition.GetDefinition(cfg.MotRows[x]);
                Def d = new Def(cfg.MotRows[x]);
                flowLayoutPanel1.Controls.Add(d);
              //  d.BringToFront();

            }

            foreach (Def d in flowLayoutPanel1.Controls)
            {
                d.ResizeDef();
            }

            flowLayoutPanel1.ResumeLayout();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prg.Show(this);
            dr.SaveVisibilityPartial(cfg.MotRows);
            cfg.CurrentPage++;
            LoadWords();
            prg.Hide(this);
        }

        private void DicoReader_Resize(object sender, EventArgs e)
        {
            int w = pnlConfig.Width;
            int h = pnlConfig.Height;

            this.Left = (Width - w) / 2;
            this.Top = (Height - h) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlControl.Visible = false;
            pnlContent.Visible = false; 
            pnlConfig.Visible = true; 
        }
    }
}
