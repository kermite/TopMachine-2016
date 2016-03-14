using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TopMachine.Topping.Common.Structures;

using TopMachine.Topping.Dawg;
using TopMachine.Desktop.Dico;
using TopMachine.Desktop.Controls;
using TopMachine.Desktop.Search;
using CMWA.Packager;
using System.IO;
using TopMachine.Desktop.Controls.Tools;
using TopMachine.Desktop.Overall;


namespace TopMachine.Desktop
{
    public partial class TopMachineMain : Form
    {

        TitledUserControl currentControl = null; 

        public static TopMachineMain MainForm
        {
            get;
            set; 
        }
        
        public Dictionary<string, TitledUserControl> ctls = new Dictionary<string, TitledUserControl>(); 

        public TopMachineMain()
        {
            TopMachineMain.MainForm = this;
            InitializeComponent();
        }

        public TopMachineMain(string fileName)
        {
            TopMachineMain.MainForm = this;
            InitializeComponent();
        }

        bool first = true;
        private void TopMachineMain_Activated(object sender, EventArgs e)
        {
            if (first)
            {

                first = false;
                Progress.Instance.Show(this);
                PackageManager.LoadProject(FileUtility.GetFile("TopMachine.xprj", LocationType.PersonalFiles));               
                Progress.Instance.Hide(this);

                navigator1.LoadData();
            }
        }

        private void TopMachineMain_KeyUp(object sender, KeyEventArgs e)
        {
            navigator1.HandleKey(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            // Set window location
            if (Properties.Settings.Default.MainWindowLocation != null)
            {
                this.Location = Properties.Settings.Default.MainWindowLocation;
            }

            // Set window size
            if (Properties.Settings.Default.MainWindowSize != null)
            {
                this.Size = Properties.Settings.Default.MainWindowSize;
            }

            base.OnLoad(e);

        }

        protected override void OnClosing(CancelEventArgs e)
        {
			// Copy window location to app settings
			// Properties.Settings.Default.MainWindowLocation = this.Location;

			navigator1.Dispose();

			// Copy window size to app settings
			if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.MainWindowSize = this.Size;
            }
            else
            {
                Properties.Settings.Default.MainWindowSize = this.RestoreBounds.Size;
            }

            // Save settings
            Properties.Settings.Default.Save();


            base.OnClosing(e);
        }

        private void TopMachineMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }


    }
}