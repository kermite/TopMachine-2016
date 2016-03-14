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
using Topping.Core.Proxy.Local.Client;


namespace TopMachine.Topping.Engine.GUI.Board
{ // besoin de GameCfg et de playedround

    /// <summary>
    /// Summary description for History.
    /// </summary>
    public class History : System.Windows.Forms.UserControl, IHistoryControl, IKeyHandler
    {


        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ListView lvPlayers;
        private IContainer components;

        private Panel panel1;
        private Label label1;
        private ImageList imageList1;
        System.Data.SqlClient.SqlConnection _dbconn;
        IGameState gameState = null;
        private TopMachine.Engine.Local.GameController.SolutionChooser.SolutionChooserSelect solutionChooserSelect1;
        private Splitter splitter1;
        LocalGameController gc = null;
        Guid MemoryCheckId;


        public event OnSelectedRoundEvent OnSelectedRound;


        public Control ResultControl
        {
            get { return this; }
        }



        public System.Data.SqlClient.SqlConnection dbconn
        {
            set { _dbconn = value; }
        }


        // Only for design PURPOSE
        public History()
        {
            InitializeComponent();
        }




        public History(LocalGameController gc, IGameState gameState)
        {
            InitializeComponent();
            this.gameState = gameState;
            this.gc = gc;
            MemoryCheckId = MemoryCheck.Register(this, "HistoryController CREATE");
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// 
         

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lvPlayers.Clear();

                if (components != null)
                {
                    components.Dispose();
                }
            }
           // if (gc != null) gc.Dispose();

