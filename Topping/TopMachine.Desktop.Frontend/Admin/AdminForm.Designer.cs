namespace TopMachine.Topping.Frontend.Admin
{
    partial class AdminForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gridUsers = new System.Windows.Forms.DataGridView();
            this.usernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastLoginDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.userRefresh = new System.Windows.Forms.ToolStripButton();
            this.userEdit = new System.Windows.Forms.ToolStripButton();
            this.userAdd = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minutesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.secondsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minLettersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxLettersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jokerDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.languageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.explosiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.topingDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.configGameDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.config = new System.Windows.Forms.ToolStrip();
            this.configRefresh = new System.Windows.Forms.ToolStripButton();
            this.ConfigEdit = new System.Windows.Forms.ToolStripButton();
            this.ConfigAdd = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userDtoBindingSource)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.configGameDtoBindingSource)).BeginInit();
            this.config.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(824, 649);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridUsers);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(816, 620);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Utilisateurs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gridUsers
            // 
            this.gridUsers.AllowUserToOrderColumns = true;
            this.gridUsers.AutoGenerateColumns = false;
            this.gridUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usernameDataGridViewTextBoxColumn,
            this.firstnameDataGridViewTextBoxColumn,
            this.lastnameDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn,
            this.LastLoginDate});
            this.gridUsers.DataSource = this.userDtoBindingSource;
            this.gridUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridUsers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridUsers.Location = new System.Drawing.Point(4, 29);
            this.gridUsers.Margin = new System.Windows.Forms.Padding(4);
            this.gridUsers.MultiSelect = false;
            this.gridUsers.Name = "gridUsers";
            this.gridUsers.ReadOnly = true;
            this.gridUsers.RowTemplate.Height = 24;
            this.gridUsers.Size = new System.Drawing.Size(808, 587);
            this.gridUsers.TabIndex = 1;
            this.gridUsers.ColumnSortModeChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.gridUsers_ColumnSortModeChanged);
            // 
            // usernameDataGridViewTextBoxColumn
            // 
            this.usernameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.usernameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
            this.usernameDataGridViewTextBoxColumn.HeaderText = "UserName";
            this.usernameDataGridViewTextBoxColumn.Name = "usernameDataGridViewTextBoxColumn";
            this.usernameDataGridViewTextBoxColumn.ReadOnly = true;
            this.usernameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.usernameDataGridViewTextBoxColumn.Width = 98;
            // 
            // firstnameDataGridViewTextBoxColumn
            // 
            this.firstnameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            this.firstnameDataGridViewTextBoxColumn.HeaderText = "FirstName";
            this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
            this.firstnameDataGridViewTextBoxColumn.ReadOnly = true;
            this.firstnameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.firstnameDataGridViewTextBoxColumn.Width = 95;
            // 
            // lastnameDataGridViewTextBoxColumn
            // 
            this.lastnameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.lastnameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastnameDataGridViewTextBoxColumn.HeaderText = "LastName";
            this.lastnameDataGridViewTextBoxColumn.Name = "lastnameDataGridViewTextBoxColumn";
            this.lastnameDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastnameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.lastnameDataGridViewTextBoxColumn.Width = 95;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.ReadOnly = true;
            this.emailDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.emailDataGridViewTextBoxColumn.Width = 67;
            // 
            // LastLoginDate
            // 
            this.LastLoginDate.DataPropertyName = "LastLoginDate";
            this.LastLoginDate.HeaderText = "Last Login";
            this.LastLoginDate.Name = "LastLoginDate";
            this.LastLoginDate.ReadOnly = true;
            // 
            // userDtoBindingSource
            // 
            this.userDtoBindingSource.DataSource = typeof(TopMachine.Topping.Dto.UserDto);
            this.userDtoBindingSource.Sort = "UserName";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userRefresh,
            this.userEdit,
            this.userAdd});
            this.toolStrip1.Location = new System.Drawing.Point(4, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(808, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // userRefresh
            // 
            this.userRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.userRefresh.Image = global::TopMachine.Topping.Frontend.Properties.Resources.database_refresh;
            this.userRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.userRefresh.Name = "userRefresh";
            this.userRefresh.Size = new System.Drawing.Size(23, 22);
            this.userRefresh.Text = "Rafraîchir la liste";
            this.userRefresh.Click += new System.EventHandler(this.userRefresh_Click);
            // 
            // userEdit
            // 
            this.userEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.userEdit.Image = global::TopMachine.Topping.Frontend.Properties.Resources.user_edit;
            this.userEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.userEdit.Name = "userEdit";
            this.userEdit.Size = new System.Drawing.Size(23, 22);
            this.userEdit.Text = "Editer";
            this.userEdit.Click += new System.EventHandler(this.userEdit_Click);
            // 
            // userAdd
            // 
            this.userAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.userAdd.Image = global::TopMachine.Topping.Frontend.Properties.Resources.user_add;
            this.userAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.userAdd.Name = "userAdd";
            this.userAdd.Size = new System.Drawing.Size(23, 22);
            this.userAdd.Text = "Nouvel utilisateur";
            this.userAdd.Click += new System.EventHandler(this.userAdd_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Controls.Add(this.config);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(816, 620);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configurations";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.minutesDataGridViewTextBoxColumn,
            this.secondsDataGridViewTextBoxColumn,
            this.minLettersDataGridViewTextBoxColumn,
            this.maxLettersDataGridViewTextBoxColumn,
            this.jokerDataGridViewCheckBoxColumn,
            this.languageDataGridViewTextBoxColumn,
            this.explosiveDataGridViewCheckBoxColumn,
            this.topingDataGridViewCheckBoxColumn});
            this.dataGridView2.DataSource = this.configGameDtoBindingSource;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(4, 29);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(808, 587);
            this.dataGridView2.TabIndex = 3;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 70;
            // 
            // minutesDataGridViewTextBoxColumn
            // 
            this.minutesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.minutesDataGridViewTextBoxColumn.DataPropertyName = "Minutes";
            this.minutesDataGridViewTextBoxColumn.HeaderText = "Minutes";
            this.minutesDataGridViewTextBoxColumn.Name = "minutesDataGridViewTextBoxColumn";
            this.minutesDataGridViewTextBoxColumn.ReadOnly = true;
            this.minutesDataGridViewTextBoxColumn.Width = 21;
            // 
            // secondsDataGridViewTextBoxColumn
            // 
            this.secondsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.secondsDataGridViewTextBoxColumn.DataPropertyName = "Seconds";
            this.secondsDataGridViewTextBoxColumn.HeaderText = "Seconds";
            this.secondsDataGridViewTextBoxColumn.Name = "secondsDataGridViewTextBoxColumn";
            this.secondsDataGridViewTextBoxColumn.ReadOnly = true;
            this.secondsDataGridViewTextBoxColumn.Width = 21;
            // 
            // minLettersDataGridViewTextBoxColumn
            // 
            this.minLettersDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.minLettersDataGridViewTextBoxColumn.DataPropertyName = "MinLetters";
            this.minLettersDataGridViewTextBoxColumn.HeaderText = "MinLetters";
            this.minLettersDataGridViewTextBoxColumn.Name = "minLettersDataGridViewTextBoxColumn";
            this.minLettersDataGridViewTextBoxColumn.ReadOnly = true;
            this.minLettersDataGridViewTextBoxColumn.Width = 21;
            // 
            // maxLettersDataGridViewTextBoxColumn
            // 
            this.maxLettersDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.maxLettersDataGridViewTextBoxColumn.DataPropertyName = "MaxLetters";
            this.maxLettersDataGridViewTextBoxColumn.HeaderText = "MaxLetters";
            this.maxLettersDataGridViewTextBoxColumn.Name = "maxLettersDataGridViewTextBoxColumn";
            this.maxLettersDataGridViewTextBoxColumn.ReadOnly = true;
            this.maxLettersDataGridViewTextBoxColumn.Width = 21;
            // 
            // jokerDataGridViewCheckBoxColumn
            // 
            this.jokerDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.jokerDataGridViewCheckBoxColumn.DataPropertyName = "Joker";
            this.jokerDataGridViewCheckBoxColumn.HeaderText = "Joker";
            this.jokerDataGridViewCheckBoxColumn.Name = "jokerDataGridViewCheckBoxColumn";
            this.jokerDataGridViewCheckBoxColumn.ReadOnly = true;
            this.jokerDataGridViewCheckBoxColumn.Width = 21;
            // 
            // languageDataGridViewTextBoxColumn
            // 
            this.languageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.languageDataGridViewTextBoxColumn.DataPropertyName = "Language";
            this.languageDataGridViewTextBoxColumn.HeaderText = "Language";
            this.languageDataGridViewTextBoxColumn.Name = "languageDataGridViewTextBoxColumn";
            this.languageDataGridViewTextBoxColumn.ReadOnly = true;
            this.languageDataGridViewTextBoxColumn.Width = 21;
            // 
            // explosiveDataGridViewCheckBoxColumn
            // 
            this.explosiveDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.explosiveDataGridViewCheckBoxColumn.DataPropertyName = "Explosive";
            this.explosiveDataGridViewCheckBoxColumn.HeaderText = "Explosive";
            this.explosiveDataGridViewCheckBoxColumn.Name = "explosiveDataGridViewCheckBoxColumn";
            this.explosiveDataGridViewCheckBoxColumn.ReadOnly = true;
            this.explosiveDataGridViewCheckBoxColumn.Width = 21;
            // 
            // topingDataGridViewCheckBoxColumn
            // 
            this.topingDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.topingDataGridViewCheckBoxColumn.DataPropertyName = "Toping";
            this.topingDataGridViewCheckBoxColumn.HeaderText = "Toping";
            this.topingDataGridViewCheckBoxColumn.Name = "topingDataGridViewCheckBoxColumn";
            this.topingDataGridViewCheckBoxColumn.ReadOnly = true;
            this.topingDataGridViewCheckBoxColumn.Width = 21;
            // 
            // configGameDtoBindingSource
            // 
            this.configGameDtoBindingSource.DataSource = typeof(TopMachine.Topping.Dto.ConfigGameDto);
            this.configGameDtoBindingSource.Sort = "Name";
            // 
            // config
            // 
            this.config.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configRefresh,
            this.ConfigEdit,
            this.ConfigAdd});
            this.config.Location = new System.Drawing.Point(4, 4);
            this.config.Name = "config";
            this.config.Size = new System.Drawing.Size(808, 25);
            this.config.TabIndex = 2;
            // 
            // configRefresh
            // 
            this.configRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.configRefresh.Image = global::TopMachine.Topping.Frontend.Properties.Resources.database_refresh;
            this.configRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.configRefresh.Name = "configRefresh";
            this.configRefresh.Size = new System.Drawing.Size(23, 22);
            this.configRefresh.Text = "Rafraîchir la liste";
            this.configRefresh.Click += new System.EventHandler(this.configRefresh_Click);
            // 
            // ConfigEdit
            // 
            this.ConfigEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ConfigEdit.Image = global::TopMachine.Topping.Frontend.Properties.Resources.application_edit;
            this.ConfigEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConfigEdit.Name = "ConfigEdit";
            this.ConfigEdit.Size = new System.Drawing.Size(23, 22);
            this.ConfigEdit.Text = "Editer";
            this.ConfigEdit.Click += new System.EventHandler(this.EditConfig_Click);
            // 
            // ConfigAdd
            // 
            this.ConfigAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ConfigAdd.Image = global::TopMachine.Topping.Frontend.Properties.Resources.application_add;
            this.ConfigAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConfigAdd.Name = "ConfigAdd";
            this.ConfigAdd.Size = new System.Drawing.Size(23, 22);
            this.ConfigAdd.Text = "Nouvel Configuration";
            this.ConfigAdd.Click += new System.EventHandler(this.addConfig_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 649);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userDtoBindingSource)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.configGameDtoBindingSource)).EndInit();
            this.config.ResumeLayout(false);
            this.config.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripButton userRefresh;
        private System.Windows.Forms.DataGridView gridUsers;
        private System.Windows.Forms.ToolStripButton userEdit;
        private System.Windows.Forms.ToolStripButton userAdd;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStrip config;
        private System.Windows.Forms.ToolStripButton configRefresh;
        private System.Windows.Forms.ToolStripButton ConfigAdd;
        private System.Windows.Forms.ToolStripButton ConfigEdit;
        private System.Windows.Forms.BindingSource configGameDtoBindingSource;
        private System.Windows.Forms.BindingSource userDtoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minutesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn secondsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minLettersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxLettersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn jokerDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn explosiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn topingDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastLoginDate;
    }
}