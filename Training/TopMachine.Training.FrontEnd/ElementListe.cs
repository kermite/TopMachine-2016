using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using config = TopMachine.Training.FrontEnd.TrainingConfig ;
using CMWA.Packager.Custom;
using CMWA.Packager;

namespace TopMachine.Training.FrontEnd
{
    public partial class ElementListe : UserControl
    {
        public delegate void NotificationDelete(ElementListe sender);
        public event NotificationDelete onNotificationDelete; 

        private string _name;
        private Package _pckg; 

        public ElementListe(Package pckg)
        {
            InitializeComponent();
           _pckg = pckg;

           lblName.Text = NameList;
           btnDelete.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Minus");
           btnDelete.Text = string.Empty;
           btnDelete.Width = btnDelete.Image.Width;


            LoadCheckBox();
        }


        private void LoadCheckBox() 
        {
            List<config.DropDownPairs> arl = new List<config.DropDownPairs>();
            arl.Add(new config.DropDownPairs("Tous les mots non-joués", 0));
            arl.Add(new config.DropDownPairs("Tous les mots trouvés", 1));
            arl.Add(new config.DropDownPairs("Tous les mots non trouvés", 2));
            arl.Add(new config.DropDownPairs("Tous les mots isolés", 3));
            
           
            chkStatus.DataSource = arl;
            chkStatus.DisplayMember = "Display";
            chkStatus.ValueMember = "Value";
        
        }

        public Package pckg 
        {
            get 
            {
                return _pckg;
            }
        }

        public string NameList
        {
            get
            {
                if (_pckg != null)
                {
                    return _pckg.Key;
                }
                else 
                {
                    return string.Empty;
                }
            }
        }

        public List<config.DropDownPairs> Status
        {
            get
            {
                List<config.DropDownPairs> lstStatus = new List<config.DropDownPairs>();

                foreach (var item in chkStatus.CheckedItems)
                {
                    lstStatus.Add(item as config.DropDownPairs);
                }

                return lstStatus;
            }

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (onNotificationDelete != null) 
            {
                onNotificationDelete(this);
            }
        }
    }
}
