using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Desktop.Controls;
using TopMachine.Training.DAL.fdbo;
using CMWA.Packager.Custom;

namespace TopMachine.Training.FrontEnd
{
    public partial class TrainingConfig : UserControl, IFinished, IDisposable
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

        public int editMode = 0;

        public TrainingConfig()
        {
            InitializeComponent();
            InitDropDowns();
        }

        public void InitDropDowns()
        {
            // les combos sont binder avec la listconfig par defaut  


            List<DropDownPairs> arl = new List<DropDownPairs>();
            arl.Add(new DropDownPairs("Français", "FR"));
            arl.Add(new DropDownPairs("Anglais SOWPODS", "UK"));
            arl.Add(new DropDownPairs("Américain TWL", "US"));
            arl.Add(new DropDownPairs("Néerlandais", "NL"));
            cbLanguage.DisplayMember = "Display";
            cbLanguage.ValueMember = "Value";
            cbLanguage.DataSource = arl;
            

            arl = new List<DropDownPairs>();
            arl.Add(new DropDownPairs("Tous les mots non-joués", (int)Tools.TypeStatus.NonJoue));
            arl.Add(new DropDownPairs("Tous les mots", (int)Tools.TypeStatus.Trouve_Tous));
            arl.Add(new DropDownPairs("Tous les mots non trouvés", (int)Tools.TypeStatus.NonTrouve));
            arl.Add(new DropDownPairs("Tous les mots isolés", (int)Tools.TypeStatus.Isole));
            arl.Add(new DropDownPairs("Tous les mots non trouvés et isolés", (int)Tools.TypeStatus.NonTrouveIsole));

            cbTypeOfPlay.DataSource = arl;
            cbTypeOfPlay.DisplayMember = "Display";
            cbTypeOfPlay.ValueMember = "Value";

            arl = new List<DropDownPairs>();
            arl.Add(new DropDownPairs("Base complète", ""));
            arl.Add(new DropDownPairs("Sans conjugaisons", "NOC"));
            arl.Add(new DropDownPairs("Entrées directes", "C"));
            arl.Add(new DropDownPairs("Nouveaux", "NEW"));
            arl.Add(new DropDownPairs("Nouveaux sans conjugaisons", "NOCNEW"));
            arl.Add(new DropDownPairs("Nouveaux entrées directes", "CNEW"));


            cbBase.DataSource = arl;
            cbBase.DisplayMember = "Display";
            cbBase.ValueMember = "Value";
          



            arl = new List<DropDownPairs>();
            arl.Add(new DropDownPairs("Monômes", 0));
            arl.Add(new DropDownPairs("Binômes (1 solution / 2)", 1));
            arl.Add(new DropDownPairs("Toutes les solutions", 2));

            cbMode.DataSource = arl;
            cbMode.DisplayMember = "Display";
            cbMode.ValueMember = "Value";


            txtGameName.Text = "Votre liste";

        }

        string packageKey;
        Config config;
        ListConfig listConfig;
        GameConfig gameConfig;

        public void DeleteConfig(Package package)
        {
            using (ListAccessor la = new ListAccessor("Training4dbo\\Lists\\" + package.Key))
            {
                la.DeletePackage(package);
            }
        }


        public void EditNew()
        {
            editMode = 0;
            listConfig = new ListConfig();
            listConfig.MinPossibilities = 1;
            listConfig.MaxPossibilities = 5; 
            gameConfig = new GameConfig();

            bindingConfig.DataSource = listConfig;
            bindingParams.DataSource = gameConfig;

            groupConfig.Enabled = true;
            groupParams.Enabled = true;
        }

