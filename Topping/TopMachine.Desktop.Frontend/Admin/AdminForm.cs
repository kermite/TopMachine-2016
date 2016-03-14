using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Topping.Core.Proxy.Local.Client;
using TopMachine.Topping.Dto;
using TopMachine.Topping.Frontend.Controls;

namespace TopMachine.Topping.Frontend.Admin
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            MessageProxy.Proxy.Dispose();
            MessageProxy.Proxy.InitializeOnline();

            tabControl1.Enabled = false;
            Login login2 = new Login();
            login2.IsLocal = false;
            login2.Left = (this.Width - login2.Width) / 2;
            login2.Top = (this.Height - login2.Height) / 2;
            this.Controls.Add(login2);
            login2.BringToFront();
            login2.OnLoginRequest += new Login.LoginRequest(login2_OnLoginRequest);

        }

        bool login2_OnLoginRequest(Control sender, bool cancel, string user, string password)
        {
            if (!MessageProxy.Proxy.Login(user, password)) return false;
            tabControl1.Enabled = true;
            return true;
        }

        List<UserDto> users;

        private void userRefresh_Click(object sender, EventArgs e)
        {
            users = MessageProxy.Proxy.GetUsers();
            userDtoBindingSource.DataSource = users.OrderBy(p=>p.UserName);
        }

        List<ConfigGameDto> configs;

        private void configRefresh_Click(object sender, EventArgs e)
        {
            configs = MessageProxy.Proxy.GetConfigurations();
            configGameDtoBindingSource.DataSource = configs.OrderBy(p=>p.Name);
        }

        private void EditConfig_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            ToppingConfig tcfg = new ToppingConfig();
            tcfg.OnFinishDelegate += new ConfigGameDelegate(tcfg_OnFinishDelegate);
            tcfg.SetItem(configGameDtoBindingSource.Current as ConfigGameDto);
            this.Controls.Add(tcfg);
            tcfg.BringToFront();
            tcfg.Top = (this.Height - tcfg.Height) / 2;
            tcfg.Left = (this.Width - tcfg.Width) / 2;
                     

        }

        private void addConfig_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            ToppingConfig tcfg = new ToppingConfig();
            tcfg.OnFinishDelegate += new ConfigGameDelegate(tcfg_OnFinishDelegate);
            tcfg.SetItem(new ConfigGameDto());
            this.Controls.Add(tcfg);
            tcfg.BringToFront();
            tcfg.Top = (this.Height - tcfg.Height) / 2;
            tcfg.Left = (this.Width - tcfg.Width) / 2;
        }

        void tcfg_OnFinishDelegate(Control ctl, ConfigGameDto cfg, bool ok)
        {
            ToppingConfig tcfg = ctl as ToppingConfig;
            this.Controls.Remove(tcfg);
            tcfg.OnFinishDelegate -= new ConfigGameDelegate(tcfg_OnFinishDelegate);
            tcfg.Dispose();
            tabControl1.Enabled = true;
        }

        private void userEdit_Click(object sender, EventArgs e)
        {

            tabControl1.Enabled = false;
            UserEdit tcfg = new UserEdit();
            tcfg.OnFinishDelegate += new UserDtoDelegate(tcfg_OnFinishDelegate);
            tcfg.SetItem(userDtoBindingSource.Current as UserDto);
            this.Controls.Add(tcfg);
            tcfg.BringToFront();
            tcfg.Top = (this.Height - tcfg.Height) / 2;
            tcfg.Left = (this.Width - tcfg.Width) / 2;
        }

        void tcfg_OnFinishDelegate(Control ctl, UserDto cfg, bool ok)
        {
            UserEdit tcfg = ctl as UserEdit;
            this.Controls.Remove(tcfg);
            tcfg.OnFinishDelegate -= new UserDtoDelegate(tcfg_OnFinishDelegate);
            tcfg.Dispose();
            tabControl1.Enabled = true;
        }

        private void userAdd_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            UserEdit tcfg = new UserEdit();
            tcfg.OnFinishDelegate += new UserDtoDelegate(tcfg_OnFinishDelegate);
            tcfg.SetItem(new UserDto());
            this.Controls.Add(tcfg);
            tcfg.BringToFront();
            tcfg.Top = (this.Height - tcfg.Height) / 2;
            tcfg.Left = (this.Width - tcfg.Width) / 2;
        }

        private void gridUsers_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {

        }


    }
}
