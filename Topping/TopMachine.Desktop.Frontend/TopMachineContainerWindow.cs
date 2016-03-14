using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TopMachine.Topping.Frontend
{
    public partial class TopMachineContainerWindow : Form
    {

        public TopMachineContainer topmachineContainer; 
        
        public TopMachineContainer GetTopMachineContainer()
        {
            return topmachineContainer;
        }

        public void InitializeContainer()
        {
            topmachineContainer = new TopMachineContainer();
            topmachineContainer.Dock = DockStyle.Fill;
            this.Controls.Add(topmachineContainer);
            this.Show();

        }


        public TopMachineContainerWindow()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            // Set window location
            if (Properties.Settings.Default.GridWindowLocation != null)
            {
                this.Location = Properties.Settings.Default.GridWindowLocation;
            }

            // Set window size
            if (Properties.Settings.Default.GridWindowSize != null)
            {
                this.Size = Properties.Settings.Default.GridWindowSize;
            }

            base.OnLoad(e);
            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Copy window location to app settings
            Properties.Settings.Default.GridWindowLocation = this.Location;

            // Copy window size to app settings
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.GridWindowSize = this.Size;
            }
            else
            {
                Properties.Settings.Default.GridWindowSize = this.RestoreBounds.Size;
            }

            // Save settings
            Properties.Settings.Default.Save();


            base.OnClosing(e);
        }

        private void TopMachineContainerWindow_KeyUp(object sender, KeyEventArgs e)
        {
             topmachineContainer.HandleKey(e);
        }

    }
}
