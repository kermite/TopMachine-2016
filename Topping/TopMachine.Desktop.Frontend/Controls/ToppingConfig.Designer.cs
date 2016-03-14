using CMWA.Packager.Tools;
using TopMachine.Desktop.Controls.Tools;
using TopMachine.Desktop.Overall;
namespace TopMachine.Topping.Frontend.Controls
{
    partial class ToppingConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToppingConfig));
            this.label1 = new System.Windows.Forms.Label();
            this.minValue = new System.Windows.Forms.NumericUpDown();
            this.bindingConfig = new System.Windows.Forms.BindingSource(this.components);
            this.chkJoker = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.minutesValue = new System.Windows.Forms.NumericUpDown();
            this.SecondsValue = new System.Windows.Forms.NumericUpDown();
            this.maxValue = new System.Windows.Forms.NumericUpDown();
            this.lblTirage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkTopping = new System.Windows.Forms.CheckBox();
            this.chkExplosive = new System.Windows.Forms.CheckBox();
            this.lblNom = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDico = new System.Windows.Forms.ComboBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbGrid = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.minValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondsValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // minValue
            // 
            this.minValue.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingConfig, "MinLetters", true));
            resources.ApplyResources(this.minValue, "minValue");
            this.minValue.Name = "minValue";
            this.minValue.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // bindingConfig
            // 
            this.bindingConfig.AllowNew = false;
            this.bindingConfig.DataSource = typeof(TopMachine.Topping.Dto.ConfigGameDto);
            // 
            // chkJoker
            // 
            this.chkJoker.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingConfig, "Joker", true));
            resources.ApplyResources(this.chkJoker, "chkJoker");
            this.chkJoker.Name = "chkJoker";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // minutesValue
            // 
            this.minutesValue.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingConfig, "Minutes", true));
            resources.ApplyResources(this.minutesValue, "minutesValue");
            this.minutesValue.Name = "minutesValue";
            this.minutesValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SecondsValue
            // 
            this.SecondsValue.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingConfig, "Seconds", true));
            resources.ApplyResources(this.SecondsValue, "SecondsValue");
            this.SecondsValue.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.SecondsValue.Name = "SecondsValue";
            this.SecondsValue.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // maxValue
            // 
            this.maxValue.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingConfig, "MaxLetters", true));
            resources.ApplyResources(this.maxValue, "maxValue");
            this.maxValue.Name = "maxValue";
            this.maxValue.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // lblTirage
            // 
            resources.ApplyResources(this.lblTirage, "lblTirage");
            this.lblTirage.Name = "lblTirage";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkTopping
            // 
            this.chkTopping.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingConfig, "Toping", true));
            resources.ApplyResources(this.chkTopping, "chkTopping");
            this.chkTopping.Name = "chkTopping";
            // 
            // chkExplosive
            // 
            this.chkExplosive.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingConfig, "Explosive", true));
            resources.ApplyResources(this.chkExplosive, "chkExplosive");
            this.chkExplosive.Name = "chkExplosive";
            // 
            // lblNom
            // 
            resources.ApplyResources(this.lblNom, "lblNom");
            this.lblNom.Name = "lblNom";
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingConfig, "Name", true));
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // cbDico
            // 
            this.cbDico.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingConfig, "Language", true));
            this.cbDico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDico.FormattingEnabled = true;
            resources.ApplyResources(this.cbDico, "cbDico");
            this.cbDico.Name = "cbDico";
            // 
            // cbType
            // 
            this.cbType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingConfig, "TypeOfGame", true));
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            resources.ApplyResources(this.cbType, "cbType");
            this.cbType.Name = "cbType";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.minValue);
            this.groupBox1.Controls.Add(this.chkJoker);
            this.groupBox1.Controls.Add(this.minutesValue);
            this.groupBox1.Controls.Add(this.SecondsValue);
            this.groupBox1.Controls.Add(this.maxValue);
            this.groupBox1.Controls.Add(this.lblTirage);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkExplosive);
            this.groupBox1.Controls.Add(this.chkTopping);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBox4
            // 
            this.checkBox4.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingConfig, "Manual", true));
            resources.ApplyResources(this.checkBox4, "checkBox4");
            this.checkBox4.Name = "checkBox4";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingConfig, "ExplosiveRating", true));
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.checkBox1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // checkBox3
            // 
            this.checkBox3.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingConfig, "HistoryOverall", true));
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            // 
            // checkBox2
            // 
            this.checkBox2.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingConfig, "HistoryGame", true));
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            // 
            // checkBox1
            // 
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingConfig, "HistoryDetail", true));
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbGrid
            // 
            this.cbGrid.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bindingConfig, "Grid", true));
            this.cbGrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGrid.FormattingEnabled = true;
            resources.ApplyResources(this.cbGrid, "cbGrid");
            this.cbGrid.Name = "cbGrid";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ToppingConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cbGrid);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.cbDico);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.button1);
            resources.ApplyResources(this, "$this");
            this.Name = "ToppingConfig";
            ((System.ComponentModel.ISupportInitialize)(this.minValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minutesValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondsValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown minValue;
        private System.Windows.Forms.CheckBox chkJoker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown minutesValue;
        private System.Windows.Forms.NumericUpDown SecondsValue;
        private System.Windows.Forms.NumericUpDown maxValue;
        private System.Windows.Forms.Label lblTirage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource bindingConfig;
        private System.Windows.Forms.CheckBox chkTopping;
        private System.Windows.Forms.CheckBox chkExplosive;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbDico;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox cbGrid;
        private System.Windows.Forms.Button button2;
    }
}
