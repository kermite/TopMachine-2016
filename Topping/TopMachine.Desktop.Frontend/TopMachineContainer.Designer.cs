using TopMachine.Topping.Engine.GUI.Board;
namespace TopMachine.Topping.Frontend
{
    partial class TopMachineContainer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopMachineContainer));
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.menuTopMachine = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.pnlRack = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlScore = new System.Windows.Forms.Panel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabChat = new System.Windows.Forms.TabPage();
            this.chatControl1 = new TopMachine.Topping.Frontend.Chat.ChatControl();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.tabClassement = new System.Windows.Forms.TabPage();
            this.shortRoomItem1 = new TopMachine.Topping.Frontend.Controls.ShortRoomItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlRack.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrid
            // 
            resources.ApplyResources(this.pnlGrid, "pnlGrid");
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Resize += new System.EventHandler(this.pnlGrid_Resize);
            // 
            // menuTopMachine
            // 
            this.menuTopMachine.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.menuItem5,
            this.menuItem4});
            resources.ApplyResources(this.menuItem1, "menuItem1");
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            resources.ApplyResources(this.menuItem3, "menuItem3");
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            resources.ApplyResources(this.menuItem5, "menuItem5");
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            resources.ApplyResources(this.menuItem4, "menuItem4");
            // 
            // pnlRack
            // 
            this.pnlRack.Controls.Add(this.btnStart);
            resources.ApplyResources(this.pnlRack, "pnlRack");
            this.pnlRack.Name = "pnlRack";
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pnlScore
            // 
            resources.ApplyResources(this.pnlScore, "pnlScore");
            this.pnlScore.Name = "pnlScore";
            this.pnlScore.GotFocus += new System.EventHandler(this.pnlScore_GotFocus);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.splitter2);
            this.pnlContainer.Controls.Add(this.pnlGrid);
            this.pnlContainer.Controls.Add(this.pnlRack);
            this.pnlContainer.Controls.Add(this.pnlScore);
            resources.ApplyResources(this.pnlContainer, "pnlContainer");
            this.pnlContainer.Name = "pnlContainer";
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.splitter2, "splitter2");
            this.splitter2.Name = "splitter2";
            this.splitter2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.shortRoomItem1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabChat);
            this.tabControl1.Controls.Add(this.tabHistory);
            this.tabControl1.Controls.Add(this.tabClassement);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            // 
            // tabChat
            // 
            this.tabChat.BackColor = System.Drawing.Color.Gray;
            this.tabChat.Controls.Add(this.chatControl1);
            resources.ApplyResources(this.tabChat, "tabChat");
            this.tabChat.Name = "tabChat";
            // 
            // chatControl1
            // 
            resources.ApplyResources(this.chatControl1, "chatControl1");
            this.chatControl1.Name = "chatControl1";
            // 
            // tabHistory
            // 
            resources.ApplyResources(this.tabHistory, "tabHistory");
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // tabClassement
            // 
            resources.ApplyResources(this.tabClassement, "tabClassement");
            this.tabClassement.Name = "tabClassement";
            this.tabClassement.UseVisualStyleBackColor = true;
            // 
            // shortRoomItem1
            // 
            resources.ApplyResources(this.shortRoomItem1, "shortRoomItem1");
            this.shortRoomItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(76)))), ((int)(((byte)(64)))));
            this.shortRoomItem1.Name = "shortRoomItem1";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // TopMachineContainer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlContainer);
            this.Name = "TopMachineContainer";
            resources.ApplyResources(this, "$this");
            this.pnlRack.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabChat.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.MainMenu menuTopMachine;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.Panel pnlRack;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pnlScore;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TabPage tabClassement;
        private System.Windows.Forms.TabPage tabChat;
        private Chat.ChatControl chatControl1;
        private System.Windows.Forms.Panel panel1;
        private Controls.ShortRoomItem shortRoomItem1;
    }
}
