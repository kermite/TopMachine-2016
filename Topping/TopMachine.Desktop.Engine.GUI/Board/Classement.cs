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
    public class Classement : System.Windows.Forms.UserControl, IClassement, IKeyHandler, IMessageHandler
    {


        private System.Windows.Forms.ColumnHeader columnPseudo;
        private System.Windows.Forms.ColumnHeader columnScore;
        private System.Windows.Forms.ColumnHeader columnTotal;
        private System.Windows.Forms.ColumnHeader columncptTopPlayer;
        private System.Windows.Forms.ColumnHeader columncptTop;
        private System.Windows.Forms.ColumnHeader columnRang;
        private System.Windows.Forms.ColumnHeader columnNegatif;
        private System.Windows.Forms.ColumnHeader columnTime;
        private System.Windows.Forms.ListView lvPlayers;
        private IContainer components;

        private Panel panel1;
        private Label label1;
        private ImageList imageList1;
        System.Data.SqlClient.SqlConnection _dbconn;
        LocalGameController gc = null;

        private ColumnHeader columnProgress;

        List<PlayerSummaryDto> lstPlayer;
        bool isTopping;
        Guid MemoryCheckId;


        public Control ResultControl
        {
            get { return this; }
        }



        public System.Data.SqlClient.SqlConnection dbconn
        {
            set { _dbconn = value; }
        }


        // Only for design PURPOSE
        public Classement()
        {
            InitializeComponent();
        }


        public Classement(LocalGameController gc)
        {
            InitializeComponent();

            this.gc = gc;
            MessageProxy.Proxy.Session.AddMessageHandler("Classement", this, true);
            MemoryCheckId = MemoryCheck.Register(this, "Classement CREATE");
            lstPlayer = new List<PlayerSummaryDto>();

            if (gc.GameConfig.observeMode == 0)
            {
                initClassement();
                RefreshClassement();
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            ;
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
            //if (gc != null) gc.Dispose();
 
            MemoryCheck.Unregister(MemoryCheckId);     
            if (MessageProxy.Proxy.Session != null)
            {
                MessageProxy.Proxy.Session.RemoveMessageHandler("Classement");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Classement));
            this.columnPseudo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columncptTop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columncptTopPlayer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNegatif = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnRang = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPlayers = new System.Windows.Forms.ListView();
            this.columnProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnPseudo
            // 
            this.columnPseudo.Text = "Pseudo";
            this.columnPseudo.Width = 80;
            // 
            // columnScore
            // 
            this.columnScore.Text = "Score";
            this.columnScore.Width = 58;
            // 
            // columnTotal
            // 
            this.columnTotal.Text = "Total";
            this.columnTotal.Width = 62;
            // 
            // columncptTop
            // 
            this.columncptTop.Text = "#Tops";
            this.columncptTop.Width = 51;
            // 
            // columncptTopPlayer
            // 
            this.columncptTopPlayer.Text = "#Tops Player";
            this.columncptTopPlayer.Width = 94;
            // 
            // columnNegatif
            // 
            this.columnNegatif.Text = "Negatif";
            this.columnNegatif.Width = 63;
            // 
            // columnRang
            // 
            this.columnRang.Text = "Rang";
            this.columnRang.Width = 52;
            // 
            // columnTime
            // 
            this.columnTime.Text = "Time";
            this.columnTime.Width = 52;
            // 
            // lvPlayers
            // 
            this.lvPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnRang,
            this.columnPseudo,
            this.columnScore,
            this.columnTotal,
            this.columnNegatif,
            this.columnTime,
            this.columncptTopPlayer,
            this.columncptTop,
            this.columnProgress});
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
            this.lvPlayers.SelectedIndexChanged += new System.EventHandler(this.lvPlayers_SelectedIndexChanged);
            this.lvPlayers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPlayers_MouseDoubleClick);
            // 
            // columnProgress
            // 
            this.columnProgress.Text = "Progress";
            this.columnProgress.Width = 74;
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
            // Classement
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.lvPlayers);
            this.Controls.Add(this.panel1);
            this.Name = "Classement";
            this.Size = new System.Drawing.Size(590, 386);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion



        public bool HandleKey(System.Windows.Forms.KeyEventArgs e)
        {
            return false;
        }

        private void replaceDto(PlayerSummaryDto NewPlayerDto, PlayerSummaryDto OldPlayerDto)
        {
            OldPlayerDto.Date = NewPlayerDto.Date;
            OldPlayerDto.Percentage = NewPlayerDto.Percentage;
            OldPlayerDto.PointsLost = NewPlayerDto.PointsLost;
            OldPlayerDto.Rounds = NewPlayerDto.Rounds;
            OldPlayerDto.Time = NewPlayerDto.Time;
            OldPlayerDto.Total = NewPlayerDto.Total;
            OldPlayerDto.TotalTop = NewPlayerDto.TotalTop;
            OldPlayerDto.Turn = NewPlayerDto.Turn;
        }

        private PlayerSummaryDto searchPlayer(string player)
        {
            PlayerSummaryDto PlayerDto;
            PlayerDto = lstPlayer.Find(x => x.Player == player);
            return PlayerDto;
        }

        private void RefreshPlayer(PlayerSummaryDto NewPlayerDto)
        {
            PlayerSummaryDto OldPlayerDto;
            OldPlayerDto = lstPlayer.Find(x => x.Player == NewPlayerDto.Player);
            if (OldPlayerDto != null)
            {
                replaceDto(NewPlayerDto, OldPlayerDto);
            }
            else
            {
                lstPlayer.Add(NewPlayerDto);
            }
        }

        private void initPlayer(string playerName)
        {
            if (lstPlayer.Find(p => p.Player == playerName) == null)
            {
                PlayerSummaryDto pdto = new PlayerSummaryDto(playerName, 0, 0, 0, 0, 0, 0);
                lstPlayer.Add(pdto);
            }
        }

        private void initClassement()
        {
            // list of players room 
            string[] listPlayers;
            isTopping = MessageProxy.Proxy.Session.CurrentRoom.Configuration.Toping;
            listPlayers = MessageProxy.Proxy.ListRoomActivePlayers(MessageProxy.Proxy.Session.CurrentRoom.RoomId);
            foreach (string playername in listPlayers)
            {
                initPlayer(playername);
            }

        }

        private int getPosition(List<PlayerSummaryDto> lst_tmp, string key)
        {
            int pos;
            pos = lst_tmp.FindIndex(x => x.Player == key);

            return pos;
        }

        private int CompareClassement(List<PlayerSummaryDto> lst_tmp, string key, int position)
        {
            int prec_pos;

            prec_pos = getPosition(lst_tmp, key);

            if (prec_pos == -1)
            {
                return position;
            }
            else
            {
                return prec_pos - position;
            }
        }

        private Color getColorProgress(int pos)
        {
            if (pos > 0)
            {
                return Color.Green;
            }
            else if (pos < 0)
            {
                return Color.Red;
            }
            else
            {
                return Color.White;
            }
        }

        private void RefreshClassement()
        {
            int progress;
            List<PlayerSummaryDto> lst_tmp;

            lst_tmp = new List<PlayerSummaryDto>(lstPlayer.ToArray());
                   
            
            lvPlayers.Items.Clear();
            int index = 1;
            lstPlayer.Sort(
                (a, b) =>
                {
                    if (a.Turn != b.Turn)
                    {
                        return -(a.Turn - b.Turn);
                    }
                    else
                    {
                        if (a.Total != b.Total)
                        {
                            return -(a.Total - b.Total);
                        }
                        else
                        {
                            return (int)(a.Time - b.Time);
                        }
                    }
                });

            foreach (PlayerSummaryDto player in lstPlayer.ToArray())
            {
                progress = CompareClassement(lst_tmp, player.Player, index - 1);
                SetPlayer(player, index, progress, false);
                index++;
            }

            lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private string getFormatValue(int progress)
        {
            string formatvalue;

            if (progress == 0)
            {
                formatvalue = "";
            }
            else
            {
                formatvalue = progress.ToString("+#;-#;0");
            }

            return formatvalue;
        }

        public void SetPlayer(PlayerSummaryDto PlayerDto, int index, int progress, bool setColumSize)
        {

            int total = PlayerDto.Total - PlayerDto.PointsLost;
            ListViewItem lvi = new ListViewItem();
            lvi.Text = index.ToString();
            lvi.SubItems.Add(PlayerDto.Player);


            if ( (!isTopping) || (isTopping && gc.GameConfig.GameState == enuGameState.FinishedAndSaved) || global::Topping.Core.Proxy.Local.Client.MessageProxy.Proxy.GamePermissions.HasFlag(GamePermissions.CanObserve))
            {
                lvi.SubItems.Add(PlayerDto.Total.ToString());
                lvi.SubItems.Add(total.ToString());
                lvi.SubItems.Add(PlayerDto.PointsLost.ToString());
            }
            else
            {
                lvi.SubItems.Add("##");
                lvi.SubItems.Add("##");
                lvi.SubItems.Add("##"); 
            }

            lvi.SubItems.Add(String.Format("{0:00}:{1:00}", ((int)PlayerDto.Time) / 60, PlayerDto.Time % 60));
            lvi.SubItems.Add(PlayerDto.TotalTop.ToString());
            lvi.SubItems.Add(PlayerDto.Turn.ToString());
            lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = getFormatValue(progress), ForeColor = Color.White, BackColor = getColorProgress(progress) });
            lvi.UseItemStyleForSubItems = false;


            lvPlayers.Items.Add(lvi);
            if (setColumSize)
            {
                lvPlayers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            this.Refresh();

        }


        public GameCfg GameConfig
        {
            set { throw new NotImplementedException(); }
        }

        public void Initialize()
        {
            if (gc.proxy.GameState.FinalRoom != null)
            {
                if (gc.proxy.GameState.FinalRoom.PlayerSummaries != null)
                {
                    foreach (PlayerSummaryDto dto in gc.proxy.GameState.FinalRoom.PlayerSummaries.Values)
                    {
                        RefreshPlayer(dto);
                    }
                }
            }

            RefreshClassement();

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

        delegate bool HandleMessageCallbackDelegate(MessageDto m);

        public bool HandleMessage(MessageDto message)
        {
  /*          return (bool)this.Invoke(new HandleMessageCallbackDelegate(HandleMessageSafe), new object[] { m });
        }

        public bool HandleMessageSafe(MessageDto message)
        {*/

            switch (message.MessageType)
            {
                case MessageType.GameSummaryUpdated:
                    PlayerSummaryDto p = new ObjectSerializer<PlayerSummaryDto>().Deserialize(message.XmlObject);
                    RefreshPlayer(p);
                    RefreshClassement();
                  break;
              
                default:
                    return false;
            }
            return true;
        }

        private void lvPlayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (global::Topping.Core.Proxy.Local.Client.MessageProxy.Proxy.GamePermissions.HasFlag(GamePermissions.CanObserve))
            {
                ListViewItem li =  lvPlayers.GetItemAt(e.X, e.Y);
                li.Selected = true;
                if (OnSelectedPlayer != null)
                {
                    string player = li.SubItems[1].Text;
                    if (MessageProxy.Proxy.GameState.PlayedGameRound.ContainsKey(player))
                    {
                        List<PlayedGameRoundDto> lst = MessageProxy.Proxy.GameState.PlayedGameRound[player];
                        OnSelectedPlayer(player, lst);
                    }

                }
            }
        }

        private void lvPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}
