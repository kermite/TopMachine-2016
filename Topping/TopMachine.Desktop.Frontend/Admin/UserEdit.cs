using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Topping.Core.Proxy.Local.Client;
using TopMachine.Topping.Dto;

namespace TopMachine.Topping.Frontend.Admin
{
    public partial class UserEdit : UserControl
    {
        public event UserDtoDelegate OnFinishDelegate;


        public UserEdit()
        {
            InitializeComponent();
        }

        public void SetItem(UserDto cfg)
        {
            bindingConfig.DataSource = cfg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Validate(true);
            UserDto cfg = (UserDto)bindingConfig.DataSource;

            if (MessageProxy.Proxy.UpdateUser(cfg) == false)
            {
                MessageBox.Show("Une erreur s'est produite", "Erreur", MessageBoxButtons.OK);
                OnFinishDelegate(this, cfg, false);
                return;
            }

            if (txtPwd.Text.Length > 0)
            {
                MessageProxy.Proxy.ChangedPwdUser(cfg, txtPwd.Text);
                txtPwd.Text = ""; 
            }

            OnFinishDelegate(this, cfg, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show(this, "Etes-vous certain d'effacer cet utilisateur ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                UserDto cfg = (UserDto)bindingConfig.DataSource;
                if (MessageProxy.Proxy.DeleteUser(cfg) == false)
                {
                    MessageBox.Show("Une erreur s'est produite", "Erreur", MessageBoxButtons.OK);
                    OnFinishDelegate(this, cfg, false);
                    return;
                }

                OnFinishDelegate(this, cfg, true);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Validate(true);
            OnFinishDelegate(this, (UserDto)bindingConfig.DataSource, false);
        }
    }
}
