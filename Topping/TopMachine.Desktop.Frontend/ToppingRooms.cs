using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TopMachine.Topping.Dto;
using Topping.Core.Logic;
using TopMachine.Topping.Engine.GameController;
using TopMachine.Topping.Common.Structures;
using Topping.Core.Proxy.Local.Client;
using TopMachine.Topping.Frontend.Controls;
using TopMachine.Desktop.Controls.Tools;
using Topping.Core.Proxy.Local;
using Topping.Core.Logic;
using Topping.Core.Logic.Client;

namespace TopMachine.Topping.Frontend
{
    public partial class ToppingRooms : UserControl, IStoppable
    {
        public delegate void StartDelegate(VRoom r);
        public event StartDelegate OnStartDelegate;
        public event StartDelegate OnObserverDelegate;
        List<VRoom> rooms = null;
        VRoom Rcurrent;

        bool Loaded = false;

        public ToppingRooms()
        {
            InitializeComponent();
            MessageProxy.Proxy.ActivateChat();
        }

        protected override void OnLoad(EventArgs e)
        {
            //friendsList1.LoadFriends();
            LoadList();

            chatControl1.Activate(false);
            base.OnLoad(e);
        }

        List<RoomItem> roomItems = new List<RoomItem>();

        private void LoadList()
        {
            if (MessageProxy.Proxy.Session == null)
            {
                MessageProxy.Proxy.Dispose();
                MessageProxy.Proxy.InitializeOnline();
                MessageProxy.Proxy.Start();
                
            }
            rooms = MessageProxy.Proxy.Session.Rooms;
            pnlConfigs.SuspendLayout();
            int max = pnlConfigs.Controls.Count - 1;
            for (int i = max; i >= 0; i--)
            {
                pnlConfigs.Controls[i].Dispose();
            }

            foreach (VRoom room in rooms.OrderBy(p => p.RoomId).ToList())
            {
                RoomItem item = new RoomItem();
                roomItems.Add(item);

                item.OnJoinRoom += new RoomItem.RoomEvent(item_OnJoinRoom);
                item.OnQuitRoom += new RoomItem.RoomEvent(item_OnQuitRoom);
                item.OnPlayers += new RoomItem.PlayerEvent(item_OnPlayers);
                item.OnStartGame += new RoomItem.RoomEvent(item_OnStartGame);
                item.SetItem(room);

                item.Dock = DockStyle.Top;

                if (Rcurrent != null) item.ChangeState(Rcurrent.RoomId != room.RoomId, Rcurrent != null);
                else item.ChangeState(true, Rcurrent != null);

                item.Visible = false;

                pnlConfigs.Controls.Add(item);
                item.BringToFront();
            }

            pnlConfigs.ResumeLayout();
            UpdateList(); 
            Loaded = true;
            timer1.Enabled = true;
        }


        private void UpdateList()
        {
            Rcurrent = MessageProxy.Proxy.Session.CurrentRoom;
			btnNewGame.Enabled = Rcurrent == null;
			pnlConfigs.SuspendLayout();

            foreach (RoomItem item in roomItems.ToArray())
            {// soit on peut rejoindre une partie car pas encore demarrer
                // soit on peut observer une partie qui a deja demarre 
                // soit on peut quitter une partie que l'on n'a ou pas demarre 

                VRoom room = rooms.Where(p => p.RoomId == item.room.RoomId).FirstOrDefault();
                bool isstart;
                if (room.GameStatus != RoomStatus.WaitingForGame
                    && room.GameStatus != RoomStatus.WaitingForStart && room.GameStatus != RoomStatus.Started)
                {
                    item.Visible = false;
                }
                else 
                {
                    isstart = room.GameStatus == RoomStatus.Started;
                                  
                                      
                        item.Visible = true;
                        item.SetItem(room);
                        if (Rcurrent != null)
                        {
                            item.ChangeState(Rcurrent.RoomId != room.RoomId, isstart );
                        }
                        else
                        {
                            item.ChangeState(true, isstart);
                        }
                 }
            }
            pnlConfigs.ResumeLayout();
        }


        //private void UpdateList()
        //{
        //    Rcurrent = MessageProxy.Proxy.Session.CurrentRoom;
        //    btnNewGame.Enabled = Rcurrent == null;
        //    pnlConfigs.SuspendLayout();

        //    foreach (RoomItem item in roomItems.ToArray())
        //    {// soit on peut rejoindre une partie car pas encore demarrer
        //     // soit on peut observer une partie qui a deja demarre 
        //     // soit on peut quitter une partie que l'on n'a ou pas demarre 

        //        Room room = rooms.Where(p => p.RoomId == item.room.RoomId).FirstOrDefault();

