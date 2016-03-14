using TopMachine.Desktop.Overall;
namespace TopMachine.Topping.Frontend.Controls
{
    partial class Login
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

            OnLoginRequest = null;
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
			this.txtPseudo = new System.Windows.Forms.TextBox();
			this.lblNom = new System.Windows.Forms.Label();
			this.txtMotDePasse = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.BtnConnect = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtPseudo
			// 
			this.txtPseudo.Location = new System.Drawing.Point(104, 6);
			this.txtPseudo.Name = "txtPseudo";
			this.txtPseudo.Size = new System.Drawing.Size(120, 20);
			this.txtPseudo.TabIndex = 41;
			// 
			// lblNom
			// 
			this.lblNom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblNom.Location = new System.Drawing.Point(13, 11);
			this.lblNom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblNom.Name = "lblNom";
			this.lblNom.Size = new System.Drawing.Size(86, 15);
			this.lblNom.TabIndex = 42;
			this.lblNom.Text = "Pseudo";
			this.lblNom.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// txtMotDePasse
			// 
			this.txtMotDePasse.Location = new System.Drawing.Point(104, 34);
			this.txtMotDePasse.Name = "txtMotDePasse";
			this.txtMotDePasse.Size = new System.Drawing.Size(120, 20);
			this.txtMotDePasse.TabIndex = 43;
			this.txtMotDePasse.UseSystemPasswordChar = true;
			// 
			// label1
			// 
			this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.label1.Location = new System.Drawing.Point(13, 39);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 15);
			this.label1.TabIndex = 44;
			this.label1.Text = "Mot de passe";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// BtnConnect
			// 
			this.BtnConnect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.BtnConnect.Location = new System.Drawing.Point(5, 62);
			this.BtnConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.BtnConnect.Name = "BtnConnect";
			this.BtnConnect.Size = new System.Drawing.Size(122, 31);
			this.BtnConnect.TabIndex = 101;
			this.BtnConnect.Text = "Connectez-vous";
			this.BtnConnect.UseVisualStyleBackColor = false;
			this.BtnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.btnCancel.Location = new System.Drawing.Point(131, 62);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 31);
			this.btnCancel.TabIndex = 102;
			this.btnCancel.Text = "Annulez";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// Login
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Silver;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.BtnConnect);
			this.Controls.Add(this.txtMotDePasse);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPseudo);
			this.Controls.Add(this.lblNom);
			this.Name = "Login";
			this.Size = new System.Drawing.Size(236, 101);
			this.Load += new System.EventHandler(this.Login_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPseudo;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtMotDePasse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button btnCancel;
    }
}
