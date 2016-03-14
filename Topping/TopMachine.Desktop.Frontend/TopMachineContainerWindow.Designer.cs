namespace TopMachine.Topping.Frontend
{
    partial class TopMachineContainerWindow
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
            if (topmachineContainer != null)
            {
                topmachineContainer.Stop();
                topmachineContainer.Dispose();
                topmachineContainer = null;

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
            this.SuspendLayout();
            // 
            // TopMachineContainerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1037, 709);
            this.KeyPreview = true;
            this.Name = "TopMachineContainerWindow";
            this.Text = "TopMachine 1.0 = Partie en cours...";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TopMachineContainerWindow_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

    }
}