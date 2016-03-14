using Topping.Core.Proxy.Local.Client;

namespace TopMachine.Topping.Frontend
{
    partial class ToppingConfigs
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

            //MessageProxy.Proxy.Dispose(); 
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			this.pnlConfigs = new System.Windows.Forms.Panel();
			this.btnNewConfig = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.gridRankings = new System.Windows.Forms.DataGridView();
			this.periodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.playerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.configDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.percentageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nbGamesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nbLostTopsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nbTopsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bestTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bestScoreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rankingDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.panel2 = new System.Windows.Forms.Panel();
			this.button2 = new System.Windows.Forms.Button();
			this.ddlConfigs = new System.Windows.Forms.ComboBox();
			this.configGameDtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.button1 = new System.Windows.Forms.Button();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.gridDetail = new System.Windows.Forms.DataGridView();
			this.gameIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.topDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.negativeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.percentageDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.timeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.topLostDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.panel3 = new System.Windows.Forms.Panel();
			this.btn_ListeParties = new System.Windows.Forms.Button();
			this.ddlConfigs2 = new System.Windows.Forms.ComboBox();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.TP_Simulation = new System.Windows.Forms.TabPage();
			this.txtNb_Games = new System.Windows.Forms.NumericUpDown();
			this.btnPlay = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txt_result = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cmb_config = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btn_generateGame = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridRankings)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.rankingDtoBindingSource)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.configGameDtoBindingSource)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			this.panel3.SuspendLayout();
			this.TP_Simulation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtNb_Games)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlConfigs
			// 
			this.pnlConfigs.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlConfigs.Location = new System.Drawing.Point(0, 26);
			this.pnlConfigs.Name = "pnlConfigs";
			this.pnlConfigs.Size = new System.Drawing.Size(480, 463);
			this.pnlConfigs.TabIndex = 1;
			// 
			// btnNewConfig
			// 
			this.btnNewConfig.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnNewConfig.Location = new System.Drawing.Point(0, 0);
			this.btnNewConfig.Name = "btnNewConfig";
			this.btnNewConfig.Size = new System.Drawing.Size(994, 26);
			this.btnNewConfig.TabIndex = 2;
			this.btnNewConfig.Text = "Nouvelle Configuration";
			this.btnNewConfig.UseVisualStyleBackColor = true;
			this.btnNewConfig.Click += new System.EventHandler(this.btnNewConfig_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tabControl1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(480, 26);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(514, 463);
			this.panel1.TabIndex = 3;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.TP_Simulation);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(514, 463);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.gridRankings);
			this.tabPage1.Controls.Add(this.panel2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(506, 437);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Statistiques";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// gridRankings
			// 
			this.gridRankings.AutoGenerateColumns = false;
			this.gridRankings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridRankings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.periodDataGridViewTextBoxColumn,
            this.playerDataGridViewTextBoxColumn,
            this.configDataGridViewTextBoxColumn,
            this.percentageDataGridViewTextBoxColumn,
            this.nbGamesDataGridViewTextBoxColumn,
            this.nbLostTopsDataGridViewTextBoxColumn,
            this.nbTopsDataGridViewTextBoxColumn,
            this.timeDataGridViewTextBoxColumn,
            this.bestTimeDataGridViewTextBoxColumn,
            this.bestScoreDataGridViewTextBoxColumn});
			this.gridRankings.DataSource = this.rankingDtoBindingSource;
			this.gridRankings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridRankings.Location = new System.Drawing.Point(3, 68);
			this.gridRankings.Name = "gridRankings";
			this.gridRankings.RowTemplate.Height = 24;
			this.gridRankings.Size = new System.Drawing.Size(500, 366);
			this.gridRankings.TabIndex = 0;
			// 
			// periodDataGridViewTextBoxColumn
			// 
			this.periodDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.periodDataGridViewTextBoxColumn.DataPropertyName = "Period";
			this.periodDataGridViewTextBoxColumn.HeaderText = "Période";
			this.periodDataGridViewTextBoxColumn.Name = "periodDataGridViewTextBoxColumn";
			this.periodDataGridViewTextBoxColumn.Width = 68;
			// 
			// playerDataGridViewTextBoxColumn
			// 
			this.playerDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.playerDataGridViewTextBoxColumn.DataPropertyName = "Player";
			this.playerDataGridViewTextBoxColumn.HeaderText = "Joueur";
			this.playerDataGridViewTextBoxColumn.Name = "playerDataGridViewTextBoxColumn";
			this.playerDataGridViewTextBoxColumn.Width = 64;
			// 
			// configDataGridViewTextBoxColumn
			// 
			this.configDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.configDataGridViewTextBoxColumn.DataPropertyName = "ConfigName";
			this.configDataGridViewTextBoxColumn.HeaderText = "Config";
			this.configDataGridViewTextBoxColumn.Name = "configDataGridViewTextBoxColumn";
			this.configDataGridViewTextBoxColumn.Width = 62;
			// 
			// percentageDataGridViewTextBoxColumn
			// 
			this.percentageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.percentageDataGridViewTextBoxColumn.DataPropertyName = "Percentage";
			dataGridViewCellStyle5.Format = "N2";
			dataGridViewCellStyle5.NullValue = null;
			this.percentageDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
			this.percentageDataGridViewTextBoxColumn.HeaderText = "%";
			this.percentageDataGridViewTextBoxColumn.Name = "percentageDataGridViewTextBoxColumn";
			this.percentageDataGridViewTextBoxColumn.ToolTipText = "%age moyen des parties";
			this.percentageDataGridViewTextBoxColumn.Width = 40;
			// 
			// nbGamesDataGridViewTextBoxColumn
			// 
			this.nbGamesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.nbGamesDataGridViewTextBoxColumn.DataPropertyName = "NbGames";
			this.nbGamesDataGridViewTextBoxColumn.HeaderText = "#";
			this.nbGamesDataGridViewTextBoxColumn.Name = "nbGamesDataGridViewTextBoxColumn";
			this.nbGamesDataGridViewTextBoxColumn.ToolTipText = "Nombre de parties";
			this.nbGamesDataGridViewTextBoxColumn.Width = 39;
			// 
			// nbLostTopsDataGridViewTextBoxColumn
			// 
			this.nbLostTopsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.nbLostTopsDataGridViewTextBoxColumn.DataPropertyName = "NbLostTops";
			dataGridViewCellStyle6.Format = "N2";
			dataGridViewCellStyle6.NullValue = null;
			this.nbLostTopsDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
			this.nbLostTopsDataGridViewTextBoxColumn.HeaderText = "NTP";
			this.nbLostTopsDataGridViewTextBoxColumn.Name = "nbLostTopsDataGridViewTextBoxColumn";
			this.nbLostTopsDataGridViewTextBoxColumn.ToolTipText = "# de tops perdus par partie";
			this.nbLostTopsDataGridViewTextBoxColumn.Width = 54;
			// 
			// nbTopsDataGridViewTextBoxColumn
			// 
			this.nbTopsDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.nbTopsDataGridViewTextBoxColumn.DataPropertyName = "NbTops";
			this.nbTopsDataGridViewTextBoxColumn.HeaderText = "N.T.";
			this.nbTopsDataGridViewTextBoxColumn.Name = "nbTopsDataGridViewTextBoxColumn";
			this.nbTopsDataGridViewTextBoxColumn.ToolTipText = "Nombre de parties au top";
			this.nbTopsDataGridViewTextBoxColumn.Width = 53;
			// 
			// timeDataGridViewTextBoxColumn
			// 
			this.timeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.timeDataGridViewTextBoxColumn.DataPropertyName = "Time";
			dataGridViewCellStyle7.Format = "N2";
			dataGridViewCellStyle7.NullValue = null;
			this.timeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
			this.timeDataGridViewTextBoxColumn.HeaderText = "Tps";
			this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
			this.timeDataGridViewTextBoxColumn.ToolTipText = "Temps moyen";
			this.timeDataGridViewTextBoxColumn.Width = 50;
			// 
			// bestTimeDataGridViewTextBoxColumn
			// 
			this.bestTimeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.bestTimeDataGridViewTextBoxColumn.DataPropertyName = "BestTime";
			this.bestTimeDataGridViewTextBoxColumn.HeaderText = "M.T.";
			this.bestTimeDataGridViewTextBoxColumn.Name = "bestTimeDataGridViewTextBoxColumn";
			this.bestTimeDataGridViewTextBoxColumn.ToolTipText = "Meilleur temps";
			this.bestTimeDataGridViewTextBoxColumn.Width = 54;
			// 
			// bestScoreDataGridViewTextBoxColumn
			// 
			this.bestScoreDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.bestScoreDataGridViewTextBoxColumn.DataPropertyName = "BestScore";
			dataGridViewCellStyle8.Format = "N2";
			dataGridViewCellStyle8.NullValue = null;
			this.bestScoreDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
			this.bestScoreDataGridViewTextBoxColumn.HeaderText = "M.S.";
			this.bestScoreDataGridViewTextBoxColumn.Name = "bestScoreDataGridViewTextBoxColumn";
			this.bestScoreDataGridViewTextBoxColumn.ToolTipText = "Meilleur score";
			this.bestScoreDataGridViewTextBoxColumn.Width = 54;
			// 
			// rankingDtoBindingSource
			// 
			this.rankingDtoBindingSource.DataSource = typeof(TopMachine.Topping.Dto.RankingDto);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.button2);
			this.panel2.Controls.Add(this.ddlConfigs);
			this.panel2.Controls.Add(this.button1);
			this.panel2.Controls.Add(this.dateTimePicker1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(500, 65);
			this.panel2.TabIndex = 1;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(217, 32);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(130, 23);
			this.button2.TabIndex = 3;
			this.button2.Text = "Liste par configuration";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// ddlConfigs
			// 
			this.ddlConfigs.DataSource = this.configGameDtoBindingSource;
			this.ddlConfigs.DisplayMember = "Name";
			this.ddlConfigs.FormattingEnabled = true;
			this.ddlConfigs.Location = new System.Drawing.Point(11, 34);
			this.ddlConfigs.Name = "ddlConfigs";
			this.ddlConfigs.Size = new System.Drawing.Size(196, 21);
			this.ddlConfigs.TabIndex = 2;
			// 
			// configGameDtoBindingSource
			// 
			this.configGameDtoBindingSource.DataSource = typeof(TopMachine.Topping.Dto.ConfigGameDto);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(217, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(130, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Liste par mois";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Location = new System.Drawing.Point(7, 7);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
			this.dateTimePicker1.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.gridDetail);
			this.tabPage2.Controls.Add(this.panel3);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(506, 437);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Dernières parties";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// gridDetail
			// 
			this.gridDetail.AutoGenerateColumns = false;
			this.gridDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gameIdDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.totalDataGridViewTextBoxColumn,
            this.topDataGridViewTextBoxColumn,
            this.negativeDataGridViewTextBoxColumn,
            this.percentageDataGridViewTextBoxColumn1,
            this.timeDataGridViewTextBoxColumn1,
            this.topLostDataGridViewTextBoxColumn});
			this.gridDetail.DataSource = this.bindingSource1;
			this.gridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridDetail.Location = new System.Drawing.Point(3, 60);
			this.gridDetail.Name = "gridDetail";
			this.gridDetail.RowTemplate.Height = 24;
			this.gridDetail.Size = new System.Drawing.Size(500, 374);
			this.gridDetail.TabIndex = 3;
			// 
			// gameIdDataGridViewTextBoxColumn
			// 
			this.gameIdDataGridViewTextBoxColumn.DataPropertyName = "GameId";
			this.gameIdDataGridViewTextBoxColumn.HeaderText = "GameId";
			this.gameIdDataGridViewTextBoxColumn.Name = "gameIdDataGridViewTextBoxColumn";
			this.gameIdDataGridViewTextBoxColumn.Visible = false;
			// 
			// userNameDataGridViewTextBoxColumn
			// 
			this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
			this.userNameDataGridViewTextBoxColumn.HeaderText = "UserName";
			this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
			this.userNameDataGridViewTextBoxColumn.Visible = false;
			// 
			// dateDataGridViewTextBoxColumn
			// 
			this.dateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
			this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
			this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
			this.dateDataGridViewTextBoxColumn.Width = 55;
			// 
			// totalDataGridViewTextBoxColumn
			// 
			this.totalDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
			this.totalDataGridViewTextBoxColumn.HeaderText = "Tot.";
			this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
			this.totalDataGridViewTextBoxColumn.Width = 51;
			// 
			// topDataGridViewTextBoxColumn
			// 
			this.topDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.topDataGridViewTextBoxColumn.DataPropertyName = "Top";
			this.topDataGridViewTextBoxColumn.HeaderText = "Top";
			this.topDataGridViewTextBoxColumn.Name = "topDataGridViewTextBoxColumn";
			this.topDataGridViewTextBoxColumn.Width = 51;
			// 
			// negativeDataGridViewTextBoxColumn
			// 
			this.negativeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.negativeDataGridViewTextBoxColumn.DataPropertyName = "Negative";
			this.negativeDataGridViewTextBoxColumn.HeaderText = "-";
			this.negativeDataGridViewTextBoxColumn.Name = "negativeDataGridViewTextBoxColumn";
			this.negativeDataGridViewTextBoxColumn.Width = 35;
			// 
			// percentageDataGridViewTextBoxColumn1
			// 
			this.percentageDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.percentageDataGridViewTextBoxColumn1.DataPropertyName = "Percentage";
			this.percentageDataGridViewTextBoxColumn1.HeaderText = "%";
			this.percentageDataGridViewTextBoxColumn1.Name = "percentageDataGridViewTextBoxColumn1";
			this.percentageDataGridViewTextBoxColumn1.Width = 40;
			// 
			// timeDataGridViewTextBoxColumn1
			// 
			this.timeDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.timeDataGridViewTextBoxColumn1.DataPropertyName = "Time";
			this.timeDataGridViewTextBoxColumn1.HeaderText = "Tps";
			this.timeDataGridViewTextBoxColumn1.Name = "timeDataGridViewTextBoxColumn1";
			this.timeDataGridViewTextBoxColumn1.Width = 50;
			// 
			// topLostDataGridViewTextBoxColumn
			// 
			this.topLostDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.topLostDataGridViewTextBoxColumn.DataPropertyName = "TopLost";
			this.topLostDataGridViewTextBoxColumn.HeaderText = "T.P.";
			this.topLostDataGridViewTextBoxColumn.Name = "topLostDataGridViewTextBoxColumn";
			this.topLostDataGridViewTextBoxColumn.Width = 52;
			// 
			// bindingSource1
			// 
			this.bindingSource1.DataSource = typeof(TopMachine.Topping.Dto.GamesDetailDto);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.btn_ListeParties);
			this.panel3.Controls.Add(this.ddlConfigs2);
			this.panel3.Controls.Add(this.dateTimePicker2);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(500, 57);
			this.panel3.TabIndex = 2;
			// 
			// btn_ListeParties
			// 
			this.btn_ListeParties.Location = new System.Drawing.Point(213, 7);
			this.btn_ListeParties.Name = "btn_ListeParties";
			this.btn_ListeParties.Size = new System.Drawing.Size(65, 45);
			this.btn_ListeParties.TabIndex = 3;
			this.btn_ListeParties.Text = "Liste";
			this.btn_ListeParties.UseVisualStyleBackColor = true;
			this.btn_ListeParties.Click += new System.EventHandler(this.btn_ListeParties_Click);
			// 
			// ddlConfigs2
			// 
			this.ddlConfigs2.DataSource = this.configGameDtoBindingSource;
			this.ddlConfigs2.DisplayMember = "Name";
			this.ddlConfigs2.FormattingEnabled = true;
			this.ddlConfigs2.Location = new System.Drawing.Point(8, 30);
			this.ddlConfigs2.Name = "ddlConfigs2";
			this.ddlConfigs2.Size = new System.Drawing.Size(196, 21);
			this.ddlConfigs2.TabIndex = 2;
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.Location = new System.Drawing.Point(7, 7);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
			this.dateTimePicker2.TabIndex = 0;
			// 
			// TP_Simulation
			// 
			this.TP_Simulation.Controls.Add(this.txtNb_Games);
			this.TP_Simulation.Controls.Add(this.btnPlay);
			this.TP_Simulation.Controls.Add(this.label3);
			this.TP_Simulation.Controls.Add(this.txt_result);
			this.TP_Simulation.Controls.Add(this.label2);
			this.TP_Simulation.Controls.Add(this.cmb_config);
			this.TP_Simulation.Controls.Add(this.label1);
			this.TP_Simulation.Controls.Add(this.btn_generateGame);
			this.TP_Simulation.Location = new System.Drawing.Point(4, 22);
			this.TP_Simulation.Margin = new System.Windows.Forms.Padding(2);
			this.TP_Simulation.Name = "TP_Simulation";
			this.TP_Simulation.Size = new System.Drawing.Size(506, 437);
			this.TP_Simulation.TabIndex = 2;
			this.TP_Simulation.Text = "Simulation";
			this.TP_Simulation.UseVisualStyleBackColor = true;
			// 
			// txtNb_Games
			// 
			this.txtNb_Games.Location = new System.Drawing.Point(147, 39);
			this.txtNb_Games.Margin = new System.Windows.Forms.Padding(2);
			this.txtNb_Games.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
			this.txtNb_Games.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.txtNb_Games.Name = "txtNb_Games";
			this.txtNb_Games.Size = new System.Drawing.Size(54, 20);
			this.txtNb_Games.TabIndex = 10;
			this.txtNb_Games.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnPlay
			// 
			this.btnPlay.Enabled = false;
			this.btnPlay.Location = new System.Drawing.Point(267, 70);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(130, 23);
			this.btnPlay.TabIndex = 9;
			this.btnPlay.Text = "Jouer";
			this.btnPlay.UseVisualStyleBackColor = true;
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(32, 116);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "result";
			// 
			// txt_result
			// 
			this.txt_result.Location = new System.Drawing.Point(82, 114);
			this.txt_result.Margin = new System.Windows.Forms.Padding(2);
			this.txt_result.Multiline = true;
			this.txt_result.Name = "txt_result";
			this.txt_result.Size = new System.Drawing.Size(240, 265);
			this.txt_result.TabIndex = 7;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(265, 44);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "config";
			// 
			// cmb_config
			// 
			this.cmb_config.FormattingEnabled = true;
			this.cmb_config.Location = new System.Drawing.Point(304, 44);
			this.cmb_config.Margin = new System.Windows.Forms.Padding(2);
			this.cmb_config.Name = "cmb_config";
			this.cmb_config.Size = new System.Drawing.Size(160, 21);
			this.cmb_config.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(2, 41);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(138, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "nombre de parties générees";
			// 
			// btn_generateGame
			// 
			this.btn_generateGame.Location = new System.Drawing.Point(13, 70);
			this.btn_generateGame.Name = "btn_generateGame";
			this.btn_generateGame.Size = new System.Drawing.Size(130, 23);
			this.btn_generateGame.TabIndex = 2;
			this.btn_generateGame.Text = "Generer";
			this.btn_generateGame.UseVisualStyleBackColor = true;
			this.btn_generateGame.Click += new System.EventHandler(this.btn_generateGame_Click);
			// 
			// ToppingConfigs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pnlConfigs);
			this.Controls.Add(this.btnNewConfig);
			this.Name = "ToppingConfigs";
			this.Size = new System.Drawing.Size(994, 489);
			this.panel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridRankings)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.rankingDtoBindingSource)).EndInit();
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.configGameDtoBindingSource)).EndInit();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.panel3.ResumeLayout(false);
			this.TP_Simulation.ResumeLayout(false);
			this.TP_Simulation.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtNb_Games)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource configGameDtoBindingSource;
        private System.Windows.Forms.Panel pnlConfigs;
        private System.Windows.Forms.Button btnNewConfig;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView gridRankings;
        private System.Windows.Forms.BindingSource rankingDtoBindingSource;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox ddlConfigs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn configDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn percentageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbGamesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbLostTopsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nbTopsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bestTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bestScoreDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_ListeParties;
        private System.Windows.Forms.ComboBox ddlConfigs2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DataGridView gridDetail;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn gameIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn topDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn negativeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn percentageDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn topLostDataGridViewTextBoxColumn;
        private System.Windows.Forms.TabPage TP_Simulation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_generateGame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_config;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.NumericUpDown txtNb_Games;
    }
}
