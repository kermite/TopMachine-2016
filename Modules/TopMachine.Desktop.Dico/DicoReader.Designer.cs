using TopMachine.Desktop.Controls.Tools;
namespace TopMachine.Desktop.Dico
{
    partial class DicoReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DicoReader));
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLenMax = new System.Windows.Forms.NumericUpDown();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtLenMin = new System.Windows.Forms.NumericUpDown();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlConfig = new System.Windows.Forms.Panel();
            this.chkNew = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.chkVisible = new System.Windows.Forms.CheckBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.prg = new TopMachine.Desktop.Controls.Tools.Progress();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLenMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLenMin)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.pnlConfig.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtLenMax
            // 
            resources.ApplyResources(this.txtLenMax, "txtLenMax");
            this.txtLenMax.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.txtLenMax.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtLenMax.Name = "txtLenMax";
            this.txtLenMax.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Name = "btnNext";
            this.btnNext.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtLenMin
            // 
            resources.ApplyResources(this.txtLenMin, "txtLenMin");
            this.txtLenMin.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.txtLenMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtLenMin.Name = "txtLenMin";
            this.txtLenMin.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.btnNext);
            this.pnlControl.Controls.Add(this.btnSearch);
            resources.ApplyResources(this.pnlControl, "pnlControl");
            this.pnlControl.Name = "pnlControl";
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlConfig
            // 
            resources.ApplyResources(this.pnlConfig, "pnlConfig");
            this.pnlConfig.BackColor = System.Drawing.Color.White;
            this.pnlConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlConfig.Controls.Add(this.chkNew);
            this.pnlConfig.Controls.Add(this.numericUpDown1);
            this.pnlConfig.Controls.Add(this.label4);
            this.pnlConfig.Controls.Add(this.txtLenMax);
            this.pnlConfig.Controls.Add(this.txtLenMin);
            this.pnlConfig.Controls.Add(this.label3);
            this.pnlConfig.Controls.Add(this.btnStart);
            this.pnlConfig.Controls.Add(this.chkVisible);
            this.pnlConfig.Controls.Add(this.txtEnd);
            this.pnlConfig.Controls.Add(this.label2);
            this.pnlConfig.Controls.Add(this.txtStart);
            this.pnlConfig.Controls.Add(this.label1);
            this.pnlConfig.Name = "pnlConfig";
            // 
            // chkNew
            // 
            resources.ApplyResources(this.chkNew, "chkNew");
            this.chkNew.Name = "chkNew";
            this.chkNew.ThreeState = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkVisible
            // 
            resources.ApplyResources(this.chkVisible, "chkVisible");
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.ThreeState = true;
            // 
            // txtEnd
            // 
            resources.ApplyResources(this.txtEnd, "txtEnd");
            this.txtEnd.Name = "txtEnd";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtStart
            // 
            resources.ApplyResources(this.txtStart, "txtStart");
            this.txtStart.Name = "txtStart";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pnlContent
            // 
            resources.ApplyResources(this.pnlContent, "pnlContent");
            this.pnlContent.Controls.Add(this.flowLayoutPanel1);
            this.pnlContent.Name = "pnlContent";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // prg
            // 
            this.prg.BackColor = System.Drawing.Color.SandyBrown;
            this.prg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.prg, "prg");
            this.prg.Name = "prg";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.pnlConfig);
            this.panel1.Controls.Add(this.pnlContent);
            this.panel1.Controls.Add(this.pnlControl);
            this.panel1.Name = "panel1";
            // 
            // DicoReader
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.prg);
            this.Controls.Add(this.panel1);
            this.Name = "DicoReader";
            this.Resize += new System.EventHandler(this.DicoReader_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLenMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLenMin)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlConfig.ResumeLayout(false);
            this.pnlConfig.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtLenMax;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.NumericUpDown txtLenMin;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel pnlConfig;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox chkVisible;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Progress prg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox chkNew;
    }
}