            MemoryCheck.Unregister(MemoryCheckId);
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
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPlayers = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.solutionChooserSelect1 = new TopMachine.Engine.Local.GameController.SolutionChooser.SolutionChooserSelect();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Tirage";
            this.columnHeader10.Width = 80;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "N°";
            this.columnHeader9.Width = 30;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Mot";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Pos";
            this.columnHeader5.Width = 30;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Pts";
            this.columnHeader6.Width = 30;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Temps";
            this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Votre Mot";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Pts";
            this.columnHeader8.Width = 30;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Pos";
            this.columnHeader11.Width = 30;
            // 
            // lvPlayers
            // 
            this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader9,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader16,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader11});
            this.lvPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPlayers.FullRowSelect = true;
            this.lvPlayers.GridLines = true;
            this.lvPlayers.Location = new System.Drawing.Point(0, 19);
            this.lvPlayers.MultiSelect = false;
            this.lvPlayers.Name = "lvPlayers";
            this.lvPlayers.Size = new System.Drawing.Size(548, 208);
            this.lvPlayers.TabIndex = 14;
            this.lvPlayers.TabStop = false;
            this.lvPlayers.UseCompatibleStateImageBehavior = false;
            this.lvPlayers.View = System.Windows.Forms.View.Details;
            this.lvPlayers.SelectedIndexChanged += new System.EventHandler(this.lvPlayed_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 19);
            this.panel1.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(548, 19);
            this.label1.TabIndex = 75;
            this.label1.Text = "[Flèche Gauche] Coup précédent - [Flèche droite] Coup suivant - 2x clic Définitio" +
                "n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 217);
            this.splitter1.MinimumSize = new System.Drawing.Size(0, 10);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(548, 10);
            this.splitter1.TabIndex = 75;
            this.splitter1.TabStop = false;
            // 
            // solutionChooserSelect1
            // 
            this.solutionChooserSelect1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.solutionChooserSelect1.Location = new System.Drawing.Point(0, 227);
            this.solutionChooserSelect1.Name = "solutionChooserSelect1";
            this.solutionChooserSelect1.Size = new System.Drawing.Size(548, 159);
            this.solutionChooserSelect1.TabIndex = 74;
            this.solutionChooserSelect1.OnRemoveTempWord += new TopMachine.Topping.Common.Interfaces.RemoveTempWordEvent(this.solutionChooserSelect1_OnRemoveTempWord);
            this.solutionChooserSelect1.OnSetTempWord += new TopMachine.Topping.Common.Interfaces.SetTempWordEvent(this.solutionChooserSelect1_OnSetTempWord);
            // 
            // History
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvPlayers);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.solutionChooserSelect1);
            this.Name = "History";
            this.Size = new System.Drawing.Size(548, 386);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion



        public bool HandleKey(System.Windows.Forms.KeyEventArgs e)
        {
            int ndx = 0;
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Left:
                    if (lvPlayers.SelectedIndices.Count > 0 && lvPlayers.SelectedIndices[0] > 0)
                    {
                        ndx = lvPlayers.SelectedIndices[0] - 1;
                        lvPlayers.Items[ndx].Selected = true;
                    }
                    break;
                case System.Windows.Forms.Keys.Right:
                    if (lvPlayers.SelectedIndices.Count > 0 && lvPlayers.SelectedIndices[0] < lvPlayers.Items.Count - 1)
                    {
                        ndx = lvPlayers.SelectedIndices[0] + 1;
                        lvPlayers.Items[ndx].Selected = true;
                    }
                    break;
            }
            return false;
        }


        public void SetHistory(IGameState gameState)
        {
            lvPlayers.Clear();
            foreach (GameRoundDto round in gameState.GeneratedGame.Rounds.ToArray())
            {
                SetRound(round);
            }
        }



        public void SetRound(GameRoundDto rndDis)
        {
            if (lvPlayers.Items.Count == 0)
            {
                ListViewItem llvi = new ListViewItem();
                llvi.Text = rndDis.Rack;
                lvPlayers.Items.Add(llvi);
            }
            else
            {
                int indx = Convert.ToInt32(rndDis.Turn) - 1;
                lvPlayers.Items[indx].Text = rndDis.Rack;
            }

            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.Add(rndDis.Turn.ToString());
            lvi.SubItems.Add(rndDis.Word);
            lvi.SubItems.Add(rndDis.Direction);
            lvi.SubItems.Add(rndDis.Points.ToString());

            PlayedGameRoundDto dto = gameState.GetPlayerSummary().Rounds.Find(p => p.Turn == rndDis.Turn);

            if (dto != null)
            {
                lvi.SubItems.Add(dto.Time.ToString("#.##"));

                int neg = dto.Points - rndDis.Points;

                if (neg != 0)
                {
                    if (dto.Points == 9)
                        lvi.BackColor = Color.DarkGray;
                    else
                        lvi.BackColor = Color.FromArgb(200, 60, 60);
                    lvi.ForeColor = Color.Yellow;
                    lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = dto.Word, ForeColor = Color.Black, BackColor = Color.Red });
                }
                else
                    lvi.SubItems.Add(dto.Word);
                lvi.SubItems.Add(neg.ToString());
                lvi.SubItems.Add(dto.Direction);

            }
            else
            {
                lvi.BackColor = Color.DarkGray;
                lvi.ForeColor = Color.Yellow;
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("-" + rndDis.Points);
                lvi.SubItems.Add("");
            }

            lvPlayers.Items.Add(lvi);
            lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }

        public void SetRound(GameRoundDto rndDis, PlayedGameRoundDto dto)
        {
            if (lvPlayers.Items.Count == 0)
            {
                ListViewItem llvi = new ListViewItem();
                llvi.Text = rndDis.Rack;
                lvPlayers.Items.Add(llvi);
            }
            else
            {
                int indx = Convert.ToInt32(rndDis.Turn) - 1;
                lvPlayers.Items[indx].Text = rndDis.Rack;
            }

            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.Add(rndDis.Turn.ToString());
            lvi.SubItems.Add(rndDis.Word);
            lvi.SubItems.Add(rndDis.Direction);
            lvi.SubItems.Add(rndDis.Points.ToString());

            //  PlayedGameRoundDto dto = gameState.PlayerSummary.Rounds.Find(p => p.Turn == rndDis.Turn);

            if (dto != null)
            {
                lvi.SubItems.Add(dto.Time.ToString("#.##"));

                int neg = dto.Points - rndDis.Points;

                if (neg != 0)
                {
                    if (dto.Points == 9)
                        lvi.BackColor = Color.DarkGray;
                    else
                        lvi.BackColor = Color.FromArgb(200, 60, 60);
                    lvi.ForeColor = Color.Yellow;
                    lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = dto.Word, ForeColor = Color.Black, BackColor = Color.Red });
                }
                else
                    lvi.SubItems.Add(dto.Word);
                lvi.SubItems.Add(neg.ToString());
                lvi.SubItems.Add(dto.Direction);

            }
            else
            {
                lvi.BackColor = Color.DarkGray;
                lvi.ForeColor = Color.Yellow;
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.SubItems.Add("-" + rndDis.Points);
                lvi.SubItems.Add("");
            }

            lvPlayers.Items.Add(lvi);
            lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvPlayers.Refresh();
        }

        public void SetRound(int round)
        {

            GameRoundDto dto = gameState.GeneratedGame.Rounds.Find(p => p.Turn == round);
            SetRound(dto);
        }

        private void lvPlayed_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (MessageProxy.Proxy.GamePermissions.HasFlag(GamePermissions.CanObserve) || gc.GameConfig.GameState == enuGameState.Finished || gc.GameConfig.GameState == enuGameState.FinishedAndSaved)
            {
                gc.GameControllers.SolutionChooser = solutionChooserSelect1;
                solutionChooserSelect1.Visible = true;

                if (lvPlayers.SelectedIndices.Count > 0 && OnSelectedRound != null)
                {
                    OnSelectedRound(lvPlayers.SelectedIndices[0]);
                }
            }
        }

        private void solutionChooserSelect1_OnSetTempWord(string word, int[] origin, int row, int col, int dir)
        {
            gc.GameControllers.Board.SetWordTmp(word, origin, row, col, dir);
        }

        private void solutionChooserSelect1_OnRemoveTempWord(bool ok)
        {
            gc.GameControllers.Board.RemoveTmp(true);
        }

        public void SetHistory(List<PlayedGameRoundDto> lstRoundPlayer)
        {
            lvPlayers.Items.Clear();

            foreach (GameRoundDto round in gameState.GeneratedGame.Rounds.ToArray())
            {
                PlayedGameRoundDto playerRound;
                playerRound = lstRoundPlayer.Find(x => x.Turn == round.Turn);
                if (MessageProxy.Proxy.GamePermissions.HasFlag(GamePermissions.CanObserve) || gameState.GetPlayerSummary().Turn >= round.Turn)
                {
                    SetRound(round, playerRound);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
