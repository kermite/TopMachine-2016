using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using TopMachine.Topping.Common.Interfaces;
using TopMachine.Topping.Common.Structures;
using TopMachine.Topping.Dawg;
using TopMachine.Desktop.Overall;
using Topping.Core.Proxy.Local.Client;
using Topping.Core.Logic.Client;
using System.Linq;
using System.Collections.Generic;

namespace TopMachine.Topping.Engine.GUI.Board
{
    /// <summary>
    /// Summary description for WordPlayer.
    /// </summary>
    public class Chevalet : UserControl, IKeyHandler, IChevalet
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;


        private string strTirage;
        private char[] chrTirage;
        private LetterPlacer[] objLetters;
        private int intNbLetters;
        private int intHeight, intFirstX, intFirstY, nbMax;
        private LetterPlacer ObjDrag;
        public Color Col_Lettre = Color.Green;

        private GameCfg gc;
        private GameControllers gctls;
        private Panel panel1;
        private Panel pnlCV;
        private Panel pnlJoker;
        private Button button2;
        private ComboBox cbLetters;
        private Panel panel2;
        private Button button1;
        private Button button3;
        Guid MemoryCheckId;

        public GameCfg GameConfig
        {
            set { gc = value; }
            get { return gc; }
        }

        public int[] Tiles_points;

        public Control ResultControl
        {
            get { return this; }
        }

        public Chevalet()
        {
            InitializeComponent();
            pnlCV.BackColor = Color.Chocolate;
            string Tirage = "";
            strTirage = Tirage;
            chrTirage = Tirage.ToCharArray();
            intNbLetters = strTirage.Length;
            this.objLetters = null;
            MemoryCheckId = MemoryCheck.Register(this, "Chevalet CREATE");

            this.SetStyle(ControlStyles.UserMouse, true);

        }

        public void InitChevalet(GameCfg g, GameControllers ctls, int nbMax)
        {
            this.nbMax = nbMax; 
            gctls = ctls;
            gc = g;
            Tiles_points = DicoTools.GetDicoPoints(g.Config.Language);
            // instancie une fois pour toute objLetters et chaque letterPlacer

            if (this.objLetters != null)
            {
                for (int x = 0; x < objLetters.Length; x++)
                {
                    if (objLetters[x] != null)
                    {
                        objLetters[x].Dispose();
                    }
                }

                objLetters = null;
            }


            this.objLetters = new LetterPlacer[nbMax];

            for (int x = 0; x < nbMax; x++)
            {
                this.objLetters[x] = new LetterPlacer('0', "0", 0,0,0,0, false); 
            }

            this.strTirage = new string('a', nbMax);
            for (int x = 0; x < nbMax; x++)
            {
                addLetterGraphique(x, getPointLetter(x));
            }
            this.strTirage = "";
        }

        public void ShuffleWord() 
        {
            System.Random rnd = new System.Random();
            List<KeyValuePair<int, char>> list = new List<KeyValuePair<int, char>>();
            foreach (char s in chrTirage)
            {
                list.Add(new KeyValuePair<int, char>(rnd.Next(), s));
            }

            // Sort the list by the random number
            var sorted = from item in list
                         orderby item.Key
                         select item;
            
            string strTirage = "";
            foreach (KeyValuePair<int, char> pair in sorted)
            {
                strTirage += pair.Value; 
            
            }


            SetTirage(strTirage);  
           
        } 


        public void SetLetter(char Letter)
        {
            int[] CheckLetters = new int[intNbLetters];

            // We First Check Characters already Played 
            for (int xx = 0; xx < intNbLetters; xx++)
                CheckLetters[xx] = 0;


            // We Check if Letter is Valid 
            bool isValid = false;
            for (int l = 0; l < chrTirage.Length; l++)
            {
                if ((chrTirage[l] == Letter ||
                        chrTirage[l] == '?')
                    && CheckLetters[l] == 0)
                {
                    isValid = true;
                    break;
                }
            }

            ResetLetters();
        }

        public void ResetLetters()
        {
            int posLetter = 0;
            int[] CheckLetters = new int[intNbLetters];

            // We First Check Characters already Played 
            for (int xx = 0; xx < intNbLetters; xx++)
                CheckLetters[xx] = 0;


            for (int l = 0; l < chrTirage.Length; l++)
            {
                if (CheckLetters[l] == 0)
                {
                    objLetters[posLetter].SetLetter(chrTirage[l]);
                    posLetter++;

                }
            }
            RepaintChevalet();

        }

