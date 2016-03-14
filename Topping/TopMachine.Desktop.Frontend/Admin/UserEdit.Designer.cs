namespace TopMachine.Topping.Frontend.Admin
{
    partial class UserEdit
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
			this.button2 = new System.Windows.Forms.Button();
			this.bindingConfig = new System.Windows.Forms.BindingSource(this.components);
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblNom = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPwd = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).BeginInit();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.button2.Location = new System.Drawing.Point(183, 149);
			this.button2.Margin = new System.Windows.Forms.Padding(2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(114, 31);
			this.button2.TabIndex = 115;
			this.button2.Text = "Effacez";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// bindingConfig
			// 
			this.bindingConfig.AllowNew = false;
			this.bindingConfig.DataSource = typeof(TopMachine.Topping.Dto.UserDto);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCancel.Location = new System.Drawing.Point(301, 150);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(86, 31);
			this.btnCancel.TabIndex = 114;
			this.btnCancel.Text = "Annulez";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblNom
			// 
			this.lblNom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblNom.Location = new System.Drawing.Point(12, 9);
			this.lblNom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblNom.Name = "lblNom";
			this.lblNom.Size = new System.Drawing.Size(86, 15);
			this.lblNom.TabIndex = 109;
			this.lblNom.Text = "Pseudo :";
			this.lblNom.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.button1.Location = new System.Drawing.Point(42, 150);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(139, 31);
			this.button1.TabIndex = 113;
			this.button1.Text = "Sauvegardez";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtName
			// 
			this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "UserName", true));
			this.txtName.Location = new System.Drawing.Point(143, 9);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(262, 20);
			this.txtName.TabIndex = 103;
			// 
			// label1
			// 
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(12, 35);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 15);
			this.label1.TabIndex = 117;
			this.label1.Text = "Nom:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textBox1
			// 
			this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "LastName", true));
			this.textBox1.Location = new System.Drawing.Point(143, 35);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(262, 20);
			this.textBox1.TabIndex = 116;
			// 
			// label2
			// 
			this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label2.Location = new System.Drawing.Point(12, 61);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 15);
			this.label2.TabIndex = 119;
			this.label2.Text = "Prénom :";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textBox2
			// 
			this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "FirstName", true));
			this.textBox2.Location = new System.Drawing.Point(143, 61);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(262, 20);
			this.textBox2.TabIndex = 118;
			// 
			// label3
			// 
			this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label3.Location = new System.Drawing.Point(12, 87);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 15);
			this.label3.TabIndex = 121;
			this.label3.Text = "Email :";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// textBox3
			// 
			this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Email", true));
			this.textBox3.Location = new System.Drawing.Point(143, 87);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(262, 20);
			this.textBox3.TabIndex = 120;
			// 
			// label4
			// 
			this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label4.Location = new System.Drawing.Point(12, 113);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(86, 15);
			this.label4.TabIndex = 123;
			this.label4.Text = "Mot de passe :";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtPwd
			// 
			this.txtPwd.Location = new System.Drawing.Point(143, 113);
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.Size = new System.Drawing.Size(262, 20);
			this.txtPwd.TabIndex = 122;
			this.txtPwd.UseSystemPasswordChar = true;
			// 
			// UserEdit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Silver;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPwd);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblNom);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtName);
			this.Name = "UserEdit";
			this.Size = new System.Drawing.Size(425, 196);
			((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource bindingConfig;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPwd;
    }
}
