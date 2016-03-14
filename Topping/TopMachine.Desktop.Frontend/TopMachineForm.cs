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
using Topping.Core.Logic;
using TopMachine.Topping.Frontend.Controls;
using Topping.Core.Proxy.Local.Client;
using TopMachine.Desktop.Controls.Tools;
using Topping.Core.Logic.Client;
using TopMachine.Topping.Dto;

namespace TopMachine.Topping.Frontend
{
    public partial class TopMachineForm : TitledUserControl, IKeyHandler
    {
        public static TopMachineForm MainForm
        {
            get;
            set;
        }


        TopMachineContainerWindow TopMachineContainerWindow = null;
        TopMachineContainer TopMachineContainer = null;
		private string _user;

        Dictionary<string, Control> ControlDico = new Dictionary<string, Control>();



        public TopMachineForm()
        {
            InitializeComponent();
            TopMachineForm.MainForm = this;

            ControlDico.Add("MAIN", toppingSelection1);
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

            btnBackToGame.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Topping");
            btnConfig.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Settings");
            btnNavigation.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Application");
            btnQuit.Image = PackageManager.Instance.Project.GetBitmap("TopMachineData\\ApplicationImages\\Cancel");
            base.OnLoad(e);
        }


        public void DeleteControl(string key)
        {
            Control ctl = ControlDico[key];

            if (ctl != null)
            {
                if (ctl is IStoppable)
                {
                    ((IStoppable)ctl).Stop();

                    pnlContainer.Controls.Remove(ctl);
                    ctl.Dispose();
                }
                else
                {
                    pnlContainer.Controls.Remove(ctl);
                    ctl.Dispose();
                }
            }
            ControlDico[key] = null;
            System.GC.Collect();

        }

