using Topping.Core.Logic;
using TopMachine.Desktop.Overall;
namespace TopMachine.Topping.Frontend.Controls
{
    partial class RoomControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (timer1 != null)
            {
                timer1.Enabled = false;
                timer1.Dispose();
                timer1 = null; 
            }

            if (state != null) 
            {
                state.Dispose(); 
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomControl));
            this.bindingConfig = new System.Windows.Forms.BindingSource(this.components);
            this.minValue = new System.Windows.Forms.Label();
            this.minutesValue = new System.Windows.Forms.Label();
            this.SecondsValue = new System.Windows.Forms.Label();
            this.maxValue = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlConfig = new System.Windows.Forms.Panel();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnDetonante = new System.Windows.Forms.Button();
            this.btnJoker = new System.Windows.Forms.Button();
            this.btnTopping = new System.Windows.Forms.Button();
            this.pnlSelectConfig = new System.Windows.Forms.Panel();
            this.cbConfigs = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCreateGame = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.roundPanel1 = new TopMachine.Desktop.Controls.RoundPanel();
            this.lstPlayersRoom = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).BeginInit();
            this.pnlConfig.SuspendLayout();
            this.pnlSelectConfig.SuspendLayout();
            this.panel1.SuspendLayout();
            this.roundPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingConfig
            // 
            this.bindingConfig.AllowNew = false;
            this.bindingConfig.DataSource = typeof(TopMachine.Topping.Dto.ConfigGameDto);
            // 
            // minValue
            // 
            this.minValue.BackColor = System.Drawing.Color.DarkGray;
            this.minValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "MinLetters", true));
            this.minValue.ForeColor = System.Drawing.Color.Black;
            this.minValue.Location = new System.Drawing.Point(8, 32);
            this.minValue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.minValue.Name = "minValue";
            this.minValue.Size = new System.Drawing.Size(33, 20);
            this.minValue.TabIndex = 2;
            this.minValue.Text = "00";
            this.minValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // minutesValue
            // 
            this.minutesValue.BackColor = System.Drawing.Color.DarkGray;
            this.minutesValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Minutes", true));
            this.minutesValue.ForeColor = System.Drawing.Color.Black;
            this.minutesValue.Location = new System.Drawing.Point(210, 32);
            this.minutesValue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.minutesValue.Name = "minutesValue";
            this.minutesValue.Size = new System.Drawing.Size(33, 20);
            this.minutesValue.TabIndex = 8;
            this.minutesValue.Text = "00";
            this.minutesValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SecondsValue
            // 
            this.SecondsValue.BackColor = System.Drawing.Color.DarkGray;
            this.SecondsValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Seconds", true));
            this.SecondsValue.ForeColor = System.Drawing.Color.Black;
            this.SecondsValue.Location = new System.Drawing.Point(257, 32);
            this.SecondsValue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SecondsValue.Name = "SecondsValue";
            this.SecondsValue.Size = new System.Drawing.Size(33, 20);
            this.SecondsValue.TabIndex = 9;
            this.SecondsValue.Text = "00";
            this.SecondsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maxValue
            // 
            this.maxValue.BackColor = System.Drawing.Color.DarkGray;
            this.maxValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "MaxLetters", true));
            this.maxValue.ForeColor = System.Drawing.Color.Black;
            this.maxValue.Location = new System.Drawing.Point(60, 32);
            this.maxValue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.maxValue.Name = "maxValue";
            this.maxValue.Size = new System.Drawing.Size(33, 20);
            this.maxValue.TabIndex = 10;
            this.maxValue.Text = "00";
            this.maxValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.DarkGray;
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Name", true));
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(7, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(325, 20);
            this.txtName.TabIndex = 52;
            this.txtName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Language", true));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(299, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 20);
            this.label1.TabIndex = 65;
            this.label1.Text = "00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(41, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 20);
            this.label2.TabIndex = 66;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(241, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 20);
            this.label3.TabIndex = 67;
            this.label3.Text = ":";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.DarkGray;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(345, 42);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(158, 14);
            this.lblStatus.TabIndex = 68;
            this.lblStatus.Tag = "Status : {0}";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlayers
            // 
            this.lblPlayers.BackColor = System.Drawing.Color.DarkGray;
            this.lblPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayers.ForeColor = System.Drawing.Color.Black;
            this.lblPlayers.Location = new System.Drawing.Point(345, 6);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(158, 14);
            this.lblPlayers.TabIndex = 69;
            this.lblPlayers.Tag = "# de joueurs : {0}";
            this.lblPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStart
            // 
            this.lblStart.BackColor = System.Drawing.Color.DarkGray;
            this.lblStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.ForeColor = System.Drawing.Color.Black;
            this.lblStart.Location = new System.Drawing.Point(345, 24);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(158, 14);
            this.lblStart.TabIndex = 70;
            this.lblStart.Tag = "Début dans {0}";
            this.lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnlConfig
            // 
            this.pnlConfig.AutoSize = true;
            this.pnlConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(76)))), ((int)(((byte)(64)))));
            this.pnlConfig.Controls.Add(this.btnManual);
            this.pnlConfig.Controls.Add(this.btnDetonante);
            this.pnlConfig.Controls.Add(this.btnJoker);
            this.pnlConfig.Controls.Add(this.btnTopping);
            this.pnlConfig.Controls.Add(this.lblStatus);
            this.pnlConfig.Controls.Add(this.lblStart);
            this.pnlConfig.Controls.Add(this.lblPlayers);
            this.pnlConfig.Controls.Add(this.minValue);
            this.pnlConfig.Controls.Add(this.txtName);
            this.pnlConfig.Controls.Add(this.minutesValue);
            this.pnlConfig.Controls.Add(this.SecondsValue);
            this.pnlConfig.Controls.Add(this.label1);
            this.pnlConfig.Controls.Add(this.maxValue);
            this.pnlConfig.Controls.Add(this.label2);
            this.pnlConfig.Controls.Add(this.label3);
            this.pnlConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConfig.Location = new System.Drawing.Point(10, 41);
            this.pnlConfig.Name = "pnlConfig";
            this.pnlConfig.Size = new System.Drawing.Size(508, 59);
            this.pnlConfig.TabIndex = 71;
            // 
            // btnManual
            // 
            this.btnManual.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingConfig, "Manual", true));
            this.btnManual.Enabled = false;
            this.btnManual.Image = ((System.Drawing.Image)(resources.GetObject("btnManual.Image")));
            this.btnManual.Location = new System.Drawing.Point(176, 29);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(26, 27);
            this.btnManual.TabIndex = 81;
            this.btnManual.UseVisualStyleBackColor = true;
            // 
            // btnDetonante
            // 
            this.btnDetonante.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingConfig, "Explosive", true));
            this.btnDetonante.Enabled = false;
            this.btnDetonante.Image = ((System.Drawing.Image)(resources.GetObject("btnDetonante.Image")));
            this.btnDetonante.Location = new System.Drawing.Point(150, 29);
            this.btnDetonante.Name = "btnDetonante";
            this.btnDetonante.Size = new System.Drawing.Size(26, 27);
            this.btnDetonante.TabIndex = 80;
            this.btnDetonante.UseVisualStyleBackColor = true;
            // 
            // btnJoker
            // 
            this.btnJoker.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingConfig, "Joker", true));
            this.btnJoker.Enabled = false;
            this.btnJoker.Image = ((System.Drawing.Image)(resources.GetObject("btnJoker.Image")));
            this.btnJoker.Location = new System.Drawing.Point(124, 29);
            this.btnJoker.Name = "btnJoker";
            this.btnJoker.Size = new System.Drawing.Size(26, 27);
            this.btnJoker.TabIndex = 79;
            this.btnJoker.UseVisualStyleBackColor = true;
            // 
            // btnTopping
            // 
            this.btnTopping.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingConfig, "Toping", true));
            this.btnTopping.Enabled = false;
            this.btnTopping.Image = ((System.Drawing.Image)(resources.GetObject("btnTopping.Image")));
            this.btnTopping.Location = new System.Drawing.Point(98, 29);
            this.btnTopping.Name = "btnTopping";
            this.btnTopping.Size = new System.Drawing.Size(26, 27);
            this.btnTopping.TabIndex = 78;
            this.btnTopping.UseVisualStyleBackColor = true;
            // 
            // pnlSelectConfig
            // 
            this.pnlSelectConfig.Controls.Add(this.cbConfigs);
            this.pnlSelectConfig.Controls.Add(this.label4);
            this.pnlSelectConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelectConfig.Location = new System.Drawing.Point(10, 10);
            this.pnlSelectConfig.Name = "pnlSelectConfig";
            this.pnlSelectConfig.Size = new System.Drawing.Size(508, 31);
            this.pnlSelectConfig.TabIndex = 72;
            this.pnlSelectConfig.Visible = false;
            // 
            // cbConfigs
            // 
            this.cbConfigs.DataSource = this.bindingConfig;
            this.cbConfigs.DisplayMember = "Name";
            this.cbConfigs.FormattingEnabled = true;
            this.cbConfigs.Location = new System.Drawing.Point(176, 4);
            this.cbConfigs.Name = "cbConfigs";
            this.cbConfigs.Size = new System.Drawing.Size(181, 21);
            this.cbConfigs.TabIndex = 1;
            this.cbConfigs.ValueMember = "Id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Sélectionnez une configuration ";
            // 
            // btnCreateGame
            // 
            this.btnCreateGame.ForeColor = System.Drawing.Color.Black;
            this.btnCreateGame.Location = new System.Drawing.Point(4, 4);
            this.btnCreateGame.Name = "btnCreateGame";
            this.btnCreateGame.Size = new System.Drawing.Size(133, 23);
            this.btnCreateGame.TabIndex = 2;
            this.btnCreateGame.Text = "Créez la partie";
            this.btnCreateGame.UseVisualStyleBackColor = true;
            this.btnCreateGame.Click += new System.EventHandler(this.btnCreateGame_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnQuit);
            this.panel1.Controls.Add(this.btnCreateGame);
            this.panel1.Controls.Add(this.btnReload);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 31);
            this.panel1.TabIndex = 73;
            // 
            // btnQuit
            // 
            this.btnQuit.ForeColor = System.Drawing.Color.Black;
            this.btnQuit.Location = new System.Drawing.Point(372, 5);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(133, 23);
            this.btnQuit.TabIndex = 4;
            this.btnQuit.Text = "Quitter la partie";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnReload
            // 
            this.btnReload.ForeColor = System.Drawing.Color.Black;
            this.btnReload.Location = new System.Drawing.Point(240, 5);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(133, 23);
            this.btnReload.TabIndex = 5;
            this.btnReload.Text = "Rechargez";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Visible = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.Gray;
            this.roundPanel1.BevelHeight = 8;
            this.roundPanel1.CenterColor = System.Drawing.Color.White;
            this.roundPanel1.Controls.Add(this.lstPlayersRoom);
            this.roundPanel1.Controls.Add(this.panel1);
            this.roundPanel1.Controls.Add(this.pnlConfig);
            this.roundPanel1.Controls.Add(this.pnlSelectConfig);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundPanel1.FocusDrawn = false;
            this.roundPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.OverColor = System.Drawing.Color.Transparent;
            this.roundPanel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.roundPanel1.RecessDepth = 0;
            this.roundPanel1.Round = 20;
            this.roundPanel1.Size = new System.Drawing.Size(528, 361);
            this.roundPanel1.TabIndex = 74;
            this.roundPanel1.Text = null;
            // 
            // lstPlayersRoom
            // 
            this.lstPlayersRoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstPlayersRoom.FormattingEnabled = true;
            this.lstPlayersRoom.Location = new System.Drawing.Point(10, 131);
            this.lstPlayersRoom.Name = "lstPlayersRoom";
            this.lstPlayersRoom.Size = new System.Drawing.Size(162, 220);
            this.lstPlayersRoom.TabIndex = 75;
            this.lstPlayersRoom.Visible = false;
            // 
            // RoomControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.roundPanel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "RoomControl";
            this.Size = new System.Drawing.Size(528, 361);
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).EndInit();
            this.pnlConfig.ResumeLayout(false);
            this.pnlSelectConfig.ResumeLayout(false);
            this.pnlSelectConfig.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingConfig;
        private System.Windows.Forms.Label minValue;
        private System.Windows.Forms.Label minutesValue;
        private System.Windows.Forms.Label SecondsValue;
        private System.Windows.Forms.Label maxValue;
        private System.Windows.Forms.Label txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlConfig;
        private System.Windows.Forms.Panel pnlSelectConfig;
        private System.Windows.Forms.ComboBox cbConfigs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreateGame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnQuit;
        private Desktop.Controls.RoundPanel roundPanel1;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnDetonante;
        private System.Windows.Forms.Button btnJoker;
        private System.Windows.Forms.Button btnTopping;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.ListBox lstPlayersRoom;
    }
}
