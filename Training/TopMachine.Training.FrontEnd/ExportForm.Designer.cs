namespace TopMachine.Training.FrontEnd
{
    partial class ExportForm
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
            this.cbList = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.groupImportSource = new System.Windows.Forms.GroupBox();
            this.pnlLists = new System.Windows.Forms.Panel();
            this.btnadd = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnClose = new TopMachine.Desktop.Controls.ImageButton();
            this.btnExport = new TopMachine.Desktop.Controls.ImageButton();
            this.groupImportSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbList
            // 
            this.cbList.DisplayMember = "Display";
            this.cbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbList.Location = new System.Drawing.Point(6, 19);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(215, 21);
            this.cbList.TabIndex = 53;
            this.cbList.ValueMember = "Value";
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(6, 26);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(92, 16);
            this.lblType.TabIndex = 52;
            this.lblType.Text = "Listes";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupImportSource
            // 
            this.groupImportSource.BackColor = System.Drawing.Color.DarkGray;
            this.groupImportSource.Controls.Add(this.pnlLists);
            this.groupImportSource.Controls.Add(this.btnadd);
            this.groupImportSource.Controls.Add(this.cbList);
            this.groupImportSource.Controls.Add(this.lblType);
            this.groupImportSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupImportSource.Location = new System.Drawing.Point(9, 8);
            this.groupImportSource.Name = "groupImportSource";
            this.groupImportSource.Size = new System.Drawing.Size(983, 316);
            this.groupImportSource.TabIndex = 50;
            this.groupImportSource.TabStop = false;
            this.groupImportSource.Tag = "EXPORT";
            this.groupImportSource.Text = "Exporter vos listes";
            // 
            // pnlLists
            // 
            this.pnlLists.AutoScroll = true;
            this.pnlLists.Location = new System.Drawing.Point(9, 46);
            this.pnlLists.Name = "pnlLists";
            this.pnlLists.Size = new System.Drawing.Size(885, 264);
            this.pnlLists.TabIndex = 55;
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(242, 19);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(75, 23);
            this.btnadd.TabIndex = 54;
            this.btnadd.Text = "ajouter";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.AddExtension = false;
            this.saveFileDialog1.FileName = "TopMachineExport";
            this.saveFileDialog1.Title = "choisir le chemin du fichier d\'export";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(200)))), ((int)(((byte)(128)))), ((int)(((byte)(68)))));
            this.btnClose.CenterColor = System.Drawing.Color.White;
            this.btnClose.FocusDrawn = false;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(500, 330);
            this.btnClose.Name = "btnClose";
            this.btnClose.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.RecessDepth = 0;
            this.btnClose.Round = 10;
            this.btnClose.Size = new System.Drawing.Size(178, 29);
            this.btnClose.TabIndex = 53;
            this.btnClose.Text = "Fermez";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.btnExport.CenterColor = System.Drawing.Color.White;
            this.btnExport.FocusDrawn = false;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Location = new System.Drawing.Point(9, 330);
            this.btnExport.Name = "btnExport";
            this.btnExport.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExport.RecessDepth = 0;
            this.btnExport.Round = 10;
            this.btnExport.Size = new System.Drawing.Size(178, 29);
            this.btnExport.TabIndex = 52;
            this.btnExport.Text = "Export";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupImportSource);
            this.Name = "ExportForm";
            this.Size = new System.Drawing.Size(912, 374);
            this.groupImportSource.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbList;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.GroupBox groupImportSource;
        private Desktop.Controls.ImageButton btnExport;
        private Desktop.Controls.ImageButton btnClose;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.Panel pnlLists;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
