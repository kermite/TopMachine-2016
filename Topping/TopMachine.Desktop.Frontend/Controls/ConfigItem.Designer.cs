using TopMachine.Desktop.Overall;
namespace TopMachine.Topping.Frontend.Controls
{
    partial class ConfigItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigItem));
            this.minValue = new System.Windows.Forms.Label();
            this.bindingConfig = new System.Windows.Forms.BindingSource(this.components);
            this.minutesValue = new System.Windows.Forms.Label();
            this.SecondsValue = new System.Windows.Forms.Label();
            this.maxValue = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTopping = new System.Windows.Forms.Button();
            this.btnJoker = new System.Windows.Forms.Button();
            this.btnDetonante = new System.Windows.Forms.Button();
            this.btnManual = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // minValue
            // 
            this.minValue.BackColor = System.Drawing.Color.DarkGray;
            this.minValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "MinLetters", true));
            this.minValue.ForeColor = System.Drawing.Color.Black;
            this.minValue.Location = new System.Drawing.Point(8, 31);
            this.minValue.Margin = new System.Windows.Forms.Padding(2);
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
            this.minutesValue.Location = new System.Drawing.Point(212, 31);
            this.minutesValue.Margin = new System.Windows.Forms.Padding(2);
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
            this.SecondsValue.Location = new System.Drawing.Point(261, 31);
            this.SecondsValue.Margin = new System.Windows.Forms.Padding(2);
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
            this.maxValue.Location = new System.Drawing.Point(60, 31);
            this.maxValue.Margin = new System.Windows.Forms.Padding(2);
            this.maxValue.Name = "maxValue";
            this.maxValue.Size = new System.Drawing.Size(33, 20);
            this.maxValue.TabIndex = 10;
            this.maxValue.Text = "00";
            this.maxValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.Silver;
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Name", true));
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(7, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(401, 20);
            this.txtName.TabIndex = 52;
            this.txtName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEdit
            // 
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(412, 27);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(62, 25);
            this.btnEdit.TabIndex = 62;
            this.btnEdit.Text = "Editez";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPlay.ForeColor = System.Drawing.Color.White;
            this.btnPlay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPlay.Location = new System.Drawing.Point(412, 3);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(62, 25);
            this.btnPlay.TabIndex = 64;
            this.btnPlay.Text = "Jouez";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Language", true));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(374, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 20);
            this.label1.TabIndex = 65;
            this.label1.Text = "00";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(43, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 20);
            this.label2.TabIndex = 66;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(245, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 20);
            this.label3.TabIndex = 67;
            this.label3.Text = ":";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 5);
            this.panel1.TabIndex = 69;
            // 
            // btnTopping
            // 
            this.btnTopping.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingConfig, "Toping", true));
            this.btnTopping.Enabled = false;
            this.btnTopping.Image = ((System.Drawing.Image)(resources.GetObject("btnTopping.Image")));
            this.btnTopping.Location = new System.Drawing.Point(99, 28);
            this.btnTopping.Name = "btnTopping";
            this.btnTopping.Size = new System.Drawing.Size(26, 27);
            this.btnTopping.TabIndex = 74;
            this.btnTopping.UseVisualStyleBackColor = true;
            // 
            // btnJoker
            // 
            this.btnJoker.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingConfig, "Joker", true));
            this.btnJoker.Enabled = false;
            this.btnJoker.Image = ((System.Drawing.Image)(resources.GetObject("btnJoker.Image")));
            this.btnJoker.Location = new System.Drawing.Point(125, 28);
            this.btnJoker.Name = "btnJoker";
            this.btnJoker.Size = new System.Drawing.Size(26, 27);
            this.btnJoker.TabIndex = 75;
            this.btnJoker.UseVisualStyleBackColor = true;
            // 
            // btnDetonante
            // 
            this.btnDetonante.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingConfig, "Explosive", true));
            this.btnDetonante.Enabled = false;
            this.btnDetonante.Image = ((System.Drawing.Image)(resources.GetObject("btnDetonante.Image")));
            this.btnDetonante.Location = new System.Drawing.Point(151, 28);
            this.btnDetonante.Name = "btnDetonante";
            this.btnDetonante.Size = new System.Drawing.Size(26, 27);
            this.btnDetonante.TabIndex = 76;
            this.btnDetonante.UseVisualStyleBackColor = true;
            // 
            // btnManual
            // 
            this.btnManual.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.bindingConfig, "Manual", true));
            this.btnManual.Enabled = false;
            this.btnManual.Image = ((System.Drawing.Image)(resources.GetObject("btnManual.Image")));
            this.btnManual.Location = new System.Drawing.Point(177, 28);
            this.btnManual.Name = "btnManual";
            this.btnManual.Size = new System.Drawing.Size(26, 27);
            this.btnManual.TabIndex = 77;
            this.btnManual.UseVisualStyleBackColor = true;
            // 
            // ConfigItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(76)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.btnManual);
            this.Controls.Add(this.btnDetonante);
            this.Controls.Add(this.btnJoker);
            this.Controls.Add(this.btnTopping);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.minValue);
            this.Controls.Add(this.minutesValue);
            this.Controls.Add(this.SecondsValue);
            this.Controls.Add(this.maxValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.txtName);
            this.Name = "ConfigItem";
            this.Size = new System.Drawing.Size(480, 61);
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
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTopping;
        private System.Windows.Forms.Button btnJoker;
        private System.Windows.Forms.Button btnDetonante;
        private System.Windows.Forms.Button btnManual;
    }
}
