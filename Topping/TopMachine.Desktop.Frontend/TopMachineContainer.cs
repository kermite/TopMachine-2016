using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq; 
using System.Windows.Forms;
using TopMachine.Topping.Engine.GUI;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Common.Interfaces;
using TopMachine.Desktop.Overall;
using TopMachine.Desktop.Controls;
using Topping.Core.Proxy.Local.Client;
using TopMachine.Desktop.Controls.Sound;
using Topping.Core.Logic.Client;


namespace TopMachine.Topping.Frontend
{
    public partial class TopMachineContainer : UserControl, IDisposable, IStoppable, IKeyHandler, IFinished
    {
        public static TopMachineContainer Instance { get; set; }

        GameControllers gctls = null;
        LocalGameController gc; 

        public TopMachineContainer()
        {
            TopMachineContainer.Instance = this;
            InitializeComponent(); 
        }

        void chatControl1_onChatActivate(bool activate)
        {
            if (activate) {
                if (gctls.Board  != null) gctls.KeyHandlers.Add((IKeyHandler)gctls.Board);
            }
            else
            {
                if (gctls.Board != null) gctls.KeyHandlers.Remove((IKeyHandler)gctls.Board);
            }
            
        }
      
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                SaveSettings();
                while (tabControl1.TabPages.Count > 0)
                {
                    TabPage tb = tabControl1.TabPages[0];
                    tabControl1.TabPages.Remove(tb);
                    tb.Dispose();
                }
                if (gc != null) gc.Dispose(); 
                tabControl1.Dispose();
                chatControl1.Dispose();

                components.Dispose();
            }

            if (gc != null)
            {
                gc.OnGameControllersCreated -= new global::TopMachine.Topping.Common.Interfaces.OnGameControllersCreatedEvent(gc_OnGameControllersCreated);
                gc.OnFinish -= new OnFinishEvent(gc_OnFinish);
                if (gctls.ClassementControl != null) gctls.ClassementControl.OnSelectedPlayer -= new OnSelectedPlayerEvent(ClassementControl_OnSelectedPlayer);
                if (gctls.PlayedGameRoundControl != null) gctls.PlayedGameRoundControl.OnSelectedPlayer -= new OnSelectedPlayerEvent(ClassementControl_OnSelectedPlayer);
                gc.Dispose();
                gc = null;
            }
            
