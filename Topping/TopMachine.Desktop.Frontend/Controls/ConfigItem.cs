using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Topping.Dto;
using TopMachine.Desktop.Overall;

namespace TopMachine.Topping.Frontend.Controls
{
    public partial class ConfigItem : UserControl
    {
        public delegate void ConfigEvent(ConfigGameDto dto);


        public event ConfigEvent OnEditConfig;
        public event ConfigEvent OnStartConfig;
        public event ConfigEvent OnStatConfig;

        Guid MemoryCheckId;
        public ConfigItem()
        {
            
            MemoryCheckId = MemoryCheck.Register(this, this.GetType().FullName);
            InitializeComponent();
        }

        public void SetItem(ConfigGameDto cfg)
        {
            bindingConfig.DataSource = cfg; 
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            OnStartConfig((ConfigGameDto) bindingConfig.DataSource);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditConfig((ConfigGameDto) bindingConfig.DataSource);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkExplosive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkTopping_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
