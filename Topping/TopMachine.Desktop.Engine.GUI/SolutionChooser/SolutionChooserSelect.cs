

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;


using TopMachine.Topping.Common.Interfaces;
using TopMachine.Desktop.Overall;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;


namespace TopMachine.Engine.Local.GameController.SolutionChooser
{  
	/// <summary>
	/// Summary description for Results.
	/// </summary>
	public class SolutionChooserSelect : System.Windows.Forms.UserControl, ISolutionChooser, IKeyHandler
	{
		private System.Windows.Forms.ListView lvSolutions;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label lbSolutions;
        private System.Windows.Forms.Panel pnlSelect;
        private Button btnSelectRound;

        private System.ComponentModel.Container components = null;

        GameCfg gc;
        bool LoadView = false;
        private Label label1;
        IGameController gctl;

        #region Public Properties and Events
        public event RemoveTempWordEvent OnRemoveTempWord;
        public event SetTempWordEvent OnSetTempWord;
        public event SelectWordEvent OnSelectWord; 

        public GameCfg GameConfig
        {
            set { gc = value; }
        }

        public Control ResultControl
        {
            get { return this; }
        }
        #endregion  

        public void Dispose()
        {
            base.Dispose();
        }

        public SolutionChooserSelect()
        {
            InitializeComponent();
        }

        Guid MemoryCheckId; 

        public SolutionChooserSelect(IGameController gctl, GameCfg gc)
		{
			InitializeComponent();
            this.gctl = gctl;
            this.gc = gc;
            this.Visible = false;
            MemoryCheckId = MemoryCheck.Register(this, this.GetType().FullName);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}

