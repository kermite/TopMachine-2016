namespace TopMachine.Training.FrontEnd
{
    partial class ImportForm
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
            this.cbExtension = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.groupDestination = new System.Windows.Forms.GroupBox();
            this.txtGameName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cbList = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupImportSource = new System.Windows.Forms.GroupBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnFile = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.chkLstCategorie = new System.Windows.Forms.CheckedListBox();
            this.criteriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileImport = new System.Windows.Forms.OpenFileDialog();
            this.btnClose = new TopMachine.Desktop.Controls.ImageButton();
            this.btnSave = new TopMachine.Desktop.Controls.ImageButton();
            this.bindingConfig = new System.Windows.Forms.BindingSource(this.components);
            this.bindingParams = new System.Windows.Forms.BindingSource(this.components);
            this.groupDestination.SuspendLayout();
            this.groupImportSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingParams)).BeginInit();
            this.SuspendLayout();
            // 
            // cbExtension
            // 
            this.cbExtension.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingConfig, "Dico", true));
            this.cbExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExtension.Location = new System.Drawing.Point(8, 46);
            this.cbExtension.Name = "cbExtension";
            this.cbExtension.Size = new System.Drawing.Size(87, 21);
            this.cbExtension.TabIndex = 53;
            this.cbExtension.SelectedIndexChanged += new System.EventHandler(this.cbExtension_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(6, 26);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(92, 16);
            this.lblType.TabIndex = 52;
            this.lblType.Text = "Type de fichier :";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupDestination
            // 
            this.groupDestination.BackColor = System.Drawing.Color.White;
            this.groupDestination.Controls.Add(this.txtGameName);
            this.groupDestination.Controls.Add(this.lblName);
            this.groupDestination.Controls.Add(this.cbList);
            this.groupDestination.Controls.Add(this.label8);
            this.groupDestination.Location = new System.Drawing.Point(391, 10);
            this.groupDestination.Name = "groupDestination";
            this.groupDestination.Size = new System.Drawing.Size(279, 290);
            this.groupDestination.TabIndex = 51;
            this.groupDestination.TabStop = false;
            this.groupDestination.Text = "Destination";
            // 
            // txtGameName
            // 
            this.txtGameName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Name", true));
            this.txtGameName.Location = new System.Drawing.Point(74, 74);
            this.txtGameName.Name = "txtGameName";
            this.txtGameName.Size = new System.Drawing.Size(144, 20);
            this.txtGameName.TabIndex = 43;
            this.txtGameName.Text = "Nouvelle Liste";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(6, 74);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(62, 16);
            this.lblName.TabIndex = 42;
            this.lblName.Text = "Nom :";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbList
            // 
            this.cbList.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingParams, "TypeOfPlay", true));
            this.cbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbList.Location = new System.Drawing.Point(6, 35);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(251, 21);
            this.cbList.TabIndex = 94;
            this.cbList.SelectedIndexChanged += new System.EventHandler(this.cbList_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 16);
            this.label8.TabIndex = 95;
            this.label8.Text = "Nom de la liste :";
            // 
            // groupImportSource
            // 
            this.groupImportSource.BackColor = System.Drawing.Color.White;
            this.groupImportSource.Controls.Add(this.txtFileName);
            this.groupImportSource.Controls.Add(this.btnFile);
            this.groupImportSource.Controls.Add(this.lblCategory);
            this.groupImportSource.Controls.Add(this.chkLstCategorie);
            this.groupImportSource.Controls.Add(this.cbExtension);
            this.groupImportSource.Controls.Add(this.lblType);
            this.groupImportSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupImportSource.Location = new System.Drawing.Point(9, 8);
            this.groupImportSource.Name = "groupImportSource";
            this.groupImportSource.Size = new System.Drawing.Size(262, 292);
            this.groupImportSource.TabIndex = 50;
            this.groupImportSource.TabStop = false;
            this.groupImportSource.Text = "Source à importer";
            // 
            // txtFileName
            // 
            this.txtFileName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Name", true));
            this.txtFileName.Location = new System.Drawing.Point(85, 75);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(171, 20);
            this.txtFileName.TabIndex = 57;
            // 
            // btnFile
            // 
            this.btnFile.BackColor = System.Drawing.Color.Silver;
            this.btnFile.Location = new System.Drawing.Point(8, 75);
            this.btnFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(72, 21);
            this.btnFile.TabIndex = 56;
            this.btnFile.Text = "liste";
            this.btnFile.UseVisualStyleBackColor = false;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(3, 147);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(92, 16);
            this.lblCategory.TabIndex = 55;
            this.lblCategory.Text = "Catégories : ";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkLstCategorie
            // 
            this.chkLstCategorie.CheckOnClick = true;
            this.chkLstCategorie.FormattingEnabled = true;
            this.chkLstCategorie.Location = new System.Drawing.Point(5, 166);
            this.chkLstCategorie.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkLstCategorie.Name = "chkLstCategorie";
            this.chkLstCategorie.Size = new System.Drawing.Size(138, 64);
            this.chkLstCategorie.TabIndex = 54;
            // 
            // criteriaBindingSource
            // 
            this.criteriaBindingSource.DataMember = "Criteria";
            this.criteriaBindingSource.DataSource = this.bindingConfig;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(200)))), ((int)(((byte)(128)))), ((int)(((byte)(68)))));
            this.btnClose.CenterColor = System.Drawing.Color.White;
            this.btnClose.FocusDrawn = false;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(492, 306);
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
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.btnSave.CenterColor = System.Drawing.Color.White;
            this.btnSave.FocusDrawn = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(9, 306);
            this.btnSave.Name = "btnSave";
            this.btnSave.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.RecessDepth = 0;
            this.btnSave.Round = 10;
            this.btnSave.Size = new System.Drawing.Size(178, 29);
            this.btnSave.TabIndex = 52;
            this.btnSave.Text = "Importer";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // bindingConfig
            // 
            this.bindingConfig.DataSource = typeof(TopMachine.Training.DAL.fdbo.ListConfig);
            // 
            // bindingParams
            // 
            this.bindingParams.DataSource = typeof(TopMachine.Training.DAL.fdbo.GameConfig);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupDestination);
            this.Controls.Add(this.groupImportSource);
            this.Name = "ImportForm";
            this.Size = new System.Drawing.Size(680, 374);
            this.groupDestination.ResumeLayout(false);
            this.groupDestination.PerformLayout();
            this.groupImportSource.ResumeLayout(false);
            this.groupImportSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingParams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbExtension;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.GroupBox groupDestination;
        private System.Windows.Forms.ComboBox cbList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupImportSource;
        private System.Windows.Forms.TextBox txtGameName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.BindingSource bindingConfig;
        private Desktop.Controls.ImageButton btnSave;
        private System.Windows.Forms.BindingSource bindingParams;
        private System.Windows.Forms.BindingSource criteriaBindingSource;
        private Desktop.Controls.ImageButton btnClose;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.CheckedListBox chkLstCategorie;
        private System.Windows.Forms.OpenFileDialog openFileImport;
    }
}
