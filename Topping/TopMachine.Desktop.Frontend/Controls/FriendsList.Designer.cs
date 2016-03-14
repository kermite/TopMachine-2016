using TopMachine.Desktop.Overall;
namespace TopMachine.Topping.Frontend.Controls
{
    partial class FriendsList
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
            this.listFriends = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nouvelAmiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jeLaimePlusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cestQuiLuiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listFriends
            // 
            this.listFriends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listFriends.ContextMenuStrip = this.contextMenuStrip1;
            this.listFriends.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFriends.GridLines = true;
            this.listFriends.Location = new System.Drawing.Point(0, 0);
            this.listFriends.Name = "listFriends";
            this.listFriends.Size = new System.Drawing.Size(150, 150);
            this.listFriends.TabIndex = 1;
            this.listFriends.UseCompatibleStateImageBehavior = false;
            this.listFriends.View = System.Windows.Forms.View.Details;
            this.listFriends.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listFriends_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Joueur";
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
            // FriendsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listFriends);
            this.Name = "FriendsList";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listFriends;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nouvelAmiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jeLaimePlusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cestQuiLuiToolStripMenuItem;
    }
}
