namespace TopMachine.Topping.Engine.GUI.Board
{
    partial class RoundControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoundControl));
            this.lblRound = new System.Windows.Forms.Label();
            this.lblWordCurrent = new System.Windows.Forms.Label();
            this.lblChronoLeft = new System.Windows.Forms.Label();
            this.lblChronoAll = new System.Windows.Forms.Label();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblWordFinal = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRound
            // 
            resources.ApplyResources(this.lblRound, "lblRound");
            this.lblRound.ForeColor = System.Drawing.Color.White;
            this.lblRound.Name = "lblRound";
            // 
            // lblWordCurrent
            // 
            resources.ApplyResources(this.lblWordCurrent, "lblWordCurrent");
            this.lblWordCurrent.BackColor = System.Drawing.Color.Chocolate;
            this.lblWordCurrent.ForeColor = System.Drawing.Color.White;
            this.lblWordCurrent.Name = "lblWordCurrent";
            // 
            // lblChronoLeft
            // 
            this.lblChronoLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.lblChronoLeft, "lblChronoLeft");
            this.lblChronoLeft.ForeColor = System.Drawing.Color.White;
            this.lblChronoLeft.Name = "lblChronoLeft";
            // 
            // lblChronoAll
            // 
            resources.ApplyResources(this.lblChronoAll, "lblChronoAll");
            this.lblChronoAll.ForeColor = System.Drawing.Color.White;
            this.lblChronoAll.Name = "lblChronoAll";
            // 
            // lblPercentage
            // 
            resources.ApplyResources(this.lblPercentage, "lblPercentage");
            this.lblPercentage.ForeColor = System.Drawing.Color.White;
            this.lblPercentage.Name = "lblPercentage";
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Name = "lblTotal";
            // 
            // lblWordFinal
            // 
            resources.ApplyResources(this.lblWordFinal, "lblWordFinal");
            this.lblWordFinal.BackColor = System.Drawing.Color.YellowGreen;
            this.lblWordFinal.ForeColor = System.Drawing.Color.Black;
            this.lblWordFinal.Name = "lblWordFinal";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.lblRound);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.lblPercentage);
            this.panel1.Controls.Add(this.lblChronoLeft);
            this.panel1.Controls.Add(this.lblChronoAll);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // RoundControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblWordCurrent);
            this.Controls.Add(this.lblWordFinal);
            resources.ApplyResources(this, "$this");
            this.Name = "RoundControl";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRound;
        private System.Windows.Forms.Label lblWordCurrent;
        private System.Windows.Forms.Label lblChronoLeft;
        private System.Windows.Forms.Label lblChronoAll;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblWordFinal;
        private System.Windows.Forms.Panel panel1;

    }
}
