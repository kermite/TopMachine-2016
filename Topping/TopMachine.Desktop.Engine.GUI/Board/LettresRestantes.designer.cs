namespace TopMachine.Topping.Engine.GUI.Board
{
    partial class LettresRestantes
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
            this.lblLettres = new System.Windows.Forms.Label();
            this.pnlLetters = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblLettres
            // 
            this.lblLettres.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLettres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLettres.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblLettres.Location = new System.Drawing.Point(0, 0);
            this.lblLettres.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLettres.Name = "lblLettres";
            this.lblLettres.Size = new System.Drawing.Size(696, 30);
            this.lblLettres.TabIndex = 1;
            this.lblLettres.Text = "Lettres Restantes : ";
            // 
            // pnlLetters
            // 
            this.pnlLetters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLetters.Location = new System.Drawing.Point(0, 30);
            this.pnlLetters.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlLetters.Name = "pnlLetters";
            this.pnlLetters.Size = new System.Drawing.Size(696, 205);
            this.pnlLetters.TabIndex = 2;
            // 
            // LettresRestantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.pnlLetters);
            this.Controls.Add(this.lblLettres);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LettresRestantes";
            this.Size = new System.Drawing.Size(696, 235);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLettres;
        private System.Windows.Forms.Panel pnlLetters;

    }
}
