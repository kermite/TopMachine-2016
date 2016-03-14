using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TopMachine.Desktop.Controls;
using TopMachine.Desktop.Overall;
using CMWA.Packager;
using TopMachine.Training.DAL.fdbo ;


namespace TopMachine.Training.FrontEnd
{
    public partial class TrainingForm : TitledUserControl, IKeyHandler
    {
        Dictionary<string, Control> ControlDico = new Dictionary<string, Control>();
        
  
        public int editMode = 0; 

        public TrainingForm()
        {
            InitializeComponent();

            ControlDico.Add("MAIN", trainingSelection1);
            ControlDico.Add("CONFIG", null); 
            ControlDico.Add("GAME", null); 
        }

        Bitmap bmpMinus;
        Bitmap bmpPlus;

        protected override void OnLoad(EventArgs e)
        {
            bmpMinus = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Minus");
            bmpPlus = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Plus");

            btnCollapse.Image = bmpMinus;


            btnNavigation.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Application");
            btnGames.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Topping");
            btnConfig.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Settings");
            btnQuit.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Cancel");

            ReloadList();

            base.OnLoad(e);
        }

        public void ReloadList()
        {
            trainingSelection1.ReloadList();
        }

        public void DeleteControl(string key)
        {
            Control ctl = ControlDico[key];

            if (ctl != null)
            {
                pnlContainer.Controls.Remove(ctl);
                ctl.Dispose();
            }
            ControlDico[key] = null; 

        }

        public void SetControl(string key, Control ctl)
        {

            try
            {
                foreach (Control c in pnlContainer.Controls)
                {
                    c.Visible = false;
                }

                ctl.Dock = DockStyle.Fill;
                ctl.Visible = true;
                ControlDico[key] = ctl;
                pnlContainer.Controls.Add(ctl);
            }
            catch (Exception)
            {
                ;

            }

        }

        #region IKeyHandler Members

        public bool HandleKey(KeyEventArgs e)
        {
            foreach (Control c in pnlContainer.Controls)
            {
                if (c is IKeyHandler)
                {
                    IKeyHandler kh = (IKeyHandler)c;
                    if (kh != null)
                    {
                        kh.HandleKey(e);
                    }
                }
            }

            return e.Handled; 
        }

        #endregion


        private void btnCollapse_Click(object sender, EventArgs e)
        {
            bool ok = base.DoCollapse();

            if (ok)
            {
                btnCollapse.Image = bmpMinus;
            }
            else
            {
                btnCollapse.Image = bmpPlus;
            }
        }


        private void trainingSelection1_ItemSelected(string tag, CMWA.Packager.Custom.Package currentPackage)
        {
            switch (tag)
            {
                case "NEW":
                    DeleteControl("CONFIG");
                    TrainingConfig config = new TrainingConfig();
                    SetControl("CONFIG", config);
                    config.EditNew();
                    config.OnFinished += new OnFinishedDelegate(config_OnFinished);
                    break;
                case "RECREATE":
                    if (currentPackage != null)
                    {
                        TopMachine.Desktop.Controls.Tools.Progress.Instance.Show(this);
                        LoadBaseIO lio = new LoadBaseIO(currentPackage);
                        lio.RecreateList();
                        lio.Dispose();
                        TopMachine.Desktop.Controls.Tools.Progress.Instance.Hide(this);
                    }
                    break;
                case "GO":
                    if (currentPackage != null)
                    {
                        DeleteControl("CONFIG");
                        DeleteControl("GAME");
                        TrainingGame game = new TrainingGame();
                        SetControl("GAME", game);
                        game.SetConfig(currentPackage);
                    }
                    break;
                case "PARAMS":
                    if (currentPackage != null)
                    {
                        DeleteControl("CONFIG");
                        DeleteControl("GAME");
                        TrainingConfig config2 = new TrainingConfig();
                        SetControl("CONFIG", config2);
                        config2.EditCurrent(currentPackage);
                        config2.OnFinished += new OnFinishedDelegate(config_OnFinished);
                    }
                    break;
                case "DELETE":
                    if (currentPackage != null)
                    {
                        DeleteControl("CONFIG");
                        DeleteControl("GAME");
                        TrainingConfig config3 = new TrainingConfig();
                        config3.DeleteConfig(currentPackage);
                        trainingSelection1.ReloadList(); 
                    }
                    break;

                case "DICO":
                        DeleteControl("CONFIG");
                        DeleteControl("GAME");
                        GenerateDicoForm Generatef = new GenerateDicoForm();
                        SetControl("CONFIG", Generatef);
                        Generatef.OnFinished += new OnFinishedDelegate(config_OnFinished);
                    break;

                case "EXPORT":

                    DeleteControl("CONFIG");
                    DeleteControl("GAME");
                    ExportForm exportf = new ExportForm();
                    SetControl("CONFIG", exportf);
                    exportf.OnFinished += new OnFinishedDelegate(config_OnFinished);

                    break;
                case "IMPORT":
                    
                        DeleteControl("CONFIG");
                        DeleteControl("GAME");
                        ImportForm importf = new ImportForm();
                        SetControl("CONFIG", importf );
                        importf .OnFinished += new OnFinishedDelegate(config_OnFinished);
                 
                    break;

                case "UPDATE":

                    DeleteControl("CONFIG");
                    DeleteControl("GAME");
                    UpdateList updatef = new UpdateList();
                    SetControl("CONFIG", updatef);
                    updatef.OnFinished += new OnFinishedDelegate(config_OnFinished);

                    break;


            }
        }

        void config_OnFinished()
        {
            DeleteControl("CONFIG");
            DeleteControl("GAME");
            SetControl("MAIN", trainingSelection1);
            ReloadList();
        }

        private void btnNavigation_Click(object sender, EventArgs e)
        {
            SetControl("MAIN", trainingSelection1);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            trainingSelection1_ItemSelected("PARAMS", trainingSelection1.currentPackage);
        }

        private void btnBackToGame_Click(object sender, EventArgs e)
        {
            trainingSelection1_ItemSelected("GO", trainingSelection1.currentPackage);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            DeleteControl("CONFIG");
            DeleteControl("GAME");
            SetControl("MAIN", trainingSelection1);
        }

    
        
    }
}
