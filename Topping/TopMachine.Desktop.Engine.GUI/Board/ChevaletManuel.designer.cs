using System.Windows.Forms;
namespace TopMachine.Topping.Engine.GUI.Board
{
    partial class ChevaletManuel
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
            this.btnManual = new Button();
            this.lblError = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chevalet1 = new Chevalet();
            this.lettresRestantes1 = new LettresRestantes();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnManual
            // 
            this.btnManual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnManual.BackColor = System.Drawing.Color.Teal;

            this.btnManual.Text = "F4 Valider";
            this.btnManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManual.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnManual.Location = new System.Drawing.Point(0, 3);
            this.btnManual.Name = "btnManual";

            this.btnManual.Size = new System.Drawing.Size(608, 24);
            this.btnManual.TabIndex = 69;

            this.btnManual.Click += new System.EventHandler(this.btnManual_Click);
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblError.BackColor = System.Drawing.Color.Silver;
            this.lblError.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.Location = new System.Drawing.Point(4, 91);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(604, 18);
            this.lblError.TabIndex = 71;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnManual);
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.chevalet1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 116);
            this.panel1.TabIndex = 72;
            // 
            // chevalet1
            // 
            this.chevalet1.AllowDrop = true;
            this.chevalet1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chevalet1.BackColor = System.Drawing.Color.Transparent;

            this.chevalet1.CausesValidation = false;
            this.chevalet1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chevalet1.GameConfig = null;

            this.chevalet1.Location = new System.Drawing.Point(8, 31);
            this.chevalet1.Margin = new System.Windows.Forms.Padding(0);
            this.chevalet1.Name = "chevalet1";

            this.chevalet1.Size = new System.Drawing.Size(595, 55);
            this.chevalet1.TabIndex = 70;

            // 
            // lettresRestantes1
            // 
            this.lettresRestantes1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lettresRestantes1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lettresRestantes1.Location = new System.Drawing.Point(0, 116);
            this.lettresRestantes1.Name = "lettresRestantes1";
            this.lettresRestantes1.Size = new System.Drawing.Size(611, 238);
            this.lettresRestantes1.TabIndex = 73;
            this.lettresRestantes1.TabStop = false;
            // 
            // ChevaletManuel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.lettresRestantes1);
            this.Controls.Add(this.panel1);
            this.Name = "ChevaletManuel";
            this.Size = new System.Drawing.Size(611, 354);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnManual;
        private Chevalet chevalet1;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Panel panel1;
        private LettresRestantes lettresRestantes1;
    }
}
