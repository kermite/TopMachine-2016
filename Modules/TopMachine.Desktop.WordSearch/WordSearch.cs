using System;

using System.Collections; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TopMachine.Topping.Dawg;
using TopMachine.Topping.DAL;
using TopMachine.Desktop.Dico;
using TopMachine.Desktop.Controls;

namespace TopMachine.Desktop.Search
{
    public partial class WordSearch : TitledUserControl
    {

        public WordSearch()
        {
            InitializeComponent();
            LoadForm();
            
        }

        public void LoadForm()
        {
        }

        public class DropDownPairs
        {
            private string display;
            private object val;

            public string Display
            {
                get { return display; }
                set { display = value; }
            }

            public object Value
            {
                get { return val; }
                set { val = value; }
            }

            public DropDownPairs(string d, object v)
            {
                display = d;
                val = v;
            }

        }

        private void btnXplus1_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0) return;
            progress1.Show(this);
            Application.DoEvents();
            listSolutions.Items.Clear(); 
            listSolutions.Columns.Clear();
            ColumnHeader col = new ColumnHeader();
            col.Width = 40;
            col.Text = "?";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = listSolutions.Width - 120; 
            col.Text = "Mot";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            ArrayList wc = DicoUtils.Instance.Do7Plus1(txtSearch.Text.ToUpper());

            listSolutions.SuspendLayout();
            foreach (WordContainer w in wc)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = w.Additional;
                lvi.SubItems.Add(w.Before);
                lvi.SubItems.Add(w.Word);
                lvi.SubItems.Add(w.After);
                lvi.Tag = w;
                listSolutions.Items.Add(lvi);
            }
            listSolutions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listSolutions.ResumeLayout();
            progress1.Hide(); 


        }

        private void btnPlusPetit_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0) return;
            progress1.Show(this);
            Application.DoEvents();
            listSolutions.Items.Clear();
            listSolutions.Columns.Clear();
            ColumnHeader col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = listSolutions.Width - 120;
            col.Text = "Mot";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            ArrayList wc = DicoUtils.Instance.SearchWords(txtSearch.Text.ToUpper());

            listSolutions.SuspendLayout();
            foreach (WordContainer w in wc)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = w.Before;
                lvi.SubItems.Add(w.Word);
                lvi.SubItems.Add(w.After);
                lvi.Tag = w;
                listSolutions.Items.Add(lvi);
            }
            listSolutions.ResumeLayout();
            listSolutions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            progress1.Hide(this);
        }

        private void btnPlusGrand_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0) return;
            progress1.Show(this);
            Application.DoEvents();
            listSolutions.Items.Clear();
            listSolutions.Columns.Clear();
            ColumnHeader col = new ColumnHeader();
            col.Width = 40;
            col.Text = "?";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = listSolutions.Width - 120;
            col.Text = "Mot";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            ArrayList wc = DicoUtils.Instance.SearchWordsPlus(txtSearch.Text.ToUpper());

            listSolutions.SuspendLayout();
            foreach (WordContainer w in wc)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = w.Additional;
                lvi.SubItems.Add(w.Before);
                lvi.SubItems.Add(w.Word);
                lvi.SubItems.Add(w.After);
                lvi.Tag = w;
                listSolutions.Items.Add(lvi);
            }
            listSolutions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            listSolutions.ResumeLayout();
            progress1.Hide(this); 
        }

        private void btnContient_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0) return;
            progress1.Show(this);
            Application.DoEvents();
            listSolutions.Items.Clear();
            listSolutions.Columns.Clear();
            ColumnHeader col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "<";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = listSolutions.Width - 120;
            col.Text = "Mot";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = ">";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            ArrayList wc = DicoUtils.Instance.DoRallonges(txtSearch.Text.ToUpper());

            listSolutions.SuspendLayout();
            foreach (WordContainer w in wc)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = w.Before;
                lvi.SubItems.Add(w.Prefix);
                lvi.SubItems.Add(w.BaseWord);
                lvi.SubItems.Add(w.Suffix);
                lvi.SubItems.Add(w.After);
                lvi.Tag = w;
                listSolutions.Items.Add(lvi);
            }
            listSolutions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            listSolutions.ResumeLayout();
            progress1.Hide(this); 
        }

        private void btnRegulier_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0) return;
            progress1.Show(this);
            Application.DoEvents();
            listSolutions.Items.Clear();
            listSolutions.Columns.Clear();
            ColumnHeader col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = listSolutions.Width - 120;
            col.Text = "Mot";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            ArrayList wc = DicoUtils.Instance.SearchMask(txtSearch.Text.ToUpper());

            listSolutions.SuspendLayout();
            foreach (WordContainer w in wc)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = w.Before;
                lvi.SubItems.Add(w.Word);
                lvi.SubItems.Add(w.After);
                lvi.Tag = w;
                listSolutions.Items.Add(lvi);
            }
            listSolutions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            listSolutions.ResumeLayout();
            progress1.Hide(this); 

        }

        private void btnPlusouMoins_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length == 0) return;
            progress1.Show(this);
            Application.DoEvents();
            listSolutions.Items.Clear();
            listSolutions.Columns.Clear();
            ColumnHeader col = new ColumnHeader();
            col.Width = 40;
            col.Text = "#";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = listSolutions.Width - 120;
            col.Text = "Mot";
            listSolutions.Columns.Add(col);

            col = new ColumnHeader();
            col.Width = 40;
            col.Text = "+";
            listSolutions.Columns.Add(col);

            ArrayList wc = DicoUtils.Instance.DoVoisins(txtSearch.Text.ToUpper());

            listSolutions.SuspendLayout();
            foreach (WordContainer w in wc)
            {
                ListViewItem lvi = new ListViewItem();

                switch (w.Type)
                {
                    case 1:
                        lvi.Text = "<1>";
                        break;
                    case 2:
                        lvi.Text = "<2>";
                        break;
                    case 3:
                        lvi.Text = "-1+";
                        break;
                    case 4:
                        lvi.Text = ">1";
                        break;
                    case 5:
                        lvi.Text = "<1";
                        break;

                }
                lvi.SubItems.Add(w.Before);
                lvi.SubItems.Add(w.Word);
                lvi.SubItems.Add(w.After);
                lvi.Tag = w;
                listSolutions.Items.Add(lvi);
            }
            listSolutions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            listSolutions.ResumeLayout();
            progress1.Hide(this); 

        }

        DicoAccessor da = new DicoAccessor(); 
        private void listSolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSolutions.SelectedItems.Count > 0)
            {
                WordContainer wc = (WordContainer) listSolutions.SelectedItems[0].Tag;
                string src = "";
                if (wc.Word == null) src = wc.BaseWord;
                else src = wc.Word;

                List<TopMachine.Topping.DAL.Dico> dicos = da.GetWord(src.ToUpper(), true);
                pnlContent.Controls.Clear();
                foreach (TopMachine.Topping.DAL.Dico d in dicos)
                {
                    DicoDefinition.GetDefinition(d); 
                    Def def = new Def();
                    def.SetDef(d);
                    def.Dock = DockStyle.Top; 
                    pnlContent.Controls.Add(def);
                    def.BringToFront(); 
                }
            }
        }

    }
}