            MemoryCheck.Unregister(MemoryCheckId);
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvSolutions = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbSolutions = new System.Windows.Forms.Label();
            this.pnlSelect = new System.Windows.Forms.Panel();
            this.btnSelectRound = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvSolutions
            // 
            this.lvSolutions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvSolutions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSolutions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSolutions.FullRowSelect = true;
            this.lvSolutions.GridLines = true;
            this.lvSolutions.LabelWrap = false;
            this.lvSolutions.Location = new System.Drawing.Point(0, 82);
            this.lvSolutions.MultiSelect = false;
            this.lvSolutions.Name = "lvSolutions";
            this.lvSolutions.Size = new System.Drawing.Size(360, 534);
            this.lvSolutions.TabIndex = 12;
            this.lvSolutions.UseCompatibleStateImageBehavior = false;
            this.lvSolutions.View = System.Windows.Forms.View.Details;
            this.lvSolutions.SelectedIndexChanged += new System.EventHandler(this.lvSolutions_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mot";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Pos";
            this.columnHeader2.Width = 40;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Pts";
            this.columnHeader3.Width = 40;
            // 
            // lbSolutions
            // 
            this.lbSolutions.BackColor = System.Drawing.Color.LightSlateGray;
            this.lbSolutions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbSolutions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSolutions.ForeColor = System.Drawing.Color.LemonChiffon;
            this.lbSolutions.Location = new System.Drawing.Point(0, 38);
            this.lbSolutions.Name = "lbSolutions";
            this.lbSolutions.Size = new System.Drawing.Size(360, 24);
            this.lbSolutions.TabIndex = 11;
            this.lbSolutions.Text = "Nombre de solutions : 0";
            this.lbSolutions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSelect
            // 
            this.pnlSelect.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlSelect.Controls.Add(this.btnSelectRound);
            this.pnlSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelect.Location = new System.Drawing.Point(0, 0);
            this.pnlSelect.Name = "pnlSelect";
            this.pnlSelect.Size = new System.Drawing.Size(360, 38);
            this.pnlSelect.TabIndex = 10;
            this.pnlSelect.Visible = false;
            // 
            // btnSelectRound
            // 
            this.btnSelectRound.BackColor = System.Drawing.Color.Teal;
            this.btnSelectRound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelectRound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSelectRound.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectRound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSelectRound.Location = new System.Drawing.Point(0, 0);
            this.btnSelectRound.Name = "btnSelectRound";
            this.btnSelectRound.Size = new System.Drawing.Size(360, 38);
            this.btnSelectRound.TabIndex = 66;
            this.btnSelectRound.UseVisualStyleBackColor = false;
            this.btnSelectRound.Click += new System.EventHandler(this.btnSelectRound_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(0, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Double clic pour définitions";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // SolutionChooserSelect
            // 
            this.Controls.Add(this.lvSolutions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbSolutions);
            this.Controls.Add(this.pnlSelect);
            this.Name = "SolutionChooserSelect";
            this.Size = new System.Drawing.Size(360, 616);
            this.pnlSelect.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        #region KeyHandler
        public bool HandleKey(System.Windows.Forms.KeyEventArgs e)
        {
            if (gc.GameState == enuGameState.GetSolution)
            {
                switch (e.KeyCode)
                {
                    case Keys.F2:
                        SelectRound(false);
                        e.Handled = true;
                        break;
                }
            }

            return e.Handled;
        }
        #endregion

        #region LIST EVENTS 
        private void btnSelectRound_Click(object sender, EventArgs e)
        {
            SelectRound(false);
        }

        private void btnGetSolutions_Click(object sender, System.EventArgs e)
        {
            GetSolutions();
        }



        private void lvSolutions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (LoadView) return; 

			if (lvSolutions.SelectedIndices.Count == 0)
			{
				if (OnRemoveTempWord != null) OnRemoveTempWord(true);
			} 
			else 
			{
				CRound round = (CRound) lvSolutions.SelectedItems[0].Tag;
				string word = lvSolutions.SelectedItems[0].Text;
                //lvSolutions.Items[0].Text;


				int[] origin = new int[word.Length]; 
				for (int x = 0; x < word.Length; x++)
				{
					origin[x] = round.Round.tileorigin[x]; 
				}

				if (OnSetTempWord != null)
					OnSetTempWord(word, origin, round.row()-1, round.column()-1, round.dir());

			}
        }

        #endregion

        #region LIST INIT 
        public void SetResults(CResults results)
        {
            SetResults(results, false);
        }

        public void SetResults(CResults results, ArrayList list, bool visible)
        {
            SetVisible(visible);
            LoadView = true;
            lvSolutions.Items.Clear();
            lvSolutions.SelectedIndices.Clear();

            for (int x = 0; x < gc.GridConfig.dimX * 2; x++)
            {
                tresults res = (tresults)list[x];
                if (res.maxPoints > 0)
                {
                    CRound  r = (CRound)res.list.GetByIndex(0);

                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = r.getwordwithjoker();

                    char h = System.Convert.ToChar(r.row() + 64);
                    string v = r.column().ToString();

                    if (r.dir() == 1)
                    {
                        lvi.SubItems.Add(h.ToString() + v.ToString());
                    }
                    else
                    {
                        lvi.SubItems.Add(v.ToString() + h.ToString());
                    }

                    lvi.SubItems.Add(r.points().ToString());

                    lvi.Tag = r;
                    lvSolutions.Items.Add(lvi);
                }
            }


            if (lvSolutions.Items.Count > 0)
            {
                if (visible) LoadView = false;
                lbSolutions.Text = "Nombre de solutions : " + lvSolutions.Items.Count;
                lvSolutions.Items[0].Selected = true;
            }
            lvSolutions.Focus();
            LoadView = false;
        }

        public void SetResults(CResults results, bool visible)
		{           
            SetVisible(visible);
			LoadView = true;
			lvSolutions.Items.Clear();
            lvSolutions.SelectedIndices.Clear();
			foreach (string s in results.Results.list.GetKeyList())
			{

                CRound r = (CRound)results.Results.list[s];

                ListViewItem lvi = new ListViewItem();
                lvi.Text = r.getwordwithjoker();

                char h = System.Convert.ToChar(r.row() + 64);
                string v = r.column().ToString();

                if (r.dir() == 1)
                {
                    lvi.SubItems.Add(h.ToString() + v.ToString());
                }
                else
                {
                    lvi.SubItems.Add(v.ToString() + h.ToString());
                }

                lvi.SubItems.Add(r.points().ToString());

                lvi.Tag = r;
                lvSolutions.Items.Add(lvi);

			}

			if (lvSolutions.Items.Count > 0)
			{
                if (visible) LoadView = false;
				lbSolutions.Text = "Nombre de solutions : " + lvSolutions.Items.Count; 
				lvSolutions.Items[0].Selected = true;
			}		
			lvSolutions.Focus();
            LoadView = false;
			
            
        }
        #endregion 

        #region private CRound GetSelectedRound()
        private CRound GetSelectedRound()
        { 
            int indice;
            CRound round;
            indice = lvSolutions.SelectedIndices[0];
            if (indice == -1) indice = 0;
            return (CRound)lvSolutions.Items[indice].Tag;
        }
        #endregion

        #region LAYOUT TOGGLING


        private void SelectRound(bool ok)
		{
            this.Visible = false; 
			btnSelectRound.Enabled = ok;
            if (OnSelectWord != null)
            {
                
                OnSelectWord(GetSelectedRound()); 
            }
		}

        

        public void SetVisible(bool ok)
		{
            this.Visible = ok;
			lvSolutions.Visible = ok; 
			lbSolutions.Visible = ok;

            if (ok)
            {
                /*if (lvSolutions.Items.Count > 0)
                {
                    lvSolutions.Items[0].Selected = true;
                }  */
                lvSolutions.Enabled = true;
                lvSolutions.Focus();
                lvSolutions_SelectedIndexChanged(null, null);
            }
            else
            {
                lvSolutions.Enabled = false;
            }
        }

        #endregion 

        #region public void GetSolutions()
        private delegate void SetVisibleDelegate(bool ok);
        public void GetSolutions()
        {
           // SetVisible(true);
           this.Invoke(new SetVisibleDelegate(SetVisible), new object[] { true });
        }
        #endregion 


	}

}
