using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using TopMachine.Topping.Common.Structures;
using System.IO;
using TopMachine.Topping.Dawg;
using CMWA.Packager.Tools;
using CMWA.Packager.Tools.Bytes;
using CMWA.Packager;
using TopMachine.Desktop.Controls;
using TopMachine.Topping.Engine.GameController;
using TopMachine.Topping.Dto;
using Topping.Core.Proxy.Local.Client;
using TopMachine.Desktop.Overall;
using TopMachine.Topping.Frontend;

namespace TopMachine.Topping.Frontend.Admin
{
    public partial class ToppingConfig : UserControl, IStoppable, IFinished
    {
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


        public event ConfigGameDelegate OnFinishDelegate;
        Guid MemoryCheckId; 

        public ToppingConfig()
        {
            MemoryCheckId = MemoryCheck.Register(this, this.GetType().FullName);
            InitializeComponent();
            InitDropDowns(); 
        }

        public void SetItem(ConfigGameDto cfg)
        {
            bindingConfig.DataSource = cfg;
        }

        public void InitDropDowns()
        {

            List<DropDownPairs> arl= new List<DropDownPairs>();
            arl.Add(new DropDownPairs("Français", "FR"));
            arl.Add(new DropDownPairs("Anglais SOWPODS", "UK"));
            arl.Add(new DropDownPairs("Américain TWL", "US"));
            arl.Add(new DropDownPairs("Néerlandais", "NL"));
            cbDico.DisplayMember = "Display";
            cbDico.ValueMember = "Value";
            cbDico.DataSource = arl;
            cbDico.SelectedIndex = 0;

            arl = new List<DropDownPairs>();
            arl.Add(new DropDownPairs("Partie Normale", 0));
            cbType.DataSource = arl;
            cbType.DisplayMember = "Display";
            cbType.ValueMember = "Value";
            cbType.SelectedIndex = 0;

            arl = new List<DropDownPairs>();
            arl.Add(new DropDownPairs("Grille Normale", 0));
            arl.Add(new DropDownPairs("Super Grille", 1));

            cbGrid.DataSource = arl;
            cbGrid.DisplayMember = "Display";
            cbGrid.ValueMember = "Value";
            cbGrid.SelectedIndex = 0;
        }

        public void Stop()
        {
            ;
        }

        private void ddlLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        #region IFinished Members

        public event OnFinishedDelegate OnFinished;

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Validate(true);
            ConfigGameDto cfg = (ConfigGameDto)bindingConfig.DataSource;
            if (MessageProxy.Proxy.UpdateConfiguration(cfg) == false)
            {
                MessageBox.Show("Une erreur s'est produite", "Erreur", MessageBoxButtons.OK);
                OnFinishDelegate(this, cfg, false);
                return;
            }
            OnFinishDelegate(this, cfg, true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Validate(true);
            OnFinishDelegate(this, (ConfigGameDto)bindingConfig.DataSource, false);
        }

        private void cbGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ((ConfigGameDto)bindingConfig.DataSource).Grid = (int)cbGrid.SelectedValue;
            } catch
            {
                ;
            }
        }


        #region IStoppable Members


        public void Reactivate()
        {
           
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Etes-vous certain d'effacer cette config ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ConfigGameDto cfg = (ConfigGameDto)bindingConfig.DataSource;
                if (MessageProxy.Proxy.DeleteConfiguration(cfg) == false)
                {
                    MessageBox.Show("Une erreur s'est produite", "Erreur", MessageBoxButtons.OK);
                    OnFinishDelegate(this, cfg, false);
                    return;
                }

                OnFinishDelegate(this, cfg, true);
            }
        }

        private void chkExplosive_CheckedChanged(object sender, EventArgs e)
        {
            NUDRatio.Visible = chkExplosive.Checked;
            lblRatio.Visible = chkExplosive.Checked;
        }
    }
}
