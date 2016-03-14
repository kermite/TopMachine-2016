using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using TopMachine.Topping.Common.Interfaces;
using TopMachine.Desktop.Overall;
using Topping.Core.Logic.Client;
using TopMachine.Topping.Dto;
using TopMachine.Topping.Common.Structures;
using System.Collections.Generic;
using Topping.Core.Logic;
using Topping.Core.Proxy.Local.Client;


namespace TopMachine.Topping.Engine.GUI.Board
{ // besoin de GameCfg et de playedround

    /// <summary>
    /// Summary description for History.
    /// </summary>
    public class PlayedGameRoundControl : System.Windows.Forms.UserControl, IPlayedGameRoundControl, IMessageHandler, IKeyHandler
    {

        private System.Windows.Forms.ColumnHeader columnPseudo;
        private System.Windows.Forms.ListView lvPlayers;
        private IContainer components;
        LocalGameController gc;
        bool _autoRefresh;

        private Panel panel1;
        private Label label1;
        private ImageList imageList1;
        bool isTopping;
        private ToolTip toolTip1;
        Guid MemoryCheckId;
       


        public Control ResultControl
        {
            get { return this; }
        }


        // Only for design PURPOSE
        public PlayedGameRoundControl()
        {
            InitializeComponent();
        }


        public PlayedGameRoundControl(LocalGameController gc)
        {
            InitializeComponent();
            this.gc = gc;
            AutoRefresh = false;
            MessageProxy.Proxy.Session.AddMessageHandler("PlayedGameRoundControl", this, true);
            MemoryCheckId = MemoryCheck.Register(this, "PlayedGameRoundControl CREATE");
            isTopping = MessageProxy.Proxy.Session.CurrentRoom.Configuration.Toping;

            for (int x = 1; x < 40; x++)
            {
                CreateColumn(x);
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
          //  if (gc != null) gc.Dispose();

            MemoryCheck.Unregister(MemoryCheckId);
            if (MessageProxy.Proxy.Session != null)
            {
                MessageProxy.Proxy.Session.RemoveMessageHandler("PlayedGameRoundControl");
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayedGameRoundControl));
            this.columnPseudo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPlayers = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnPseudo
            // 
            this.columnPseudo.Text = "Pseudo";
            this.columnPseudo.Width = 80;
            // 
            // lvPlayers
            // 
            this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPseudo});
            this.lvPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPlayers.FullRowSelect = true;
            this.lvPlayers.GridLines = true;
            this.lvPlayers.Location = new System.Drawing.Point(0, 19);
            this.lvPlayers.MultiSelect = false;
            this.lvPlayers.Name = "lvPlayers";
            this.lvPlayers.Size = new System.Drawing.Size(590, 367);
            this.lvPlayers.TabIndex = 14;
            this.lvPlayers.TabStop = false;
            this.lvPlayers.UseCompatibleStateImageBehavior = false;
            this.lvPlayers.View = System.Windows.Forms.View.Details;
            this.lvPlayers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPlayers_MouseDoubleClick);
            this.lvPlayers.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvPlayers_MouseMove);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(590, 19);
            this.panel1.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(590, 19);
            this.label1.TabIndex = 75;
            this.label1.Text = "double click sur un pseudo pour voir sa partie";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "arrow-down5.gif");
            this.imageList1.Images.SetKeyName(1, "up_alt.png");
            // 
            // toolTip1
            // 
            this.toolTip1.ShowAlways = true;
            // 
            // PlayedGameRoundControl
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.lvPlayers);
            this.Controls.Add(this.panel1);
            this.Name = "PlayedGameRoundControl";
            this.Size = new System.Drawing.Size(590, 386);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private void CreateColumn(int pos)
        {
            string key = pos.ToString();
            if (lvPlayers.Columns.ContainsKey(key)) 
            {
                return; 
            }
          
            lvPlayers.Columns.Add(key, key);
        }

        public void RefreshBoardRound(PlayedGameRoundDto psd)
        {
            SetPlayerGameRound(psd);
        }

        public void RefreshBoardRound()
        {
            lvPlayers.Items.Clear();

            SerializableDictionary<string, List<PlayedGameRoundDto>> dico = MessageProxy.Proxy.GameState.PlayedGameRound;  
            int index = 1;
            if (dico != null)
            {
                foreach (string pseudo in dico.Keys)
                {
                    SetPlayerRounds(pseudo, dico[pseudo]);
                    index++;
                }
            }
        }

        public void SetPlayerGameRound(PlayedGameRoundDto pgr) 
        {
            ListViewItem lvi;
            GameRoundDto dto;
            dto = MessageProxy.Proxy.GameState.GeneratedGame.Rounds.Find(p => p.Turn == pgr.Turn);
            int neg =  pgr.Points - dto.Points;

            if (!lvPlayers.Items.ContainsKey("lvPlayer_" +  pgr.Player))
            {
                lvi = new ListViewItem();
                lvi.UseItemStyleForSubItems = false; 
                lvPlayers.Items.Add(lvi);
                lvi.Text = pgr.Player;
                lvi.Name ="lvPlayer_" +  pgr.Player;
                for (int x = 0; x < 40; x++)
                {
                    lvi.SubItems.Add(""); 
                }
            }

            else 
            {
                lvi = lvPlayers.Items.Find("lvPlayer_" +pgr.Player, false)[0];
            }

            System.Windows.Forms.ListViewItem.ListViewSubItem si = lvi.SubItems[pgr.Turn];
            si.Name = lvi.Name + pgr.Turn.ToString();

            if (isTopping && neg == 0)
            {
                si.Text = String.Format("{0:00}:{1:00}", ((int)pgr.Time) / 60, pgr.Time % 60);
            }
            else if (neg != 0)
            {
                si.Text = neg.ToString();
                si.ForeColor = Color.Black;
                si.BackColor = Color.Red;
              //  si.Font = lvPlayers.Font;

            }
            else
            {
                si.Text = neg.ToString();
            }

            si.Tag = pgr.Word;
            
            lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        public void SetPlayerRounds(string player, List<PlayedGameRoundDto> pgr)
        {
            int index = 1;

            ListViewItem lvi = new ListViewItem();

            if (!lvPlayers.Items.ContainsKey("lvPlayer_" +  player))
            {
                lvi = new ListViewItem();
                lvi.UseItemStyleForSubItems = false;
                lvPlayers.Items.Add(lvi);
                lvi.Text = player;
                lvi.Name = "lvPlayer_" + player;
                for (int x = 0; x < 40; x++)
                {
                    lvi.SubItems.Add("");
                }
            }
            else
            {
                lvi = lvPlayers.Items.Find("lvPlayer_" + player, false)[0];
            }

            GameRoundDto dto;

            foreach (var round in pgr)
            {
                dto = MessageProxy.Proxy.GameState.GeneratedGame.Rounds.Find(p => p.Turn == round.Turn);
                int neg = round.Points - dto.Points;


                System.Windows.Forms.ListViewItem.ListViewSubItem si = lvi.SubItems[round.Turn];
                si.Name = lvi.Name + round.Turn.ToString();

                if (isTopping && neg == 0)
                {
                    si.Text = String.Format("{0:00}:{1:00}", ((int)round.Time) / 60, round.Time % 60);
                }
                else if (neg != 0)
                {
                    si.Text = neg.ToString();
                    si.ForeColor = Color.Black;
                    si.BackColor = Color.Red;
                  //  si.Font = lvPlayers.Font;

                }
                else
                {
                    si.Text = neg.ToString();
                }

                si.Tag = round.Word;

                index++;
            }

            lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvPlayers.Refresh();  

        }


        public GameCfg GameConfig
        {
            set { throw new NotImplementedException(); }
        }


        public void DisplayTurn(int turn, int temps, int total)
        {
            throw new NotImplementedException();
        }

        public void UpdateTurn(int sec)
        {
            throw new NotImplementedException();
        }

        public event OnSelectedPlayerEvent OnSelectedPlayer;


        private void lvPlayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (global::Topping.Core.Proxy.Local.Client.MessageProxy.Proxy.GamePermissions.HasFlag(GamePermissions.CanObserve))
            {
                ListViewItem li = lvPlayers.GetItemAt(e.X, e.Y);
                li.Selected = true;
                if (OnSelectedPlayer != null)
                {
                    string player = li.SubItems[0].Text;
                    if (MessageProxy.Proxy.GameState.PlayedGameRound.ContainsKey(player))
                    {
                        List<PlayedGameRoundDto> lst = MessageProxy.Proxy.GameState.PlayedGameRound[player];
                        OnSelectedPlayer(player, lst);
                    }

                }
            }
        }

        delegate bool HandleMessageCallbackDelegate(MessageDto m);

        public bool HandleMessage(MessageDto message)
        {
    /*        return (bool) this.Invoke(new HandleMessageCallbackDelegate(HandleMessageSafe), new object[] { m });
        }
           
        public bool HandleMessageSafe(MessageDto message)
        {*/

            switch (message.MessageType)
            {
                case MessageType.GameSummaryRoundUpdated:
                    if (AutoRefresh) 
                    {
                        PlayedGameRoundDto psd = new ObjectSerializer<PlayedGameRoundDto>().Deserialize(message.XmlObject);
                        RefreshBoardRound(psd); 
                    } 
                    break;

               case MessageType.Private :

                  if (message.MessageModule == MessageModule.Game)
                  {
                      RefreshBoardRound(); 
                  }
                    break;
            }

            return false;
        }

        public bool HandleKey(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool AutoRefresh
        {
            get 
            {
                return _autoRefresh; 
            }

            set 
            {
                _autoRefresh = value;
            }
        
        }

        public ListViewItem.ListViewSubItem oldSI;

        private void lvPlayers_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem i = lvPlayers.GetItemAt(e.X, e.Y);
            if (i != null)
            {
            ListViewItem.ListViewSubItem lvs = i.GetSubItemAt(e.X, e.Y);
            if (oldSI != lvs)
            {
                if (lvs != null)
                {
                    //toolTip1.SetToolTip(lvPlayers, lvs.Tag == null ? "Error" : lvs.Tag.ToString());
                    toolTip1 = null;

                    toolTip1 = new ToolTip(); 
                   // toolTip1.Show("", lvPlayers, 0);
                    if (lvs.Tag != null)
                    {
                        toolTip1.Show(lvs.Tag.ToString(), lvPlayers, 1000);
                    }
                    //else 
                    //{
                    //    toolTip1.Show("Erreur", lvPlayers, 1000);
                    //}
                } oldSI = lvs;
            }
            else
            {
                ;
            }
            }
        }
      
    }
}