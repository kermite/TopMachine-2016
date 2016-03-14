using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMWA.Packager.Custom;
using TopMachine.Desktop.Controls;
using CMWA.Packager;

namespace TopMachine.Training.Frontend
{

    public partial class TrainingSelection : UserControl, IFinished
    {
        public delegate void ItemSelectedEvent(string tag, Package currentPackage);
        public event ItemSelectedEvent ItemSelected;

        public Package currentPackage = null; 

        public TrainingSelection()
        {
            InitializeComponent();
        }

        public void ReloadList()
        {
            lblSelectedList.Text = "Aucune liste sélectionnée";
            currentPackage = null;
            RefreshButton(false);
            listGames.Items.Clear();

            Package p = (Package)PackageManager.Instance.Project.GetPackageItem("Training4dbo\\Lists");

          

            foreach (Package pList in p.Items)
            {
                ListViewItem lvi = new ListViewItem(pList.Key);
                lvi.Tag = pList;
                listGames.Items.Add(lvi);
            }
        }

        private void lblLocal_Click(object sender, EventArgs e)
        {

                if (ItemSelected != null)
                {
                    ItemSelected(((LinkLabel)sender).Tag.ToString(), currentPackage);
                }

        }

        private void RefreshButton(bool isSelectedList) 
        {
            roundPanelStart.Enabled = isSelectedList;
            roundPanelRecreate.Enabled = isSelectedList;
            roundPanelSetting.Enabled = isSelectedList;
            roundPanelDelete.Enabled = isSelectedList;
            
            

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listGames.SelectedItems.Count > 0)
            {
                currentPackage = (Package)listGames.SelectedItems[0].Tag;
                lblSelectedList.Text = currentPackage.Key;
                RefreshButton(true);
            }
            else
            {
                currentPackage = null;
                lblSelectedList.Text = "Aucune liste sélectionnée";
                RefreshButton(false);
            }

        }

        #region IFinished Members

        public event OnFinishedDelegate OnFinished;

        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Etes-vous certain d'effacer cette liste {0}?", lblSelectedList.Text), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (ItemSelected != null)
                {
                    ItemSelected(((LinkLabel)sender).Tag.ToString(), currentPackage);
                }
            }
        }

    
      

    }
}
