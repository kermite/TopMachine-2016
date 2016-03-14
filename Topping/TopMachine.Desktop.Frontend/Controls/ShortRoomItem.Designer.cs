using Topping.Core.Logic;
namespace TopMachine.Topping.Frontend.Controls
{
    partial class ShortRoomItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShortRoomItem));
            this.minValue = new System.Windows.Forms.Label();
            this.bindingConfig = new System.Windows.Forms.BindingSource(this.components);
            this.minutesValue = new System.Windows.Forms.Label();
            this.SecondsValue = new System.Windows.Forms.Label();
            this.maxValue = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.btnManual = new System.Windows.Forms.Button();
            this.btnDetonante = new System.Windows.Forms.Button();
            this.btnJoker = new System.Windows.Forms.Button();
            this.btnTopping = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // minValue
            // 
            this.minValue.BackColor = System.Drawing.Color.DarkGray;
            this.minValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "MinLetters", true));
            this.minValue.ForeColor = System.Drawing.Color.Black;
            this.minValue.Location = new System.Drawing.Point(9, 29);
            this.minValue.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.minValue.Name = "minValue";
            this.minValue.Size = new System.Drawing.Size(33, 20);
            this.minValue.TabIndex = 2;
            this.minValue.Text = "00";
            this.minValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bindingConfig
            // 
            this.bindingConfig.AllowNew = false;
            this.bindingConfig.DataSource = typeof(TopMachine.Topping.Dto.ConfigGameDto);
            // 
            // minutesValue
            // 
            this.minutesValue.BackColor = System.Drawing.Color.DarkGray;
            this.minutesValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Minutes", true));
            this.minutesValue.ForeColor = System.Drawing.Color.Black;
            this.minutesValue.Location = new System.Drawing.Point(109, 29);
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
            this.SecondsValue.Location = new System.Drawing.Point(156, 29);
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
            this.maxValue.Location = new System.Drawing.Point(61, 29);
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
            this.txtName.Location = new System.Drawing.Point(9, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(317, 20);
            this.txtName.TabIndex = 52;
            this.txtName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRestart
            // 
            this.btnRestart.Enabled = false;
            this.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRestart.ForeColor = System.Drawing.Color.White;
            this.btnRestart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRestart.Location = new System.Drawing.Point(258, 85);
            this.btnRestart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(122, 23);
            this.btnRestart.TabIndex = 64;
            this.btnRestart.Text = "Relancez";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Language", true));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(200, 29);
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
            this.label2.Location = new System.Drawing.Point(42, 29);
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
            this.label3.Location = new System.Drawing.Point(140, 28);
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
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(8, 58);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(241, 23);
            this.lblStatus.TabIndex = 68;
            this.lblStatus.Tag = "Status : {0}";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlayers
            // 
            this.lblPlayers.BackColor = System.Drawing.Color.DarkGray;
            this.lblPlayers.ForeColor = System.Drawing.Color.Black;
            this.lblPlayers.Location = new System.Drawing.Point(259, 58);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(121, 23);
            this.lblPlayers.TabIndex = 69;
            this.lblPlayers.Tag = "# de joueurs : {0}";
            this.lblPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStart
            // 
            this.lblStart.BackColor = System.Drawing.Color.DarkGray;
            this.lblStart.ForeColor = System.Drawing.Color.Black;
            this.lblStart.Location = new System.Drawing.Point(9, 85);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(240, 23);
            this.lblStart.TabIndex = 70;
            this.lblStart.Tag = "Début dans {0}";
            this.lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnManual
            // 
            this.btnManual.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingConfig, "Manual", true));
            this.btnManual.Enabled = false;
            this.btnManual.Image = ((System.Drawing.Image)(resources.GetObject("btnManual.Image")));
            this.btnManual.Location = new System.Drawing.Point(356, 28);
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
            this.btnDetonante.Location = new System.Drawing.Point(330, 28);
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
            this.btnJoker.Location = new System.Drawing.Point(357, 1);
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
            this.btnTopping.Location = new System.Drawing.Point(331, 1);
            this.btnTopping.Name = "btnTopping";
            this.btnTopping.Size = new System.Drawing.Size(26, 27);
            this.btnTopping.TabIndex = 78;
            this.btnTopping.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ShortRoomItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(76)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.btnDetonante);
            this.Controls.Add(this.btnJoker);
            this.Controls.Add(this.btnTopping);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.minValue);
            this.Controls.Add(this.minutesValue);
            this.Controls.Add(this.SecondsValue);
            this.Controls.Add(this.maxValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.txtName);
            this.Name = "ShortRoomItem";
            this.Size = new System.Drawing.Size(447, 118);
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingConfig;
        private System.Windows.Forms.Label minValue;
        private System.Windows.Forms.Label minutesValue;
        private System.Windows.Forms.Label SecondsValue;
        private System.Windows.Forms.Label maxValue;
        private System.Windows.Forms.Label txtName;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Button btnManual;
        private System.Windows.Forms.Button btnDetonante;
        private System.Windows.Forms.Button btnJoker;
        private System.Windows.Forms.Button btnTopping;
        private System.Windows.Forms.Timer timer1;
    }
}