        //        if (room.GameStatus != RoomStatus.WaitingForGame
        //            && room.GameStatus != RoomStatus.WaitingForStart && room.GameStatus != RoomStatus.Started)
        //        {
        //            item.Visible = false;
        //        }
        //        else
        //        {
        //            if (room.GameStatus != RoomStatus.WaitingForGame
        //                && room.GameStatus != RoomStatus.WaitingForStart)
        //            {
        //                item.Visible = true;
        //                item.SetItem(room);
        //                if (Rcurrent != null)
        //                {
        //                    item.ChangeState(Rcurrent.RoomId != room.RoomId, true);
        //                }
        //                else
        //                {
        //                    item.ChangeState(true, true);
        //                }

        //            }
        //            else
        //            {
        //                item.Visible = true;
        //               // item.ChangeState(true, true);
        //                item.SetItemForObservation(room);
        //            }
        //        }
        //    }
        //    pnlConfigs.ResumeLayout();
        //}

        #region  ITEM Actions
        void item_OnStartGame(global::Topping.Core.Logic.VRoom room)
        {
            if (OnStartDelegate != null)
            {
                Rcurrent = null;
                OnStartDelegate(room);
            }
        }

        void item_OnPlayers(RoomItem room, string[] player)
        {
            if (player != null && player.Length > 0)
            {
                listPlayersRoom.refreshList(player);
                listPlayersRoom.Visible = true;
                tabControl1.SelectedTab = tabPlayersRoom;
            }
        }

        void item_OnQuitRoom(global::Topping.Core.Logic.VRoom room)
        {
            TopMachineForm.MainForm.DeletePreviousGame();
            Rcurrent = null;
            btnNewGame.Enabled = true;
            UpdateList();
        }

        void item_OnJoinRoom(global::Topping.Core.Logic.VRoom room)
        {
            if (room.GameStatus == RoomStatus.Started)
            {
                MessageProxy.Proxy.JoinRoom(room.RoomId);
                if (OnObserverDelegate != null)
                {
                    OnObserverDelegate(room);
                }
            }
            else
            {
                MessageProxy.Proxy.JoinRoom(room.RoomId);
                Rcurrent = room;
                btnNewGame.Enabled = false;

                pnlGlobal.Enabled = true;
                UpdateList();
            }
        }

        void gameState_OnCreated(VRoom r)
        {
            Progress.Instance.Hide(this);
            OnStartDelegate(r);
        }

        #endregion

        #region New Config
        private void btnNewConfig_Click(object sender, EventArgs e)
        {
            pnlConfigs.Enabled = false;
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
           
            this.Controls.Remove(ctl);
            ctl.Dispose();
            pnlConfigs.Enabled = true;
        }
        #endregion

        #region Create Room
        private void LoadRoomControl(VRoom r)
        {
            pnlGlobal.Enabled = false;
            RoomControl tcfg = new RoomControl();
            tcfg.OnQuitRoom += new RoomControl.RoomEvent(tcfg_OnQuitRoom);
            tcfg.OnJoinRoom += new RoomControl.RoomEvent(tcfg_OnJoinRoom);
            tcfg.OnCreateRoom += new RoomControl.RoomEvent(tcfg_OnCreateRoom);
            tcfg.OnStartGame += new RoomControl.RoomEvent(tcfg_OnStartGame);
            tcfg.SetItem(r);
            this.Controls.Add(tcfg);
            tcfg.BringToFront();
            tcfg.Top = (this.Height - tcfg.Height) / 2;
            tcfg.Left = (this.Width - tcfg.Width) / 2;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            LoadRoomControl(null);
        }

        void tcfg_OnStartGame(RoomControl ctrl, global::Topping.Core.Logic.VRoom room)
        {
            if (OnStartDelegate != null)
            {
                OnStartDelegate(room);
            }
        }

        void tcfg_OnCreateRoom(RoomControl ctrl, global::Topping.Core.Logic.VRoom room)
        {
            Rcurrent = room;

            btnNewGame.Enabled = false;
            pnlGlobal.Enabled = true;
        }

        void tcfg_OnJoinRoom(RoomControl ctrl, global::Topping.Core.Logic.VRoom room)
        {
            throw new NotImplementedException();
        }

        void tcfg_OnQuitRoom(RoomControl ctrl, global::Topping.Core.Logic.VRoom room)
        {
            MessageProxy.Proxy.Session.CurrentRoom = null;
            this.Controls.Remove(ctrl);
            pnlGlobal.Enabled = true;
        }

        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPlayers)
            {
                listPlayersGlobal.refreshList(MessageProxy.Proxy.Session.Pseudos.ToArray());
            }
        }


        protected override void OnVisibleChanged(EventArgs e)
        {
            // When we come back to the control we check
            // That the Current Room is still available 
            // And update the controls
            if (this.Visible)
            {
                UpdateList();
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }

            base.OnVisibleChanged(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateList();
        }

        #region IStoppable Members

        public void Stop()
        {
            chatControl1.Deactivate();
        }

        public void Reactivate()
        {
            chatControl1.Activate(false);
        }

        #endregion

        private void chkObserve_CheckedChanged(object sender, EventArgs e)
        {
           
        }


    }
}
