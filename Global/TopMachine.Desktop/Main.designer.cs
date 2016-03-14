using TopMachine.Desktop.Controls;
namespace TopMachine.Desktop
{
    partial class TopMachineMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.navigator1 = new TopMachine.Desktop.Navigator();
            this.SuspendLayout();
            // 
            // navigator1
            // 
            this.navigator1.AutoSize = true;
            this.navigator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.navigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigator1.Location = new System.Drawing.Point(0, 0);
            this.navigator1.Margin = new System.Windows.Forms.Padding(2);
            this.navigator1.Name = "navigator1";
            this.navigator1.Size = new System.Drawing.Size(668, 505);
            this.navigator1.TabIndex = 0;
            // 
            // TopMachineMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(668, 505);
            this.Controls.Add(this.navigator1);
            this.KeyPreview = true;
            this.Name = "TopMachineMain";
            this.Text = "TopMachine.Desktop 6.0";
            this.Activated += new System.EventHandler(this.TopMachineMain_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TopMachineMain_FormClosed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TopMachineMain_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private Navigator navigator1;

    }
}