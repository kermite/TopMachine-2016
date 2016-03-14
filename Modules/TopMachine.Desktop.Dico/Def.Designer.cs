namespace TopMachine.Desktop.Dico
{
    partial class Def
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Def));
            this.lblWord = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblGenre = new System.Windows.Forms.Label();
            this.lblDef = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWord
            // 
            resources.ApplyResources(this.lblWord, "lblWord");
            this.lblWord.Name = "lblWord";
            this.lblWord.Click += new System.EventHandler(this.Def_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.lblWord);
            this.panel1.Controls.Add(this.lblGenre);
            this.panel1.Name = "panel1";
            this.panel1.Click += new System.EventHandler(this.Def_Click);
            // 
            // lblGenre
            // 
            resources.ApplyResources(this.lblGenre, "lblGenre");
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Click += new System.EventHandler(this.Def_Click);
            // 
            // lblDef
            // 
            resources.ApplyResources(this.lblDef, "lblDef");
            this.lblDef.BackColor = System.Drawing.Color.DarkGray;
            this.lblDef.ForeColor = System.Drawing.Color.Black;
            this.lblDef.Name = "lblDef";
            this.lblDef.Click += new System.EventHandler(this.Def_Click);
            // 
            // Def
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.lblDef);
            this.Controls.Add(this.panel1);
            resources.ApplyResources(this, "$this");
            this.Name = "Def";
            this.Click += new System.EventHandler(this.Def_Click);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Label lblDef;
    }
}
