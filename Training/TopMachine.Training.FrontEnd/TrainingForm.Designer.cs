
namespace TopMachine.Training.FrontEnd
{
    partial class TrainingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingForm));
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MenuTraining = new System.Windows.Forms.ToolStrip();
            this.btnNavigation = new System.Windows.Forms.ToolStripButton();
            this.btnConfig = new System.Windows.Forms.ToolStripButton();
            this.btnGames = new System.Windows.Forms.ToolStripButton();
            this.btnQuit = new System.Windows.Forms.ToolStripButton();
            this.btnCollapse = new TopMachine.Desktop.Controls.ImageButton();
            this.trainingSelection1 = new TopMachine.Training.Frontend.TrainingSelection();
            this.pnlContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.MenuTraining.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.trainingSelection1);
            resources.ApplyResources(this.pnlContainer, "pnlContainer");
            this.pnlContainer.Name = "pnlContainer";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.MenuTraining);
            this.panel1.Controls.Add(this.btnCollapse);
            this.panel1.Name = "panel1";
            // 
            // MenuTraining
            // 
            resources.ApplyResources(this.MenuTraining, "MenuTraining");
            this.MenuTraining.BackColor = System.Drawing.Color.OrangeRed;
            this.MenuTraining.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MenuTraining.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNavigation,
            this.btnConfig,
            this.btnGames,
            this.btnQuit});
            this.MenuTraining.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.MenuTraining.Name = "MenuTraining";
            // 
            // btnNavigation
            // 
            this.btnNavigation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNavigation.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnNavigation, "btnNavigation");
            this.btnNavigation.Name = "btnNavigation";
            this.btnNavigation.Click += new System.EventHandler(this.btnNavigation_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnConfig.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnConfig, "btnConfig");
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnGames
            // 
            this.btnGames.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGames.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnGames, "btnGames");
            this.btnGames.Name = "btnGames";
            this.btnGames.Click += new System.EventHandler(this.btnBackToGame_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnQuit.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnQuit, "btnQuit");
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnCollapse
            // 
            resources.ApplyResources(this.btnCollapse, "btnCollapse");
            this.btnCollapse.CenterColor = System.Drawing.Color.White;
            this.btnCollapse.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnCollapse.FlatAppearance.BorderSize = 0;
            this.btnCollapse.FocusDrawn = false;
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.OverColor = System.Drawing.Color.Transparent;
            this.btnCollapse.RecessDepth = 0;
            this.btnCollapse.Round = 0;
            this.btnCollapse.UseVisualStyleBackColor = true;
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // trainingSelection1
            // 
            resources.ApplyResources(this.trainingSelection1, "trainingSelection1");
            this.trainingSelection1.Name = "trainingSelection1";
            this.trainingSelection1.ItemSelected += new TopMachine.Training.Frontend.TrainingSelection.ItemSelectedEvent(this.trainingSelection1_ItemSelected);
            // 
            // TrainingForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.panel1);
            resources.ApplyResources(this, "$this");
            this.Name = "TrainingForm";
            this.pnlContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.MenuTraining.ResumeLayout(false);
            this.MenuTraining.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip MenuTraining;
        private TopMachine.Desktop.Controls.ImageButton btnCollapse;
        private System.Windows.Forms.ToolStripButton btnNavigation;
        private System.Windows.Forms.ToolStripButton btnQuit;
        private Frontend.TrainingSelection trainingSelection1;
        private System.Windows.Forms.ToolStripButton btnGames;
        private System.Windows.Forms.ToolStripButton btnConfig;

    }
}
