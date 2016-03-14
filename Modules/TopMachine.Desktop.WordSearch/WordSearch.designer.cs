using CMWA.Packager.Tools;
using TopMachine.Desktop.Controls.Tools;
namespace TopMachine.Desktop.Search
{
    partial class WordSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordSearch));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnXplus1 = new System.Windows.Forms.Button();
            this.btnPlusPetit = new System.Windows.Forms.Button();
            this.btnPlusGrand = new System.Windows.Forms.Button();
            this.btnContient = new System.Windows.Forms.Button();
            this.btnPlusouMoins = new System.Windows.Forms.Button();
            this.btnRegulier = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.wordContainerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listSolutions = new System.Windows.Forms.ListView();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.progress1 = new TopMachine.Desktop.Controls.Tools.Progress();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wordContainerBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            // 
            // btnXplus1
            // 
            resources.ApplyResources(this.btnXplus1, "btnXplus1");
            this.btnXplus1.ForeColor = System.Drawing.Color.White;
            this.btnXplus1.Name = "btnXplus1";
            this.btnXplus1.Click += new System.EventHandler(this.btnXplus1_Click);
            // 
            // btnPlusPetit
            // 
            resources.ApplyResources(this.btnPlusPetit, "btnPlusPetit");
            this.btnPlusPetit.ForeColor = System.Drawing.Color.White;
            this.btnPlusPetit.Name = "btnPlusPetit";
            this.btnPlusPetit.Click += new System.EventHandler(this.btnPlusPetit_Click);
            // 
            // btnPlusGrand
            // 
            resources.ApplyResources(this.btnPlusGrand, "btnPlusGrand");
            this.btnPlusGrand.ForeColor = System.Drawing.Color.White;
            this.btnPlusGrand.Name = "btnPlusGrand";
            this.btnPlusGrand.Click += new System.EventHandler(this.btnPlusGrand_Click);
            // 
            // btnContient
            // 
            resources.ApplyResources(this.btnContient, "btnContient");
            this.btnContient.ForeColor = System.Drawing.Color.White;
            this.btnContient.Name = "btnContient";
            this.btnContient.Click += new System.EventHandler(this.btnContient_Click);
            // 
            // btnPlusouMoins
            // 
            resources.ApplyResources(this.btnPlusouMoins, "btnPlusouMoins");
            this.btnPlusouMoins.ForeColor = System.Drawing.Color.White;
            this.btnPlusouMoins.Name = "btnPlusouMoins";
            this.btnPlusouMoins.Click += new System.EventHandler(this.btnPlusouMoins_Click);
            // 
            // btnRegulier
            // 
            resources.ApplyResources(this.btnRegulier, "btnRegulier");
            this.btnRegulier.ForeColor = System.Drawing.Color.White;
            this.btnRegulier.Name = "btnRegulier";
            this.btnRegulier.Click += new System.EventHandler(this.btnRegulier_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.btnXplus1);
            this.panel1.Controls.Add(this.btnRegulier);
            this.panel1.Controls.Add(this.btnPlusPetit);
            this.panel1.Controls.Add(this.btnPlusouMoins);
            this.panel1.Controls.Add(this.btnPlusGrand);
            this.panel1.Controls.Add(this.btnContient);
            this.panel1.Name = "panel1";
            // 
            // listSolutions
            // 
            this.listSolutions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            resources.ApplyResources(this.listSolutions, "listSolutions");
            this.listSolutions.FullRowSelect = true;
            this.listSolutions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listSolutions.HideSelection = false;
            this.listSolutions.Name = "listSolutions";
            this.listSolutions.UseCompatibleStateImageBehavior = false;
            this.listSolutions.View = System.Windows.Forms.View.Details;
            this.listSolutions.SelectedIndexChanged += new System.EventHandler(this.listSolutions_SelectedIndexChanged);
            // 
            // pnlContent
            // 
            resources.ApplyResources(this.pnlContent, "pnlContent");
            this.pnlContent.Name = "pnlContent";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listSolutions);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.txtSearch);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // progress1
            // 
            this.progress1.BackColor = System.Drawing.Color.SandyBrown;
            this.progress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.progress1, "progress1");
            this.progress1.Name = "progress1";
            // 
            // WordSearch
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progress1);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.panel2);
            this.Name = "WordSearch";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wordContainerBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnXplus1;
        private System.Windows.Forms.Button btnPlusPetit;
        private System.Windows.Forms.Button btnPlusGrand;
        private System.Windows.Forms.Button btnContient;
        private System.Windows.Forms.Button btnPlusouMoins;
        private System.Windows.Forms.Button btnRegulier;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource wordContainerBindingSource;
        private System.Windows.Forms.ListView listSolutions;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel panel2;
        private Progress progress1;
    }
}

