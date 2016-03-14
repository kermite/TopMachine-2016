using TopMachine.Desktop.Controls;
using Topping.Core.Proxy.Local.Client;
namespace TopMachine.Topping.Frontend
{
    partial class TopMachineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopMachineForm));
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.toppingSelection1 = new TopMachine.Topping.Frontend.ToppingSelection();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNavigation = new System.Windows.Forms.ToolStripButton();
            this.btnConfig = new System.Windows.Forms.ToolStripButton();
            this.btnBackToGame = new System.Windows.Forms.ToolStripButton();
            this.btnQuit = new System.Windows.Forms.ToolStripButton();
            this.btnCollapse = new TopMachine.Desktop.Controls.ImageButton();
            this.pnlContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.toppingSelection1);
            resources.ApplyResources(this.pnlContainer, "pnlContainer");
            this.pnlContainer.Name = "pnlContainer";
            // 
            // toppingSelection1
            // 
            resources.ApplyResources(this.toppingSelection1, "toppingSelection1");
            this.toppingSelection1.Name = "toppingSelection1";
            this.toppingSelection1.ItemSelected += new TopMachine.Topping.Frontend.ToppingSelection.ItemSelectedEvent(this.toppingSelection1_ItemSelected);
            this.toppingSelection1.Load += new System.EventHandler(this.toppingSelection1_Load);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.btnCollapse);
            this.panel1.Name = "panel1";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackColor = System.Drawing.Color.OrangeRed;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNavigation,
            this.btnConfig,
            this.btnBackToGame,
            this.btnQuit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnNavigation
            // 
            this.btnNavigation.BackColor = System.Drawing.Color.OrangeRed;
            this.btnNavigation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNavigation.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnNavigation, "btnNavigation");
            this.btnNavigation.Name = "btnNavigation";
            this.btnNavigation.Click += new System.EventHandler(this.btnNavigation_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.BackColor = System.Drawing.Color.OrangeRed;
            this.btnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnConfig.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnConfig, "btnConfig");
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnBackToGame
            // 
            this.btnBackToGame.BackColor = System.Drawing.Color.OrangeRed;
            this.btnBackToGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBackToGame.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnBackToGame, "btnBackToGame");
            this.btnBackToGame.Name = "btnBackToGame";
            this.btnBackToGame.Click += new System.EventHandler(this.btnBackToGame_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.BackColor = System.Drawing.Color.OrangeRed;
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
            // TopMachineForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.panel1);
            resources.ApplyResources(this, "$this");
            this.Name = "TopMachineForm";
            this.pnlContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private ImageButton btnCollapse;
        private ToppingSelection toppingSelection1;
        private System.Windows.Forms.ToolStripButton btnNavigation;
        private System.Windows.Forms.ToolStripButton btnConfig;
        private System.Windows.Forms.ToolStripButton btnBackToGame;
        private System.Windows.Forms.ToolStripButton btnQuit;
    }
}