        public void EditCurrent(Package package)
        {
            editMode = 1;
            groupConfig.Enabled = false;
            groupParams.Enabled = true;

            packageKey = package.Key;

            using (ListAccessor la = new ListAccessor("Training4dbo\\Lists\\" + packageKey))
            {
                config = la.GetConfig();

                listConfig = new TopMachine.Desktop.Overall.ObjectSerializer<ListConfig>().Deserialize(config.XMLConfig);
                gameConfig = new TopMachine.Desktop.Overall.ObjectSerializer<GameConfig>().Deserialize(config.XMLPlay);

                bindingConfig.DataSource = listConfig;
                bindingParams.DataSource = gameConfig;
            }
        }


        #region IFinished Members

        public event OnFinishedDelegate OnFinished;

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            Validate();

            if (editMode == 0)
            {
                AddNew();
            }
            else
            {
                Update();
            }

        }

        private void Update()
        {
            using (ListAccessor la = new ListAccessor("Training4dbo\\Lists\\" + packageKey))
            {
                config = la.GetConfig();
                config.XMLConfig = new TopMachine.Desktop.Overall.ObjectSerializer<ListConfig>().Serialize(listConfig);
                config.XMLPlay = new TopMachine.Desktop.Overall.ObjectSerializer<GameConfig>().Serialize(gameConfig);
                la.UpdateConfig(config);
            }

            if (OnFinished != null)
            {
                OnFinished();
            }
        }

        private void AddNew()
        {
            using (ListAccessor la = new ListAccessor())
            {
                config = la.CreateConfig(listConfig, gameConfig);

                if (config == null) 
                {
                    MessageBox.Show("Veuillez changer le nom car cette liste existe déja");
                    return;
                }
            }

            TopMachine.Desktop.Controls.Tools.Progress.Instance.Show(this);

            LoadBaseIO lbi = new LoadBaseIO(listConfig.Name, config);
            lbi.DoData();

            TopMachine.Desktop.Controls.Tools.Progress.Instance.Hide(this);

            if (OnFinished != null)
            {
                OnFinished();
            }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            base.Dispose();
        }

        #endregion

        private void imageButton1_Click(object sender, EventArgs e)
        {
            if (OnFinished != null)
            {
                OnFinished();
            }
        }

        private void imageButton2_Click(object sender, EventArgs e)
        {
            this.Validate();
            ListCriterium crit = new ListCriterium();
            listConfig.Criteria.Add(crit);
            criteriaBindingSource.DataSource = listConfig.Criteria;
            dgvRegEx.DataSource = null;
            dgvRegEx.DataSource = criteriaBindingSource;

        }

        private void imageButton3_Click(object sender, EventArgs e)
        {
            this.Validate();
            if (criteriaBindingSource.Current != null)
            {
                listConfig.Criteria.Remove(criteriaBindingSource.Current as ListCriterium);
                criteriaBindingSource.DataSource = listConfig.Criteria;
                dgvRegEx.DataSource = null;
                dgvRegEx.DataSource = criteriaBindingSource;

            }
        }

        private void imageButton4_Click(object sender, EventArgs e)
        {
            new RegExExamples().Show(this);
        }

        private void imgExpertMode_Click(object sender, EventArgs e)
        {
            bool btnvisible;
            if (imgBtnExpertMode.Text == "Mode Classique")
            {
                imgBtnExpertMode.Text = "Mode Expert";
                btnvisible = false;
            }
            else
            {
                imgBtnExpertMode.Text = "Mode Classique";
                btnvisible = true;
            }

            dgvRegEx.Visible = btnvisible;
            imgbtnSample.Visible = btnvisible;
            imgBtnDelCritere.Visible = btnvisible;
            imgBtnAddCritere.Visible = btnvisible;
            lblInfoRegEx.Visible = btnvisible;
            chkAllCritere.Visible = btnvisible;
            groupParams.Visible = btnvisible;
            btnSave.Visible = btnvisible;
            imgbtnCancel.Visible = btnvisible;
            imgbtnSave2.Visible = !btnvisible;
            imgbtnCancel2.Visible = !btnvisible;
        }
    }
}
