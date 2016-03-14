using TopMachine.Desktop.Overall;
namespace TopMachine.Topping.Frontend.Controls
{
    partial class ListPlayers
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
            this.components = new System.ComponentModel.Container();
            this.listViewPlayers = new System.Windows.Forms.ListView();
            this.colPseudo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nouvelAmiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jeLaimePlusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cestQuiLuiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewPlayers
            // 
            this.listViewPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPseudo});
            this.listViewPlayers.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPlayers.GridLines = true;
            this.listViewPlayers.Location = new System.Drawing.Point(0, 0);
            this.listViewPlayers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listViewPlayers.MultiSelect = false;
            this.listViewPlayers.Name = "listViewPlayers";
            this.listViewPlayers.Size = new System.Drawing.Size(279, 321);
            this.listViewPlayers.TabIndex = 0;
            this.listViewPlayers.TabStop = false;
            this.listViewPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewPlayers.View = System.Windows.Forms.View.Details;
            this.listViewPlayers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewPlayers_MouseClick);
            // 
            // colPseudo
            // 
            this.colPseudo.Text = "Joueur";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouvelAmiToolStripMenuItem,
            this.jeLaimePlusToolStripMenuItem,
            this.cestQuiLuiToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(233, 70);
            // 
            // nouvelAmiToolStripMenuItem
            // 
            this.nouvelAmiToolStripMenuItem.Name = "nouvelAmiToolStripMenuItem";
            this.nouvelAmiToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.nouvelAmiToolStripMenuItem.Text = "J\'ai un nouvel ami !";
            this.nouvelAmiToolStripMenuItem.Click += new System.EventHandler(this.nouvelAmiToolStripMenuItem_Click);
            // 
            // jeLaimePlusToolStripMenuItem
            // 
            this.jeLaimePlusToolStripMenuItem.Name = "jeLaimePlusToolStripMenuItem";
            this.jeLaimePlusToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.jeLaimePlusToolStripMenuItem.Text = "Je l\'aime plus ! C\'est un #{[#{#";
            this.jeLaimePlusToolStripMenuItem.Click += new System.EventHandler(this.jeLaimePlusToolStripMenuItem_Click);
            // 
            // cestQuiLuiToolStripMenuItem
            // 
            this.cestQuiLuiToolStripMenuItem.Enabled = false;
            this.cestQuiLuiToolStripMenuItem.Name = "cestQuiLuiToolStripMenuItem";
            this.cestQuiLuiToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.cestQuiLuiToolStripMenuItem.Text = "Mais t\'es qui toi ?";
            // 
            // ListPlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewPlayers);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ListPlayers";
            this.Size = new System.Drawing.Size(279, 321);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewPlayers;
        private System.Windows.Forms.ColumnHeader colPseudo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nouvelAmiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jeLaimePlusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cestQuiLuiToolStripMenuItem;
    }
}
