namespace TopMachine.Training.FrontEnd
{
    partial class TrainingConfig
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
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.bindingConfig = new System.Windows.Forms.BindingSource(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.groupParams = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bindingParams = new System.Windows.Forms.BindingSource(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbTypeOfPlay = new System.Windows.Forms.ComboBox();
            this.tbMax = new System.Windows.Forms.TextBox();
            this.txtChrono = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbAffichage = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbAffPoss = new System.Windows.Forms.CheckBox();
            this.cbPause = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupConfig = new System.Windows.Forms.GroupBox();
            this.lblInfoRegEx = new System.Windows.Forms.Label();
            this.chkAllCritere = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtJokers = new System.Windows.Forms.TextBox();
            this.cbBase = new System.Windows.Forms.ComboBox();
            this.txtMaxLetters = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPossibilitiesMax = new System.Windows.Forms.TextBox();
            this.txtGameName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWithoutLetters = new System.Windows.Forms.TextBox();
            this.cbJoker = new System.Windows.Forms.CheckBox();
            this.lblNa = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMinLetters = new System.Windows.Forms.TextBox();
            this.txtWithLetters = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPossibilities = new System.Windows.Forms.TextBox();
            this.txtIncludeLetters = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new TopMachine.Desktop.Controls.ImageButton();
            this.criteriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.imgbtnCancel = new TopMachine.Desktop.Controls.ImageButton();
            this.dgvRegEx = new System.Windows.Forms.DataGridView();
            this.inclusionRegExDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inclusionRegExCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exclusionRegExDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exclusionRegExCountCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imgBtnAddCritere = new TopMachine.Desktop.Controls.ImageButton();
            this.imgBtnDelCritere = new TopMachine.Desktop.Controls.ImageButton();
            this.imgbtnSample = new TopMachine.Desktop.Controls.ImageButton();
            this.imgBtnExpertMode = new TopMachine.Desktop.Controls.ImageButton();
            this.imgbtnCancel2 = new TopMachine.Desktop.Controls.ImageButton();
            this.imgbtnSave2 = new TopMachine.Desktop.Controls.ImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).BeginInit();
            this.groupParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingParams)).BeginInit();
            this.groupConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegEx)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLanguage
            // 
            this.cbLanguage.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingConfig, "Dico", true));
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.Location = new System.Drawing.Point(116, 57);
            this.cbLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(324, 24);
            this.cbLanguage.TabIndex = 53;
            // 
            // bindingConfig
            // 
            this.bindingConfig.DataSource = typeof(TopMachine.Training.DAL.fdbo.ListConfig);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(24, 58);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 20);
            this.label9.TabIndex = 52;
            this.label9.Text = "Langue :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupParams
            // 
            this.groupParams.BackColor = System.Drawing.Color.DarkGray;
            this.groupParams.Controls.Add(this.textBox1);
            this.groupParams.Controls.Add(this.label10);
            this.groupParams.Controls.Add(this.cbMode);
            this.groupParams.Controls.Add(this.label14);
            this.groupParams.Controls.Add(this.cbTypeOfPlay);
            this.groupParams.Controls.Add(this.tbMax);
            this.groupParams.Controls.Add(this.txtChrono);
            this.groupParams.Controls.Add(this.label13);
            this.groupParams.Controls.Add(this.cbAffichage);
            this.groupParams.Controls.Add(this.label8);
            this.groupParams.Controls.Add(this.cbAffPoss);
            this.groupParams.Controls.Add(this.cbPause);
            this.groupParams.Controls.Add(this.label12);
            this.groupParams.Location = new System.Drawing.Point(521, 12);
            this.groupParams.Margin = new System.Windows.Forms.Padding(4);
            this.groupParams.Name = "groupParams";
            this.groupParams.Padding = new System.Windows.Forms.Padding(4);
            this.groupParams.Size = new System.Drawing.Size(372, 357);
            this.groupParams.TabIndex = 51;
            this.groupParams.TabStop = false;
            this.groupParams.Text = "Paramètres du jeu";
            this.groupParams.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingParams, "TotalRounds", true));
            this.textBox1.Location = new System.Drawing.Point(16, 318);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(51, 22);
            this.textBox1.TabIndex = 101;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "100";
            // 
            // bindingParams
            // 
            this.bindingParams.DataSource = typeof(TopMachine.Training.DAL.fdbo.GameConfig);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(91, 322);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(273, 20);
            this.label10.TabIndex = 100;
            this.label10.Text = "Nombre de tirages par session";
            // 
            // cbMode
            // 
            this.cbMode.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingParams, "PlayMode", true));
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.Location = new System.Drawing.Point(13, 277);
            this.cbMode.Margin = new System.Windows.Forms.Padding(4);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(333, 24);
            this.cbMode.TabIndex = 98;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(12, 254);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(164, 20);
            this.label14.TabIndex = 99;
            this.label14.Text = "Mode de résolution :";
            // 
            // cbTypeOfPlay
            // 
            this.cbTypeOfPlay.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingParams, "TypeOfPlay", true));
            this.cbTypeOfPlay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeOfPlay.Location = new System.Drawing.Point(11, 223);
            this.cbTypeOfPlay.Margin = new System.Windows.Forms.Padding(4);
            this.cbTypeOfPlay.Name = "cbTypeOfPlay";
            this.cbTypeOfPlay.Size = new System.Drawing.Size(333, 24);
            this.cbTypeOfPlay.TabIndex = 94;
            // 
            // tbMax
            // 
            this.tbMax.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingParams, "Suppress", true));
            this.tbMax.Location = new System.Drawing.Point(8, 34);
            this.tbMax.Margin = new System.Windows.Forms.Padding(4);
            this.tbMax.Name = "tbMax";
            this.tbMax.Size = new System.Drawing.Size(51, 22);
            this.tbMax.TabIndex = 91;
            this.tbMax.TabStop = false;
            this.tbMax.Text = "0";
            // 
            // txtChrono
            // 
            this.txtChrono.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingParams, "Chrono", true));
            this.txtChrono.Location = new System.Drawing.Point(7, 81);
            this.txtChrono.Margin = new System.Windows.Forms.Padding(4);
            this.txtChrono.Name = "txtChrono";
            this.txtChrono.Size = new System.Drawing.Size(55, 22);
            this.txtChrono.TabIndex = 97;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(68, 85);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 20);
            this.label13.TabIndex = 96;
            this.label13.Text = "Temps Initial (sec.) :";
            // 
            // cbAffichage
            // 
            this.cbAffichage.BackColor = System.Drawing.Color.Transparent;
            this.cbAffichage.Checked = true;
            this.cbAffichage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAffichage.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingParams, "DisplayPlusOne", true));
            this.cbAffichage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAffichage.Location = new System.Drawing.Point(16, 114);
            this.cbAffichage.Margin = new System.Windows.Forms.Padding(4);
            this.cbAffichage.Name = "cbAffichage";
            this.cbAffichage.Size = new System.Drawing.Size(141, 25);
            this.cbAffichage.TabIndex = 89;
            this.cbAffichage.TabStop = false;
            this.cbAffichage.Text = "Affichage X + 1";
            this.cbAffichage.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 204);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 20);
            this.label8.TabIndex = 95;
            this.label8.Text = "Type de partie :";
            // 
            // cbAffPoss
            // 
            this.cbAffPoss.BackColor = System.Drawing.Color.Transparent;
            this.cbAffPoss.Checked = true;
            this.cbAffPoss.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAffPoss.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingParams, "DisplayPossibilities", true));
            this.cbAffPoss.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbAffPoss.Location = new System.Drawing.Point(165, 128);
            this.cbAffPoss.Margin = new System.Windows.Forms.Padding(4);
            this.cbAffPoss.Name = "cbAffPoss";
            this.cbAffPoss.Size = new System.Drawing.Size(129, 58);
            this.cbAffPoss.TabIndex = 93;
            this.cbAffPoss.TabStop = false;
            this.cbAffPoss.Text = "Afficher le nombre de possibilités";
            this.cbAffPoss.UseVisualStyleBackColor = false;
            // 
            // cbPause
            // 
            this.cbPause.BackColor = System.Drawing.Color.Transparent;
            this.cbPause.Checked = true;
            this.cbPause.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPause.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingParams, "Pause", true));
            this.cbPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPause.Location = new System.Drawing.Point(16, 146);
            this.cbPause.Margin = new System.Windows.Forms.Padding(4);
            this.cbPause.Name = "cbPause";
            this.cbPause.Size = new System.Drawing.Size(116, 47);
            this.cbPause.TabIndex = 90;
            this.cbPause.TabStop = false;
            this.cbPause.Text = "Pause entre les tirages";
            this.cbPause.UseVisualStyleBackColor = false;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(67, 22);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(145, 49);
            this.label12.TabIndex = 92;
            this.label12.Text = "Supprimer de la liste des non-trouvés après X succès";
            // 
            // groupConfig
            // 
            this.groupConfig.BackColor = System.Drawing.Color.DarkGray;
            this.groupConfig.Controls.Add(this.lblInfoRegEx);
            this.groupConfig.Controls.Add(this.chkAllCritere);
            this.groupConfig.Controls.Add(this.label6);
            this.groupConfig.Controls.Add(this.cbLanguage);
            this.groupConfig.Controls.Add(this.txtJokers);
            this.groupConfig.Controls.Add(this.label9);
            this.groupConfig.Controls.Add(this.cbBase);
            this.groupConfig.Controls.Add(this.txtMaxLetters);
            this.groupConfig.Controls.Add(this.label16);
            this.groupConfig.Controls.Add(this.txtPossibilitiesMax);
            this.groupConfig.Controls.Add(this.txtGameName);
            this.groupConfig.Controls.Add(this.label5);
            this.groupConfig.Controls.Add(this.txtWithoutLetters);
            this.groupConfig.Controls.Add(this.cbJoker);
            this.groupConfig.Controls.Add(this.lblNa);
            this.groupConfig.Controls.Add(this.label3);
            this.groupConfig.Controls.Add(this.label4);
            this.groupConfig.Controls.Add(this.label7);
            this.groupConfig.Controls.Add(this.txtMinLetters);
            this.groupConfig.Controls.Add(this.txtWithLetters);
            this.groupConfig.Controls.Add(this.label11);
            this.groupConfig.Controls.Add(this.label2);
            this.groupConfig.Controls.Add(this.txtPossibilities);
            this.groupConfig.Controls.Add(this.txtIncludeLetters);
            this.groupConfig.Controls.Add(this.label1);
            this.groupConfig.Enabled = false;
            this.groupConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupConfig.Location = new System.Drawing.Point(12, 10);
            this.groupConfig.Margin = new System.Windows.Forms.Padding(4);
            this.groupConfig.Name = "groupConfig";
            this.groupConfig.Padding = new System.Windows.Forms.Padding(4);
            this.groupConfig.Size = new System.Drawing.Size(489, 359);
            this.groupConfig.TabIndex = 50;
            this.groupConfig.TabStop = false;
            this.groupConfig.Text = "Configuration de la liste";
            // 
            // lblInfoRegEx
            // 
            this.lblInfoRegEx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lblInfoRegEx.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoRegEx.Location = new System.Drawing.Point(161, 300);
            this.lblInfoRegEx.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfoRegEx.Name = "lblInfoRegEx";
            this.lblInfoRegEx.Size = new System.Drawing.Size(320, 47);
            this.lblInfoRegEx.TabIndex = 93;
            this.lblInfoRegEx.Text = "Si cette option est activée tous les critères doivent correspondre pour qu\'un mot" +
    " soit ajouté dans la liste. Sinon seulement un critère doit être validé.";
            this.lblInfoRegEx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInfoRegEx.Visible = false;
            // 
            // chkAllCritere
            // 
            this.chkAllCritere.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingConfig, "MatchAllCriteria", true));
            this.chkAllCritere.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAllCritere.Location = new System.Drawing.Point(27, 313);
            this.chkAllCritere.Margin = new System.Windows.Forms.Padding(4);
            this.chkAllCritere.Name = "chkAllCritere";
            this.chkAllCritere.Size = new System.Drawing.Size(143, 30);
            this.chkAllCritere.TabIndex = 55;
            this.chkAllCritere.Text = "Tous les critères doivent correspondre";
            this.chkAllCritere.Visible = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(157, 238);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 20);
            this.label6.TabIndex = 54;
            this.label6.Text = "Lettres du joker";
            // 
            // txtJokers
            // 
            this.txtJokers.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "LetterJoker", true));
            this.txtJokers.Location = new System.Drawing.Point(156, 260);
            this.txtJokers.Margin = new System.Windows.Forms.Padding(4);
            this.txtJokers.Name = "txtJokers";
            this.txtJokers.Size = new System.Drawing.Size(132, 22);
            this.txtJokers.TabIndex = 21;
            // 
            // cbBase
            // 
            this.cbBase.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingConfig, "Base", true));
            this.cbBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBase.Location = new System.Drawing.Point(116, 87);
            this.cbBase.Margin = new System.Windows.Forms.Padding(4);
            this.cbBase.Name = "cbBase";
            this.cbBase.Size = new System.Drawing.Size(324, 24);
            this.cbBase.TabIndex = 49;
            // 
            // txtMaxLetters
            // 
            this.txtMaxLetters.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "MaxLetters", true));
            this.txtMaxLetters.Location = new System.Drawing.Point(97, 209);
            this.txtMaxLetters.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxLetters.Name = "txtMaxLetters";
            this.txtMaxLetters.Size = new System.Drawing.Size(33, 22);
            this.txtMaxLetters.TabIndex = 25;
            this.txtMaxLetters.Text = "15";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(24, 87);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 20);
            this.label16.TabIndex = 48;
            this.label16.Text = "Base :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPossibilitiesMax
            // 
            this.txtPossibilitiesMax.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "MaxPossibilities", true));
            this.txtPossibilitiesMax.Location = new System.Drawing.Point(100, 260);
            this.txtPossibilitiesMax.Margin = new System.Windows.Forms.Padding(4);
            this.txtPossibilitiesMax.Name = "txtPossibilitiesMax";
            this.txtPossibilitiesMax.Size = new System.Drawing.Size(33, 22);
            this.txtPossibilitiesMax.TabIndex = 30;
            this.txtPossibilitiesMax.Text = "1";
            // 
            // txtGameName
            // 
            this.txtGameName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Name", true));
            this.txtGameName.Location = new System.Drawing.Point(116, 30);
            this.txtGameName.Margin = new System.Windows.Forms.Padding(4);
            this.txtGameName.Name = "txtGameName";
            this.txtGameName.Size = new System.Drawing.Size(324, 22);
            this.txtGameName.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(27, 238);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 20);
            this.label5.TabIndex = 33;
            this.label5.Text = "# Possibilités";
            // 
            // txtWithoutLetters
            // 
            this.txtWithoutLetters.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "WithoutLetter", true));
            this.txtWithoutLetters.Location = new System.Drawing.Point(155, 161);
            this.txtWithoutLetters.Margin = new System.Windows.Forms.Padding(4);
            this.txtWithoutLetters.Name = "txtWithoutLetters";
            this.txtWithoutLetters.Size = new System.Drawing.Size(132, 22);
            this.txtWithoutLetters.TabIndex = 31;
            // 
            // cbJoker
            // 
            this.cbJoker.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingConfig, "Joker", true));
            this.cbJoker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbJoker.Location = new System.Drawing.Point(160, 209);
            this.cbJoker.Margin = new System.Windows.Forms.Padding(4);
            this.cbJoker.Name = "cbJoker";
            this.cbJoker.Size = new System.Drawing.Size(143, 30);
            this.cbJoker.TabIndex = 0;
            this.cbJoker.Text = "Utliser un joker ?";
            // 
            // lblNa
            // 
            this.lblNa.Location = new System.Drawing.Point(24, 32);
            this.lblNa.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNa.Name = "lblNa";
            this.lblNa.Size = new System.Drawing.Size(83, 20);
            this.lblNa.TabIndex = 42;
            this.lblNa.Text = "Nom :";
            this.lblNa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(23, 190);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "# Lettres :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(69, 209);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 20);
            this.label4.TabIndex = 32;
            this.label4.Text = "à";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(155, 140);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 20);
            this.label7.TabIndex = 30;
            this.label7.Text = "Lettres Exclues :";
            // 
            // txtMinLetters
            // 
            this.txtMinLetters.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "MinLetters", true));
            this.txtMinLetters.Location = new System.Drawing.Point(27, 209);
            this.txtMinLetters.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinLetters.Name = "txtMinLetters";
            this.txtMinLetters.Size = new System.Drawing.Size(33, 22);
            this.txtMinLetters.TabIndex = 23;
            this.txtMinLetters.Text = "1";
            // 
            // txtWithLetters
            // 
            this.txtWithLetters.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "WithAllLetters", true));
            this.txtWithLetters.Location = new System.Drawing.Point(295, 161);
            this.txtWithLetters.Margin = new System.Windows.Forms.Padding(4);
            this.txtWithLetters.Name = "txtWithLetters";
            this.txtWithLetters.Size = new System.Drawing.Size(132, 22);
            this.txtWithLetters.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(71, 260);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 25);
            this.label11.TabIndex = 31;
            this.label11.Text = "à";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(295, 139);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Lettres Obligatoires :";
            // 
            // txtPossibilities
            // 
            this.txtPossibilities.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "MinPossibilities", true));
            this.txtPossibilities.Location = new System.Drawing.Point(25, 260);
            this.txtPossibilities.Margin = new System.Windows.Forms.Padding(4);
            this.txtPossibilities.Name = "txtPossibilities";
            this.txtPossibilities.Size = new System.Drawing.Size(33, 22);
            this.txtPossibilities.TabIndex = 29;
            this.txtPossibilities.Text = "1";
            // 
            // txtIncludeLetters
            // 
            this.txtIncludeLetters.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "WithAnyLetter", true));
            this.txtIncludeLetters.Location = new System.Drawing.Point(13, 161);
            this.txtIncludeLetters.Margin = new System.Windows.Forms.Padding(4);
            this.txtIncludeLetters.Name = "txtIncludeLetters";
            this.txtIncludeLetters.Size = new System.Drawing.Size(132, 22);
            this.txtIncludeLetters.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 142);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Lettres dans tirages :";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.btnSave.CenterColor = System.Drawing.Color.White;
            this.btnSave.FocusDrawn = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(12, 620);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.RecessDepth = 0;
            this.btnSave.Round = 10;
            this.btnSave.Size = new System.Drawing.Size(237, 36);
            this.btnSave.TabIndex = 52;
            this.btnSave.Text = "Sauvegarde";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // criteriaBindingSource
            // 
            this.criteriaBindingSource.DataMember = "Criteria";
            this.criteriaBindingSource.DataSource = this.bindingConfig;
            // 
            // imgbtnCancel
            // 
            this.imgbtnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(200)))), ((int)(((byte)(128)))), ((int)(((byte)(68)))));
            this.imgbtnCancel.CenterColor = System.Drawing.Color.White;
            this.imgbtnCancel.FocusDrawn = false;
            this.imgbtnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgbtnCancel.ForeColor = System.Drawing.Color.Black;
            this.imgbtnCancel.Location = new System.Drawing.Point(656, 620);
            this.imgbtnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.imgbtnCancel.Name = "imgbtnCancel";
            this.imgbtnCancel.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.imgbtnCancel.RecessDepth = 0;
            this.imgbtnCancel.Round = 10;
            this.imgbtnCancel.Size = new System.Drawing.Size(237, 36);
            this.imgbtnCancel.TabIndex = 53;
            this.imgbtnCancel.Text = "Annulez";
            this.imgbtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imgbtnCancel.UseVisualStyleBackColor = false;
            this.imgbtnCancel.Visible = false;
            this.imgbtnCancel.Click += new System.EventHandler(this.imageButton1_Click);
            // 
            // dgvRegEx
            // 
            this.dgvRegEx.AllowUserToAddRows = false;
            this.dgvRegEx.AllowUserToDeleteRows = false;
            this.dgvRegEx.AutoGenerateColumns = false;
            this.dgvRegEx.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRegEx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegEx.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.inclusionRegExDataGridViewTextBoxColumn,
            this.inclusionRegExCountDataGridViewTextBoxColumn,
            this.exclusionRegExDataGridViewTextBoxColumn,
            this.exclusionRegExCountCountDataGridViewTextBoxColumn});
            this.dgvRegEx.DataSource = this.criteriaBindingSource;
            this.dgvRegEx.Location = new System.Drawing.Point(12, 410);
            this.dgvRegEx.Margin = new System.Windows.Forms.Padding(4);
            this.dgvRegEx.Name = "dgvRegEx";
            this.dgvRegEx.Size = new System.Drawing.Size(873, 203);
            this.dgvRegEx.TabIndex = 55;
            this.dgvRegEx.Visible = false;
            // 
            // inclusionRegExDataGridViewTextBoxColumn
            // 
            this.inclusionRegExDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.inclusionRegExDataGridViewTextBoxColumn.DataPropertyName = "InclusionRegEx";
            this.inclusionRegExDataGridViewTextBoxColumn.HeaderText = "RegEx";
            this.inclusionRegExDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.inclusionRegExDataGridViewTextBoxColumn.Name = "inclusionRegExDataGridViewTextBoxColumn";
            this.inclusionRegExDataGridViewTextBoxColumn.Width = 200;
            // 
            // inclusionRegExCountDataGridViewTextBoxColumn
            // 
            this.inclusionRegExCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.inclusionRegExCountDataGridViewTextBoxColumn.DataPropertyName = "InclusionRegExCount";
            this.inclusionRegExCountDataGridViewTextBoxColumn.HeaderText = "#";
            this.inclusionRegExCountDataGridViewTextBoxColumn.Name = "inclusionRegExCountDataGridViewTextBoxColumn";
            this.inclusionRegExCountDataGridViewTextBoxColumn.Width = 41;
            // 
            // exclusionRegExDataGridViewTextBoxColumn
            // 
            this.exclusionRegExDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.exclusionRegExDataGridViewTextBoxColumn.DataPropertyName = "ExclusionRegEx";
            this.exclusionRegExDataGridViewTextBoxColumn.HeaderText = "Exclusion RegEx";
            this.exclusionRegExDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.exclusionRegExDataGridViewTextBoxColumn.Name = "exclusionRegExDataGridViewTextBoxColumn";
            this.exclusionRegExDataGridViewTextBoxColumn.Width = 200;
            // 
            // exclusionRegExCountCountDataGridViewTextBoxColumn
            // 
            this.exclusionRegExCountCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.exclusionRegExCountCountDataGridViewTextBoxColumn.DataPropertyName = "ExclusionRegExCountCount";
            this.exclusionRegExCountCountDataGridViewTextBoxColumn.HeaderText = "#";
            this.exclusionRegExCountCountDataGridViewTextBoxColumn.Name = "exclusionRegExCountCountDataGridViewTextBoxColumn";
            this.exclusionRegExCountCountDataGridViewTextBoxColumn.Width = 41;
            // 
            // imgBtnAddCritere
            // 
            this.imgBtnAddCritere.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.imgBtnAddCritere.CenterColor = System.Drawing.Color.White;
            this.imgBtnAddCritere.FocusDrawn = false;
            this.imgBtnAddCritere.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.imgBtnAddCritere.ForeColor = System.Drawing.Color.Black;
            this.imgBtnAddCritere.Location = new System.Drawing.Point(12, 370);
            this.imgBtnAddCritere.Margin = new System.Windows.Forms.Padding(4);
            this.imgBtnAddCritere.Name = "imgBtnAddCritere";
            this.imgBtnAddCritere.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.imgBtnAddCritere.RecessDepth = 0;
            this.imgBtnAddCritere.Round = 10;
            this.imgBtnAddCritere.Size = new System.Drawing.Size(173, 32);
            this.imgBtnAddCritere.TabIndex = 56;
            this.imgBtnAddCritere.Text = "Ajout d\'un critère";
            this.imgBtnAddCritere.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imgBtnAddCritere.UseVisualStyleBackColor = false;
            this.imgBtnAddCritere.Visible = false;
            this.imgBtnAddCritere.Click += new System.EventHandler(this.imageButton2_Click);
            // 
            // imgBtnDelCritere
            // 
            this.imgBtnDelCritere.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.imgBtnDelCritere.CenterColor = System.Drawing.Color.White;
            this.imgBtnDelCritere.FocusDrawn = false;
            this.imgBtnDelCritere.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.imgBtnDelCritere.ForeColor = System.Drawing.Color.Yellow;
            this.imgBtnDelCritere.Location = new System.Drawing.Point(189, 370);
            this.imgBtnDelCritere.Margin = new System.Windows.Forms.Padding(4);
            this.imgBtnDelCritere.Name = "imgBtnDelCritere";
            this.imgBtnDelCritere.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.imgBtnDelCritere.RecessDepth = 0;
            this.imgBtnDelCritere.Round = 10;
            this.imgBtnDelCritere.Size = new System.Drawing.Size(173, 32);
            this.imgBtnDelCritere.TabIndex = 57;
            this.imgBtnDelCritere.Text = "Effacer un critère";
            this.imgBtnDelCritere.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imgBtnDelCritere.UseVisualStyleBackColor = false;
            this.imgBtnDelCritere.Visible = false;
            this.imgBtnDelCritere.Click += new System.EventHandler(this.imageButton3_Click);
            // 
            // imgbtnSample
            // 
            this.imgbtnSample.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.imgbtnSample.CenterColor = System.Drawing.Color.White;
            this.imgbtnSample.FocusDrawn = false;
            this.imgbtnSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.imgbtnSample.ForeColor = System.Drawing.Color.Black;
            this.imgbtnSample.Location = new System.Drawing.Point(719, 370);
            this.imgbtnSample.Margin = new System.Windows.Forms.Padding(4);
            this.imgbtnSample.Name = "imgbtnSample";
            this.imgbtnSample.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.imgbtnSample.RecessDepth = 0;
            this.imgbtnSample.Round = 10;
            this.imgbtnSample.Size = new System.Drawing.Size(173, 32);
            this.imgbtnSample.TabIndex = 58;
            this.imgbtnSample.Text = "Exemples";
            this.imgbtnSample.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imgbtnSample.UseVisualStyleBackColor = false;
            this.imgbtnSample.Visible = false;
            this.imgbtnSample.Click += new System.EventHandler(this.imageButton4_Click);
            // 
            // imgBtnExpertMode
            // 
            this.imgBtnExpertMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.imgBtnExpertMode.CenterColor = System.Drawing.Color.White;
            this.imgBtnExpertMode.FocusDrawn = false;
            this.imgBtnExpertMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.imgBtnExpertMode.ForeColor = System.Drawing.Color.Black;
            this.imgBtnExpertMode.Location = new System.Drawing.Point(370, 370);
            this.imgBtnExpertMode.Margin = new System.Windows.Forms.Padding(4);
            this.imgBtnExpertMode.Name = "imgBtnExpertMode";
            this.imgBtnExpertMode.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.imgBtnExpertMode.RecessDepth = 0;
            this.imgBtnExpertMode.Round = 10;
            this.imgBtnExpertMode.Size = new System.Drawing.Size(173, 32);
            this.imgBtnExpertMode.TabIndex = 59;
            this.imgBtnExpertMode.Text = "Mode Expert";
            this.imgBtnExpertMode.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imgBtnExpertMode.UseVisualStyleBackColor = false;
            this.imgBtnExpertMode.Click += new System.EventHandler(this.imgExpertMode_Click);
            // 
            // imgbtnCancel2
            // 
            this.imgbtnCancel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(200)))), ((int)(((byte)(128)))), ((int)(((byte)(68)))));
            this.imgbtnCancel2.CenterColor = System.Drawing.Color.White;
            this.imgbtnCancel2.FocusDrawn = false;
            this.imgbtnCancel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgbtnCancel2.ForeColor = System.Drawing.Color.Black;
            this.imgbtnCancel2.Location = new System.Drawing.Point(656, 410);
            this.imgbtnCancel2.Margin = new System.Windows.Forms.Padding(4);
            this.imgbtnCancel2.Name = "imgbtnCancel2";
            this.imgbtnCancel2.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.imgbtnCancel2.RecessDepth = 0;
            this.imgbtnCancel2.Round = 10;
            this.imgbtnCancel2.Size = new System.Drawing.Size(237, 36);
            this.imgbtnCancel2.TabIndex = 61;
            this.imgbtnCancel2.Text = "Annulez";
            this.imgbtnCancel2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imgbtnCancel2.UseVisualStyleBackColor = false;
            this.imgbtnCancel2.Click += new System.EventHandler(this.imageButton1_Click);
            // 
            // imgbtnSave2
            // 
            this.imgbtnSave2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(128)))), ((int)(((byte)(200)))), ((int)(((byte)(68)))));
            this.imgbtnSave2.CenterColor = System.Drawing.Color.White;
            this.imgbtnSave2.FocusDrawn = false;
            this.imgbtnSave2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imgbtnSave2.ForeColor = System.Drawing.Color.Black;
            this.imgbtnSave2.Location = new System.Drawing.Point(12, 410);
            this.imgbtnSave2.Margin = new System.Windows.Forms.Padding(4);
            this.imgbtnSave2.Name = "imgbtnSave2";
            this.imgbtnSave2.OverColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.imgbtnSave2.RecessDepth = 0;
            this.imgbtnSave2.Round = 10;
            this.imgbtnSave2.Size = new System.Drawing.Size(237, 36);
            this.imgbtnSave2.TabIndex = 60;
            this.imgbtnSave2.Text = "Sauvegarde";
            this.imgbtnSave2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.imgbtnSave2.UseVisualStyleBackColor = false;
            this.imgbtnSave2.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TrainingConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imgbtnCancel2);
            this.Controls.Add(this.imgbtnSave2);
            this.Controls.Add(this.imgBtnExpertMode);
            this.Controls.Add(this.imgbtnSample);
            this.Controls.Add(this.imgBtnDelCritere);
            this.Controls.Add(this.imgBtnAddCritere);
            this.Controls.Add(this.dgvRegEx);
            this.Controls.Add(this.imgbtnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupParams);
            this.Controls.Add(this.groupConfig);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TrainingConfig";
            this.Size = new System.Drawing.Size(907, 674);
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).EndInit();
            this.groupParams.ResumeLayout(false);
            this.groupParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingParams)).EndInit();
            this.groupConfig.ResumeLayout(false);
            this.groupConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegEx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupParams;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbTypeOfPlay;
        private System.Windows.Forms.TextBox tbMax;
        private System.Windows.Forms.TextBox txtChrono;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox cbAffichage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbAffPoss;
        private System.Windows.Forms.CheckBox cbPause;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupConfig;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtJokers;
        private System.Windows.Forms.ComboBox cbBase;
        private System.Windows.Forms.TextBox txtMaxLetters;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPossibilitiesMax;
        private System.Windows.Forms.TextBox txtGameName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWithoutLetters;
        private System.Windows.Forms.CheckBox cbJoker;
        private System.Windows.Forms.Label lblNa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMinLetters;
        private System.Windows.Forms.TextBox txtWithLetters;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPossibilities;
        private System.Windows.Forms.TextBox txtIncludeLetters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource bindingConfig;
        private Desktop.Controls.ImageButton btnSave;
        private System.Windows.Forms.BindingSource bindingParams;
        private System.Windows.Forms.BindingSource criteriaBindingSource;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private Desktop.Controls.ImageButton imgbtnCancel;
        private System.Windows.Forms.DataGridView dgvRegEx;
        private System.Windows.Forms.Label lblInfoRegEx;
        private System.Windows.Forms.CheckBox chkAllCritere;
        private System.Windows.Forms.DataGridViewTextBoxColumn inclusionRegExDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn inclusionRegExCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exclusionRegExDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exclusionRegExCountCountDataGridViewTextBoxColumn;
        private Desktop.Controls.ImageButton imgBtnAddCritere;
        private Desktop.Controls.ImageButton imgBtnDelCritere;
        private Desktop.Controls.ImageButton imgbtnSample;
        private Desktop.Controls.ImageButton imgBtnExpertMode;
        private Desktop.Controls.ImageButton imgbtnCancel2;
        private Desktop.Controls.ImageButton imgbtnSave2;
    }
}