        public void SetControl(string key, Control ctl)
        {
            if (ctl != null)
            {

                foreach (Control c in pnlContainer.Controls)
                {
                    c.Visible = false;
                }

                ctl.Dock = DockStyle.Fill;
                ctl.Visible = true;
                ControlDico[key] = ctl;
                if (!pnlContainer.Contains(ctl)) 
                {
                    pnlContainer.Controls.Add(ctl);
                }

                if (ctl is IStoppable)
                {
                    ((IStoppable)ctl).Reactivate();
                }
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


        private void toppingSelection1_ItemSelected(string tag)
        {
            switch (tag)
            {
                case "LOCAL":
                    pnlContainer.Enabled = false;
                    toolStrip1.Enabled = false;
                    Login login = new Login();
                    //login.IsLocal = true;
                    //login.Left = (this.Width - login.Width) / 2;
                    //login.Top = (this.Height - login.Height) / 2;
                    //this.Controls.Add(login);
                    //login.BringToFront();
                    //login.OnLoginRequest += new Login.LoginRequest(locallogin_OnLoginRequest);
                     string user = Properties.Settings.Default.LocalUser;
                    if (string.IsNullOrEmpty(user)) 
                    {
                        user = "TopMachine";
                    }

                    locallogin_OnLoginRequest(login, false, user, "");

                    break; 
                case "DIRECT":
                    pnlContainer.Enabled = false;
                    toolStrip1.Enabled = false;
                    Login login2 = new Login();
                    login2.IsLocal = false;
                    login2.Left = (this.Width - login2.Width) / 2;
                    login2.Top = (this.Height - login2.Height) / 2;
                    this.Controls.Add(login2);
                    login2.BringToFront();
                    login2.OnLoginRequest += new Login.LoginRequest(login_OnLoginRequest);
                    break;

            }
        }

        bool login_OnLoginRequest(Control ctl, bool cancel, string user, string password)
        {

            if (cancel)
            {
                this.Controls.Remove(ctl);
                pnlContainer.Enabled = true;
                toolStrip1.Enabled = true;
                return true;
            }

            if (user.Length == 0)
            {
                return false;
            }

            MessageProxy.Proxy.Dispose();
            MessageProxy.Proxy.InitializeOnline();

            if (MessageProxy.Proxy.Login(user, password))
            {
                MessageProxy.Proxy.Start();
                DeleteControl("CONFIG");
                DeletePreviousGame();
                //DeleteControl("GAME");

                ToppingRooms config = new ToppingRooms();
                SetControl("CONFIG", config);
                config.OnStartDelegate += new ToppingRooms.StartDelegate(configNetwork_OnStartDelegate);
                config.OnObserverDelegate += new ToppingRooms.StartDelegate(config_OnObserverDelegate);
                btnConfig.Enabled = true;
                this.Controls.Remove(ctl);
                pnlContainer.Enabled = true;
                toolStrip1.Enabled = true;
                return true;
            }

            return false;
        }


        bool locallogin_OnLoginRequest(Control ctl, bool cancel, string user, string password)
        {
            if (cancel)
            {
                this.Controls.Remove(ctl);
                pnlContainer.Enabled = true;
                toolStrip1.Enabled = true;
                return true;
            }

            if (user.Length == 0)
            {
                return false; 
            }

            MessageProxy.Proxy.Dispose();
            MessageProxy.Proxy.InitializeLocal(); 

            if (MessageProxy.Proxy.Login(user, password))
            {
				_user = user;
                MessageProxy.Proxy.Start();
                DeleteControl("CONFIG");
                //DeleteControl("GAME");
                DeletePreviousGame();

                ToppingConfigs config = new ToppingConfigs();
                SetControl("CONFIG", config);
                config.OnStartDelegate += new ToppingConfigs.StartDelegate(config_OnStartDelegate);
                btnConfig.Enabled = true;
                this.Controls.Remove(ctl);
                pnlContainer.Enabled = true;
                toolStrip1.Enabled = true;
                return true;
            }

            return false; 
        }


        public void DeletePreviousGame()
        {
            if (TopMachineContainerWindow != null)
            {
                TopMachineContainerWindow.Dispose();
                TopMachineContainerWindow = null;
                TopMachineContainer = null;
            }
        }
        void configNetwork_OnStartDelegate(VRoom r)
        {
           // DeleteControl("GAME");
            DeletePreviousGame();

            MessageProxy.Proxy.StartGame();
            MessageProxy.Proxy.DeactivateChat();

            MessageProxy.Proxy.GamePermissions =GamePermissions.CanChat | GamePermissions.CanValidate; 
            btnBackToGame.Enabled = true;

            TopMachineContainerWindow  = new TopMachineContainerWindow();
            TopMachineContainerWindow.FormClosed += new FormClosedEventHandler(TopMachineContainerWindow_FormClosed);
            TopMachineContainerWindow.InitializeContainer();
            TopMachineContainer = TopMachineContainerWindow.GetTopMachineContainer();
            TopMachineContainer.OnFinished += new OnFinishedDelegate(ct_OnFinished);


            //SetControl("GAME", ct);
            TopMachineContainer.Initialize();
            TopMachineContainer.ShowRoomItem();
            TopMachineContainer.StartGame();
        }

        void config_OnObserverDelegate(VRoom r)
        {
            // DeleteControl("GAME");
            DeletePreviousGame();

            btnBackToGame.Enabled = true;
            MessageProxy.Proxy.GamePermissions = GamePermissions.CanObserve | GamePermissions.CanChat;

            TopMachineContainerWindow = new TopMachineContainerWindow();
            TopMachineContainerWindow.InitializeContainer();
            TopMachineContainerWindow.FormClosed += new FormClosedEventHandler(TopMachineContainerWindow_FormClosed);
            TopMachineContainer = TopMachineContainerWindow.GetTopMachineContainer();
            TopMachineContainer.OnFinished += new OnFinishedDelegate(ct_OnFinished);

            MessageProxy.Proxy.Session.SetCompleteGame(r.RoomId); 
            TopMachineContainer.Initialize();
            TopMachineContainer.ShowRoomItem();
            TopMachineContainer.ObserveGame();
        
        }

        void TopMachineContainerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
           
			MessageDto m = new MessageDto();
			m.From = _user;
			m.Text = "[" + DateTime.Now + "]" + " je suis parti";
			m.MessageModule = MessageModule.Chat;
			m.MessageType = MessageType.NoType;
			MessageProxy.Proxy.SendMessage(m);

			MessageProxy.Proxy.LeaveAnyRoom();
			//DeleteControl("GAME");
			DeletePreviousGame();
            ControlDico["CONFIG"].Enabled = true;
            SetControl("CONFIG", ControlDico["CONFIG"]);
        }

        void config_OnStartDelegate(VRoom r)
        {


            MessageProxy.Proxy.StartGame();
            MessageProxy.Proxy.DeactivateChat();

            btnBackToGame.Enabled = true;

            TopMachineContainerWindow = new TopMachineContainerWindow();
            TopMachineContainerWindow.InitializeContainer();
            TopMachineContainerWindow.FormClosed += new FormClosedEventHandler(TopMachineContainerWindow_FormClosed);
            TopMachineContainer = TopMachineContainerWindow.GetTopMachineContainer();
            TopMachineContainer.OnFinished += new OnFinishedDelegate(ct_OnFinished);
            TopMachineContainer.Initialize();
        }

        void ct_OnFinished()
        {
            ControlDico["Config"].Enabled = true; 
        }

        private void btnNavigation_Click(object sender, EventArgs e)
        {
            SetControl("MAIN", toppingSelection1);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            SetControl("CONFIG", ControlDico["CONFIG"]);
        }

        private void btnBackToGame_Click(object sender, EventArgs e)
        {
            SetControl("GAME", ControlDico["GAME"]);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (ControlDico["GAME"] != null)
            {
                MessageProxy.Proxy.LeaveAnyRoom();
                DeletePreviousGame();
                //DeleteControl("GAME");
                ControlDico["CONFIG"].Enabled = true;
                SetControl("CONFIG", ControlDico["CONFIG"]);
            }
        }

        private void toppingSelection1_Load(object sender, EventArgs e)
        {

        }

        //private void btnBackToGame_Click_1(object sender, EventArgs e)
        //{
        //    SetControl("GAME", ControlDico["GAME"]);
        //}

        #region IDisposable Members

        #endregion
    }
}
