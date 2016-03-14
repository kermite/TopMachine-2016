namespace TopMachine.Training.FrontEnd
{
    partial class TrainingGame
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

            if (la != null)
            {
                la.Dispose();
            }

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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbScoreTP = new System.Windows.Forms.Label();
            this.lbScoreMP = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbScoreT = new System.Windows.Forms.Label();
            this.lbScoreM = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNbWordsNJP = new System.Windows.Forms.Label();
            this.lblNbWordsIP = new System.Windows.Forms.Label();
            this.lblNbWordsPP = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblNbWordsT = new System.Windows.Forms.Label();
            this.lblNbWordsI = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblNbWords = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblNbWordsNJ = new System.Windows.Forms.Label();
            this.lblNbWordsTP = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblNbWordsP = new System.Windows.Forms.Label();
            this.roundPanel1 = new TopMachine.Desktop.Controls.RoundPanel();
            this.roundPanel2 = new TopMachine.Desktop.Controls.RoundPanel();
            this.listWords = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReview = new TopMachine.Desktop.Controls.ImageButton();
            this.btnStop = new TopMachine.Desktop.Controls.ImageButton();
            this.btnNewGame = new TopMachine.Desktop.Controls.ImageButton();
            this.roundPanel3 = new TopMachine.Desktop.Controls.RoundPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dicoPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.listSolutions = new System.Windows.Forms.ListView();
            this.solutionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblWordRack = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblWordTotal = new System.Windows.Forms.Label();
            this.lblWordFound = new System.Windows.Forms.Label();
            this.lblWordLost = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageButton6 = new TopMachine.Desktop.Controls.ImageButton();
            this.imageButton5 = new TopMachine.Desktop.Controls.ImageButton();
            this.imageButton4 = new TopMachine.Desktop.Controls.ImageButton();
            this.imageButton2 = new TopMachine.Desktop.Controls.ImageButton();
            this.imageButton3 = new TopMachine.Desktop.Controls.ImageButton();
            this.chevalet1 = new TopMachine.Training.FrontEnd.Controls.WordPlayer();
            this.btnSnapshot = new TopMachine.Desktop.Controls.ImageButton();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.roundPanel1.SuspendLayout();
            this.roundPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.roundPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbScoreTP, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lbScoreMP, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbScoreT, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbScoreM, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 7);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(284, 90);
            this.tableLayoutPanel2.TabIndex = 136;
            // 
            // lbScoreTP
            // 
            this.lbScoreTP.BackColor = System.Drawing.Color.Transparent;
            this.lbScoreTP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbScoreTP.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScoreTP.ForeColor = System.Drawing.Color.White;
            this.lbScoreTP.Location = new System.Drawing.Point(5, 64);
            this.lbScoreTP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbScoreTP.Name = "lbScoreTP";
            this.lbScoreTP.Size = new System.Drawing.Size(132, 25);
            this.lbScoreTP.TabIndex = 123;
            this.lbScoreTP.Text = "0%";
            this.lbScoreTP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbScoreMP
            // 
            this.lbScoreMP.BackColor = System.Drawing.Color.Transparent;
            this.lbScoreMP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbScoreMP.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScoreMP.ForeColor = System.Drawing.Color.White;
            this.lbScoreMP.Location = new System.Drawing.Point(146, 64);
            this.lbScoreMP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbScoreMP.Name = "lbScoreMP";
            this.lbScoreMP.Size = new System.Drawing.Size(133, 25);
            this.lbScoreMP.TabIndex = 120;
            this.lbScoreMP.Text = "0%";
            this.lbScoreMP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Khaki;
            this.tableLayoutPanel2.SetColumnSpan(this.label3, 2);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(282, 20);
            this.label3.TabIndex = 113;
            this.label3.Text = "Scores";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(5, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 20);
            this.label4.TabIndex = 114;
            this.label4.Text = "Trouvés";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(146, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 20);
            this.label5.TabIndex = 115;
            this.label5.Text = "Perdus";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbScoreT
            // 
            this.lbScoreT.BackColor = System.Drawing.Color.Transparent;
            this.lbScoreT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbScoreT.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScoreT.ForeColor = System.Drawing.Color.White;
            this.lbScoreT.Location = new System.Drawing.Point(5, 43);
            this.lbScoreT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbScoreT.Name = "lbScoreT";
            this.lbScoreT.Size = new System.Drawing.Size(132, 20);
            this.lbScoreT.TabIndex = 118;
            this.lbScoreT.Text = "0";
            this.lbScoreT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbScoreM
            // 
            this.lbScoreM.BackColor = System.Drawing.Color.Transparent;
            this.lbScoreM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbScoreM.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScoreM.ForeColor = System.Drawing.Color.White;
            this.lbScoreM.Location = new System.Drawing.Point(146, 43);
            this.lbScoreM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbScoreM.Name = "lbScoreM";
            this.lbScoreM.Size = new System.Drawing.Size(133, 20);
            this.lbScoreM.TabIndex = 121;
            this.lbScoreM.Text = "0";
            this.lbScoreM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblNbWordsNJP, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNbWordsIP, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblNbWordsPP, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTotal, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNbWordsT, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblNbWordsI, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label19, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNbWords, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label17, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label18, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblNbWordsNJ, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblNbWordsTP, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label20, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblNbWordsP, 2, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 103);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 89);
            this.tableLayoutPanel1.TabIndex = 135;
            // 
            // lblNbWordsNJP
            // 
            this.lblNbWordsNJP.BackColor = System.Drawing.Color.Transparent;
            this.lblNbWordsNJP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNbWordsNJP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.lblNbWordsNJP.ForeColor = System.Drawing.Color.White;
            this.lblNbWordsNJP.Location = new System.Drawing.Point(213, 1);
            this.lblNbWordsNJP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNbWordsNJP.Name = "lblNbWordsNJP";
            this.lblNbWordsNJP.Size = new System.Drawing.Size(71, 20);
            this.lblNbWordsNJP.TabIndex = 120;
            this.lblNbWordsNJP.Text = "100%";
            this.lblNbWordsNJP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNbWordsIP
            // 
            this.lblNbWordsIP.BackColor = System.Drawing.Color.Transparent;
            this.lblNbWordsIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNbWordsIP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.lblNbWordsIP.ForeColor = System.Drawing.Color.White;
            this.lblNbWordsIP.Location = new System.Drawing.Point(213, 64);
            this.lblNbWordsIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNbWordsIP.Name = "lblNbWordsIP";
            this.lblNbWordsIP.Size = new System.Drawing.Size(71, 25);
            this.lblNbWordsIP.TabIndex = 125;
            this.lblNbWordsIP.Text = "100%";
            this.lblNbWordsIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNbWordsPP
            // 
            this.lblNbWordsPP.BackColor = System.Drawing.Color.Transparent;
            this.lblNbWordsPP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNbWordsPP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.lblNbWordsPP.ForeColor = System.Drawing.Color.White;
            this.lblNbWordsPP.Location = new System.Drawing.Point(213, 43);
            this.lblNbWordsPP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNbWordsPP.Name = "lblNbWordsPP";
            this.lblNbWordsPP.Size = new System.Drawing.Size(71, 20);
            this.lblNbWordsPP.TabIndex = 122;
            this.lblNbWordsPP.Text = "100%";
            this.lblNbWordsPP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(5, 1);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(53, 20);
            this.lblTotal.TabIndex = 112;
            this.lblTotal.Text = " Mots";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNbWordsT
            // 
            this.lblNbWordsT.BackColor = System.Drawing.Color.Transparent;
            this.lblNbWordsT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNbWordsT.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.lblNbWordsT.ForeColor = System.Drawing.Color.White;
            this.lblNbWordsT.Location = new System.Drawing.Point(151, 22);
            this.lblNbWordsT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNbWordsT.Name = "lblNbWordsT";
            this.lblNbWordsT.Size = new System.Drawing.Size(53, 20);
            this.lblNbWordsT.TabIndex = 118;
            this.lblNbWordsT.Text = "20000";
            this.lblNbWordsT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNbWordsI
            // 
            this.lblNbWordsI.BackColor = System.Drawing.Color.Transparent;
            this.lblNbWordsI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNbWordsI.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.lblNbWordsI.ForeColor = System.Drawing.Color.White;
            this.lblNbWordsI.Location = new System.Drawing.Point(151, 64);
            this.lblNbWordsI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNbWordsI.Name = "lblNbWordsI";
            this.lblNbWordsI.Size = new System.Drawing.Size(53, 25);
            this.lblNbWordsI.TabIndex = 124;
            this.lblNbWordsI.Text = "20000";
            this.lblNbWordsI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label19.Location = new System.Drawing.Point(67, 1);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 20);
            this.label19.TabIndex = 114;
            this.label19.Text = "Non joués";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNbWords
            // 
            this.lblNbWords.BackColor = System.Drawing.Color.Transparent;
            this.lblNbWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNbWords.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.lblNbWords.ForeColor = System.Drawing.Color.White;
            this.lblNbWords.Location = new System.Drawing.Point(5, 22);
            this.lblNbWords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNbWords.Name = "lblNbWords";
            this.lblNbWords.Size = new System.Drawing.Size(53, 20);
            this.lblNbWords.TabIndex = 116;
            this.lblNbWords.Text = "20000";
            this.lblNbWords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label17.Location = new System.Drawing.Point(67, 64);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(75, 25);
            this.label17.TabIndex = 123;
            this.label17.Text = " Isolés";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label18.Location = new System.Drawing.Point(67, 22);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(75, 20);
            this.label18.TabIndex = 113;
            this.label18.Text = " Trouvés";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNbWordsNJ
            // 
            this.lblNbWordsNJ.BackColor = System.Drawing.Color.Transparent;
            this.lblNbWordsNJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNbWordsNJ.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.lblNbWordsNJ.ForeColor = System.Drawing.Color.White;
            this.lblNbWordsNJ.Location = new System.Drawing.Point(151, 1);
            this.lblNbWordsNJ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNbWordsNJ.Name = "lblNbWordsNJ";
            this.lblNbWordsNJ.Size = new System.Drawing.Size(53, 20);
            this.lblNbWordsNJ.TabIndex = 117;
            this.lblNbWordsNJ.Text = "20000";
            this.lblNbWordsNJ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNbWordsTP
            // 
            this.lblNbWordsTP.BackColor = System.Drawing.Color.Transparent;
            this.lblNbWordsTP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNbWordsTP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.lblNbWordsTP.ForeColor = System.Drawing.Color.White;
            this.lblNbWordsTP.Location = new System.Drawing.Point(213, 22);
            this.lblNbWordsTP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNbWordsTP.Name = "lblNbWordsTP";
            this.lblNbWordsTP.Size = new System.Drawing.Size(71, 20);
            this.lblNbWordsTP.TabIndex = 121;
            this.lblNbWordsTP.Text = "100%";
            this.lblNbWordsTP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label20.Location = new System.Drawing.Point(67, 43);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 20);
            this.label20.TabIndex = 115;
            this.label20.Text = " Perdus";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNbWordsP
            // 
            this.lblNbWordsP.BackColor = System.Drawing.Color.Transparent;
            this.lblNbWordsP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNbWordsP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.lblNbWordsP.ForeColor = System.Drawing.Color.White;
            this.lblNbWordsP.Location = new System.Drawing.Point(151, 43);
            this.lblNbWordsP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNbWordsP.Name = "lblNbWordsP";
            this.lblNbWordsP.Size = new System.Drawing.Size(53, 20);
            this.lblNbWordsP.TabIndex = 119;
            this.lblNbWordsP.Text = "20000";
            this.lblNbWordsP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // roundPanel1
            // 
            this.roundPanel1.CenterColor = System.Drawing.Color.White;
            this.roundPanel1.Controls.Add(this.tableLayoutPanel2);
            this.roundPanel1.Controls.Add(this.tableLayoutPanel1);
            this.roundPanel1.FocusDrawn = false;
            this.roundPanel1.ForeColor = System.Drawing.Color.White;
            this.roundPanel1.Location = new System.Drawing.Point(4, 4);
            this.roundPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.OverColor = System.Drawing.Color.Transparent;
            this.roundPanel1.RecessDepth = 0;
            this.roundPanel1.Round = 0;
            this.roundPanel1.Size = new System.Drawing.Size(300, 198);
            this.roundPanel1.TabIndex = 137;
            this.roundPanel1.Text = null;
            // 
            // roundPanel2
            // 
            this.roundPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.roundPanel2.CenterColor = System.Drawing.Color.White;
            this.roundPanel2.Controls.Add(this.listWords);
            this.roundPanel2.FocusDrawn = false;
            this.roundPanel2.Location = new System.Drawing.Point(8, 209);
            this.roundPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.OverColor = System.Drawing.Color.Transparent;
            this.roundPanel2.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.roundPanel2.RecessDepth = 0;
            this.roundPanel2.Round = 0;
            this.roundPanel2.Size = new System.Drawing.Size(296, 469);
            this.roundPanel2.TabIndex = 138;
            this.roundPanel2.Text = null;
            // 
            // listWords
            // 
            this.listWords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listWords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listWords.Location = new System.Drawing.Point(7, 6);
            this.listWords.Margin = new System.Windows.Forms.Padding(4);
            this.listWords.Name = "listWords";
            this.listWords.Size = new System.Drawing.Size(282, 457);
            this.listWords.TabIndex = 0;
            this.listWords.UseCompatibleStateImageBehavior = false;
            this.listWords.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Derniers tirages";
            this.columnHeader1.Width = 600;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnSnapshot);
            this.panel1.Controls.Add(this.btnReview);
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnNewGame);
            this.panel1.Location = new System.Drawing.Point(311, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 44);
            this.panel1.TabIndex = 139;
            // 
            // btnReview
            // 
            this.btnReview.AutoSize = true;
            this.btnReview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnReview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReview.CenterColor = System.Drawing.Color.White;
            this.btnReview.Enabled = false;
            this.btnReview.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnReview.FlatAppearance.BorderSize = 0;
            this.btnReview.FocusDrawn = false;
            this.btnReview.ForeColor = System.Drawing.Color.LightGray;
            this.btnReview.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnReview.Location = new System.Drawing.Point(316, 4);
            this.btnReview.Margin = new System.Windows.Forms.Padding(5);
            this.btnReview.Name = "btnReview";
            this.btnReview.OverColor = System.Drawing.Color.Transparent;
            this.btnReview.RecessDepth = 0;
            this.btnReview.Round = 5;
            this.btnReview.Size = new System.Drawing.Size(152, 33);
            this.btnReview.TabIndex = 13;
            this.btnReview.Text = "Révision";
            this.btnReview.UseVisualStyleBackColor = false;
            this.btnReview.Click += new System.EventHandler(this.btnReview_Click);
            // 
            // btnStop
            // 
            this.btnStop.AutoSize = true;
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnStop.CenterColor = System.Drawing.Color.White;
            this.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnStop.FlatAppearance.BorderSize = 0;
            this.btnStop.FocusDrawn = false;
            this.btnStop.ForeColor = System.Drawing.Color.LightGray;
            this.btnStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStop.Location = new System.Drawing.Point(161, 4);
            this.btnStop.Margin = new System.Windows.Forms.Padding(5);
            this.btnStop.Name = "btnStop";
            this.btnStop.OverColor = System.Drawing.Color.Transparent;
            this.btnStop.RecessDepth = 0;
            this.btnStop.Round = 5;
            this.btnStop.Size = new System.Drawing.Size(152, 33);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.AutoSize = true;
            this.btnNewGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnNewGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNewGame.CenterColor = System.Drawing.Color.White;
            this.btnNewGame.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnNewGame.FlatAppearance.BorderSize = 0;
            this.btnNewGame.FocusDrawn = false;
            this.btnNewGame.ForeColor = System.Drawing.Color.LightGray;
            this.btnNewGame.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNewGame.Location = new System.Drawing.Point(4, 4);
            this.btnNewGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.OverColor = System.Drawing.Color.Transparent;
            this.btnNewGame.RecessDepth = 0;
            this.btnNewGame.Round = 5;
            this.btnNewGame.Size = new System.Drawing.Size(152, 33);
            this.btnNewGame.TabIndex = 11;
            this.btnNewGame.Text = "Nouvelle Partie";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // roundPanel3
            // 
            this.roundPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roundPanel3.CenterColor = System.Drawing.Color.White;
            this.roundPanel3.Controls.Add(this.splitContainer1);
            this.roundPanel3.Controls.Add(this.solutionsPanel);
            this.roundPanel3.Controls.Add(this.tableLayoutPanel3);
            this.roundPanel3.Controls.Add(this.panel2);
            this.roundPanel3.Controls.Add(this.chevalet1);
            this.roundPanel3.FocusDrawn = false;
            this.roundPanel3.Location = new System.Drawing.Point(311, 45);
            this.roundPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.roundPanel3.Name = "roundPanel3";
            this.roundPanel3.OverColor = System.Drawing.Color.Transparent;
            this.roundPanel3.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.roundPanel3.RecessDepth = 0;
            this.roundPanel3.Round = 0;
            this.roundPanel3.Size = new System.Drawing.Size(775, 632);
            this.roundPanel3.TabIndex = 140;
            this.roundPanel3.Text = null;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(7, 220);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dicoPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listSolutions);
            this.splitContainer1.Size = new System.Drawing.Size(761, 406);
            this.splitContainer1.SplitterDistance = 474;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 144;
            // 
            // dicoPanel
            // 
            this.dicoPanel.AutoScroll = true;
            this.dicoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dicoPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.dicoPanel.Location = new System.Drawing.Point(0, 0);
            this.dicoPanel.Margin = new System.Windows.Forms.Padding(4);
            this.dicoPanel.Name = "dicoPanel";
            this.dicoPanel.Size = new System.Drawing.Size(474, 406);
            this.dicoPanel.TabIndex = 143;
            // 
            // listSolutions
            // 
            this.listSolutions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listSolutions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSolutions.FullRowSelect = true;
            this.listSolutions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listSolutions.HideSelection = false;
            this.listSolutions.Location = new System.Drawing.Point(0, 0);
            this.listSolutions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listSolutions.Name = "listSolutions";
            this.listSolutions.Size = new System.Drawing.Size(282, 406);
            this.listSolutions.TabIndex = 12;
            this.listSolutions.UseCompatibleStateImageBehavior = false;
            this.listSolutions.View = System.Windows.Forms.View.Details;
            this.listSolutions.SelectedIndexChanged += new System.EventHandler(this.listSolutions_SelectedIndexChanged);
            // 
            // solutionsPanel
            // 
            this.solutionsPanel.AutoSize = true;
            this.solutionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.solutionsPanel.Location = new System.Drawing.Point(7, 183);
            this.solutionsPanel.Margin = new System.Windows.Forms.Padding(4);
            this.solutionsPanel.MinimumSize = new System.Drawing.Size(0, 37);
            this.solutionsPanel.Name = "solutionsPanel";
            this.solutionsPanel.Size = new System.Drawing.Size(761, 37);
            this.solutionsPanel.TabIndex = 142;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.lblWordRack, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblStatus, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblWordTotal, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblWordFound, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblWordLost, 2, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(7, 124);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(761, 59);
            this.tableLayoutPanel3.TabIndex = 141;
            // 
            // lblWordRack
            // 
            this.lblWordRack.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.lblWordRack, 2);
            this.lblWordRack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWordRack.Location = new System.Drawing.Point(257, 0);
            this.lblWordRack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWordRack.Name = "lblWordRack";
            this.lblWordRack.Size = new System.Drawing.Size(500, 26);
            this.lblWordRack.TabIndex = 4;
            this.lblWordRack.Text = "Tirage :";
            this.lblWordRack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Green;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(4, 0);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(245, 26);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWordTotal
            // 
            this.lblWordTotal.AutoSize = true;
            this.lblWordTotal.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblWordTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWordTotal.Location = new System.Drawing.Point(4, 26);
            this.lblWordTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWordTotal.Name = "lblWordTotal";
            this.lblWordTotal.Size = new System.Drawing.Size(245, 26);
            this.lblWordTotal.TabIndex = 0;
            this.lblWordTotal.Text = "Total";
            this.lblWordTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWordFound
            // 
            this.lblWordFound.AutoSize = true;
            this.lblWordFound.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblWordFound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWordFound.Location = new System.Drawing.Point(257, 26);
            this.lblWordFound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWordFound.Name = "lblWordFound";
            this.lblWordFound.Size = new System.Drawing.Size(245, 26);
            this.lblWordFound.TabIndex = 1;
            this.lblWordFound.Text = "Trouvés";
            this.lblWordFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWordLost
            // 
            this.lblWordLost.AutoSize = true;
            this.lblWordLost.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblWordLost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWordLost.Location = new System.Drawing.Point(510, 26);
            this.lblWordLost.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWordLost.Name = "lblWordLost";
            this.lblWordLost.Size = new System.Drawing.Size(247, 26);
            this.lblWordLost.TabIndex = 2;
            this.lblWordLost.Text = "Perdus";
            this.lblWordLost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.imageButton6);
            this.panel2.Controls.Add(this.imageButton5);
            this.panel2.Controls.Add(this.imageButton4);
            this.panel2.Controls.Add(this.imageButton2);
            this.panel2.Controls.Add(this.imageButton3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(7, 76);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(761, 48);
            this.panel2.TabIndex = 140;
            // 
            // imageButton6
            // 
            this.imageButton6.AutoSize = true;
            this.imageButton6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.imageButton6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imageButton6.CenterColor = System.Drawing.Color.White;
            this.imageButton6.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.imageButton6.FlatAppearance.BorderSize = 0;
            this.imageButton6.FocusDrawn = false;
            this.imageButton6.ForeColor = System.Drawing.Color.LightGray;
            this.imageButton6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imageButton6.Location = new System.Drawing.Point(787, 2);
            this.imageButton6.Margin = new System.Windows.Forms.Padding(5);
            this.imageButton6.Name = "imageButton6";
            this.imageButton6.OverColor = System.Drawing.Color.Transparent;
            this.imageButton6.RecessDepth = 0;
            this.imageButton6.Round = 5;
            this.imageButton6.Size = new System.Drawing.Size(171, 41);
            this.imageButton6.TabIndex = 15;
            this.imageButton6.Text = "melanger (F1)";
            this.imageButton6.UseVisualStyleBackColor = false;
            // 
            // imageButton5
            // 
            this.imageButton5.AutoSize = true;
            this.imageButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.imageButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imageButton5.CenterColor = System.Drawing.Color.White;
            this.imageButton5.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.imageButton5.FlatAppearance.BorderSize = 0;
            this.imageButton5.FocusDrawn = false;
            this.imageButton5.ForeColor = System.Drawing.Color.LightGray;
            this.imageButton5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imageButton5.Location = new System.Drawing.Point(4, 2);
            this.imageButton5.Margin = new System.Windows.Forms.Padding(4);
            this.imageButton5.Name = "imageButton5";
            this.imageButton5.OverColor = System.Drawing.Color.Transparent;
            this.imageButton5.RecessDepth = 0;
            this.imageButton5.Round = 5;
            this.imageButton5.Size = new System.Drawing.Size(141, 33);
            this.imageButton5.TabIndex = 14;
            this.imageButton5.Text = "Suivant (SPC)";
            this.imageButton5.UseVisualStyleBackColor = false;
            this.imageButton5.Click += new System.EventHandler(this.imageButton5_Click);
            // 
            // imageButton4
            // 
            this.imageButton4.AutoSize = true;
            this.imageButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.imageButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imageButton4.CenterColor = System.Drawing.Color.White;
            this.imageButton4.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.imageButton4.FlatAppearance.BorderSize = 0;
            this.imageButton4.FocusDrawn = false;
            this.imageButton4.ForeColor = System.Drawing.Color.LightGray;
            this.imageButton4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imageButton4.Location = new System.Drawing.Point(424, 2);
            this.imageButton4.Margin = new System.Windows.Forms.Padding(4);
            this.imageButton4.Name = "imageButton4";
            this.imageButton4.OverColor = System.Drawing.Color.Transparent;
            this.imageButton4.RecessDepth = 0;
            this.imageButton4.Round = 5;
            this.imageButton4.Size = new System.Drawing.Size(248, 33);
            this.imageButton4.TabIndex = 13;
            this.imageButton4.Text = "Isoler le dernier tirage (F6)";
            this.imageButton4.UseVisualStyleBackColor = false;
            this.imageButton4.Click += new System.EventHandler(this.imageButton4_Click);
            // 
            // imageButton2
            // 
            this.imageButton2.AutoSize = true;
            this.imageButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.imageButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imageButton2.CenterColor = System.Drawing.Color.White;
            this.imageButton2.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.imageButton2.FlatAppearance.BorderSize = 0;
            this.imageButton2.FocusDrawn = false;
            this.imageButton2.ForeColor = System.Drawing.Color.LightGray;
            this.imageButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imageButton2.Location = new System.Drawing.Point(269, 2);
            this.imageButton2.Margin = new System.Windows.Forms.Padding(4);
            this.imageButton2.Name = "imageButton2";
            this.imageButton2.OverColor = System.Drawing.Color.Transparent;
            this.imageButton2.RecessDepth = 0;
            this.imageButton2.Round = 5;
            this.imageButton2.Size = new System.Drawing.Size(155, 33);
            this.imageButton2.TabIndex = 12;
            this.imageButton2.Text = "Passer OK (F3)";
            this.imageButton2.UseVisualStyleBackColor = false;
            this.imageButton2.Click += new System.EventHandler(this.imageButton2_Click);
            // 
            // imageButton3
            // 
            this.imageButton3.AutoSize = true;
            this.imageButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.imageButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.imageButton3.CenterColor = System.Drawing.Color.White;
            this.imageButton3.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.imageButton3.FlatAppearance.BorderSize = 0;
            this.imageButton3.FocusDrawn = false;
            this.imageButton3.ForeColor = System.Drawing.Color.LightGray;
            this.imageButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.imageButton3.Location = new System.Drawing.Point(147, 2);
            this.imageButton3.Margin = new System.Windows.Forms.Padding(4);
            this.imageButton3.Name = "imageButton3";
            this.imageButton3.OverColor = System.Drawing.Color.Transparent;
            this.imageButton3.RecessDepth = 0;
            this.imageButton3.Round = 5;
            this.imageButton3.Size = new System.Drawing.Size(123, 33);
            this.imageButton3.TabIndex = 11;
            this.imageButton3.Text = "Passer (F2)";
            this.imageButton3.UseVisualStyleBackColor = false;
            this.imageButton3.Click += new System.EventHandler(this.imageButton3_Click);
            // 
            // chevalet1
            // 
            this.chevalet1.AllowDrop = true;
            this.chevalet1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chevalet1.Location = new System.Drawing.Point(7, 6);
            this.chevalet1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chevalet1.Name = "chevalet1";
            this.chevalet1.Size = new System.Drawing.Size(761, 70);
            this.chevalet1.TabIndex = 0;
            // 
            // btnSnapshot
            // 
            this.btnSnapshot.AutoSize = true;
            this.btnSnapshot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.btnSnapshot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSnapshot.CenterColor = System.Drawing.Color.White;
            this.btnSnapshot.Enabled = false;
            this.btnSnapshot.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnSnapshot.FlatAppearance.BorderSize = 0;
            this.btnSnapshot.FocusDrawn = false;
            this.btnSnapshot.ForeColor = System.Drawing.Color.LightGray;
            this.btnSnapshot.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSnapshot.Location = new System.Drawing.Point(478, 5);
            this.btnSnapshot.Margin = new System.Windows.Forms.Padding(5);
            this.btnSnapshot.Name = "btnSnapshot";
            this.btnSnapshot.OverColor = System.Drawing.Color.Transparent;
            this.btnSnapshot.RecessDepth = 0;
            this.btnSnapshot.Round = 5;
            this.btnSnapshot.Size = new System.Drawing.Size(123, 33);
            this.btnSnapshot.TabIndex = 14;
            this.btnSnapshot.Tag = "loadsnapshot";
            this.btnSnapshot.Text = "charger l\'etat";
            this.btnSnapshot.UseVisualStyleBackColor = false;
            this.btnSnapshot.Click += new System.EventHandler(this.btnSnapshot_Click);
            // 
            // TrainingGame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.roundPanel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.roundPanel2);
            this.Controls.Add(this.roundPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TrainingGame";
            this.Size = new System.Drawing.Size(1093, 686);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.roundPanel1.ResumeLayout(false);
            this.roundPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.roundPanel3.ResumeLayout(false);
            this.roundPanel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbScoreT;
        private System.Windows.Forms.Label lbScoreM;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblNbWordsNJP;
        private System.Windows.Forms.Label lblNbWordsIP;
        private System.Windows.Forms.Label lblNbWordsPP;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblNbWordsT;
        private System.Windows.Forms.Label lblNbWordsI;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblNbWords;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblNbWordsNJ;
        private System.Windows.Forms.Label lblNbWordsTP;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblNbWordsP;
        private Desktop.Controls.RoundPanel roundPanel1;
        private Desktop.Controls.RoundPanel roundPanel2;
        private System.Windows.Forms.ListView listWords;
        private System.Windows.Forms.Panel panel1;
        private Desktop.Controls.ImageButton btnNewGame;
        private Desktop.Controls.ImageButton btnStop;
        private Desktop.Controls.RoundPanel roundPanel3;
        private Controls.WordPlayer chevalet1;
        private System.Windows.Forms.Panel panel2;
        private Desktop.Controls.ImageButton imageButton4;
        private Desktop.Controls.ImageButton imageButton2;
        private Desktop.Controls.ImageButton imageButton3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblWordRack;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblWordTotal;
        private System.Windows.Forms.Label lblWordFound;
        private System.Windows.Forms.Label lblWordLost;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.FlowLayoutPanel solutionsPanel;
        private System.Windows.Forms.FlowLayoutPanel dicoPanel;
        private System.Windows.Forms.Label lbScoreTP;
        private System.Windows.Forms.Label lbScoreMP;
        private Desktop.Controls.ImageButton imageButton5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listSolutions;
        private Desktop.Controls.ImageButton imageButton6;
        private Desktop.Controls.ImageButton btnReview;
        private Desktop.Controls.ImageButton btnSnapshot;
    }
}
