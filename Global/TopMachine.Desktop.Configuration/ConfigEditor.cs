using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TopMachine.Desktop.Configuration
{
    public partial class ConfigEditor : Form
    {
        public ConfigEditor()
        {
            InitializeComponent();
        }

        public void Load()
        {
            bindingSourceUrls.DataSource = Configuration.Instance.Urls; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();
            Configuration.Instance.WriteConfiguration();
            this.Close();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
            this.Dispose(); 
        }


    }
}