            base.Dispose(disposing);
        }

        public void SaveSettings()
        {
            int x = Properties.Settings.Default.WidthOfGrid;

            if (pnlContainer.Width != x && pnlContainer.Width != 0)
            {
                Properties.Settings.Default.WidthOfGrid = pnlContainer.Width;
                Properties.Settings.Default.Save();
            }
        }

        public void Initialize()
        {
            if (MessageProxy.Proxy.GamePermissions.HasFlag(GamePermissions.CanChat))
            {
                MessageProxy.Proxy.ActivateChat();
                chatControl1.onChatActivate += new Chat.ChatEvent(chatControl1_onChatActivate);
            }
            else
            {
                MessageProxy.Proxy.DeactivateChat();
                chatControl1.Dispose();
                tabChat.Dispose(); 
            }

            pnlContainer.Width = Properties.Settings.Default.WidthOfGrid;

            if (gctls != null)
            {
                gctls.Chevalet.ResultControl.Dispose();
                gctls.HistoryControl.ResultControl.Dispose();
                gctls.HistoryPlayerControl.ResultControl.Dispose();
                gctls.ClassementControl.ResultControl.Dispose();
                gctls = null; 
            }

            if (gc != null)
            {
                gc.Stop(false);
                gc.Dispose();
                gc = null; 
            }

            shortRoomItem1.SetItem(MessageProxy.Proxy.Session.CurrentRoom);

            gc = new LocalGameController(MessageProxy.Proxy);
            gc.OnGameControllersCreated += new global::TopMachine.Topping.Common.Interfaces.OnGameControllersCreatedEvent(gc_OnGameControllersCreated);
            gc.OnFinish += new OnFinishEvent(gc_OnFinish);
            gc.InitGame();
            chatControl1.Activate(true);
           

        }

        public void ShowRoomItem()
        {
            shortRoomItem1.Visible = true;
        }

        private void RefreshPlayedRoundControl() 
        {
            string key = "coup par coup";

            if (!tabControl1.TabPages.ContainsKey(key))
            {
                System.Windows.Forms.TabPage tabPlayed = new TabPage(key);
                tabPlayed.Name = key;  
                tabControl1.TabPages.Add(tabPlayed);
                tabPlayed.Controls.Add(gctls.PlayedGameRoundControl.ResultControl);
                gctls.PlayedGameRoundControl.ResultControl.Dock = DockStyle.Fill;
                gctls.PlayedGameRoundControl.OnSelectedPlayer += new OnSelectedPlayerEvent(ClassementControl_OnSelectedPlayer);
                gctls.PlayedGameRoundControl.AutoRefresh = true; 
            }

             gctls.PlayedGameRoundControl.RefreshBoardRound(); 

        }

        void gc_OnFinish()
        {
            RefreshPlayedRoundControl(); 
        }

        public void StartGame()
        {
            
            btnStart.Visible = false;
            Sound.play(Sound.SoundType.STARTGAME);

            gctls.Chevalet.ResultControl.Dock = DockStyle.Fill;
            pnlRack.Controls.Add(gctls.Chevalet.ResultControl);

            gctls.HistoryControl.ResultControl.Dock = DockStyle.Fill;
            gctls.HistoryPlayerControl.ResultControl.Dock = DockStyle.Fill;
            gctls.ClassementControl.ResultControl.Dock = DockStyle.Fill;

            tabHistory.Controls.Add(gctls.HistoryControl.ResultControl);          
            tabClassement.Controls.Add(gctls.ClassementControl.ResultControl);
            gctls.ClassementControl.OnSelectedPlayer += new OnSelectedPlayerEvent (ClassementControl_OnSelectedPlayer);
           
            gc.NextRound();
        }

        public void ObserveGame()
        {
            this.SuspendLayout();
            btnStart.Visible = false;
            Sound.play(Sound.SoundType.STARTGAME);

            gctls.Chevalet.ResultControl.Dock = DockStyle.Fill;
            pnlRack.Controls.Add(gctls.Chevalet.ResultControl);

            gctls.HistoryControl.ResultControl.Dock = DockStyle.Fill;
            gctls.HistoryPlayerControl.ResultControl.Dock = DockStyle.Fill;
            gctls.ClassementControl.ResultControl.Dock = DockStyle.Fill;
            gctls.PlayedGameRoundControl.ResultControl.Dock = DockStyle.Fill;

            tabHistory.Controls.Add(gctls.HistoryControl.ResultControl);
            tabClassement.Controls.Add(gctls.ClassementControl.ResultControl);
            gctls.ClassementControl.OnSelectedPlayer += new OnSelectedPlayerEvent(ClassementControl_OnSelectedPlayer);

            tabClassement.Controls.Add(gctls.PlayedGameRoundControl.ResultControl);
            gctls.PlayedGameRoundControl.OnSelectedPlayer += new OnSelectedPlayerEvent(ClassementControl_OnSelectedPlayer);
                       
            gc.ObserveGame();
            this.ResumeLayout();

        }

        bool ClassementControl_OnSelectedPlayer(string player, List<Dto.PlayedGameRoundDto> lstPlayerRound)
        {
            TabPage tp;
            if (!tabControl1.TabPages.ContainsKey("HISTOPLAYER"))
            {
                tp = new TabPage(player);
                tp.Name = "HISTOPLAYER";
                tabControl1.TabPages.Add(tp);  
                tp.Controls.Add(gctls.HistoryPlayerControl.ResultControl);
            }
            else 
            {
                tp = tabControl1.TabPages["HISTOPLAYER"];
                tp.Text = player;
            }
                                     
            gctls.HistoryPlayerControl.SetHistory(lstPlayerRound);
           
          return false;
        }

        void gc_OnGameControllersCreated(GameControllers g)
        {
            gctls = g;

            pnlGrid.Controls.Add(gctls.Board.ResultControl);

            int w = pnlGrid.Width > pnlGrid.Height ? pnlGrid.Height : pnlGrid.Width;
            gctls.Board.ResultControl.Width = w;
            gctls.Board.ResultControl.Height= w;

            gctls.Board.ResultControl.Left = (pnlGrid.Width - w) / 2;
            gctls.Board.ResultControl.Top = (pnlGrid.Height - w) / 2;

            gctls.RoundControl.ResultControl.Dock = DockStyle.Fill; 
            pnlScore.Controls.Add(gctls.RoundControl.ResultControl);
        }

        private void pnlGrid_Resize(object sender, EventArgs e)
        {
            if (gctls != null)
            {
                int w = pnlGrid.Width > pnlGrid.Height ? pnlGrid.Height : pnlGrid.Width;
                gctls.Board.ResultControl.Width = w;
                gctls.Board.ResultControl.Height= w;

                gctls.Board.ResultControl.Left = (pnlGrid.Width - w) / 2;
                gctls.Board.ResultControl.Top = (pnlGrid.Height - w) / 2;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void pnlScore_GotFocus(object sender, EventArgs e)
        {

        }

        public bool HandleKey(KeyEventArgs e)
        {
            if (this.Parent.Visible)
            {
                return gc.HandleKey(e);
            }
            return false; 
        }


        #region IStoppable Members
        public void Stop()
        {
            if (gc != null)
            {
                gc.Dispose();
            }
            if (gctls != null) 
            {
                gctls.Dispose(); 
            }
            if (chatControl1 != null)
            {
                chatControl1.Deactivate();
                chatControl1.Dispose();
            }
        }

        #endregion


        #region IFinished Members

        public event OnFinishedDelegate OnFinished;

        #endregion


        #region IStoppable Members


        public void Reactivate()
        {
            chatControl1.Activate(true); 
        }

        #endregion

    }
}