        public void ChangeColor(Color c)
        {
            for (int l = 0; l < intNbLetters; l++)
            {
                if (this.objLetters[l].enmStatus == LetterPlacer.Status.Chevalet)
                {
                    this.Col_Lettre = c;
                    this.objLetters[l].Col_Lettre = c;
                }
            }

        }

        private string getPointLetter(int pos)
        {
            if (Tiles_points != null)
            {
                if (strTirage[pos] == '?') return "0";
                else
                {
                    return Tiles_points[strTirage[pos] - 96].ToString();
                }
            }

            return "0"; 
        }

        public void SetLetterGraphique(int pos, string pts)
        {
            int left, top, height, width;
            try
            {
                left = intFirstX + (intHeight + 5) * pos;
                top = intFirstY;
                width = height = intHeight;
                objLetters[pos].SetLetterPlacer(strTirage[pos], pts, left, top, height, width, true);
                // pnlCV.Controls.Add(objLetters[pos]);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
      
        
        # region Event Drag Letter
        public void Switch(LetterPlacer O1, LetterPlacer O2,int posO1,int posO2 )
        {
            char c;
            string pt;
            int st;

            c = O1.strLetter;
            pt = O1.strPts;
            st = (int)O1.enmStatus;

            //O1.strLetter = O2.strLetter;
            //O1.strPts = O2.strPts;
            //O1.enmStatus = O2.enmStatus;

            char[] lstCharTirage = strTirage.ToCharArray();  
            lstCharTirage[posO1] = O2.strLetter;  
            lstCharTirage[posO2] = c;

            strTirage = new string(lstCharTirage);  

            //O2.strLetter = c;
            //O2.strPts = pt;
            //O2.enmStatus = (LetterPlacer.Status)st;

            
             

        }
     
        # endregion
     

        public void ResetSize()
        {
            try
            {
                DisplayChevalet();
                if (objLetters != null)
                {
                    for (int x = 0; x < objLetters.Length; x++)
                    {
                        objLetters[x].Left = intFirstX + (intHeight + 5) * x;
                        objLetters[x].Top = intFirstY;
                        objLetters[x].Width = intHeight;
                        objLetters[x].Height = intHeight;
                        objLetters[x].ResetBmp(); 
                    }
                }

                RepaintChevalet();
                this.Refresh();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public void addLetterGraphique(int pos, string pts)
        {
            int left, top, height, width;
            try
            {
                left = intFirstX + (intHeight + 5) * pos;
                top = intFirstY;
                width = height = intHeight;
                
                objLetters[pos].SetLetterPlacer(strTirage[pos], pts, left, top, height, width, true);

                //objLetters[pos].MouseDown += new MouseEventHandler(Lettre_MouseDown);
                //objLetters[pos].DragDrop += new DragEventHandler(Lettre_DragDrop);
                //objLetters[pos].Click += new EventHandler(Chevalet_Click); 

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        void LetterClick(LetterPlacer p)
        {

            Keys kp = Keys.A;
            System.Windows.Forms.KeyEventArgs ke = new KeyEventArgs(Keys.A);

            string pl = p.strLetter.ToString().ToUpper();

            if (pl[0] >= 'A' && pl[0] <= 'Z')
            {
                kp = (Keys)Enum.Parse(typeof(Keys), pl, true);
                ke = new KeyEventArgs(kp);
            }

            if (pl[0] == '?')
            {
                pnlJoker.Visible = true;
                return;
            }


            ((IKeyHandler)gctls.Board).HandleKey(ke);
           
        }

        private void DisplayTirage()
        {
            // affiche toute les lettres non place 
            if (intNbLetters == 0) return;

            for (int x = 0; x < intNbLetters; x++)
            {
                this.SetLetterGraphique(x, getPointLetter(x));
            }

        }

       
        private int GetPosLetter(char Letter, LetterPlacer.Status state)
        {
            char c;
            int posJoker = -1;
            // si majuscule Joker
            // Si minuscule Lettre si plus de lettre joker

            // renvoie -1 si non trouve
            int pos = -1;

            for (int l = 0; l < intNbLetters; l++)
            {
                c = objLetters[l].strLetter;
                if ((c == Letter || (char.IsUpper(Letter) && c == '?')) && objLetters[l].enmStatus == state)
                {
                    pos = l;
                    break;
                }
                else if (c == '?' && objLetters[l].enmStatus == state)
                    posJoker = l;
            }

            if (pos == -1)
                return posJoker;
            else
                return pos;
        }

        public int SearchPosInChevalet(char letter)
        {
            //recherche la lettre qui est sur le chevalet
            return this.GetPosLetter(letter, LetterPlacer.Status.Chevalet);
        }

        private int SearchPosInTableau(char letter)
        {
            //recherche la position du chevalet correspondant a la lettre sur la grille  
            return this.GetPosLetter(letter, LetterPlacer.Status.Tableau);
        }

        private void DisplayLetter(char lettre, int posLetter)
        {

            //objLetters[posLetter].SetColor(true);
            objLetters[posLetter].SetLetter(lettre);

        }

        protected override void OnResize(EventArgs e)
        {
            pnlCV_Resize(null, null);
            base.OnResize(e);
        }

        public void replaceAllLetters()
        {
            for (int l = 0; l < intNbLetters; l++)
            {
                if (objLetters[l].enmStatus == LetterPlacer.Status.Tableau)
                {
                    objLetters[l].enmStatus = LetterPlacer.Status.Chevalet;
                    //objLetters[l].SetColor(true);
                }
            }

        }
        public void EnleveLettre(int pos)
        {
            objLetters[pos].CacheLetter();
        }

        public void PlaceLettre(char letter)
        {
            int pos;

            pos = SearchPosInTableau(letter);
            if (pos > -1)
                objLetters[pos].ShowLetter();
        }

        public void DisplayChevalet()
        {

            if (intNbLetters == 0)
            {
                pnlCV.Controls.Clear();
                return;
            }

            int maxWidth = (pnlCV.Width) / nbMax;
            maxWidth -= 5;

            if (maxWidth < pnlCV.Height - 10)
                intHeight = maxWidth;
            else
                intHeight = pnlCV.Height - 10;

            if (intHeight > pnlCV.Height)
            {
                intHeight = pnlCV.Height - 10;
            }

            intFirstY = ((pnlCV.Height - (intHeight)) / 2);
            intFirstX = (pnlCV.Width - ((intHeight * intNbLetters) + 5 * (intNbLetters - 1))) / 2;

            if (intFirstY < 1) intFirstY = 1;


        }


        public void SetTirage(string Tirage)
        {
            pnlCV.SuspendLayout();
            Tirage = Tirage.ToLower();
            p_setTirage(Tirage);//, Current);

            this.DisplayChevalet();
            this.DisplayTirage();
            RepaintChevalet();
        }

        public void RepaintChevalet()
        {
            PaintTirage(); 
            pnlCV.Refresh();
        }

        private void p_setTirage(string Tirage)//, string Current)
        {

            if (strTirage != Tirage)// || strCurrent != Current)
            {
                strTirage = Tirage;
                chrTirage = Tirage.ToCharArray();

                intNbLetters = strTirage.Length;

            }
        }

        private string ToString(char[] listeC)
        {
            string str = "";
            if (listeC == null) return "NULL";
            for (int i = 0; i < listeC.Length; i++)
            {
                str += listeC[i].ToString();
            }
            return str + "[" + listeC.Length.ToString() + "]";
        }

        public string ToString()
        {
            string str = "";
            try
            {
                str += ToString(this.chrTirage) + " :Tirage /";
                str += this.strTirage + " :strTirage/";
                str += this.intNbLetters.ToString();
            }
            catch
            {
                str = " ";
            }
            return str;
        }
        public void InitRack()
        {

            this.DisplayChevalet();
            this.DisplayTirage();

        }



        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            MemoryCheck.Unregister(MemoryCheckId);

            if (objLetters != null)
            {
                for (int x = 0; x < objLetters.Length; x++)
                {
                    if (objLetters[x] != null)
                    {
                        objLetters[x].Dispose();
                    }
                }

                objLetters = null;
            }

            if (buffer != null)
            {
                buffer.Dispose();
                buffer = null;
            }

            base.Dispose(disposing);
        }

        private void WordPlayer_Resize(object sender, System.EventArgs e)
        {
            InitRack();
        }

        public bool HandleKey(System.Windows.Forms.KeyEventArgs e)
        {

            if (e.KeyData == Keys.F6)
            {
                ShuffleWord();
                return true;
            }
           
            return false;
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.pnlCV = new System.Windows.Forms.Panel();
            this.pnlJoker = new System.Windows.Forms.Panel();
            this.cbLetters = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlJoker.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(30, 31);
            this.panel1.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkRed;
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 31);
            this.button3.TabIndex = 1;
            this.button3.TabStop = false;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnCancel_Click);
            this.button3.Enter += new System.EventHandler(this.button3_Enter);
            // 
            // pnlCV
            // 
            this.pnlCV.AllowDrop = true;
            this.pnlCV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCV.Location = new System.Drawing.Point(30, 0);
            this.pnlCV.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCV.Name = "pnlCV";
            this.pnlCV.Size = new System.Drawing.Size(337, 31);
            this.pnlCV.TabIndex = 1;
            this.pnlCV.SizeChanged += new System.EventHandler(this.pnlCV_Resize);
            this.pnlCV.Click += new System.EventHandler(this.pnlCV_Click);
            this.pnlCV.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlCV_DragDrop);
            this.pnlCV.DragOver += new System.Windows.Forms.DragEventHandler(this.pnlCV_DragOver);
            this.pnlCV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlCV_MouseMove);
            this.pnlCV.Resize += new System.EventHandler(this.pnlCV_Resize);
            // 
            // pnlJoker
            // 
            this.pnlJoker.Controls.Add(this.cbLetters);
            this.pnlJoker.Controls.Add(this.button2);
            this.pnlJoker.Location = new System.Drawing.Point(39, 2);
            this.pnlJoker.Margin = new System.Windows.Forms.Padding(2);
            this.pnlJoker.Name = "pnlJoker";
            this.pnlJoker.Size = new System.Drawing.Size(84, 26);
            this.pnlJoker.TabIndex = 0;
            this.pnlJoker.Visible = false;
            this.pnlJoker.GotFocus += new System.EventHandler(this.pnlJoker_GotFocus);
            // 
            // cbLetters
            // 
            this.cbLetters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbLetters.Font = new System.Drawing.Font("Tahoma", 11F);
            this.cbLetters.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z"});
            this.cbLetters.Location = new System.Drawing.Point(0, 0);
            this.cbLetters.Margin = new System.Windows.Forms.Padding(2);
            this.cbLetters.Name = "cbLetters";
            this.cbLetters.Size = new System.Drawing.Size(64, 30);
            this.cbLetters.TabIndex = 5;
            this.cbLetters.SelectedIndexChanged += new System.EventHandler(this.cbLetters_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Green;
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Font = new System.Drawing.Font("Tahoma", 5F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(64, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 26);
            this.button2.TabIndex = 6;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(367, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 31);
            this.panel2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Green;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.ForeColor = System.Drawing.Color.Yellow;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 31);
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.Enter += new System.EventHandler(this.button3_Enter);
            // 
            // Chevalet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.pnlJoker);
            this.Controls.Add(this.pnlCV);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Chevalet";
            this.Size = new System.Drawing.Size(397, 31);
            this.Resize += new System.EventHandler(this.WordPlayer_Resize);
            this.panel1.ResumeLayout(false);
            this.pnlJoker.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      
        #region pnlCV
        int LastX;
        int LastY;
        int LastPosition;

       
        void pnlCV_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void pnlCV_DragDrop(object sender, DragEventArgs e)
        {
           
           
            Point clientPoint = pnlCV.PointToClient(new Point(e.X, e.Y));

            LastX = clientPoint.X;
            LastY = clientPoint.Y;   
            LetterPlacer current,move;
            move = (LetterPlacer)e.Data.GetData(typeof(LetterPlacer)) ;
            int temp = 0; 
            current = getLetter(ref temp);

            if (move != null && current != null) 
            {
                Switch(move, current,LastPosition,temp);

              //  this.DisplayChevalet();
                this.DisplayTirage();
                RepaintChevalet();
            }
           
        }
        private void pnlCV_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(pnlCV.BackColor);
        }

       
        private void pnlCV_MouseMove(object sender, MouseEventArgs e)
        {
            LastX = e.X;
            LastY = e.Y;

                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                   

                    LetterPlacer current;
                    int temp = 0;
                    current = getLetter(ref temp);
                    if (current != null)
                    {
                        LastPosition = temp;
                        this.DoDragDrop(current, DragDropEffects.All | DragDropEffects.Move);

                    }
                }
        }

        private void pnlCV_Click(object sender, EventArgs e)
        {

            pnlJoker.Visible = false;

            LetterPlacer current;
            int temp = 0;
            current = getLetter(ref temp);

            if (current != null)
            {
                LetterClick(current);
            }
            else
            {
                System.Windows.Forms.KeyEventArgs ke = new KeyEventArgs(Keys.Back);
                ((IKeyHandler)gctls.GameController).HandleKey(ke);
            }

          
        

        }
        # endregion

        private LetterPlacer getLetter(ref int pos) 
        {
            Point p = new Point(LastX, LastY);

            for (int x = 0; x < intNbLetters; x++)
            {
                LetterPlacer lp = objLetters[x];
                if (lp.Bounds.Contains(p))
                {
                    pos = x; 
                    return lp;
                    
                }
            }
            return null;
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.KeyEventArgs ke = new KeyEventArgs(Keys.Space);
            ((IKeyHandler)gctls.GameController).HandleKey(ke);
            ((IKeyHandler)gctls.GameController).HandleKey(ke);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageProxy.Proxy.GamePermissions.HasFlag(GamePermissions.CanEndRound))
            {
                ComponentResourceManager resources = new ComponentResourceManager(this.GetType());
                string confirm = resources.GetString("NextRoundConfirmation");
                string confirmTitle = resources.GetString("NextRoundTitle");

                if (MessageBox.Show(confirm, confirmTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                {
                    System.Windows.Forms.KeyEventArgs ke = new KeyEventArgs(Keys.F1);
                    ((IKeyHandler)gctls.GameController).HandleKey(ke);
                }
            }
        }
        private string sortStr(string str) 
        {
            char[] lstc = strTirage.ToCharArray();
            Array.Sort(lstc);

            return new string(lstc);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            strTirage = sortStr(strTirage);
            this.DisplayChevalet();
            this.DisplayTirage();
            RepaintChevalet();

            //if (MessageProxy.Proxy.GamePermissions.HasFlag(GamePermissions.CanValidate))
            //{
            //    System.Windows.Forms.KeyEventArgs ke = new KeyEventArgs(Keys.Enter);
            //    ((IKeyHandler)gctls.GameController).HandleKey(ke);
            //}
        }

   


        SafeBitmap buffer; 


        private void PaintTirage()
        {
            if (buffer == null)
            {
                buffer = new SafeBitmap(new Bitmap(pnlCV.Width, pnlCV.Height), this);
            }

            try
            {
                buffer.g.Clear(pnlCV.BackColor);

                for (int x = 0; x < intNbLetters; x++)
                {
                    LetterPlacer lp = objLetters[x];
                    buffer.g.DrawImage(lp.GetBmp(), lp.Left, lp.Top);
                }

                pnlCV.BackgroundImageLayout = ImageLayout.None; 
                pnlCV.BackgroundImage = buffer.bmp; 
            }
            catch
            {
                ;
            }
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            Keys kp = Keys.A;
            System.Windows.Forms.KeyEventArgs ke = new KeyEventArgs(Keys.A);

            string pl = cbLetters.Text;
            if (pl.Length > 0)
            {
                kp = (Keys)Enum.Parse(typeof(Keys), pl, true);
                ke = new KeyEventArgs(kp | Keys.Shift);
                ((IKeyHandler)gctls.Board).HandleKey(ke);
            }
            else
            {
                MessageBox.Show("Sélectionnez une lettre joker à droite.");
            }

            pnlJoker.Visible = false;

        }

        private void pnlJoker_GotFocus(object sender, EventArgs e)
        {

        }

        private void cbLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            Keys kp = Keys.A;
            System.Windows.Forms.KeyEventArgs ke = new KeyEventArgs(Keys.A);

            string pl = cbLetters.Text;
            if (pl.Length > 0)
            {
                kp = (Keys)Enum.Parse(typeof(Keys), pl, true);
                ke = new KeyEventArgs(kp | Keys.Shift);
                ((IKeyHandler)gctls.Board).HandleKey(ke);
            }
            else
            {
                MessageBox.Show("Sélectionnez une lettre joker à droite.");
            }

            pnlJoker.Visible = false;
        }

        private void pnlCV_Resize(object sender, EventArgs e)
        {
            if (buffer != null)
            {
                buffer.Dispose();
            }
            buffer = null;
            ResetSize();
        }


        private void button3_Enter(object sender, EventArgs e)
        {
        }




    }
}
