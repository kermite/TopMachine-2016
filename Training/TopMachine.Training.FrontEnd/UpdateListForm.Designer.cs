namespace TopMachine.Training.FrontEnd
{
    partial class UpdateList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupDestination = new System.Windows.Forms.GroupBox();
            this.GVWords = new System.Windows.Forms.DataGridView();
            this.Mot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Trouve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Perdu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tirage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtWordRack = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cbList = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.criteriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileImport = new System.Windows.Forms.OpenFileDialog();
            this.BtnDel = new TopMachine.Desktop.Controls.ImageButton();
            this.BtnAdd = new TopMachine.Desktop.Controls.ImageButton();
            this.btnClose = new TopMachine.Desktop.Controls.ImageButton();
            this.btnDisplayList = new TopMachine.Desktop.Controls.ImageButton();
            this.btnSearchRack = new TopMachine.Desktop.Controls.ImageButton();
            this.BtnSearchWord = new TopMachine.Desktop.Controls.ImageButton();
            this.bindingConfig = new System.Windows.Forms.BindingSource(this.components);
            this.bindingParams = new System.Windows.Forms.BindingSource(this.components);
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnUpdate = new TopMachine.Desktop.Controls.ImageButton();
            this.groupDestination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVWords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingParams)).BeginInit();
            this.SuspendLayout();
            // 
            // groupDestination
            // 
            this.groupDestination.BackColor = System.Drawing.Color.DarkGray;
            this.groupDestination.Controls.Add(this.lblType);
            this.groupDestination.Controls.Add(this.cbCategory);
            this.groupDestination.Controls.Add(this.btnDisplayList);
            this.groupDestination.Controls.Add(this.btnSearchRack);
            this.groupDestination.Controls.Add(this.GVWords);
            this.groupDestination.Controls.Add(this.BtnSearchWord);
            this.groupDestination.Controls.Add(this.txtWordRack);
            this.groupDestination.Controls.Add(this.lblName);
            this.groupDestination.Controls.Add(this.cbList);
            this.groupDestination.Controls.Add(this.label8);
            this.groupDestination.Location = new System.Drawing.Point(21, 10);
            this.groupDestination.Name = "groupDestination";
            this.groupDestination.Size = new System.Drawing.Size(659, 290);
            this.groupDestination.TabIndex = 51;
            this.groupDestination.TabStop = false;
            this.groupDestination.Text = "Destination";
            // 
            // GVWords
            // 
            this.GVWords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVWords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GVWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVWords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mot,
            this.Trouve,
            this.Perdu,
            this.Total,
            this.Tirage,
            this.Type});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GVWords.DefaultCellStyle = dataGridViewCellStyle2;
            this.GVWords.Location = new System.Drawing.Point(9, 146);
            this.GVWords.Name = "GVWords";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVWords.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GVWords.Size = new System.Drawing.Size(644, 138);
            this.GVWords.TabIndex = 96;
            // 
            // Mot
            // 
            this.Mot.HeaderText = "Mot";
            this.Mot.Name = "Mot";
            // 
            // Trouve
            // 
            this.Trouve.HeaderText = "Trouvé";
            this.Trouve.Name = "Trouve";
            // 
            // Perdu
            // 
            this.Perdu.HeaderText = "Perdu";
            this.Perdu.Name = "Perdu";
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            // 
            // Tirage
            // 
            this.Tirage.HeaderText = "Tirage";
            this.Tirage.Name = "Tirage";
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            // 
            // txtWordRack
            // 
            this.txtWordRack.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Name", true));
            this.txtWordRack.Location = new System.Drawing.Point(135, 70);
            this.txtWordRack.Name = "txtWordRack";
            this.txtWordRack.Size = new System.Drawing.Size(144, 20);
            this.txtWordRack.TabIndex = 43;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(6, 70);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(80, 20);
            this.lblName.TabIndex = 42;
            this.lblName.Text = "Mot /Tirage :";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbList
            // 
            this.cbList.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingParams, "TypeOfPlay", true));
            this.cbList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbList.Location = new System.Drawing.Point(135, 34);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(251, 21);
            this.cbList.TabIndex = 94;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 16);
            this.label8.TabIndex = 95;
            this.label8.Text = "Nom de la liste :";
            // 
            // criteriaBindingSource
            // 
            this.criteriaBindingSource.DataMember = "Criteria";
            this.criteriaBindingSource.DataSource = this.bindingConfig;
            // 
            // BtnDel
            // 
            this.BtnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnDel.CenterColor = System.Drawing.Color.White;
            this.BtnDel.Enabled = false;
            this.BtnDel.FocusDrawn = false;
            this.BtnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.BtnDel.ForeColor = System.Drawing.Color.Yellow;
            this.BtnDel.Location = new System.Drawing.Point(163, 309);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnDel.RecessDepth = 0;
            this.BtnDel.Round = 10;
            this.BtnDel.Size = new System.Drawing.Size(130, 26);
            this.BtnDel.TabIndex = 59;
            this.BtnDel.Text = "Suppression";
            this.BtnDel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.BtnDel.UseVisualStyleBackColor = false;
            this.BtnDel.Click += new System.EventHandler(this.BtnDelWord_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.BtnAdd.CenterColor = System.Drawing.Color.White;
            this.BtnAdd.Enabled = false;
            this.BtnAdd.FocusDrawn = false;
            this.BtnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.BtnAdd.ForeColor = System.Drawing.Color.Black;
            this.BtnAdd.Location = new System.Drawing.Point(30, 309);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnAdd.RecessDepth = 0;
            this.BtnAdd.Round = 10;
            this.BtnAdd.Size = new System.Drawing.Size(130, 26);
            this.BtnAdd.TabIndex = 58;
            this.BtnAdd.Text = "Ajout";
            this.BtnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAddWord_Click);
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
            this.btnClose.Text = "Fermer";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDisplayList
            // 
            this.btnDisplayList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDisplayList.CenterColor = System.Drawing.Color.White;
            this.btnDisplayList.FocusDrawn = false;
            this.btnDisplayList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplayList.ForeColor = System.Drawing.Color.Black;
            this.btnDisplayList.Location = new System.Drawing.Point(409, 111);
            this.btnDisplayList.Name = "btnDisplayList";
            this.btnDisplayList.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDisplayList.RecessDepth = 0;
            this.btnDisplayList.Round = 10;
            this.btnDisplayList.Size = new System.Drawing.Size(178, 29);
            this.btnDisplayList.TabIndex = 98;
            this.btnDisplayList.Text = "Afficher la liste";
            this.btnDisplayList.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnDisplayList.UseVisualStyleBackColor = false;
            this.btnDisplayList.Click += new System.EventHandler(this.btnDisplayList_Click);
            // 
            // btnSearchRack
            // 
            this.btnSearchRack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSearchRack.CenterColor = System.Drawing.Color.White;
            this.btnSearchRack.FocusDrawn = false;
            this.btnSearchRack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchRack.ForeColor = System.Drawing.Color.Black;
            this.btnSearchRack.Location = new System.Drawing.Point(225, 111);
            this.btnSearchRack.Name = "btnSearchRack";
            this.btnSearchRack.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSearchRack.RecessDepth = 0;
            this.btnSearchRack.Round = 10;
            this.btnSearchRack.Size = new System.Drawing.Size(178, 29);
            this.btnSearchRack.TabIndex = 97;
            this.btnSearchRack.Text = "Chercher par tirage";
            this.btnSearchRack.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnSearchRack.UseVisualStyleBackColor = false;
            this.btnSearchRack.Click += new System.EventHandler(this.btnSearchRack_Click);
            // 
            // BtnSearchWord
            // 
            this.BtnSearchWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.BtnSearchWord.CenterColor = System.Drawing.Color.White;
            this.BtnSearchWord.FocusDrawn = false;
            this.BtnSearchWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearchWord.ForeColor = System.Drawing.Color.Black;
            this.BtnSearchWord.Location = new System.Drawing.Point(29, 111);
            this.BtnSearchWord.Name = "BtnSearchWord";
            this.BtnSearchWord.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnSearchWord.RecessDepth = 0;
            this.BtnSearchWord.Round = 10;
            this.BtnSearchWord.Size = new System.Drawing.Size(178, 29);
            this.BtnSearchWord.TabIndex = 54;
            this.BtnSearchWord.Text = "Chercher par mot";
            this.BtnSearchWord.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.BtnSearchWord.UseVisualStyleBackColor = false;
            this.BtnSearchWord.Click += new System.EventHandler(this.BtnSearchWord_Click);
            // 
            // bindingConfig
            // 
            this.bindingConfig.DataSource = typeof(TopMachine.Training.DAL.fdbo.ListConfig);
            // 
            // bindingParams
            // 
            this.bindingParams.DataSource = typeof(TopMachine.Training.DAL.fdbo.GameConfig);
            // 
            // cbCategory
            // 
            this.cbCategory.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingParams, "TypeOfPlay", true));
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.Location = new System.Drawing.Point(387, 69);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(141, 21);
            this.cbCategory.TabIndex = 99;
            // 
            // lblType
            // 
            this.lblType.Location = new System.Drawing.Point(301, 71);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(80, 20);
            this.lblType.TabIndex = 100;
            this.lblType.Text = "Type :";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.btnUpdate.CenterColor = System.Drawing.Color.White;
            this.btnUpdate.Enabled = false;
            this.btnUpdate.FocusDrawn = false;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.Location = new System.Drawing.Point(299, 309);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnUpdate.RecessDepth = 0;
            this.btnUpdate.Round = 10;
            this.btnUpdate.Size = new System.Drawing.Size(130, 26);
            this.btnUpdate.TabIndex = 60;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // UpdateList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.BtnDel);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupDestination);
            this.Name = "UpdateList";
            this.Size = new System.Drawing.Size(680, 374);
            this.groupDestination.ResumeLayout(false);
            this.groupDestination.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVWords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingParams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupDestination;
        private System.Windows.Forms.ComboBox cbList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWordRack;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.BindingSource bindingConfig;
        private System.Windows.Forms.BindingSource bindingParams;
        private System.Windows.Forms.BindingSource criteriaBindingSource;
        private Desktop.Controls.ImageButton btnClose;
        private System.Windows.Forms.OpenFileDialog openFileImport;
        private Desktop.Controls.ImageButton BtnSearchWord;
        private System.Windows.Forms.DataGridView GVWords;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Trouve;
        private System.Windows.Forms.DataGridViewTextBoxColumn Perdu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tirage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private Desktop.Controls.ImageButton BtnDel;
        private Desktop.Controls.ImageButton BtnAdd;
        private Desktop.Controls.ImageButton btnSearchRack;
        private Desktop.Controls.ImageButton btnDisplayList;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbCategory;
        private Desktop.Controls.ImageButton btnUpdate;
    }
}
